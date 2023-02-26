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
    public partial class NewOrderingForm : Form
    {
        private OrderingOptions orderingOptions;
        public NewOrderingForm(OrderingOptions orderingOptions)
        {
            InitializeComponent();
            this.orderingOptions = orderingOptions;
        }

        private void NewOrderingForm_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select concat(customer.name, ' ', customer.surname) as customer from customer", DatabaseSingleton.GetInstance());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            comboBox1.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                comboBox1.Items.Add(dr["customer"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OrderingDAO orderingDAO = new OrderingDAO();
            orderingDAO.Save(new Ordering(DateTime.Now, comboBox1.Text.Split(' ')[0], comboBox1.Text.Split(' ')[1]));
            this.Hide();
            this.orderingOptions.OrderingOptions_Load(sender, e);
        }
    }
}
