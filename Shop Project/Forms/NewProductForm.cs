using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Project.Forms
{
    public partial class NewProductForm : Form
    {
        private ProductOptions productOptions;

        public NewProductForm(ProductOptions productOptions)
        {
            InitializeComponent();
            this.productOptions = productOptions;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tbxProduct.Text == "" || this.comboBox1.Text == "Choose your supplier")
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                string name = this.tbxProduct.Text;
                string supplier = this.comboBox1.Text;
                bool inStock = raBtnYes.Checked;
                Product p = new Product(name, (float)this.numericUpDown1.Value, supplier, inStock);
                ProductDAO productDAO = new ProductDAO();
                productDAO.Save(p);
                this.Hide();
                this.productOptions.ProductOptions_Load(sender, e);
            }
        }

        private void NewProductForm_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from supplier", DatabaseSingleton.GetInstance());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            foreach (DataRow dr in table.Rows)
            {
                comboBox1.Items.Add(dr["name"].ToString());
            }
        }
    }
}
