using Shop_Project.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            CustomerOptions customerOptions = new CustomerOptions();
            customerOptions.Show();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            SupplierOptions supplierOptions = new SupplierOptions();
            supplierOptions.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderingOptions orderOpt = new OrderingOptions();
            orderOpt.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductOptions productOpt = new ProductOptions();
            productOpt.Show();
        }
    }
}
