#pragma once
#pragma warning(disable:4996)
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "Existing.h"
#include "Drugstore.h"

#define CLIENT_IND "drugstore.ind"
#define CLIENT_DATA "drugstore.fl"
#define CLIENT_GARBAGE "garbage_drugstore.txt"
#define INDEXER_SIZE sizeof(struct Indexer)
#define CLIENT_SIZE sizeof(struct Drugstore)

void overwriteGarbageId(int garbageCount, FILE* garbageZone, struct Drugstore* record)
{
    int* delIds = (int*)malloc(garbageCount * sizeof(int));    // ������ ��������� �������

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%d", delIds + i);

    record->id = delIds[0];

    fclose(garbageZone);
    fopen(CLIENT_GARBAGE, "wb");              // �������� ����� garbage
    fprintf(garbageZone, "%d", garbageCount - 1);      // ����� ����� ��������� �������

    for (int i = 1; i < garbageCount; i++)
        fprintf(garbageZone, " %d", delIds[i]);        // �������� ���� ������� �������

    free(delIds);                      // ��������� ���'���
    fclose(garbageZone);
}

void noteDeletedDrugstore(int id)
{
    FILE* garbageZone = fopen(CLIENT_GARBAGE, "rb");    // "rb": ��������� ������� ���� ��� �������

    int garbageCount;
    fscanf(garbageZone, "%d", &garbageCount);

    int* delIds = (int*)malloc(garbageCount * sizeof(int));    // �������� ���� ��� ��������� �������

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%d", delIds + i);

    fclose(garbageZone);
    garbageZone = fopen(CLIENT_GARBAGE, "wb");
    fprintf(garbageZone, "%d", garbageCount + 1);      // �������� ��� ������� �������

    for (int i = 0; i < garbageCount; i++)
        fprintf(garbageZone, " %d", delIds[i]);        // ��������� ������� �������

    fprintf(garbageZone, " %d", id);            // �������� ������� ��������� ������
    free(delIds);                      // ��������� ���'���
    fclose(garbageZone);
}

int insertClient(struct Drugstore record)
{
    FILE* indexTable = fopen(CLIENT_IND, "a+b");      // "a+b": ������� ������� ���� ��� ������ � ����� �� �������
    FILE* database = fopen(CLIENT_DATA, "a+b");
    FILE* garbageZone = fopen(CLIENT_GARBAGE, "rb");    // "rb": ������� ������� ���� ���� ��� �������

    struct Indexer indexer;
    int garbageCount;

    fscanf(garbageZone, "%d", &garbageCount);

    // ���� � ������� ������, ���������� ������ � ���
    if (garbageCount)
    {
        overwriteGarbageId(garbageCount, garbageZone, &record);

        fclose(indexTable);                  // ������� ����� � ������� �� ������� � �����
        fclose(database);

        indexTable = fopen(CLIENT_IND, "r+b");
        database = fopen(CLIENT_DATA, "r+b");

        fseek(indexTable, (record.id - 1) * INDEXER_SIZE, SEEK_SET);
        fread(&indexer, INDEXER_SIZE, 1, indexTable);
        fseek(database, indexer.address, SEEK_SET);      // ������ ������ �� "�����" ���  ����������  
    }
    // ���� ���� ��������� ������
    else
    {
        long indexerSize = INDEXER_SIZE;

        fseek(indexTable, 0, SEEK_END);            // ������� ������ � ����� �����

        // ���� ����� �������� �������� ����������
        if (ftell(indexTable))
        {
            fseek(indexTable, -indexerSize, SEEK_END);    // ������� ������ �� ������� ����������
            fread(&indexer, INDEXER_SIZE, 1, indexTable);  // ������ ������� ����������

            record.id = indexer.id + 1;            // �������� ����� ��������� ��������
        }
        // ���� �������� �������� ������� ��������� ��� ����� �� ������
        else
            record.id = 1;
    }

    record.drug_one = -1;
    record.drug_count = 0;

    fwrite(&record, CLIENT_SIZE, 1, database);        // �������� � ������� ���� ��-�������� �������� ���������

    indexer.id = record.id;                  // ������� ����� ������ � ����������
    indexer.address = (record.id - 1) * CLIENT_SIZE;    // ������� ������ ������ � ����������
    indexer.exists = 1;                    // �����, ��� ����� �� ��������� ������

    printf("Your client\'s id is %d\n", record.id); 
        fseek(indexTable, (record.id - 1) * INDEXER_SIZE, SEEK_SET);
    fwrite(&indexer, INDEXER_SIZE, 1, indexTable);      // �������� ���������� � �������� ��������
    fclose(indexTable);
    fclose(database);

    return 1;
}

