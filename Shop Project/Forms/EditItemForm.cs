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
    public partial class EditItemForm : Form
    {
        private int itemId;
        private EditOrderForm editOrder;
        public EditItemForm(int id, EditOrderForm editOrder, string name)
        {
            InitializeComponent();
            this.itemId = id;
            this.editOrder = editOrder;
            this.lbItem.Text = name;
        }

        private void EditItemForm_Load(object sender, EventArgs e)
        {
            /*SqlCommand cmd = new SqlCommand($"select product.name as product from item inner join product on item.product_id = product.id where item.ordering_id = {this.itemId}", DatabaseSingleton.GetInstance());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            comboBox1.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                comboBox1.Items.Add(dr["product"].ToString());
            }*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            ItemDAO itemDAO = new ItemDAO();
            itemDAO.Update(itemId, new Item(this.lbItem.Text, editOrder.OrderId, (int)this.numericUpDown1.Value));
            this.Hide();
            this.editOrder.EditOrderForm_Load(sender, e);
            this.editOrder.OrderingOptions.OrderingOptions_Load(sender, e);
            
        }
    }
}
