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
    public partial class EditCustomerForm : Form
    {
        private CustomerOptions customer;
        public EditCustomerForm(CustomerOptions customerOptions)
        {
            InitializeComponent();
            customer = customerOptions;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                int id = int.Parse(this.tbxId.Text);
                customerDAO.GetById(id, this.dataGridView1);
            }
            catch(FormatException)
            {
                MessageBox.Show("ID must be a number");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if(this.tbxPhone.Text == ""|| this.tbxName.Text == "" || this.tbxSurname.Text == "")
            {
                MessageBox.Show("All fields must be filled in.");
            }
            else
            {
                try
                {
                    CustomerDAO customerDAO = new CustomerDAO();
                    int id = int.Parse(this.tbxId.Text);
                    string name = this.tbxName.Text;
                    string surn = this.tbxSurname.Text;
                    string phone = this.tbxPhone.Text;
                    customerDAO.Update(id, new Customer(name, surn, phone));
                    this.Hide();
                    this.customer.CustomerOptions_Load(sender, e);
                }
                catch (FormatException)
                {
                    MessageBox.Show("ID must be a number");
                }
            }
            
        }
    }
}
