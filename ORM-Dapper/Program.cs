using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace ORM_Dapper
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();
            var connString = config.GetConnectionString("DefaultConnection");
            
            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DepartmentRepository(conn);
            var departments = repo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }
                                   
            var productRepo = new DapperProductRepository(conn);
                                   
            var products = productRepo.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"ID: {prod.ProductID}, Name: {prod.Name}, Price: ${prod.Price}");
            }
                                   
           //UpdateProductName();
        }

        public static void UpdateProductName()
        {
            var config = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json")
                             .Build();
            var connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var prodRepo = new DapperProductRepository(conn);

            Console.WriteLine($"What is the productID of the product you would like to update?");
            var productID = int.Parse(Console.ReadLine());

            Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
            var updatedName = Console.ReadLine();
                       
            prodRepo.UpdateProductName(productID, updatedName);
        }
    }
}
