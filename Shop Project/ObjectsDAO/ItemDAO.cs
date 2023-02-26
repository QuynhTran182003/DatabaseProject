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
    public class ItemDAO : IDAO<Item>
    {
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from item where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item deleted successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have customer_id as FK");
            }

            DatabaseSingleton.CloseConnection();
        }

        public void GetById(int id, DataGridView dataview)
        {
            SqlCommand cmd = new SqlCommand("select * from Item where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataview.DataSource = dt;
            DatabaseSingleton.CloseConnection();
        }

        public void Save(Item ele)
        {
            SqlCommand cmd = new SqlCommand("insert into Item values( (select product.id from product where product.name = @Name), @OrderingId, @Quantity)", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Name", ele.ProductName);
            cmd.Parameters.AddWithValue("@OrderingId", ele.OrderingId);
            cmd.Parameters.AddWithValue("@Quantity", ele.Quantity);
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

        public void Update(int id, Item newEle)
        {
            //todo
            SqlCommand cmd = new SqlCommand("update Item set product_id = (select product.id from product where product.name = @ProductName), ordering_id = (@OrderingId), quantity = @Quantity where id = @Id ", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@ProductName", newEle.ProductName);
            cmd.Parameters.AddWithValue("@OrderingId", newEle.OrderingId);
            cmd.Parameters.AddWithValue("@Quantity", newEle.Quantity);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DatabaseSingleton.CloseConnection();
        }

        public void GetAll(int orderId, DataGridView dataView)
        {
            SqlCommand cmd = new SqlCommand($"select item.id, product.name as product, quantity, product.price as price_pcs, quantity*product.price as celkem from item inner join product on item.product_id = product.id where ordering_id = {orderId}", DatabaseSingleton.GetInstance());
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
