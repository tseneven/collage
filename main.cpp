#include<iostream>

int main(){

    std::cout << "Введите число n"<< std::endl;

    short n;

    std::cin >> n;

    while (n != 0)
    {
        n = n / 10;
        std::cout << n << std::endl;
    }
    


}