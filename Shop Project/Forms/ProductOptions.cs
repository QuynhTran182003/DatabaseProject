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
    public partial class ProductOptions : Form
    {
        public ProductOptions()
        {
            InitializeComponent();
        }

        public void ProductOptions_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select concat( product.id, ' - ', product.name) as product from product", DatabaseSingleton.GetInstance());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            comboBox2.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                comboBox2.Items.Add(dr["product"].ToString());
            }

            ProductDAO productDAO = new ProductDAO();
            productDAO.GetAll(this.dataGridView1);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewProductForm newProductForm = new NewProductForm(this);
            newProductForm.Show();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("Are u sure to delete product?", "Delete product", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    {
                        ProductDAO productDAO = new ProductDAO();
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                        productDAO.Delete(id);
                        this.ProductOptions_Load(sender, e);
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
            string name = dataGridView1.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();

            EditProductForm newProductForm = new EditProductForm(this, id, name);
            newProductForm.Show();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(this.comboBox2.Text == "Select product")
            {
                MessageBox.Show("Please select product");
            }
            else
            {
                ProductDAO productDAO = new ProductDAO();
                productDAO.GetById(int.Parse(comboBox2.Text.Split('-')[0]), this.dataGridView1);
            }
            
        }

        private void btnShowALl_Click(object sender, EventArgs e)
        {
            ProductDAO productDAO = new ProductDAO();
            productDAO.GetAll(this.dataGridView1);

            
        }
    }
}
