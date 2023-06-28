using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using T8;

namespace T8
{
    public class Screen 
    {
        //private static readonly string logPath = "T7log.txt";
        private bool loged = false;
        private int idLogin;
        EmployeeManager manager = new EmployeeManager();
        public Screen() { }
        public void Start()
        {            
            do
            {
                string login = Login();
                switch (login)
                {
                    case "manager":
                        this.ManagerScreen();
                        break;
                    case "user":
                        this.UserScreen();
                        break;
                }
            } while (!loged);
        }    
        
        //Login
        public string Login()
        {
            string passwordLogin;
            string role = "";

            Console.Clear();
            Console.WriteLine("***EMPLOYEE MANAGER***");
            Console.WriteLine("***  LOGIN SCREEN  ***");
            Console.WriteLine("----------------------");
            do
            {
                Console.Write("ID:");
                idLogin = (Convert.ToInt32(Console.ReadLine()));
                Console.Write("Password:");
                passwordLogin = (Console.ReadLine() + "");
                if (!manager.IsValid(idLogin))
                {
                    Console.WriteLine("Invalid username");
                    Console.ReadLine();
                    continue;
                }
                if (!manager.IsPassword(idLogin, passwordLogin))
                {
                    Console.WriteLine("Wrong password");
                    Console.ReadLine();
                    continue;
                }                
                this.loged = true;
                if (manager.IsManager(idLogin)) role = "manager";
                if (!manager.IsManager(idLogin)) role = "user";
                Console.WriteLine("Login succesful");
                Console.ReadLine();
                Console.Clear();
            }
            while (!loged);
            return role;
        }

        // Module Manager Screen
        public void ManagerScreen()
        {
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("*** MANAGER SCREEN ***");
                Console.WriteLine("----------------------");
                Console.WriteLine("ID Login: {0}", idLogin);
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Show all Employee sorted");
                Console.WriteLine("6. Import a list of Employee");
                Console.WriteLine("7. Export a list of Employee");
                Console.WriteLine("8. Logout");
                Console.WriteLine("9. Exit");
                Console.Write("   Select (1-9): ");
                
                // Try get a select with right numeric
                try 
                { 
                    selected = Convert.ToInt16(Console.ReadLine()); 
                }
                catch { }
                                
                // Route program
                switch (selected)
                {
                    case 1:
                        FindScreen();
                        break;
                    case 2:
                        AddNewScreen();
                        break;
                    case 3:
                        UpdateScreen();
                        break;
                    case 4:
                        DeleteScreen();
                        break;
                    case 5:
                        SortScreen();
                        break;
                    case 6:
                        ImportScreen();
                        break;
                    case 7:
                        ExportScreen();
                        break;
                    case 8:
                        loged = false;
                        Console.WriteLine("Logging out");                        
                        break;
                    case 9:                        
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 9 && loged);
        }

        // Module User Screen
        public void UserScreen()
        {
            int selected = 0;
            do
            {
                Console.Clear();                
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("***  USER SCREEN   ***");
                Console.WriteLine("----------------------");
                Console.WriteLine("ID Login: {0}", idLogin);
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Show all Employee");
                Console.WriteLine("3. Log out");
                Console.WriteLine("4. Exit");
                Console.Write("   Select (1-4): ");

                // Try get a select with right numeric
                try
                {
                    selected = Convert.ToInt16(Console.ReadLine());
                }
                catch { }

                // Route program
                switch (selected)
                {
                    case 1:
                        FindScreen();
                        break;
                    case 2:
                        SortScreen();
                        break;
                    case 3:
                        loged = false;
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 4 && loged);
        }
        
        // Find Screen
        public void FindScreen() 
        {
            Console.Clear();
            Console.WriteLine("ID:");
            int keyFind = 0;
            try
            {
                keyFind = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong format!");
            }
            manager.Find(keyFind);
        }
        
        // AddNew Screen
        public void AddNewScreen() 
        {
            int id ;
            string name, password, email, phone, birthday, address;
            bool male, isManager;
            Console.Clear();
            Console.WriteLine("ID:");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Name:");
                name = Console.ReadLine() + "";
                Console.WriteLine("Password:");
                password = Console.ReadLine() + "";
                Console.WriteLine("Email:");
                email = Console.ReadLine() + "";
                Console.WriteLine("Phone:");
                phone = Console.ReadLine() + "";
                Console.WriteLine("Birthday:");
                birthday = Console.ReadLine() + "";
                Console.WriteLine("Address:");
                address = Console.ReadLine() + "";
                Console.WriteLine("Male:");
                male = Convert.ToBoolean(Console.ReadLine());
                Console.WriteLine("Is Manager:");
                isManager = Convert.ToBoolean(Console.ReadLine());
                manager.AddNew(id, name, password, email, phone,birthday, address,male,isManager);
            }
            catch
            {
                Console.WriteLine("Wrong format!");
            }
        }

        // Update Screen
        public void UpdateScreen() 
        {
            Console.Clear();
            Console.WriteLine("ID:");
            int keyUpdate = 0;            
            string name, password, email, phone, birthday, address;
            bool male, isManager;
            try
            {
                keyUpdate = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong format!");
            }
            if (manager.IsValid(keyUpdate)) manager.Find(keyUpdate);
            try
            {
                Console.WriteLine("name:");
                name = Console.ReadLine() + "";
                Console.WriteLine("Password:");
                password = Console.ReadLine() + "";
                Console.WriteLine("Email:");
                email = Console.ReadLine() + "";
                Console.WriteLine("Phone:");
                phone = Console.ReadLine() + "";
                Console.WriteLine("Birthday:");
                birthday = Console.ReadLine() + "";
                Console.WriteLine("Address:");
                address = Console.ReadLine() + "";
                Console.WriteLine("Male:");
                male = Convert.ToBoolean(Console.ReadLine());
                Console.WriteLine("Is Manager:");
                isManager = Convert.ToBoolean(Console.ReadLine());
                manager.Update(keyUpdate, name, password, email, phone, birthday, address, male, isManager);
            }
            catch
            {
                Console.WriteLine("Wrong format!");
            }
        }

        // Delete Screen
        public void DeleteScreen()
        {
            Console.Clear();
            Console.WriteLine("ID:");
            int keyDelete;
            try
            {
                keyDelete = Convert.ToInt32(Console.ReadLine());
                manager.Delete(keyDelete);
            }
            catch
            {
                Console.WriteLine("Wrong format!");
            }
        }

        // Import Screen
        public void ImportScreen() 
        {
            Console.Clear();
            Console.WriteLine("File name:");
            string fileName = @""+(Console.ReadLine()+".csv");
            try
            {
                if (File.Exists(fileName)) manager.Import(fileName);
            }
            catch
            {
                Console.WriteLine("Bad file name!");
            }
        }

        // Export Screen
        public void ExportScreen() 
        {
            Console.Clear();
            Console.WriteLine("File name:");
            string fileName = @"" + (Console.ReadLine() + ".csv");
            try
            {
                if (!File.Exists(fileName)) manager.Export(fileName);
            }
            catch
            {
                Console.WriteLine("Bad file name!");
            }
        }

        // Sort Sreen
        public void SortScreen() 
        { 
            manager.Sort();
        }
        


    }
}
