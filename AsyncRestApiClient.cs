using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace AsyncRestApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                TestRestApi(i);
               
            }
            Console.WriteLine("Started");
            Thread.Sleep(5000);
            Console.ReadKey();
        }

        static async void TestRestApi(int i)
        {
           
            switch (i)
            {
                // Get All Employee Details
                case 0:
                    var res = await GetEmployee("http://localhost:8664/api/Employee");
                    var objEmp = JsonSerializer.Deserialize<IList<Employee>>(res);

                    foreach (var item in objEmp)
                    {
                        Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Age: {item.Age}");
                    }
                    Console.WriteLine("Get All Employee Details");

                    break;
                // Get Specific Employee Details
                case 1:
                    res = await GetEmployee("http://localhost:8664/api/Employee/4");
                    var options = new JsonSerializerOptions();
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            
                    Employee resEmp = JsonSerializer.Deserialize<Employee>(res);

                    Console.WriteLine($"Id: {resEmp.Id}, Name: {resEmp.Name}, Age: {resEmp.Age}");
                    Console.WriteLine("Get Specific Employee Details");
                    break;
                // Post Employee
                case 2:
                    Employee jsonRequest = new Employee()
                    {
                        Id = 55,
                        Name = "JSONPost9",
                        Age = 50,
                        isActive = true
                    };
                    string jsonString = JsonSerializer.Serialize(jsonRequest);
                    res = await AddEmployee(jsonString, "http://localhost:8664/api/Employee");
                    Console.WriteLine(res);
                    Console.WriteLine("Post Employee Details");
                    break;
                // Put Employee
                case 3:
                    Employee jsonPutRequest = new Employee()
                    {
                        Id = 2,
                        Name = "JSONPost10",
                        Age = 50,
                        isActive = true
                    };
                    jsonString = JsonSerializer.Serialize(jsonPutRequest);
                    res = await UpdateEmployee(jsonString, "http://localhost:8664/api/Employee");
                    Console.WriteLine(res);
                    Console.WriteLine("Put Employee Details");
                    break;
                // Delete Employee
                case 4:
                    res = await DeleteEmployee("http://localhost:8664/api/Employee/3");
                    Console.WriteLine(res);
                    Console.WriteLine("Delete Employee Details");
                    break;
                default:
                    break;
            }
            
           
        }
        static async Task<string> GetEmployee(string url)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.GetAsync(new Uri(url)))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }
        static async Task<string> AddEmployee(string jsonString, string url)
        {
            using (var client = new HttpClient())
            {
                var Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                using (var r = await client.PostAsync(url, Content))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }
        static async Task<string> UpdateEmployee(string jsonString, string url)
        {
            using (var client = new HttpClient())
            {
                var Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                using (var r = await client.PutAsync(url, Content))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }
        static async Task<string> DeleteEmployee(string url)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.DeleteAsync(new Uri(url)))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }
    }
    public class Employee
    {
        public  int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public bool isActive { get; set; }
    }
}
