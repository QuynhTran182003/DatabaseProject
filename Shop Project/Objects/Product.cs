using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project
{
    public class Product : IBaseClass<Product>
    {
        private int id;
        private string name;
        private float price;
        private int supplier_id;
        private string supplier_name;
        private bool in_stock;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public int Supplier_id { get => supplier_id; set => supplier_id = value; }
        public string Supplier_name { get => supplier_name; set => supplier_name = value; }
        public bool In_stock { get => in_stock; set => in_stock = value; }

        public Product() { }

        public Product(int id, string name, float price, int supplier_id)
        {
            Id = id;
            Name = name;
            Price = price;
            Supplier_id = supplier_id;
        }

        public Product(string name, float price, int supplier_id)
        {
            Name = name;
            Price = price;
            Supplier_id = supplier_id;
        }

        public Product(string name, float price, string supplier_name, bool in_stock)
        {
            Name = name;
            Price = price;
            Supplier_name = supplier_name;
            In_stock = in_stock;
        }
    }
}
