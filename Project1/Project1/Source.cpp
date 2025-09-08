#include<iostream>

int main() {

    std::cout << "Enter n" << std::endl;

    int n;

    std::cin >> n;



    while (n > 10) {
        int result = 0;
        int n1 = n;
        int nfirsthalf = n;
        short x = 1;
        short x2 = 0;

        while (n1 > 1)
        {
            n1 = n1 / 10;
            x++;
        }
     
        x = x / 2;
        x2 = x;     

        while (x != -1) {
            nfirsthalf = nfirsthalf / 10;
            x--;
        }

        int op = nfirsthalf;
        int nsecondhalf = n;
        int tp = nsecondhalf;

        std::cout <<  nfirsthalf << "  " << nsecondhalf << std::endl;


        while (x2 != 0) {
                
            int x3 = x2;
            

            op = nfirsthalf;
            tp = nsecondhalf;
            while (x3 != 1) {
                op = op / 10;

                x3--;
            }
            if (op > 10) {
                op = op % 10;
            }
            tp = tp % 10;
            x2--;
            nsecondhalf = nsecondhalf / 10;

            result += tp + op;
        
        }


        if (x % 2 != 0) {
            result += nsecondhalf % 10;
        }

        n = result;
        
        if (result % 10 == 0) {
            result = result / 10;
            std::cout << "Result: " << result << std::endl;
        }
        else {
            std::cout << "Result: " << result << std::endl;
        }

    }

       

    
    




}