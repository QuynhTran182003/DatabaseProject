using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Project.Forms
{
    public partial class NewCustomerForm : Form
    {
        private CustomerOptions customerOptions;
        public NewCustomerForm(CustomerOptions form)
        {
            InitializeComponent();
            customerOptions = form;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tbxPhone.Text == "" || this.tbxName.Text == "" || this.tbxSurname.Text == "")
            {
                MessageBox.Show("All fields must be filled in.");
            }
            else
            {
                string name = this.tbxName.Text;
                string surname = this.tbxSurname.Text;
                string phone = tbxPhone.Text;
                Customer c = new Customer(name, surname, phone);
                CustomerDAO customerDAO = new CustomerDAO();
                customerDAO.Save(c);
                this.Hide();
                this.customerOptions.CustomerOptions_Load(sender, e);
            }
               
            

        }
    }
}
