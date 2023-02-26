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
    public partial class EditOrderForm : Form
    {
        private OrderingOptions orderingOptions;
        private int orderId;

        public OrderingOptions OrderingOptions { get { return orderingOptions; } set { orderingOptions = value; } }
        public int OrderId { get { return orderId; } set { orderId = value; } }

        public EditOrderForm(int orderId, string name, string date, OrderingOptions orderingOptions)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.orderingOptions = orderingOptions;
            this.lbCustomer.Text += name;
            this.lbDate.Text += date;
            this.lbOrderId.Text += orderId.ToString();
        }

        public void EditOrderForm_Load(object sender, EventArgs e)
        {
            ItemDAO itemDAO = new ItemDAO();
            itemDAO.GetAll(orderId, this.dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewItemForm newItemForm = new NewItemForm(this);
            newItemForm.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridView1.Rows[e.RowIndex].Cells["product"].FormattedValue.ToString();
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
            EditItemForm editIF = new EditItemForm(id, this, name);
            editIF.Show();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("Are u sure to delete item?", "Delete item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    {
                        ItemDAO itemDAO = new ItemDAO();
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                        itemDAO.Delete(id);
                        this.EditOrderForm_Load(sender, e);
                        this.OrderingOptions.OrderingOptions_Load(sender, e);
                    }
                }
            }
        }
    }
}
