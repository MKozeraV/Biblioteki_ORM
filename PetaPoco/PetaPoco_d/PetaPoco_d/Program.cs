using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPoco_d
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = new PetaPoco.Database("Dyplomowa");

            //pobranie wszystkich 
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                var ksiazki = db.Query<Book>("SELECT * FROM Books1");
            }
            sw.Stop();
            double time = (double)sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("Sredni czas wykonania operacji pobierania listy w milisekundach - " + time);
            //pobranie jednej 
            sw = new Stopwatch();
            sw.Start();
            for(int i = 10001;i < 20000;i++)
            {
                var ksiazka = db.Single<Book>(i);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 9999;
            Console.WriteLine("Sredni czas wykonania operacji pobierania 1 elementu w milisekundach - " + time);
            //dodawanie
            sw = new Stopwatch();
            sw.Start();
            for (int i = 10001; i < 20001; i++)
            {
                var book = new Book { nazwa = "testowa"+i.ToString(), autor = "testowy"+i.ToString(), gatunek = "horror"+i.ToString() };
                db.Save(book);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji dodania 1 elementu w milisekundach - " + time);
            //update elementu
            sw = new Stopwatch();
            sw.Start();
            for (int i = 10001; i < 20000; i++)
            {
                var book = db.Single<Book>(i);
                book.nazwa = "nowiutka";
                book.autor = "nowiutku";
                book.gatunek = "nowiutki";  
                db.Update(book);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 9999;
            Console.WriteLine("Sredni czas wykonania operacji updateu 1 elementu w milisekundach - " + time);
            //delete elementu
            sw = new Stopwatch();
            sw.Start();
            for (int i = 30001; i < 40001; i++)
            {
                db.Delete<Book>(i);
            }
            sw.Stop();
            time = (double)sw.ElapsedMilliseconds / 10000;
            Console.WriteLine("Sredni czas wykonania operacji usuniecia 1 elementu w milisekundach - " + time);
            Console.ReadLine();
            // var book = new Book { nazwa="testowa1", autor="testowy2", gatunek="horror"};
            // db.Save("Books1", "id_k",   book);
        }
    }
}
