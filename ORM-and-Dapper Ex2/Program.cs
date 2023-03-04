using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace ORM_and_Dapper_Ex2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Dapper - Exercise 2\n" +
                              "1. The Products table has a new column Deleted (datetime).\n" +
                              "   which is used for soft-deletes since you wouldn't typically do\n" +
                              "   hard-deletes on a table like this and destroy historical sales records.\n" +
                              "2. I also added a column Updated (timestamp), which defaults to the\n" +
                              "   current timestamp on the server and is also updated by the DB whenever\n" +
                              "   the record changes. This is to support concurrency checks, which I've\n" +
                              "   never done with Dapper. Really just wanted to see if it could be done.\n" +
                              "3. I used abstract classes rather than interfaces here. I did Ex 1 as\n" +
                              "   specified in the directions but took liberties with this one, because\n" +
                              "   I hate not being able to implement anything in an interface... no doubt\n" +
                              "   it's just a personal preference. So OurDBObjects and OurDBTools are\n" +
                              "   sort of styled like I would do a base DB object in a real system.\n" +
                              "4. Please enter legitimate values when prompted. I've focused on the DB\n" +
                              "   and not on error-proofing user input on a console app!!");

            Console.Write("\n\nPress a ENTER for the demo...");
            Console.ReadLine();
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Common_App_Settings.json").Build();
            string connString = config.GetSection("ConnectionStrings")["DefaultConnection"];
            OurDBTools.Conn = new MySqlConnection(connString);           
            Console.WriteLine("The new configuration tools are implemented again; however, I've moved the JSON file to a shared location\n" +
                              "so that it can be re-used on future projects.");
            Console.WriteLine("----------");

            Console.Write("\n\nPress a ENTER to see a listing of all products, which uses the GetAll() method of OurDBTools...");
            Console.ReadLine();
            var pTools = new ProductTools();
            var prods = pTools.GetAll();
            foreach (Product pr in prods)
                Console.WriteLine(pr.Summary());
            Console.WriteLine("----------");

            Console.Write("\n\nEnter a product ID to see the Get() method used to retrieve a single record...");
            var tProd = Console.ReadLine();
            var p = (Product)pTools.Get(int.Parse(tProd));
            Console.WriteLine(p.Summary());
            Console.WriteLine("----------");

            Console.Write("\n\nEnter a product ID to be deleted, which uses the Delete() method...");
            var tProd2 = Console.ReadLine();
            Console.WriteLine($"Deletion of product {tProd2} {(pTools.Delete(int.Parse(tProd2)) ? "Succeeded" : "Failed")}.");
            Console.WriteLine("----------");

            Console.Write("\n\nEnter a product ID to be modifed, which uses the Update() method...");
            var tProd3 = Console.ReadLine();
            Console.Write("Enter its new name...");
            var newNm = Console.ReadLine();
            var p3 = (Product)pTools.Get(int.Parse(tProd3));
            p3.Name = newNm;
            Console.WriteLine(p3.Summary());
            Console.WriteLine($"Update of product {tProd3} {(pTools.Update(p3) ? "Succeeded" : "Failed")}.");
            Console.WriteLine("----------");

            Console.Write("\n\nNow we're creating a new product record using the Create() method...");
            Console.ReadLine();
            var newP = new Product() { CategoryID = 1, Name = "Sample Record", OnSale = 0, Price = 99, StockLevel = "1" };
            Console.WriteLine($"Product creation {(pTools.Create(newP) ? "Succeeded" : "Failed")}.");
            Console.WriteLine(newP.Summary());
            Console.WriteLine("----------");

            Console.WriteLine("\n\nWe'll display the entire list again to show the affected records in context...");
            Console.ReadLine();
            prods = pTools.GetAll();
            foreach (Product pr in prods)
                Console.WriteLine(pr.Summary());
            Console.WriteLine("----------");

            OurDBTools.Conn.Close();
        }
    }
}