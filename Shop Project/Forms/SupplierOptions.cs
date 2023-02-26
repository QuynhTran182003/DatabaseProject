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
    public partial class SupplierOptions : Form
    {
        
        public SupplierOptions()
        {
            InitializeComponent();
        }

        private void btnNewSupp_Click(object sender, EventArgs e)
        {
            NewSupplierForm newSupplierForm = new NewSupplierForm(this);
            newSupplierForm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSupplierForm editSupplierForm = new EditSupplierForm(this);
            editSupplierForm.Show();
        }

        private void btnImData_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.ShowDialog();

            string file = browseFile.FileName;
            if (file != null)
            {
                ImportFrom(file);
                this.SupplierOptions_Load(sender, e);
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
                    var q = JsonConvert.DeserializeObject<Supplier>(root[i].ToString());
                    SupplierDAO supplierDAO = new SupplierDAO();
                    supplierDAO.Save(q);
                }
                MessageBox.Show("Imported successfully.");
                
            }
            catch
            {

            }
        }

        public void SupplierOptions_Load(object sender, EventArgs e)
        {
            SupplierDAO supplierDAO = new SupplierDAO();
            supplierDAO.GetAll(this.dataGridView1);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("Are u sure to delete supplier?", "Delete supplier", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    {
                        SupplierDAO supplierDAO = new SupplierDAO();
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                        supplierDAO.Delete(id);
                        this.SupplierOptions_Load(sender, e);
                    }
                }
            }
        }
    }
}
