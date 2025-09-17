#include <iostream>

struct Parents
{
	char fioMother[50];
	char fioFather[50];
};

union PersonsData {
	Parents parents;
	int pasportData;
};


struct Person {
	char fio[50];
	int age;
	PersonsData personsData;
};





int main() {
	Person personsData[10] = { {"Tim", 17, {122}}, {"Zak", 15, {"Mieln, Nik"}}, {"Nik", 18, {178}, }, {"Alex", 19, {183}, }, {"Nikolay", 12, {"Zyani", "Robert"}}, {"Robert", 20, {192}, }, {"Iorvert", 21, {123}, }, {"Aleksandr", 23, {176}, }, {"Alex", 16, {"Milen", "Zak"}}, {"Robert", 19, {194}, }, };

	Person sortedData[10] = {};
	for (int i = 0; i < 10; i++) {
		Person person = personsData[i];			
		if (person.age > 16) {
			sortedData[i] = personsData[i];
		}
	}

	int listLength = sizeof(sortedData) / sizeof(sortedData[0]);
	while (listLength--)
	{
		bool swapped = false;
		for (int i = 0; i < listLength; i++)
		{
			if (sortedData[i].personsData.pasportData > sortedData[i + 1].personsData.pasportData)
			{
				Person temp = sortedData[i];
				sortedData[i] = sortedData[i + 1];
				sortedData[i + 1] = temp;
				swapped = true;
			}
		}

		if (swapped == false)
			break;
	}


	for (int i = 0; i < 10; i++) {

		if (sortedData[i].personsData.pasportData != 0) {
			std::cout << sortedData[i].fio << std::endl;
			std::cout << sortedData[i].age << std::endl;
			std::cout << sortedData[i].personsData.pasportData << std::endl;
		}
	}


	return 0;
}