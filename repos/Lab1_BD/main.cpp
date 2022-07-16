#include <stdio.h>
#include "Tables.h"
#include "Drugstore.h"
#include "Drug.h"
#include "Input.h"
#include "Output.h"


int main()
{
    struct Drugstore drugstore;
    struct Drug drug;

    while (1)
    {
        int id;
        int key;
        char error[64];

        printf("\t   Choose option:\n\t0 - Quit");
        printf("\n\t1 - Insert drugstore");
        printf("\n\t2 - Get drugstore");
        printf("\n\t3 - Update drugstore");
        printf("\n\t4 - Delete drugstore");
        printf("\n\t5 - Insert drug");
        printf("\n\t6 - Get drug");
        printf("\n\t7 - Update drug");
        printf("\n\t8 - Delete drug");
        printf("\n\t9 - Info\n\n");

        printf(">> ");
        scanf("%d", &key);
        printf("\n");

        switch (key) {
        case 0:
            return 0;
        case 1:
            readDrugstore(&drugstore);
            insertClient(drugstore);
            break;
        case 2:
            printf("Enter ID: ");
            scanf("%d", &id);
            if (getDrugstore(&drugstore, id, error))
            {
                printDrugstore(drugstore);
            }
            else
            {
                printf("Error: %s\n", error);
            }
            break;
        case 3:
            printf("Enter ID: ");
            scanf("%d", &id);
            drugstore.id = id;

            readDrugstore(&drugstore);
            updateDrugstore(drugstore, error) ? printf("Updated successfully\n") : printf("Error: %s\n", error);
            break;
        case 4:
            printf("Enter ID: ");
            scanf("%d", &id);
            deleteDrugstore(id, error) ? printf("Deleted successfully\n") : printf("Error: %s\n", error);
            break;
        case 5:
            printf("Enter drugstore\'s ID: ");
            scanf("%d", &id);
            if (getDrugstore(&drugstore, id, error))
            {
                drug.drugstoreId = id;
                printf("Enter drug\'s ID: ");
                scanf("%d", &id);
                drug.drugId = id;

                readDrug(&drug);
                insertDrug(drugstore, drug, error);
                printf("Inserted successfully. To access, use drugstore\'s and drug\'s IDs\n");
            }
            else
            {
                printf("Error: %s\n", error);
            }
            break;
        case 6:
            printf("Enter drugstore\'s ID: ");
            scanf("%d", &id);

            if (getDrugstore(&drugstore, id, error))
            {
                printf("Enter drug\'s ID: ");
                scanf("%d", &id);
                if (getDrug(drugstore, &drug, id, error))
                {
                    printDrug(drug, drugstore);
                }
                else
                {
                    printf("Error: %s\n", error);
                }
            }
            else
            {
                printf("Error: %s\n", error);
            }
            break;
        case 7:
            printf("Enter drugstore\'s ID: ");
            scanf("%d", &id);

            if (getDrugstore(&drugstore, id, error))
            {
                printf("Enter drug\'s ID: ");
                scanf("%d", &id);

                if (getDrug(drugstore, &drug, id, error))
                {
                    readDrug(&drug);
                    updateDrug(drug, id);
                    printf("Updated successfully\n");
                }
                else
                    printf("Error: %s\n", error);
            }
            else
                printf("Error: %s\n", error);
            break;
        case 8:
            printf("Enter Drugstore\'s ID: ");
            scanf("%d", &id);

            if (getDrugstore(&drugstore, id, error))
            {
                printf("Enter drug ID: ");
                scanf("%d", &id);

                if (getDrug(drugstore, &drug, id, error))
                {
                    deleteDrug(drugstore, drug, id, error);
                    printf("Deleted successfully\n");
                }
                else
                    printf("Error: %s\n", error);
            }
            else
                printf("Error: %s\n", error);
            break;
        case 9:
            info();
            break;

        default:
            printf("Invalid input, please try again\n");
        }

        printf("\n----------------------\n\n");
    }

    return 0;
}