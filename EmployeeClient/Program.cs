using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeClient
{
    class Program
    {
        //static void Main()
        //{
        //    Client Response = new Client();
        //    Thread t1 = new Thread(new ThreadStart(Response.Employeeresponse));
        //    t1.Start();
        //    Thread.Sleep(60000);
        //}
        static async Task Main(string[] args)
        {
            int count = 0;
            using var client = new HttpClient();
            Employee source = new Employee();
            string[] lines = File.ReadAllLines("Emp.txt");
            foreach (var line in lines)
            {
                source = JsonConvert.DeserializeObject<Employee>(line);

                Employee Emp = new Employee();

                Emp.EmployeeID = source.EmployeeID;
                Emp.FirstName = source.FirstName;
                Emp.LastName = source.LastName;
                Emp.Gender = source.Gender;
                Emp.Email = source.Email;
                var json = JsonConvert.SerializeObject(Emp);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "https://localhost:44371/Emp/InsertEmp";
         

                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
                count++;
                Console.WriteLine("loop count"+count);
            }
            Console.ReadLine();
           
        }
    }
}
