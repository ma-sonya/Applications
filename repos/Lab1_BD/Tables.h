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
	char name[64];      //��'�
	char date[128];      //���� ����������
	int status;        //������/������
	long drug_one; //����� ������� ����������
	int drug_count;    //ʳ������ ���������
};

struct Drug
{
	int drugstoreId;      //ID �볺���
	int drugId;      //ID ����������
	char provider[256];
	char name[64];      //��'�
	char date_creating[128];  //����, ���� ���������
	int exists;        //��������
	long selfAddress;    //������ ����������
	long nextAddress;    //������ ���������
};