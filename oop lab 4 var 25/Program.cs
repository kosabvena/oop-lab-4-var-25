using System;

class Program
{
    const int K = 25;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Benchmark.Run();  
        return;

        int n = (int)(20 + 0.6 * K);
        var rnd = new Random();

        int[] a = new int[n];
        for (int i = 0; i < n; i++) a[i] = rnd.Next(10, 101);

        Console.WriteLine($"N = {n}");
        Console.WriteLine("Вихідний масив: " + string.Join(" ", a));

        InsertionSort(a);
        Console.WriteLine("Відсортований:  " + string.Join(" ", a));

        Console.Write("Введіть ключове значення: ");
        int key = int.Parse(Console.ReadLine()!);

        int count = CountOccurrences(a, key);
        Console.WriteLine($"Значення {key} зустрічається {count} раз(ів)");

        Console.WriteLine($"Лінійний пошук: {LinearCount(a, key)} раз(ів)");
    }

    static void InsertionSort(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            int cur = a[i];
            int j = i - 1;
            while (j >= 0 && a[j] > cur)
            {
                a[j + 1] = a[j];
                j--;
            }
            a[j + 1] = cur;
        }
    }

    static int BinarySearch(int[] a, int key)
    {
        int lo = 0, hi = a.Length - 1;
        while (lo <= hi)
        {
            int mid = lo + (hi - lo) / 2;
            if (a[mid] == key) return mid;
            if (a[mid] < key) lo = mid + 1;
            else hi = mid - 1;
        }
        return -1;
    }

    static int CountOccurrences(int[] a, int key)
    {
        int idx = BinarySearch(a, key);
        if (idx == -1) return 0;
        int count = 1;
        for (int l = idx - 1; l >= 0 && a[l] == key; l--) count++;
        for (int r = idx + 1; r < a.Length && a[r] == key; r++) count++;
        return count;
    }

    static int LinearCount(int[] a, int key)
    {
        int c = 0;
        foreach (int x in a) if (x == key) c++;
        return c;
    }
}