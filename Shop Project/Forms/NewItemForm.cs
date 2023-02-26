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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Shop_Project.Forms
{
    public partial class NewItemForm : Form
    {
        private EditOrderForm editOrder;
        public NewItemForm(EditOrderForm editOrder)
        {
            InitializeComponent();
            this.editOrder = editOrder;
        }

        private void NewItemForm_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select product.name from product", DatabaseSingleton.GetInstance());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            comboBox1.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                comboBox1.Items.Add(dr["name"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(this.comboBox1.Text== "Select product")
            {
                MessageBox.Show("Please select product");
            }
            else
            {
                ItemDAO itemDAO = new ItemDAO();
                itemDAO.Save(new Item(comboBox1.Text, this.editOrder.OrderId, (int)this.numericUpDown1.Value));
                this.Hide();
                this.editOrder.EditOrderForm_Load(sender, e);
                this.editOrder.OrderingOptions.OrderingOptions_Load(sender, e);
            }
        }
    }
}
