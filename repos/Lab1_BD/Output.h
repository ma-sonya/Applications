#pragma once

#include <stdio.h>
#include "Drugstore.h"
#include "Tables.h"

void printDrugstore(struct Drugstore drugstore)
{
	printf("Drugstore\'s name: %s\n", drugstore.name);
	printf("Number of drugs: %d\n", drugstore.drug_count);

}

void printDrug(struct Drug drug, struct Drugstore drugstore)
{
	printf("Drugstore name: %s\n", drugstore.name);
	printf("Drug\'s Id: %s\n", drug.drugId);
	printf("Drug  name: %s\n", drug.name);
	printf("Provider: %s\n", drug.provider);
	printf("Date of creating: %s\n", drug.date_creating);
}