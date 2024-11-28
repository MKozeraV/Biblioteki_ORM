using Mighty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mighty_d
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            MightyOrm books = new MightyOrm("ProviderName=System.Data.SqlClient;Data Source=LAPTOP-HDG3IOL6;Initial Catalog=Dyplomowa;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true", "Books1", "id_k");
            //pobieranie listy
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                IEnumerable<dynamic> lista = books.All();
            }
            sw.Stop();
            double time = (double)sw.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji pobierania listy w milisekundach - " + time);
            //pobieranie jednej pozycji
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            for (int i = 0; i < 10000; i++)
            {
                dynamic book = books.Single(1);
            }
            sw1.Stop();
            time = (double)sw1.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji pobierania jednej pozycji w milisekundach - " + time);
            //dodawanie
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            for (int i = 0; i < 10000; i++)
            {
                books.Insert(new { nazwa = "nazwa1", autor = "Wazowski", gatunek = "fikcja" });
            }
            sw2.Stop();
            time = (double)sw2.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji dodania w milisekundach - " + time);
            //usuwanie
            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            for (int i = 1; i < 10000; i++)
            {
                books.Delete(new {id_k = i});
            }
            sw3.Stop();
            time = (double)sw3.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji usuwania w milisekundach - " + time);
            //aktualizacja
            Stopwatch sw4 = new Stopwatch();
            sw4.Start();
            for (int i = 10002; i < 11002; i++)
            {
                
                var p = new { id_k = i, nazwa="zmiana",autor="zmiana1",gatunek="zmiana2"};
                books.Update(p);
            }

            sw4.Stop();
            time = (double)sw4.ElapsedMilliseconds / 1000;
            Console.WriteLine("Sredni czas wykonania operacji aktualizacji w milisekundach - " + time);
            //dodawanie transakcją

            sw2 = new Stopwatch();
            sw2.Start();
            for (int i = 0; i < 10000; i++)
            {

                using (var conn = books.OpenConnection())
                {
                    using (var transakcja = conn.BeginTransaction())
                    {
                        books.Insert(new { nazwa = "nazwa1T", autor = "WazowskiT", gatunek = "fikcjaT" });
                        transakcja.Commit();
                    }
                }
            }
            sw2.Stop();
            time = (double)sw2.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji dodania z uzyciem transakcji w milisekundach - " + time);
            Console.ReadLine();
        }
    }
}
