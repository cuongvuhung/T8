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
        Dictionary<int, Employee> employees = new();

        // Contructor
        public EmployeeManager()
        {            
            if (!File.Exists(dataFileName))
            {
                Console.WriteLine("No database found!");
                Console.WriteLine("Creating new database!");
                // Create a new database file and a manager id 0, pass 0.
                File.AppendAllLinesAsync(dataFileName, new string[] { "0,name,0,email,phone,birthday,address,true,true" });
            }
            
            // Try get data
            try
            {
                Console.Write("Loading database!");Console.ReadLine();
                Import(dataFileName);
            }
            catch 
            { 
                Console.WriteLine("Bad data file"); Console.ReadLine();
            }            
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
            Export(dataFileName);
        }

        // Update
        public void Update(int id, string name, string password, string email, string phone, string birthday, string address, bool male,bool isManager)
        {
            employees.Remove(id);
            employees.Add(id, new Employee(id, name, password, email, phone, birthday, address, male, isManager));
            Export(dataFileName);
        }

        // Delete
        public void Delete(int id)
        {
            if (employees.ContainsKey(id))
            {
                employees.Remove(id);
                Export(dataFileName);
            }
            else
            {
                Console.Write("Invalid ID!"); Console.ReadLine();
            }
        }

        // Sort
        public void Sort()
        {
            Console.Clear();
            Console.WriteLine("Sorted list all employees:");
            foreach (KeyValuePair<int, Employee> author in employees.OrderBy(key => key.Value.name))
            {
                Console.WriteLine(author.Value.ToString());
            }
            Console.ReadLine();
        }

        //Export
        public void Export(string filename)
        {
            string[] content = new string[employees.Count];
            if (File.Exists(filename)) { File.Delete(filename); }
            int i = 0;
            foreach (var item in employees) 
            {
                content[i++] = item.Value.ToStringForExport();
            }
            File.AppendAllLinesAsync(filename, content);
            Console.WriteLine("Successful!"); Console.ReadLine();
        }

        //Import
        public void Import(string filename)
        {
            string[] content = File.ReadAllLines(filename);
            Dictionary<int,Employee> lobby = new();
            foreach (string line in content)
            {
                string[] cell = line.Split(",");
                if (!employees.ContainsKey(Convert.ToInt32(cell[0])))
                {                    
                    lobby.Add(Convert.ToInt32(cell[0]), new Employee(Convert.ToInt32(cell[0]), cell[1], cell[2], cell[3], cell[4], cell[5], cell[6], Convert.ToBoolean(cell[7]), Convert.ToBoolean(cell[8])));
               }
            }
            //Console.Write("Get {0} Employee to database",lobby.Count); Console.ReadLine();
            employees = employees.Concat(lobby.Where(x=>!employees.ContainsKey(x.Key))).ToDictionary(x=>x.Key,x=>x.Value);
            Export(dataFileName);            
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
