using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            for (int i = 30000; i < 40000; i++)
            {
                var bookOne = table.All(where: "WHERE id_k=@0", args: 1);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;
            // pobranie jednego drugi sposób
            sw = new Stopwatch();
            sw.Start();
            for (int i = 30000; i < 40000; i++)
            {
                var bookOne = table.Single(30000);
                //Console.Write(bookOne+"\n");
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji pobrania 1 elementu w milisekundach - " + time);
            //dodanie 
            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                var inserted = table.Insert(new { nazwa = "nowaNazwa", autor = "nowyAutor", gatunek = "nowy gatunek" }); 
                var id_k = inserted.id_k;
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;

            Console.WriteLine("Sredni czas wykonania operacji dodania 1 elementu w milisekundach - " + time);
            //updateowanie
            sw = new Stopwatch();
            sw.Start();
            for (int i = 30000; i < 40000; i++)
            {
                 
                var updated =new { nazwa = "innaNazwa", autor = "innyAutor", gatunek = "inny gatunek" }; 
                table.Update(updated,i);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji update'u 1 elementu w milisekundach - " + time);
            //usuwanie
            sw = new Stopwatch();
            sw.Start();
            for (int i = 20000; i < 30000; i++)
            {
                table.Delete(i);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji usunięcia 1 elementu w milisekundach - " + time);
            // komenda ze zwyklym SQL   
            sw = new Stopwatch();
            sw.Start();
            for (int i = 30000; i < 40000; i++)
            {
                var bookOne = table.Query("SELECT * FROM Books1 where id_k=@0;", i);
                /*string json = JsonSerializer.Serialize(bookOne, new JsonSerializerOptions { WriteIndented=true});
                Console.WriteLine(json);*/
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds/ 10000;
            Console.WriteLine("Sredni czas wykonania operacji pobrania 1 elementu w milisekundach z uzyciem SQL - " + time);

            //test transakcji 
            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 300; i++)
            {
                using (var transakcja = table.OpenConnection().BeginTransaction())
                {
                    try
                    {
                        var inserted = table.Insert(new { nazwa = "nowaNazwat", autor = "nowyAutort", gatunek = "nowy gatunekt" });
                        var id_k = inserted.id_k;
                        transakcja.Commit();
                    }
                    catch
                    {
                        transakcja?.Rollback();
                    }
                }
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("Sredni czas wykonania operacji dodania 1 elementu w milisekundach z uzyciem transakcji - " + time);
            Console.ReadLine();
        }
    }
}
