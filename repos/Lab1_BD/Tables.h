#pragma once

struct Indexer
{
	int id;
	int address;
	int exists;
};

struct Drugstore
{
	int id;          //ID
	char name[64];      //Ім'я
	char date[128];      //Дата відвідування
	int status;        //Онлайн/офлайн
	long drug_one; //Номер першого замовлення
	int drug_count;    //Кількість замовлень
};

struct Drug
{
	int drugstoreId;      //ID Клієнта
	int drugId;      //ID Замовлення
	char provider[256];
	char name[64];      //Ім'я
	char date_creating[128];  //Дата, коли замовлено
	int exists;        //Наявність
	long selfAddress;    //Адреса замовлення
	long nextAddress;    //Адреса замовника
};