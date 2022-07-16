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
    reopenDatabase(database);                // Змінюємо режим на читання з та запис

    struct Drug previous;

    fseek(database, drugstore.drug_one, SEEK_SET);

    for (int i = 0; i < drugstore.drug_count; i++)        // Пробігаємомо зв'язаний список до останньої поставки
    {
        fread(&previous, ORDER_SIZE, 1, database);
        fseek(database, previous.nextAddress, SEEK_SET);
    }

    previous.nextAddress = drug.selfAddress;        // Зв'язуємо адреси
    fwrite(&previous, ORDER_SIZE, 1, database);        // Заносимо оновлений запис до файлу
}

void relinkAddresses(FILE* database, struct Drug previous, struct Drug drug, struct Drugstore* drugstore)
{
    // Якщо немає попередника
    if (drug.selfAddress == drugstore->drug_one)
    {
        if (drug.selfAddress == drug.nextAddress)      // Лише один запис
            drugstore->drug_one = -1;          // Неможлива адреса
        else
            drugstore->drug_one = drug.nextAddress;
    }
    // Якщо є попередник
    else
    {
        if (drug.selfAddress == drug.nextAddress)      // Останній запис
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
    long* delIds = (long*)malloc(garbageCount * sizeof(long));    // Виділяємо місце під список видалених адрес

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%d", delIds + i);

    record->selfAddress = delIds[0];
    record->nextAddress = delIds[0];

    fclose(garbageZone);                  // Очищуємо garbage_zone та записуємо нові видалені адреси
    fopen(ORDER_GARBAGE, "wb");
    fprintf(garbageZone, "%d", garbageCount - 1);

    for (int i = 1; i < garbageCount; i++)
        fprintf(garbageZone, " %d", delIds[i]);        // Записуємо решту видалених адрес

    free(delIds);                      // Звільняємо пам'ять
    fclose(garbageZone);
}

void noteDeletedDrug(long address)
{
    FILE* garbageZone = fopen(ORDER_GARBAGE, "rb");      // "rb": відкриваємо бінарний файл для читання

    int garbageCount;
    fscanf(garbageZone, "%d", &garbageCount);

    long* delAddresses = (long*)malloc(garbageCount * sizeof(long)); // Виділяємо місце під видалені адреси

    for (int i = 0; i < garbageCount; i++)
        fscanf(garbageZone, "%ld", delAddresses + i);

    fclose(garbageZone);                  // Очищення файлу з видаленими адресами
    garbageZone = fopen(ORDER_GARBAGE, "wb");
    fprintf(garbageZone, "%ld", garbageCount + 1);      // Запис нових видалених адрес

    for (int i = 0; i < garbageCount; i++)
        fprintf(garbageZone, " %ld", delAddresses[i]);

    fprintf(garbageZone, " %d", address);          // Запис останньої видаленої адреси
    free(delAddresses);                    // Звільняємо пам'ять
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
        reopenDatabase(database);                // Змінюємо режим доступу файлу
        fseek(database, drug.selfAddress, SEEK_SET);      // Ставимо курсор на "сміття" для перезапису
    }
    else                                                        // Видалених немає
    {
        fseek(database, 0, SEEK_END);

        int dbSize = ftell(database);

        drug.selfAddress = dbSize;
        drug.nextAddress = dbSize;
    }

    fwrite(&drug, ORDER_SIZE, 1, database);          // Записуємо замовлення до файлу

    if (!drugstore.drug_count)                    // Замовлень немає
        drugstore.drug_one = drug.selfAddress;
    else                                                        // Замовлення є
        linkAddresses(database, drugstore, drug);

    fclose(database);

    drugstore.drug_count++;                    // Зросла кількість замовлень
    updateDrugstore(drugstore, error);

    return 1;
}

int getDrug(struct Drugstore drugstore, struct Drug* drug, int orderId, char* error)
{
    if (!drugstore.drug_count)                  // В клієнта немає замовлень
    {
        strcpy(error, "This client has no orders yet");
        return 0;
    }

    FILE* database = fopen(ORDER_DATA, "rb");


    fseek(database, drugstore.drug_one, SEEK_SET);    // Отримуємо перший запис
    fread(drug, ORDER_SIZE, 1, database);

    for (int i = 0; i < drugstore.drug_count; i++)        // Шукаємо потрібний запис по коду замовлення
    {
        // Знайдено
        if (drug->drugId == orderId)
        {
            fclose(database);
            return 1;
        }

        fseek(database, drug->nextAddress, SEEK_SET);
        fread(drug, ORDER_SIZE, 1, database);
    }

    // Не знайдено
    strcpy(error, "No such order in database");
    fclose(database);
    return 0;
}

// На вхід подано замовлення з оновленими значеннями
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

    do                                                        // Пошук попередника запису 
    {
        fread(&previous, ORDER_SIZE, 1, database);
        fseek(database, previous.nextAddress, SEEK_SET);
    } while (previous.nextAddress != drug.selfAddress && drug.selfAddress != drugstore.drug_one);

    relinkAddresses(database, previous, drug, &drugstore);
    noteDeletedDrug(drug.selfAddress);            // Заносимо адресу видаленого запису у видалені

    drug.exists = 0;

    fseek(database, drug.selfAddress, SEEK_SET);
    fwrite(&drug, ORDER_SIZE, 1, database);
    fclose(database);

    drugstore.drug_count--;                    // Зменшення кількості замовлень
    updateDrugstore(drugstore, error);

    return 1;
}