using System;
using System.Collections.Generic;
using System.Diagnostics;

static class Benchmark
{
    public static void Run()
    {
        int[] sizes = { 1000, 5000, 10000, 20000 };
        var rnd = new Random(42);

        Console.WriteLine("N\tint[] ms\tList<int> ms\tBinIns ms\tBinSearch ns\tLinSearch ns");
        foreach (int n in sizes)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rnd.Next(10, 101);

            int[] a1 = (int[])arr.Clone();
            var list = new List<int>(arr);
            int[] a3 = (int[])arr.Clone();

            var sw = Stopwatch.StartNew();
            InsertionSortArray(a1);
            double tArr = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            InsertionSortList(list);
            double tList = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            BinaryInsertionSort(a3);
            double tBin = sw.Elapsed.TotalMilliseconds;

            const int reps = 100000;
            sw.Restart();
            for (int r = 0; r < reps; r++) Array.BinarySearch(a1, 55);
            double tBs = sw.Elapsed.TotalMilliseconds * 1e6 / reps;

            sw.Restart();
            for (int r = 0; r < reps; r++) Array.IndexOf(a1, 55);
            double tLs = sw.Elapsed.TotalMilliseconds * 1e6 / reps;

            Console.WriteLine($"{n}\t{tArr:F2}\t\t{tList:F2}\t\t{tBin:F2}\t\t{tBs:F0}\t\t{tLs:F0}");
        }
    }

    static void InsertionSortArray(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            int cur = a[i], j = i - 1;
            while (j >= 0 && a[j] > cur) { a[j + 1] = a[j]; j--; }
            a[j + 1] = cur;
        }
    }

    static void InsertionSortList(List<int> a)
    {
        for (int i = 1; i < a.Count; i++)
        {
            int cur = a[i], j = i - 1;
            while (j >= 0 && a[j] > cur) { a[j + 1] = a[j]; j--; }
            a[j + 1] = cur;
        }
    }

    static void BinaryInsertionSort(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            int cur = a[i], lo = 0, hi = i;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                if (a[mid] <= cur) lo = mid + 1; else hi = mid;
            }
            Array.Copy(a, lo, a, lo + 1, i - lo);
            a[lo] = cur;
        }
    }
}