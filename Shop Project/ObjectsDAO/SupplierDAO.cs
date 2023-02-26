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
    public class SupplierDAO : IDAO<Supplier>
    {
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from Supplier where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Supplier deleted successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have supplier_id as FK");
            }

            DatabaseSingleton.CloseConnection();
        }

        public void GetById(int id, DataGridView dataview)
        {
            SqlCommand cmd = new SqlCommand("select * from Supplier where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataview.DataSource = dt;
            DatabaseSingleton.CloseConnection();
        }

        public void Save(Supplier ele)
        {
            SqlCommand cmd = new SqlCommand("insert into Supplier values(@Name, @Phone)", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Name", ele.Name);
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

        public void Update(int id, Supplier newEle)
        {
            SqlCommand cmd = new SqlCommand("update Supplier set name = @Name, phone = @Phone where id = @Id ", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", newEle.Name);
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
        }

        public void GetAll(DataGridView dataView)
        {
            SqlCommand cmd = new SqlCommand("select * from Supplier", DatabaseSingleton.GetInstance());
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
