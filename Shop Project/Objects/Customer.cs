using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project
{
    public class Customer : IBaseClass<Customer>
    {
        private int id;
        private string name;
        private string surname;
        private string phone;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Phone { get => phone; set => phone = value; }

        public Customer() { }
        public Customer(string name, string surname, string phone)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
        }

        public Customer(int id, string name, string surname, string phone)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Phone = phone;
        }
    }
}
