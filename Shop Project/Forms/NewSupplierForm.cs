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
    public partial class NewSupplierForm : Form
    {
        private SupplierOptions supplierOptions;
        public NewSupplierForm(SupplierOptions form)
        {
            InitializeComponent();
            this.supplierOptions = form;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tbxPhone.Text == "" || this.tbxName.Text == "")
            {
                MessageBox.Show("All fields must be filled in.");
            }
            else
            {
                SupplierDAO DAO = new SupplierDAO();
                DAO.Save(new Supplier(this.tbxName.Text, this.tbxPhone.Text));
                this.Hide();
                this.supplierOptions.SupplierOptions_Load(sender, e);
            }
                
        }
    }
}
