using Shop_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Project
{
    public class OrderingDAO : IDAO<Ordering>
    {
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from item where ordering_id = @Id;delete from ordering where id =@Id;", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order deleted successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have customer_id as FK");
            }

            DatabaseSingleton.CloseConnection();
        }

        public void GetById(int id, DataGridView dataview)
        {
            SqlCommand cmd = new SqlCommand("select * from ordering where ordering.id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order deleted successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have customer_id as FK");
            }

            DatabaseSingleton.CloseConnection();
        }

        public void GetByCustomer(string name, string surname, DataGridView dataview)
        {
            SqlCommand cmd = new SqlCommand("select ordering.id, ordering.date_order, concat(customer.name, ' ', customer.surname) as Customer, sum(quantity*product.price) as Celkem \r\nfrom item inner join product on item.product_id = product.id\r\nright join ordering on item.ordering_id = ordering.id\r\ninner join customer on ordering.customer_id = customer.id\r\nwhere customer.name = @CustomerName and customer.surname = @CustomerSurname\r\ngroup by ordering.id, ordering.date_order, customer.name, customer.surname", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@CustomerName", name);
            cmd.Parameters.AddWithValue("@CustomerSurname", surname);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have customer_id as FK");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataview.DataSource = dt;
            DatabaseSingleton.CloseConnection();
        }

        public void Save(Ordering ele)
        {
            SqlCommand cmd = new SqlCommand("insert into Ordering values(@dateTime, (select customer.id from customer where customer.name = @Name and customer.surname = @Surname))", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@dateTime", ele.DateOrder);
            cmd.Parameters.AddWithValue("@Name", ele.CustomerName);
            cmd.Parameters.AddWithValue("@Surname", ele.CustomerSurname);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            DatabaseSingleton.CloseConnection();
        }

        //nelze provest update order (tj. customer_id, date_order). Je lepší smazat ten order a udělat novou objednávku aby vyhnul zmatek. Lze však update jenom položky v objednávce
        public void Update(int id, Ordering newEle)
        {
        }

        public void GetAll(DataGridView dataView)
        {
            SqlCommand cmd = new SqlCommand("select ordering.id, ordering.date_order, concat(customer.name, ' ', customer.surname) as Customer, sum(quantity*product.price) as Celkem from item inner join product on item.product_id = product.id right join ordering on item.ordering_id = ordering.id inner join customer on ordering.customer_id = customer.id group by ordering.id, ordering.date_order, customer.name, customer.surname\r\n", DatabaseSingleton.GetInstance());
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataView.DataSource = dt;

            DatabaseSingleton.CloseConnection();

        }
    }
}
