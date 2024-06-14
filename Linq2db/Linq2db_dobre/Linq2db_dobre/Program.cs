﻿//using DataModel;
using DataModels;
using LinqToDB.Configuration;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Data;
using static System.Diagnostics.Stopwatch;
using System.Diagnostics;

namespace Linq2db_dobre
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
            => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                // note that you can return multiple ConnectionStringSettings instances here
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "Dyplomowa   ",
                        ProviderName = ProviderName.SqlServer,
                        ConnectionString =
                            @"Server=.\;Database=Dyplomowa;Trusted_Connection=True;"
                    };
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            DataConnection.DefaultSettings = new MySettings();
            //pobieranie listy
            using (var db = new DyplomowaDB())
            {

                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var q =
                        from c in db.Books
                        select c;
                }
                //Console.WriteLine("Liczba elementow - "+q.Count());
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                Console.WriteLine("Sredni czas wykonania operacji pobrania listy w milisekundach - " + time);
            }
            //pobieranie konkretnego elementu
            using (var db = new DyplomowaDB())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var q =
                    from p in db.Books
                    where p.IdK == 5
                    select p;
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                Console.WriteLine("Sredni czas wykonania operacji pobrania konkretnego w milisekundach - " + time);
            }
            //dodawanie
            using (var db = new DyplomowaDB())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    db.Books
                        .Value(p => p.IdK, i)
                        .Value(p => p.Nazwa, "test1")
                        .Value(p => p.Autor, "test2")
                        .Value(p => p.Gatunek, "test3")
                        .Insert();
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                Console.WriteLine("Sredni czas wykonania operacji dodania w milisekundach - " + time);
            }
            //aktualizacja
            using (var db = new DyplomowaDB())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    db.Books
                        .Where(p=>p.IdK==i)
                        .Set(p => p.Nazwa, "testNowy1")
                        .Set(p => p.Autor, "testNowy2")
                        .Set(p => p.Gatunek, "testNowy3")
                        .Update();
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                Console.WriteLine("Sredni czas wykonania operacji aktualizacji w milisekundach - " + time);
            }
            //usuwanie
            using (var db = new DyplomowaDB())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    db.Books
                        .Where(p => p.IdK == i)
                        .Delete();
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                Console.WriteLine("Sredni czas wykonania operacji usuniecia w milisekundach - " + time);
            }






        }
    }
}