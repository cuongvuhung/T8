using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace T8
{
    internal class EmployeeManager
    {
        private readonly string dataFileName = @"T8data.csv";
        Dictionary<int, Employee> employees = new ();

        // Contructor
        public EmployeeManager()
        {
            string[] content;
            if (!File.Exists(dataFileName))
            {
                Console.WriteLine("No database found!");
                Console.WriteLine("Creating new database!");
                // Create a database file and a manager id 0, pass 0.
                File.AppendAllTextAsync(dataFileName, "[0, 0,name,password,email,phone,birthday,address,true,true]");
            }
            content = File.ReadAllLines(dataFileName);
            // Try get data
            try
            {
                foreach (string line in content)
                {
                    string[] cell = line.Split(",");
                    employees.Add(Convert.ToInt32(cell[0]), new Employee(Convert.ToInt32(cell[0]), cell[1], cell[2], cell[3], cell[4], cell[5], cell[6], Convert.ToBoolean(cell[7]), Convert.ToBoolean(cell[8])));
                }
            }
            catch 
            { 
                Console.WriteLine("Bad data file");
            }
            Console.Write("Successful load data file"); Console.ReadLine();
        }

        // MODULE
        // Find
        public void Find(int key)
        {
            if ((employees.Count > 0) && (employees.ContainsKey(key))) Console.WriteLine(employees[key]);
            else Console.WriteLine("Invalid Employee"); Console.ReadLine();
        }

        // Add
        public void AddNew(int id, string name, string password, string email, string phone, string birthday, string address, bool male, bool isManager)
        {
            employees.Add(id, new Employee(id, name, password, email, phone, birthday, address, male, isManager));
            Console.WriteLine("Successful!"); Console.ReadLine();
        }

        // Update
        public void Update(int id, string name, string password, string email, string phone, string birthday, string address, bool male,bool isManager)
        {
            employees.Remove(id);
            employees.Add(id, new Employee(id, name, password, email, phone, birthday, address, male, isManager));
            Console.WriteLine("Successful!"); Console.ReadLine();
        }

        // Delete
        public void Delete(int id)
        {
            employees.Remove(id);
            Console.WriteLine("Successful!"); Console.ReadLine();
        }

        // Sort
        public void Sort()
        {
            Console.Clear();
            foreach (KeyValuePair<int, Employee> author in employees.OrderBy(key => key.Value.name))
            {
                Console.WriteLine(author.Value.ToString());
            }
            Console.ReadLine();
        }

        //Export
        public void Export(string filename)
        {
            foreach (var item in employees) 
            {
                File.AppendAllTextAsync(filename, item.Value.ToStringForExport());
            }
            Console.WriteLine("Successful!"); Console.ReadLine();
        }

        //Import
        public void Import(string filename)
        {
            string[] content = File.ReadAllLines(filename);
            foreach (string line in content)
            {
                string[] cell = line.Split(",");
                employees.Add(Convert.ToInt32(cell[0]), new Employee(Convert.ToInt32(cell[0]), cell[1], cell[2], cell[3], cell[4], cell[5], cell[6], Convert.ToBoolean(cell[7]), Convert.ToBoolean(cell[8])));
            }
            Console.WriteLine("Successful!"); Console.ReadLine();
        }

        //IsValid
        public bool IsValid(int id) 
        { 
            if (employees.ContainsKey(id)) return true;
            else return false;
        }
        //IsPassword
        public bool IsPassword(int id,string password)
        {
            if (employees.ContainsKey(id) && employees[id].password == password) return true;
            else return false;
        }
        //IsManager
        public bool IsManager(int id)
        {
            return employees[id].isManager;            
        }
    }
}
