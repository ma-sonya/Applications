#pragma once
#pragma warning(disable:4996)

#include <stdio.h>
#include <stdlib.h>
#include "Tables.h"
#include "Existing.h"
#include "Drugstore.h"

#define ORDER_DATA "drug.fl"
#define ORDER_GARBAGE "garbadge_drug.txt"
#define ORDER_SIZE sizeof(struct Drug)

int updateDrugstore(struct Drugstore drugstore, char* error);

void reopenDatabase(FILE* database)
{
    fclose(database);
    database = fopen(ORDER_DATA, "r+b");
}

void linkAddresses(FILE* database, struct Drugstore drugstore, struct Drug drug)
{
    reopenDatabase(database);                // ������� ����� �� ������� � �� �����

    struct Drug previous;

    fseek(database, drugstore.drug_one, SEEK_SET);

    for (int i = 0; i < drugstore.drug_count; i++)        // ���������� ��'������ ������ �� �������� ��������
    {
        fread(&previous, ORDER_SIZE, 1, database);
        fseek(database, previous.nextAddress, SEEK_SET);
    }

    previous.nextAddress = drug.selfAddress;        // ��'����� ������
    fwrite(&previous, ORDER_SIZE, 1, database);        // �������� ��������� ����� �� �����
}

void relinkAddresses(FILE* database, struct Drug previous, struct Drug drug, struct Drugstore* drugstore)
{
    // ���� ���� �����������
    if (drug.selfAddress == drugstore->drug_one)
    {
        if (drug.selfAddress == drug.nextAddress)      // ���� ���� �����
            drugstore->drug_one = -1;          // ��������� ������
        else
            drugstore->drug_one = drug.nextAddress;
    }
    // ���� � ����������
    else
    {
        if (drug.selfAddress == drug.nextAddress)      // ������� �����
        {
            previous.nextAddress = previous.selfAddress;
        }
        else
        {
            previous.nextAddress = drug.nextAddress;
        }

        fseek(database, previous.selfAddress, SEEK_SET);
        fwrite(&previous, ORDER_SIZE, 1, database);
    }
}

void overwriteGarbageAddress(int garbageCount, FILE* garbageZone, struct Drug* record)
{
    long* delIds = (long*)malloc(garbageCount * sizeof(long));    // �������� ���� �� ������ ��������� �����

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%d", delIds + i);

    record->selfAddress = delIds[0];
    record->nextAddress = delIds[0];

    fclose(garbageZone);                  // ������� garbage_zone �� �������� ��� ������� ������
    fopen(ORDER_GARBAGE, "wb");
    fprintf(garbageZone, "%d", garbageCount - 1);

    for (int i = 1; i < garbageCount; i++)
        fprintf(garbageZone, " %d", delIds[i]);        // �������� ����� ��������� �����

    free(delIds);                      // ��������� ���'���
    fclose(garbageZone);
}

void noteDeletedDrug(long address)
{
    FILE* garbageZone = fopen(ORDER_GARBAGE, "rb");      // "rb": ��������� ������� ���� ��� �������

    int garbageCount;
    fscanf(garbageZone, "%d", &garbageCount);

    long* delAddresses = (long*)malloc(garbageCount * sizeof(long)); // �������� ���� �� ������� ������

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%ld", delAddresses + i);

    fclose(garbageZone);                  // �������� ����� � ���������� ��������
    garbageZone = fopen(ORDER_GARBAGE, "wb");
    fprintf(garbageZone, "%ld", garbageCount + 1);      // ����� ����� ��������� �����

    for (int i = 0; i < garbageCount; i++)
        fprintf(garbageZone, " %ld", delAddresses[i]);

    fprintf(garbageZone, " %d", address);          // ����� �������� �������� ������
    free(delAddresses);                    // ��������� ���'���
    fclose(garbageZone);
}

int insertDrug(struct Drugstore drugstore, struct Drug drug, char* error)
{
    drug.exists = 1;

    FILE* database = fopen(ORDER_DATA, "a+b");
    FILE* garbageZone = fopen(ORDER_GARBAGE, "rb");

    int garbageCount;

    fscanf(garbageZone, "%d", &garbageCount);
    if (garbageCount)
    {
        overwriteGarbageAddress(garbageCount, garbageZone, &drug);
        reopenDatabase(database);                // ������� ����� ������� �����
        fseek(database, drug.selfAddress, SEEK_SET);      // ������� ������ �� "�����" ��� ����������
    }
    else                                                        // ��������� ����
    {
        fseek(database, 0, SEEK_END);

        int dbSize = ftell(database);

        drug.selfAddress = dbSize;
        drug.nextAddress = dbSize;
    }

    fwrite(&drug, ORDER_SIZE, 1, database);          // �������� ���������� �� �����

    if (!drugstore.drug_count)                    // ��������� ����
        drugstore.drug_one = drug.selfAddress;
    else                                                        // ���������� �
        linkAddresses(database, drugstore, drug);

    fclose(database);

    drugstore.drug_count++;                    // ������ ������� ���������
    updateDrugstore(drugstore, error);

    return 1;
}

int getDrug(struct Drugstore drugstore, struct Drug* drug, int orderId, char* error)
{
    if (!drugstore.drug_count)                  // � �볺��� ���� ���������
    {
        strcpy(error, "This client has no orders yet");
        return 0;
    }

    FILE* database = fopen(ORDER_DATA, "rb");


    fseek(database, drugstore.drug_one, SEEK_SET);    // �������� ������ �����
    fread(drug, ORDER_SIZE, 1, database);

    for (int i = 0; i < drugstore.drug_count; i++)        // ������ �������� ����� �� ���� ����������
    {
        // ��������
        if (drug->drugId == orderId)
        {
            fclose(database);
            return 1;
        }

        fseek(database, drug->nextAddress, SEEK_SET);
        fread(drug, ORDER_SIZE, 1, database);
    }

    // �� ��������
    strcpy(error, "No such order in database");
    fclose(database);
    return 0;
}

// �� ���� ������ ���������� � ���������� ����������
int updateDrug(struct Drug drug, int orderId)
{
    FILE* database = fopen(ORDER_DATA, "r+b");

    fseek(database, drug.selfAddress, SEEK_SET);
    fwrite(&drug, ORDER_SIZE, 1, database);
    fclose(database);

    return 1;
}

int deleteDrug(struct Drugstore drugstore, struct Drug drug, int orderId, char* error)
{
    FILE* database = fopen(ORDER_DATA, "r+b");
    struct Drug previous;

    fseek(database, drugstore.drug_one, SEEK_SET);

    do                                                        // ����� ����������� ������ 
    {
        fread(&previous, ORDER_SIZE, 1, database);
        fseek(database, previous.nextAddress, SEEK_SET);
    } while (previous.nextAddress != drug.selfAddress && drug.selfAddress != drugstore.drug_one);

    relinkAddresses(database, previous, drug, &drugstore);
    noteDeletedDrug(drug.selfAddress);            // �������� ������ ���������� ������ � �������

    drug.exists = 0;

    fseek(database, drug.selfAddress, SEEK_SET);
    fwrite(&drug, ORDER_SIZE, 1, database);
    fclose(database);

    drugstore.drug_count--;                    // ��������� ������� ���������
    updateDrugstore(drugstore, error);

    return 1;
}