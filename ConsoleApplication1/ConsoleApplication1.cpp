
#include <iostream>
#include <math.h> 



// struct Contact
// {
// 	char phone[255]; 
// 	char name[255];
// };

// void EnterContact(Contact contact[], int index, Contact values) {
// 	contact[index] = values;
// }

// void PrintContact(Contact contact[], int count) {

// 	for (int i = 0; i < count; i++) {
// 		std::cout << i+1 << " Contact " << contact[i].name << std::endl;
// 		std::cout << contact[i].phone << std::endl;
// 	}
// }
// void DeleteContact(Contact contact[], int index) {
// 	int strlenname = strlen(contact[index - 1].name);
// 	int strlenphone = strlen(contact[index - 1].phone);


// 	for (int i = strlenname; i > 0; i--) {
// 		contact[index-1].name[i-1] = contact[index-1].name[i];
// 	}

// 	for (int i = strlenphone; i > 0; i--) {
// 		contact[index-1].phone[i - 1] = contact[index-1].phone[i];
// 	}
// }
// void EditContact(Contact contact[], int index, Contact newValues) {
// 	contact[index-1] = newValues;
// }

class Triangle {
private:
	double side1;
	double side2;
	double side3;
public:
	Triangle(double Side1, double Side2, double Side3) {
		side1 = Side1;
		side2 = Side2;
		side3 = Side3;
	}
	double S() {
		double p = (side1 + side2 + side3) / 2;
		return sqrt(p * (p - side1) * (p - side2) * (p - side3));
	}
};

int main()
{
	//int mass[] = {6, 1, 7, 9, 0, 6, 7};
	//int temp = 0;
	//int x = sizeof(mass) / sizeof(mass[0]);

	//for (int i = 0; i < x/2; i++) {
	//	temp = mass[i];
	//	mass[i] = mass[x - 1 - i];
	//	mass[x - 1 - i] = temp;
	//}
	//for (int i = 0; i < x; i++) {
	//	std::cout << mass[i] << std::endl;
	//}


	//// Вторая

	//int mass[3][3][3] = {
	//	{{5,2,4},{7,3,9},{4,7,10}}, 
	//	{{7,3,8},{12,22,34},{13,21,36}}, 
	//	{{13,26,31},{19,21,30},{11,29,39}} 
	//} ;
	//
	//int one = sizeof(mass) / sizeof(mass[0]);
	//int two = sizeof(mass[0]) / sizeof(mass[0][0]);
	//int three = sizeof(mass[0][0]) / sizeof(mass[0][0][0]);

	//int min = 0;

	//
	//for (int i = 0; i < one; i++) {
	//	min = mass[i][0][0];
	//	for (int j = 0; j < two; j++) {
	//		for (int t = 0; t < three; t++) {
	//			if (mass[i][j][t] < min) {
	//				min = mass[i][j][t];
	//			}
	//		}
	//	}
	//	std::cout << min << std::endl;
	//	min = 0;
	//}

	//// Третья

	//Contact myContacts[20] = { {"1200", "Tim"}, {"1100", "Nik"}};

	//int index = 2;
	//short action = 0;


	//while (true) {
	//	std::cout << "Select action \n1 - Contact List \n2- Add contact \n3-Delete contact \n4-Edit contact\n5-Exit \nMemory = 20" << std::endl;

	//	std::cin >> action;

	//	if (action == 1) {
	//		int count = sizeof(myContacts) / sizeof(myContacts[0]);
	//		PrintContact(myContacts, count);
	//	}
	//	else if (action == 2) {
	//		Contact contact;

	//		std::cout << "Enter name" << std::endl;
	//		std::cin >> contact.name;
	//		std::cout << "Enter phone" << std::endl;
	//		std::cin >> contact.phone;



	//		EnterContact(myContacts, index, contact);
	//		int count = sizeof(myContacts) / sizeof(myContacts[0]);
	//		index++;
	//		PrintContact(myContacts, count);
	//	}
	//	else if (action == 3) {
	//		int deleteIndex = 0;
	//		int count = sizeof(myContacts) / sizeof(myContacts[0]);

	//		std::cout << "Enter index contact" << std::endl;
	//		std::cin >> deleteIndex;

	//		DeleteContact(myContacts, deleteIndex);
	//		PrintContact(myContacts, count);
	//	}
	//	else if (action == 4) {
	//		Contact contact;
	//		int editIndex = 0;

	//		std::cout << "Enter index contact" << std::endl;
	//		std::cin >> editIndex;
	//		std::cout << "Enter new name" << std::endl;
	//		std::cin >> contact.name;
	//		std::cout << "Enter new phone" << std::endl;
	//		std::cin >> contact.phone;

	//		EditContact(myContacts, editIndex, contact);
	//		int count = sizeof(myContacts) / sizeof(myContacts[0]);

	//		PrintContact(myContacts, count);
	//	}
	//	else if (action == 5) {
	//		break;
	//	}

	//}

	//// Четвертая
	double a;
	double b;
	double c;

	std::cout << "Enter a" << std::endl;
	std::cin >> a;
	std::cout << "Enter b" << std::endl;
	std::cin >> b;
	std::cout << "Enter c" << std::endl;
	std::cin >> c;

	Triangle triangle(a, b, c);
	double result = triangle.S();

	std::cout << result << std::endl;

	return 0;
}

