using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Project.Forms
{
    public partial class CustomerOptions : Form
    {
        public CustomerOptions()
        {
            InitializeComponent();
        }

        private void btnNewCus_Click(object sender, EventArgs e)
        {
            NewCustomerForm addCustomerForm = new NewCustomerForm(this);
            addCustomerForm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditCustomerForm editCustomerForm = new EditCustomerForm(this);
            editCustomerForm.Show();
        }


        private void btnImData_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.ShowDialog();

            string file = browseFile.FileName;
            if (file != null)
            {
                ImportFrom(file);
                this.CustomerOptions_Load(sender, e);
            }
        }

        public void ImportFrom(string file)
        {
            try
            {
                var jsonString = File.ReadAllText(file);
                JsonDocument doc = JsonDocument.Parse(jsonString);
                JsonElement root = doc.RootElement;
                for (int i = 0; i < root.GetArrayLength(); i++)
                {
                    var q = JsonConvert.DeserializeObject<Customer>(root[i].ToString());
                    CustomerDAO customerDAO = new CustomerDAO();
                    customerDAO.Save(q);
                }
                MessageBox.Show("Imported successfully.");
            }
            catch
            {

            }
        }

        public void CustomerOptions_Load(object sender, EventArgs e)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            customerDAO.GetAll(this.dataGridView1);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(MessageBox.Show("Are u sure to delete customer?", "Delete customer", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    {
                        CustomerDAO customerDAO = new CustomerDAO();
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                        customerDAO.Delete(id);
                        this.CustomerOptions_Load(sender, e);
                    } 
                }
            }
        }
    }
}
