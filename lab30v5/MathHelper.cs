using System;

namespace lab30v5
{
    public class MathHelper
    {
        public bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            if (number == 2)
                return true;

            if (number % 2 == 0)
                return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        public long Factorial(int number)
        {
            if (number < 0)
                throw new ArgumentException("Factorial is not defined for negative numbers.");

            long result = 1;

            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }

        public int Fibonacci(int number)
        {
            if (number < 0)
                throw new ArgumentException("Fibonacci is not defined for negative numbers.");

            if (number == 0)
                return 0;

            if (number == 1)
                return 1;

            int a = 0;
            int b = 1;

            for (int i = 2; i <= number; i++)
            {
                int temp = a + b;
                a = b;
                b = temp;
            }

            return b;
        }

        public int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0 && b == 0)
                throw new ArgumentException("GCD is undefined for both values equal to zero.");

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}