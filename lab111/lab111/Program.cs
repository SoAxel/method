using System;

namespace ChordMethodLab1
{
    class Program
    {
        static double Function(double x)
        {
            return Math.Cos(Math.Sin(Math.Pow(x, 3))) - 0.7;
        }

        static double ChordMethod(double a, double b, double epsilon, out int iterations)
        {
            iterations = 0;
            double x_prev = a;
            double x_curr = b;
            double x_next;

            Console.WriteLine("Ітерація\t a\t\t b\t\t x\t\t f(x)");

            while (iterations < 1000)
            {
                iterations++;
                x_next = x_curr - Function(x_curr) * (x_curr - x_prev) / (Function(x_curr) - Function(x_prev));

                Console.WriteLine($"{iterations}\t\t {x_prev:F6}\t {x_curr:F6}\t {x_next:F6}\t {Function(x_next):F6}");

                if (Math.Abs(x_next - x_curr) < epsilon)
                    return x_next;

                if (Function(x_next) * Function(x_prev) < 0)
                    x_curr = x_next;
                else
                    x_prev = x_next;
            }

            return x_curr;
        }

        static void FindRoots(double a, double b, double step, double epsilon)
        {
            double x = a;
            while (x < b)
            {
                double xNext = x + step;
                if (Function(x) * Function(xNext) <= 0) // Знак змінився
                {
                    Console.WriteLine($"\nЗнайдено підінтервал з коренем: [{x:F6}, {xNext:F6}]");
                    int iterations;
                    double root = ChordMethod(x, xNext, epsilon, out iterations);
                    Console.WriteLine($"Корінь: {root:F6}, f(x) = {Function(root):F6}, ітерацій: {iterations}");
                }
                x = xNext;
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            double a = -Math.PI / 2;
            double b = Math.PI / 2;
            double epsilon = 0.01;
            double step = 0.1; // Крок для пошуку підінтервалів

            Console.WriteLine($"Пошук коренів на інтервалі [{a:F6}, {b:F6}]...");
            FindRoots(a, b, step, epsilon);

            Console.ReadLine();
        }
    }
}