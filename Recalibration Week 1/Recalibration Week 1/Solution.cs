using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Simon is coding...");

        Console.WriteLine(add(1, 2));
    }

    private static int add(int a, int b)
    {
        return a + b;
    }
}