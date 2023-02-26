using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project
{
    public class Item : IBaseClass<Item>
    {
        private int id;
        private int product_id;
        private int ordering_id;
        private int quantity;
        private string product_name;

        public int Id { get => id; set => id = value; }
        public int ProductId { get => product_id; set => product_id = value; }
        public int OrderingId { get => ordering_id; set => ordering_id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string ProductName { get => product_name; set => product_name = value; }

        public Item() { }
        public Item(int id, int product_id, int ordering_id, int quantity)
        {
            Id = id;
            ProductId = product_id;
            OrderingId = ordering_id;
            Quantity = quantity;
        }
        public Item(int product_id, int ordering_id, int quantity)
        {
            ProductId = product_id;
            OrderingId = ordering_id;
            Quantity = quantity;
        }
        public Item(string product_name, int ordering_id, int quantity)
        {
            ProductName = product_name;
            OrderingId = ordering_id;
            Quantity = quantity;
        }
    }
}