int getDrugstore(struct Drugstore* drugstore, int id, char* error)
{
    FILE* indexTable = fopen(CLIENT_IND, "rb");        // "rb": ������� ������� ���� ���� ��� �������
    FILE* database = fopen(CLIENT_DATA, "rb");

    if (!checkFileExistence(indexTable, database, error))
        return 0;

    struct Indexer indexer;

    if (!checkIndexExistence(indexTable, error, id))
        return 0;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);  // �������� ���������� �������� ������ �� �������� �������
    fread(&indexer, INDEXER_SIZE, 1, indexTable);

    if (!checkRecordExistence(indexer, error))
        return 0;

    fseek(database, indexer.address, SEEK_SET);        // �������� ������� ����� � ��-�������� �� ��������� �������
    fread(drugstore, sizeof(struct Drugstore), 1, database);
    fclose(indexTable);
    fclose(database);

    return 1;
}

int updateDrugstore(struct Drugstore drugstore, char* error)
{
    FILE* indexTable = fopen(CLIENT_IND, "r+b");      // "r+b": ������� ������� ���� ��� ������� �� ������
    FILE* database = fopen(CLIENT_DATA, "r+b");

    if (!checkFileExistence(indexTable, database, error))
        return 0;

    struct Indexer indexer;
    int id = drugstore.id;

    if (!checkIndexExistence(indexTable, error, id))
        return 0;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);  // �������� ���������� �������� ������ �� �������
    fread(&indexer, INDEXER_SIZE, 1, indexTable);

    if (!checkRecordExistence(indexer, error))
        return 0;

    fseek(database, indexer.address, SEEK_SET);        // ������������� �� ������� ������ � ��
    fwrite(&drugstore, CLIENT_SIZE, 1, database);        // ��������� �����
    fclose(indexTable);
    fclose(database);

    return 1;
}

int deleteDrugstore(int id, char* error)
{
    FILE* indexTable = fopen(CLIENT_IND, "r+b");      // "r+b": ������� ������� ���� ��� ������� � ������

    if (indexTable == NULL)
    {
        strcpy(error, "database files are not created yet");
        return 0;
    }

    if (!checkIndexExistence(indexTable, error, id))
        return 0;

    struct Drugstore drugstore;
    getDrugstore(&drugstore, id, error);

    struct Indexer indexer;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);  // �������� ���������� �������� ������ �� �������
    fread(&indexer, INDEXER_SIZE, 1, indexTable);

    indexer.exists = 0;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);

    fwrite(&indexer, INDEXER_SIZE, 1, indexTable);
    fclose(indexTable);                    // ��������� ����

    noteDeletedDrugstore(id);                  // �������� ������ ���������� ������ �� garbage


    if (drugstore.drug_count)                // ���� ���� ����������, ���������
    {
        FILE* ordersDb = fopen(ORDER_DATA, "r+b");
        struct Drug order;

        fseek(ordersDb, drugstore.drug_one, SEEK_SET);

        for (int i = 0; i < drugstore.drug_count; i++)
        {
            fread(&order, ORDER_SIZE, 1, ordersDb);
            fclose(ordersDb);
            deleteDrug(drugstore, order, order.drugId, error);

            ordersDb = fopen(ORDER_DATA, "r+b");
            fseek(ordersDb, order.nextAddress, SEEK_SET);
        }

        fclose(ordersDb);
    }
    return 1;
}
