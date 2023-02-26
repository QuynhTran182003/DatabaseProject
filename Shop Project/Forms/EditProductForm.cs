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
    public partial class EditProductForm : Form
    {
        private ProductOptions _options;
        private int idProduct;
        public EditProductForm(ProductOptions options, int idProduct, string name)
        {
            InitializeComponent();
            _options = options;
            this.idProduct = idProduct;
            this.label5.Text += name;
        }

        private void EditProductForm_Load(object sender, EventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.tbxProduct.Text == "" || this.comboBox1.Text == "Select supplier")
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                float price = (float) this.numericUpDown1.Value;
                string supplier = this.comboBox1.Text;
                bool inStock = raBtnYes.Checked;
                ProductDAO productDAO = new ProductDAO();

                productDAO.Update(idProduct, new Product(this.tbxProduct.Text, price, supplier, inStock));
                this.Hide();
                this._options.ProductOptions_Load(sender, e);
            }
        }
    }
}
