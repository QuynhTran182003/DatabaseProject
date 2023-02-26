using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project
{
    public class Supplier : IBaseClass<Supplier>
    {
        private int id;
        private string name;
        private string phone;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }

        public Supplier() { }
        public Supplier(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public Supplier(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}
