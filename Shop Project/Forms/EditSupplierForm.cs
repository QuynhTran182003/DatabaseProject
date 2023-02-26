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
    public partial class EditSupplierForm : Form
    {
        private SupplierOptions supplierOptions;
        public EditSupplierForm(SupplierOptions supplierOptions)
        {
            InitializeComponent();
            this.supplierOptions = supplierOptions;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                SupplierDAO supplierDAO = new SupplierDAO();
                int id = int.Parse(this.tbxId.Text);
                supplierDAO.GetById(id, this.dataGridView1);
            }
            catch (FormatException)
            {
                MessageBox.Show("ID must be a number");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.tbxPhone.Text == "" || this.tbxName.Text == "")
            {
                MessageBox.Show("All fields must be filled in.");
            }
            else
            {
                try
                {
                    SupplierDAO supplierDAO = new SupplierDAO();
                    int id = int.Parse(this.tbxId.Text);
                    string name = this.tbxName.Text;
                    string phone = this.tbxPhone.Text;
                    supplierDAO.Update(id, new Supplier(name, phone));
                    this.Hide();
                    this.supplierOptions.SupplierOptions_Load(sender, e);
                }
                catch (FormatException)
                {
                    MessageBox.Show("ID must be a number");
                }
            }
        }
    }
}
