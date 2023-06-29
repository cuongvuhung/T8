using Microsoft.Data.SqlClient;
using System.Data;

namespace T8
{
    internal class EmployeeManager
    {
        Config config = new();
        Dictionary<int, Employee> employees = new();
        //private readonly string dataFileName = @"T8data.csv";

        // Contructor
        // CSV data
        /*public EmployeeManager()
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
        }*/
        // MSSQL data => employees
        public EmployeeManager()
        {
            {
                SqlConnection cnn = new SqlConnection(config.conStr);
                Dictionary<int, Employee> databaseLoad = new Dictionary<int, Employee>();
                cnn.Open();
                string sql = "Select * from Employee";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee item = new Employee(id: Convert.ToInt32(rdr.GetValue(0)),
                        name: Convert.ToString(rdr.GetValue(1) + "").Trim(),
                        password: Convert.ToString(rdr.GetValue(2) + "").Trim(),
                        email: Convert.ToString(rdr.GetValue(3) + "").Trim(),
                        phone: Convert.ToString(rdr.GetValue(4) + "").Trim(),
                        birthday: Convert.ToString(rdr.GetValue(5) + "").Trim(),
                        address: Convert.ToString(rdr.GetValue(6) + "").Trim(),
                        male: Convert.ToBoolean(rdr.GetValue(7)),
                        isManager: Convert.ToBoolean(rdr.GetValue(8)));
                    databaseLoad.Add(Convert.ToInt32(rdr.GetValue(0)), item);
                }
                employees = databaseLoad;
                cnn.Close();
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
            string sql = "Insert into Employee values (" + id + ",'" + name + "','" + password + "','" +
                email + "','" + phone + "','" + birthday + "','" + address + "'," + Convert.ToByte(male) + "," + Convert.ToByte(isManager) + ");";
            Export(sql);
            Console.Write("Successful!"); Console.ReadLine();
        }

        // Update
        public void Update(int id, string name, string password, string email, string phone, string birthday, string address, bool male, bool isManager)
        {
            employees.Remove(id);
            employees.Add(id, new Employee(id, name, password, email, phone, birthday, address, male, isManager));
            string sql = "Update Employee set id =" + id + ",name ='" + name + "',password ='" + password
                + "',email ='" + email + "',phone ='" + phone + "',birthday ='" + birthday
                + "',address ='" + address + "',male =" + Convert.ToByte(male) + ",isManager =" + Convert.ToByte(isManager)
                + "where id =" + id;
            Export(sql);
            Console.Write("Successful!"); Console.ReadLine();
        }

        // Delete
        public void Delete(int id)
        {
            if (employees.ContainsKey(id))
            {
                employees.Remove(id);
                string sql = "Delete from Employee where id=" + id;
                Export(sql);
                Console.Write("Successful!"); Console.ReadLine();
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
            Console.WriteLine("Unsorted list all employees:");
            foreach (var item in employees)
            {
                Console.WriteLine(item.Value.ToString());
            }
            Console.ReadLine();

            Console.WriteLine("Sorted list all employees:");
            foreach (KeyValuePair<int, Employee> item in employees.OrderBy(key => key.Value.name))
            {
                Console.WriteLine(item.Value.ToString());
            }
            Console.ReadLine();
        }

        //Import
        public void Import(string filename)
        {
            string[] content = File.ReadAllLines(filename);
            Dictionary<int, Employee> lobby = new();
            foreach (string line in content)
            {
                string[] cell = line.Split(",");
                if (!employees.ContainsKey(Convert.ToInt32(cell[0])))
                {
                    lobby.Add(Convert.ToInt32(cell[0]), new Employee(Convert.ToInt32(cell[0]), cell[1], cell[2], cell[3], cell[4], cell[5], cell[6], Convert.ToBoolean(cell[7]), Convert.ToBoolean(cell[8])));
                }
            }
            //Console.Write("Get {0} Employee to database",lobby.Count); Console.ReadLine();
            employees = employees.Concat(lobby.Where(x => !employees.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in lobby)
            {
                string sql = "Insert into Employee values (" + item.Value.id + ",'" + item.Value.name + "','" + item.Value.password + "','" +
                    item.Value.email + "','" + item.Value.phone + "','" + item.Value.birthday + "','" + item.Value.address + "'," + Convert.ToByte(item.Value.male) + "," + Convert.ToByte(item.Value.isManager) + ");";
                Export(sql);
                Console.Write("Successful!"); Console.ReadLine();
            }

        }
        //Export
        //CSV export
        public void ExportToCSV(string filename)
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
        //MS SQL
        public void Export(string sql)
        {
            SqlConnection cnn = new SqlConnection(config.conStr);
            SqlDataAdapter adt = new SqlDataAdapter();
            SqlCommand cmd;
            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            adt.InsertCommand = new SqlCommand(sql, cnn);
            adt.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();
        }
        //IsValid
        public bool IsValid(int id)
        {
            if (employees.ContainsKey(id)) return true;
            else return false;
        }
        //IsPassword
        public bool IsPassword(int id, string password)
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
