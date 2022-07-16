#include <stdio.h>
#include<iostream>
#include "winsock2.h"
#include<string>
#include<ctime>
#include <fstream>
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#pragma comment(lib, "ws2_32.lib")

#pragma warning(disable: 4996)

using namespace std;

const string currentDateTime() {
  time_t     now = time(0);
  struct tm  tstruct;
  char       buf[80];
  tstruct = *localtime(&now);
  strftime(buf, sizeof(buf), "%Y-%m-%d.%X", &tstruct);
  return buf;
}

string convertFiletoString(string& nameOfFile) {
  string msg1;
  ifstream file(nameOfFile);   // відкриваємо файл
  if (file.is_open() == true) {
    cout << "File is open!" << endl;
  }
  else {
    cout << "File is not open!" << endl;
  }

  char sym;
  char lastSym = '\0';
  while (!file.eof()) // поки файл не порожній, зчитуємо по одному символу і вводимо значення змінної
  {
    file >> sym;
    if (sym == '\n') {
      msg1.push_back(' ');
      continue;
    }
    else if ((lastSym == ' ') && (sym == lastSym)) {
      continue;
    }
    msg1.push_back(sym);
    lastSym = sym;
  }
  file.close();
  return msg1;
}



void fSend(const SOCKET& Connection, string& msg1, string& nameOfFile, ofstream& log) {

  msg1 = convertFiletoString(nameOfFile);
  int msg_size = msg1.size();
  send(Connection, (char*)&msg_size, sizeof(int), NULL);  //надсилаємо size
  send(Connection, msg1.c_str(), msg_size, NULL);      //надсилаємо message

  string logPr = "Sent: " + msg1 + " at: " + currentDateTime(); // записуємо в журнал подій
  log << "[" << logPr.size() << "]: [" << logPr << "]\n";
}

void Send(const SOCKET& Connection, string& msg1, ofstream& log) {

  int msg_size = msg1.size();
  send(Connection, (char*)&msg_size, sizeof(int), NULL);  //надсилаємо size
  send(Connection, msg1.c_str(), msg_size, NULL);      //надсилаємо message

  string logPr = "Sent: " + msg1 + " at: " + currentDateTime(); // записуємо в журнал подій
  log << "[" << logPr.size() << "]: [" << logPr << "]\n";
}

void Recv(const SOCKET& Connection, string& msg1, ofstream& log) {
  int msg_size = msg1.size();
  recv(Connection, (char*)&msg_size, sizeof(int), NULL);//Приймаємо розмір масиву
  char* msg = new char[msg_size + 1];
  msg[msg_size] = '\0';
  if (recv(Connection, msg, msg_size, NULL) >= 0) {//Приймаємо повідомлення
    string str(msg);
    msg1 = str;

    cout << "You get message!\nShow it?  //\"+\" if yes, \"-\" if no" << endl; //показати повідомлення?
    char a = '-';
    cin >> a;
    switch (a) {
    case '+':
      cout << msg1;
      break;
    case '-':
      break;
    }
  }

  string logPr = "Recieved: " + msg1 + " at: " + currentDateTime();// записуємо в журнал подій
  log << "[" << logPr.size() << "]: [" << logPr << "]\n";

  delete[] msg;
}



int main()
{
  WSADATA wsaDATA;
  if (WSAStartup(MAKEWORD(2, 2), &wsaDATA) != 0) {
    printf("Error at WSAStartup()\n");
  }


  SOCKADDR_IN clientService;
  int sizeoffCS = sizeof(clientService);
  clientService.sin_family = AF_INET;
  clientService.sin_addr.s_addr = inet_addr("127.0.0.1");
  clientService.sin_port = htons(1044);          //1025+19

  SOCKET ConnectSocket = socket(AF_INET, SOCK_STREAM, NULL);
  if (ConnectSocket == INVALID_SOCKET) {
    printf("Error at socket(): %ld\n", WSAGetLastError());
    WSACleanup();
  }

  if (connect(ConnectSocket, (SOCKADDR*)&clientService, *(&sizeoffCS)) == SOCKET_ERROR) {
    printf("Failed to connect.\n");
    WSACleanup();
  }
  else {
    printf("Connected to server!\n");
  }

  //Надсилаємо повідомлення

  ofstream log("ClientLog.txt");//потрібно створити "ClientLog.txt"
  if (log.is_open() == true) {
    cout << "Log is open!" << endl;
  }
  else {
    cout << "Log is not open!" << endl;
  }

  ifstream file1("1C.txt");
  ifstream file2("2C.txt");

  string name1 = "1C.txt";
  string text1 = convertFiletoString(name1);

  string name2 = "2C.txt";
  string text2 = convertFiletoString(name2);

string msg;
  Recv(ConnectSocket, msg, log);
  while (1) {
    getline(cin, msg, '\n');
    if (msg == "Who") {
      Send(ConnectSocket, msg, log);
      Recv(ConnectSocket, msg, log);
    }
    else if (msg == "Break") {
      Send(ConnectSocket, msg, log);
      break;
    }
    else if (msg == text1) {
      string n = "1C.txt";
      fSend(ConnectSocket, msg, text1, log);
      Recv(ConnectSocket, msg, log);
    }
    else if (msg == text2) {
      string n = "2C.txt";
      fSend(ConnectSocket, msg, text2, log);
      Recv(ConnectSocket, msg, log);
    }
    else {
      cout << "Wrong command. Try again" << endl;
    }
  }
  Recv(ConnectSocket, msg, log);
  log.close();
  if (log.is_open() != true) {
    cout << "Log is closed!" << endl;
  }
  else {
    cout << "Log is not closed!" << endl;
  }
  system("pause");


  closesocket(ConnectSocket);
  WSACleanup();
  system("pause");
  return 0;
}