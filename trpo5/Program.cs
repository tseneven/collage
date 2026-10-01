namespace trpo5;

public class Program()
{
    // 13 => 13
    // 22 => 2 11 => 11 2
    // 24 => 2 2 2 3
    // 32 => 2 2 2 2 2
    // 49 => 7 7
    // 33 => 3 11 => 11 3

    // 1 => throw
    // n < 0 => throw
    // 2 => 2
    static void Main()
    {
        Console.WriteLine("Введите число\n");
        int n = int.Parse(Console.ReadLine());

        if (n <= 1)
            throw new InvalidOperationException("Ну че-то не то, число не может быть меньше 2");

        int result = 0;
        int countFirstSimple = 0;
        int countNumber = 0;

        while (n % 2 == 0)
        {
            n /= 2;
            countFirstSimple++;
        }

        for (int i = 1; i < countFirstSimple + 1; i++)
        {
            result = AddToResult(result, 2, ref countNumber);
        }


        for (int i = 3; i * i <= n; i += 2)
        {
            while (n % i == 0)
            {
                result = AddToResult(result, i, ref countNumber);
                n /= i;
            }
        }

        if (n > 1)
        {
            result = AddToResult(result, n, ref countNumber);
        }

        Console.WriteLine(result * countNumber);
    }

    private static int AddToResult(int result, int n, ref int countNumber)
    {
        if (result > 1)
        {
            if (result > n)
                result *= 10;
            else
                n *= 10;
        }

        result += n;
        countNumber++;
        return result;
    }
}