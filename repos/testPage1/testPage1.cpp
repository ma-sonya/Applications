#include <iostream>
#include<fstream>
#include <string>
#include <sstream>

using namespace std;

int main() {

    ifstream file("1C.txt");
	string msg1;
    if (file.is_open()) {
		char sym;
		char lastSym = '\0';
		while (!file.eof()) // пока файл не пуст, считываем из файла по одному символу и вводим значение в переменную
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
    }
    else {
        cout << "File isn't open" << endl;
    }
	cout << msg1 << endl;
    return 0;
}