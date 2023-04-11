using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCoban
{
    internal class Program
    {
        static int[] bubbleSort(ref int[] arr, int n, int p)
        {
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (arr[j] * p > arr[j - 1] * p)
                    {
                        var tmp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = tmp;
                    }
                }
            }
            return arr;
        }
        static void showArrBy(ref int[] arr, ArrayList<int> a, ArrayList<int> b, int n)
        {
            foreach (var item in arr)
            {
                a.Add(item % 2 == 0 ? item);
            }
        }
        static void Main(string[] args)
        {
            int[] arr = new int[10];
            Random rd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rd.Next(10, 100);
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            bubbleSort(ref arr, arr.Length, -1);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            showArrBy(ref arr, 10);

        }
    }
}
