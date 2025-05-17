using System;

namespace SeidelMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            // Початкова система рівнянь:
            // 2x1 + x2 + 5x3 = 17
            // 3x1 + 4.5x2 + 4x3 = 25
            // 4x1 + 2x2 + x3 = 20

            double[,] A = {
                { 4, 2, 1 },  // Переставили рядки для кращої діагональної переваги
                { 3, 4.5, 4 },
                { 2, 1, 5 }
            };

            double[] B = { 20, 25, 17 };  // Відповідно переставили вільні члени
            double[] X = { 1, 1, 1 };     // Краще початкове наближення

            int maxIterations = 1000;      // Збільшили кількість ітерацій
            double tolerance = 0.0001;

            Console.WriteLine("Розв'язання системи рівнянь методом Зейделя:");
            Console.WriteLine("Початкова система:");
            PrintSystem(A, B);

            bool converged = SolveSeidel(A, B, ref X, maxIterations, tolerance);

            if (converged)
            {
                Console.WriteLine("\nРозв'язок:");
                Console.WriteLine($"x1 = {X[0]:F6}");
                Console.WriteLine($"x2 = {X[1]:F6}");
                Console.WriteLine($"x3 = {X[2]:F6}");

                Console.WriteLine("\nПеревірка, підставляючи розв'язок у початкову систему:");
                CheckSolution(A, B, X);
            }
            else
            {
                Console.WriteLine("Метод не зійшовся за вказану кількість ітерацій.");
                Console.WriteLine("Можливі причини:");
                Console.WriteLine("- Система не має діагональної переваги");
                Console.WriteLine("- Система не має розв'язку");
                Console.WriteLine("- Недостатньо ітерацій для збіжності");
            }
        }

        static bool SolveSeidel(double[,] A, double[] B, ref double[] X, int maxIterations, double tolerance)
        {
            int n = X.Length;
            double[] previousX = new double[n];

            for (int iter = 0; iter < maxIterations; iter++)
            {
                Array.Copy(X, previousX, n);

                for (int i = 0; i < n; i++)
                {
                    double sum = B[i];

                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            sum -= A[i, j] * X[j];
                        }
                    }

                    X[i] = sum / A[i, i];
                }

                // Перевірка на збіжність
                double maxDiff = 0;
                for (int i = 0; i < n; i++)
                {
                    double diff = Math.Abs(X[i] - previousX[i]);
                    if (diff > maxDiff) maxDiff = diff;
                }

                if (maxDiff < tolerance)
                {
                    Console.WriteLine($"Метод зійшовся за {iter + 1} ітерацій.");
                    Console.ReadLine();
                    return true;
                }
            }
            return false;
        }

        static void PrintSystem(double[,] A, double[] B)
        {
            int n = B.Length;
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{A[i, 0]}x1 + {A[i, 1]}x2 + {A[i, 2]}x3 = {B[i]}");
                Console.WriteLine();
            }
        }

        static void CheckSolution(double[,] A, double[] B, double[] X)
        {
            int n = B.Length;
            double[] residuals = new double[n];

            for (int i = 0; i < n; i++)
            {
                residuals[i] = B[i];
                for (int j = 0; j < n; j++)
                {
                    residuals[i] -= A[i, j] * X[j];
                }

                Console.WriteLine($"Рівняння {i + 1}: Нев'язка = {residuals[i]:F6}");
            }
        }
    }
}
