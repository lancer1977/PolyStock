using System;
using System.Diagnostics;
using System.IO;
using Autofac;
using PolyStock.Console.Setup;
using PolyhydraGames.SQLite;
using PolyhydraGames.SQLite.Interfaces;

namespace PolyStock.Console
{
    class MainClass
    {
        public static   void Main(string[] args)
        {
            Debug.WriteLine("Hello World!");
            Bootstrapper.Initialize();
            var tools = new CovidTools();
              tools.StartPump();
        }
    }

 
}
