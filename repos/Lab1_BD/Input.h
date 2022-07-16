#pragma once
#include <stdio.h>
#include <string.h>
#include "Existing.h"
#include "Drugstore.h"

void readDrugstore(struct Drugstore* drugstore)
{
	char name[32];
	int status;

	name[0] = '\0';

	printf("Enter drugstore\'s name: ");
	scanf("%s", name);
	strcpy(drugstore->name, name);

	printf("Enter drugstore\'s status: ");
	scanf("%d", &status);
	drugstore->status = status;
	drugstore->drug_count = 0;
}

void readDrug(struct Drug* drug)
{

	char provider[256];
	char name[64];
	char date_creating[128];
	name[0] = date_creating[0] = provider[0] = '\0';

	printf("Enter drug\'s name: ");
	scanf("%s", name);
	strcpy(drug->name, name);

	printf("Enter drug\'s provider: ");
	scanf("%s", provider);
	strcpy(drug->provider, provider);

	printf("Date drug creating: ");
	scanf("%s", date_creating);
	strcpy(drug->date_creating, date_creating);

}