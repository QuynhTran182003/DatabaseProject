using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project
{
    public class Ordering : IBaseClass<Ordering>
    {
        private int id;
        private DateTime date_order;
        //private int customer_id;
        private string customer_nane;
        private string customer_surname;

        public int Id { get => id; set => id = value; }
        public DateTime DateOrder { get => date_order; set => date_order = value; }
        //public int CustomerId { get => customer_id; set => customer_id = value; }
        public string CustomerName { get => customer_nane; set => customer_nane = value; }
        public string CustomerSurname { get => customer_surname; set => customer_surname = value; }
        public Ordering(DateTime date, string cus, string customer_surname)
        {
            DateOrder = date;
            CustomerName = cus;
            CustomerSurname = customer_surname;
        }

        public Ordering() { }
    }
}
