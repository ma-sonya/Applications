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
    int* delIds = (int*)malloc(garbageCount * sizeof(int));    // Список видалених індексів

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%d", delIds + i);

    record->id = delIds[0];

    fclose(garbageZone);
    fopen(CLIENT_GARBAGE, "wb");              // Очищення файлів garbage
    fprintf(garbageZone, "%d", garbageCount - 1);      // Запис нових видалених індексів

    for (int i = 1; i < garbageCount; i++)
        fprintf(garbageZone, " %d", delIds[i]);        // Записуємо інші видалені індекси

    free(delIds);                      // Звільняємо пам'ять
    fclose(garbageZone);
}

void noteDeletedDrugstore(int id)
{
    FILE* garbageZone = fopen(CLIENT_GARBAGE, "rb");    // "rb": відкриваємо бінарний файл для читання

    int garbageCount;
    fscanf(garbageZone, "%d", &garbageCount);

    int* delIds = (int*)malloc(garbageCount * sizeof(int));    // Виділяємо місце для видалених індексів

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%d", delIds + i);

    fclose(garbageZone);
    garbageZone = fopen(CLIENT_GARBAGE, "wb");
    fprintf(garbageZone, "%d", garbageCount + 1);      // Записуємо нові видалені індекси

    for (int i = 0; i < garbageCount; i++)
        fprintf(garbageZone, " %d", delIds[i]);        // Повертаємо видалені індекси

    fprintf(garbageZone, " %d", id);            // записуємо останній видалений індекс
    free(delIds);                      // Звільняємо пам'ять
    fclose(garbageZone);
}

int insertClient(struct Drugstore record)
{
    FILE* indexTable = fopen(CLIENT_IND, "a+b");      // "a+b": відкрити бінарний файл для запису в кінець та читання
    FILE* database = fopen(CLIENT_DATA, "a+b");
    FILE* garbageZone = fopen(CLIENT_GARBAGE, "rb");    // "rb": відкрити бінарний файл лише для читання

    struct Indexer indexer;
    int garbageCount;

    fscanf(garbageZone, "%d", &garbageCount);

    // Якщо є видалені записи, перепишемо перший з них
    if (garbageCount)
    {
        overwriteGarbageId(garbageCount, garbageZone, &record);

        fclose(indexTable);                  // Змінюємо режим з читання на читання і запис
        fclose(database);

        indexTable = fopen(CLIENT_IND, "r+b");
        database = fopen(CLIENT_DATA, "r+b");

        fseek(indexTable, (record.id - 1) * INDEXER_SIZE, SEEK_SET);
        fread(&indexer, INDEXER_SIZE, 1, indexTable);
        fseek(database, indexer.address, SEEK_SET);      // Рухаємо курсор на "сміття" для  перезапису  
    }
    // Якщо немає видалених записів
    else
    {
        long indexerSize = INDEXER_SIZE;

        fseek(indexTable, 0, SEEK_END);            // Ставимо курсор у кінець файлу

        // Якщо розмір індексної таблички ненульовий
        if (ftell(indexTable))
        {
            fseek(indexTable, -indexerSize, SEEK_END);    // Ставимо курсор на останній індексатор
            fread(&indexer, INDEXER_SIZE, 1, indexTable);  // Читаємо останній індексатор

            record.id = indexer.id + 1;            // Нумеруємо запис наступним індексом
        }
        // Якщо індексна табличка порожня індексуємо наш запис як перший
        else
            record.id = 1;
    }

    record.drug_one = -1;
    record.drug_count = 0;

    fwrite(&record, CLIENT_SIZE, 1, database);        // Записуємо в потрібне місце БД-таблички передану структуру

    indexer.id = record.id;                  // Вносимо номер запису в індексатор
    indexer.address = (record.id - 1) * CLIENT_SIZE;    // Вносимо адресу запису в індексатор
    indexer.exists = 1;                    // Змінна, яка вказує на існування запису

    printf("Your client\'s id is %d\n", record.id); 
        fseek(indexTable, (record.id - 1) * INDEXER_SIZE, SEEK_SET);
    fwrite(&indexer, INDEXER_SIZE, 1, indexTable);      // Записуємо індексатор у відповідну табличку
    fclose(indexTable);
    fclose(database);

    return 1;
}

int getDrugstore(struct Drugstore* drugstore, int id, char* error)
{
    FILE* indexTable = fopen(CLIENT_IND, "rb");        // "rb": відкрити бінарний файл лише для читання
    FILE* database = fopen(CLIENT_DATA, "rb");

    if (!checkFileExistence(indexTable, database, error))
        return 0;

    struct Indexer indexer;

    if (!checkIndexExistence(indexTable, error, id))
        return 0;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);  // Отримуємо індексатор шуканого запису за вказаним номером
    fread(&indexer, INDEXER_SIZE, 1, indexTable);

    if (!checkRecordExistence(indexer, error))
        return 0;

    fseek(database, indexer.address, SEEK_SET);        // Отримуємо шуканий запис з БД-таблички за знайденою адресою
    fread(drugstore, sizeof(struct Drugstore), 1, database);
    fclose(indexTable);
    fclose(database);

    return 1;
}

int updateDrugstore(struct Drugstore drugstore, char* error)
{
    FILE* indexTable = fopen(CLIENT_IND, "r+b");      // "r+b": відкрити бінарний файл для читання та запису
    FILE* database = fopen(CLIENT_DATA, "r+b");

    if (!checkFileExistence(indexTable, database, error))
        return 0;

    struct Indexer indexer;
    int id = drugstore.id;

    if (!checkIndexExistence(indexTable, error, id))
        return 0;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);  // Отримуємо індексатор шуканого запису за номером
    fread(&indexer, INDEXER_SIZE, 1, indexTable);

    if (!checkRecordExistence(indexer, error))
        return 0;

    fseek(database, indexer.address, SEEK_SET);        // Позиціонуємося за адресою запису в БД
    fwrite(&drugstore, CLIENT_SIZE, 1, database);        // Оновлюємо запис
    fclose(indexTable);
    fclose(database);

    return 1;
}

int deleteDrugstore(int id, char* error)
{
    FILE* indexTable = fopen(CLIENT_IND, "r+b");      // "r+b": відкрити бінарний файл для читання і запису

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

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);  // Отримуємо індексатор шуканого запису за номером
    fread(&indexer, INDEXER_SIZE, 1, indexTable);

    indexer.exists = 0;

    fseek(indexTable, (id - 1) * INDEXER_SIZE, SEEK_SET);

    fwrite(&indexer, INDEXER_SIZE, 1, indexTable);
    fclose(indexTable);                    // Закриваємо файл

    noteDeletedDrugstore(id);                  // Заносимо індекс видаленого запису до garbage


    if (drugstore.drug_count)                // Якщо були замовлення, видаляємо
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
