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
    public partial class OrderingOptions : Form
    {
        public OrderingOptions()
        {
            InitializeComponent();
        }

        public void OrderingOptions_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select concat(customer.name , ' ', customer.surname) as customer from ordering inner join customer on ordering.customer_id = customer.id group by customer.name, customer.surname", DatabaseSingleton.GetInstance());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            comboBox2.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                comboBox2.Items.Add(dr["customer"].ToString());
            }

            OrderingDAO orderingDAO = new OrderingDAO();
            orderingDAO.GetAll(this.dataGridView1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.Text == "Select customer")
            {
                MessageBox.Show("Please select customer");
            }
            else
            {
                OrderingDAO orderingDAO = new OrderingDAO();

                orderingDAO.GetByCustomer(comboBox2.Text.Split(' ')[0], comboBox2.Text.Split(' ')[1], this.dataGridView1);
            }
        }

        private void btnShowALl_Click(object sender, EventArgs e)
        {
            OrderingDAO orderingDAO = new OrderingDAO();
            orderingDAO.GetAll(this.dataGridView1);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewOrderingForm newOrderingForm = new NewOrderingForm(this);
            newOrderingForm.Show();
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("Are u sure to delete order?", "Delete order", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    {
                        OrderingDAO orderingDAO = new OrderingDAO();
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                        orderingDAO.Delete(id);
                        this.OrderingOptions_Load(sender, e);
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
            string customer = dataGridView1.Rows[e.RowIndex].Cells["customer"].FormattedValue.ToString();
            string date = dataGridView1.Rows[e.RowIndex].Cells["date_order"].FormattedValue.ToString();

            EditOrderForm editoF = new EditOrderForm(id, customer, date, this);
            editoF.Show();
        }
    }
}
