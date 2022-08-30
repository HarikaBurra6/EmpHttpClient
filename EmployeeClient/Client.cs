using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeClient
{
    public class Client
    {
        public async void Employeeresponse()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = null;
                client.BaseAddress = new Uri("https://localhost:44371/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                for (int i = 0; i < 10; i++)
                {
                    result = await client.GetAsync("Emp/GetEmp");
                }
                if (result.Content is object && result.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var JsonString = await result.Content.ReadAsStringAsync();
                    try
                    {
                        List<Employee> json = JsonConvert.DeserializeObject<List<Employee>>(JsonString);
                        foreach (var j in json)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", j.EmployeeID, j.FirstName, j.LastName, j.Email, j.Gender);
                        }
                        Console.ReadLine();
                    }
                    catch (JsonReaderException)
                    {
                        Console.WriteLine("Invalid JSON.");
                    }
                }
                else
                {
                    Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                }
            }
        }
    }
}
