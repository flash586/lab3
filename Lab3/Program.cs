using System;
using System.Threading;
namespace Lab3
{
    class Program
    {
        public static int[,] A;
        public static int[,] B;
        public static int[,] res;
        public static int n;
        public static Thread myThread;
        public static void Show(int n, int[,] array)
        {
            Console.WriteLine("------------------------------------------");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }
        public static void Res(object i)
        {
            if((n%2==0 && n/2<=(int)i) || (n%2!=0 && (n+1)/2<= (int)i +1))
            {
                for(int m=0; m<n; m++)
                {
                    if ((int)i == n - 1)
                        res[(int)i, m] = B[(int)i, m];
                    else
                        res[(int)i, m] = B[(int)i, m] + res[(int)i +1,m ];
                }
            }
            else
            {
                for(int m=0; m<n; m++)
                {
                    res[(int)i, m] = res[n - (int)i -1, m];
                }
            }
            if ((int)i == 0)
            {
                return;
            }
            Res((int)i - 1);
        }
        static void Main(string[] args)
        {
            Console.Write("Input n: ");
            n = Int32.Parse(Console.ReadLine());
            A = new int[n, n];
            B = new int[n, n];
            res = new int[n, n];
            for (int j = 0; j < n / 2; j++)
            {
                for (int i = j; i < n && i < n - j; i++)
                {
                    A[i, j] = 1;
                }
            }
            for (int j = n - 1; j > n / 2 - 1; j--)
            {
                for (int i = Math.Abs(n - j) - 1; i < n && i <= j; i++)
                {
                    A[i, j] = 1;
                }

            }
            Console.WriteLine("-------------------A--------------------");
            Show(n, A);

            int k = 0;
            Random r = new Random();
            for (int i = n - 1; i > n / 2 - 1; i--)
            {
                for (int j = k; j < n - k; j++)
                {
                    B[i, j] = r.Next(1, 15);
                }
                k++;
            }
            Console.WriteLine("--------------------B---------------------");
            Show(n, B);

            myThread = new Thread(new ParameterizedThreadStart(Res));
            myThread.Start(n-1);
            myThread.Join();
            Console.WriteLine("--------------------RES---------------------");
            Show(n, res);
            Console.ReadKey();
        }
       
    }
}
