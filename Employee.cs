using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T8
{
    internal class Employee
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = "";
        public string password { get; set; } = "";
        public string email { get; set; } = "";
        public string phone { get; set; } = "";
        public string birthday { get; set; } = "1/1/1980";
        public string address { get; set; } = "";
        public bool male { get; set; } = true;
        public bool isManager { get; set; } = false;

        public Employee() { }
        public Employee(int id,string name, string password, string email, string phone, string birthday, string address, bool male, bool isManager) 
        { 
            this.id =id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
            this.birthday = birthday;
            this.address = address;
            this.male = male;
            this.isManager = isManager;
        }
        public override string ToString()
        {
            return (id + "," + name + "," + email + "," + phone + "," + birthday + "," + address + "," + male);
        }
        public string ToStringForExport()
        {
            return (id + "," + name + "," + password + "," + email + "," + phone + "," + birthday + "," + address + "," + male + "," + isManager);
        }
    }
}
