using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive_d
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            var table = new Books();
            //pobranie listy
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                var books = table.All();
            }
            sw.Stop();
            double time = (double)sw.ElapsedMilliseconds / 10000;

            Console.WriteLine("Sredni czas wykonania operacji pobrania listy w milisekundach - " + time);
            //pobranie jednego elementu
            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                var bookOne = table.All(where: "WHERE id_k=@0", args: 1);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;

            Console.WriteLine("Sredni czas wykonania operacji pobrania 1 elementu w milisekundach - " + time);
            //dodanie 
            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                var inserted = table.Insert(new { nazwa = "nowaNazwa", autor = "nowyAutor", gatunek = "nowy gatunek" }); //do poprawy 
                var id_k = inserted.id_k;
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;

            Console.WriteLine("Sredni czas wykonania operacji dodania 1 elementu w milisekundach - " + time);
            //usuwanie
            Console.ReadLine();
        }
    }
}
