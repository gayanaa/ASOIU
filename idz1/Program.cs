using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Введите первую строку (или 'exit' для выхода): ");
            string s1 = Console.ReadLine();

            if (s1 == "exit")
            {
                break;
            }

            Console.WriteLine("Введите вторую строку:");
            string s2 = Console.ReadLine();

            int result = DamerauLevenshtein(s1, s2);
            Console.WriteLine("Расстояние: " + result);
            Console.WriteLine();
        }
    }

    static int DamerauLevenshtein(string a, string b)
    {
        int n = a.Length;
        int m = b.Length;

        // Создаём массив массивов
        int[][] d = new int[n + 1][];

        // Для каждой строки создаём свой массив
        for (int i = 0; i <= n; i++)
        {
            d[i] = new int[m + 1];
        }

        for (int i = 0; i <= n; i++)
        {
            d[i][0] = i;
        }

        for (int j = 0; j <= m; j++)
        {
            d[0][j] = j;
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;

                int insert = d[i][j - 1] + 1;
                int delete = d[i - 1][j] + 1;
                int replace = d[i - 1][j - 1] + cost;

                int min = insert;
                if (delete < min) min = delete;
                if (replace < min) min = replace;

                d[i][j] = min;

                if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1])
                {
                    int transp = d[i - 2][j - 2] + 1;
                    if (transp < d[i][j])
                    {
                        d[i][j] = transp;
                    }
                }
            }
        }
        return d[n][m];
    }
}