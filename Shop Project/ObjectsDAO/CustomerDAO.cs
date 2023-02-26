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
    public class CustomerDAO : IDAO<Customer>
    {
        public CustomerDAO()
        {

        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from Customer where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer deleted successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have customer_id as FK");
            }

            DatabaseSingleton.CloseConnection();

        }

        public void GetById(int id, DataGridView dataView)
        {

            SqlCommand cmd = new SqlCommand("select * from Customer where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataView.DataSource = dt;
            DatabaseSingleton.CloseConnection();
        }

        public void GetAll(DataGridView dataView)
        {
            SqlCommand cmd = new SqlCommand("select * from Customer", DatabaseSingleton.GetInstance());
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

        public void Save(Customer ele)
        {
            SqlCommand cmd = new SqlCommand("insert into Customer values(@Name, @Surname, @Phone)", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Name", ele.Name);
            cmd.Parameters.AddWithValue("@Surname", ele.Surname);
            cmd.Parameters.AddWithValue("@Phone", ele.Phone);
            try
            {
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Successfully saved.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            DatabaseSingleton.CloseConnection();
        }


        public void Update(int id, Customer newEle)
        {
            SqlCommand cmd = new SqlCommand("update Customer set name = @Name, surname = @Surname, phone = @Phone where id = @Id ", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", newEle.Name);
            cmd.Parameters.AddWithValue("@Surname", newEle.Surname);
            cmd.Parameters.AddWithValue("@Phone", newEle.Phone);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DatabaseSingleton.CloseConnection();
        }
    }
}

