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
    public class ProductDAO : IDAO<Product>
    {
        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from Product where id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product deleted successfully");
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to delete: Conflict with some tables in DB, which have customer_id as FK");
            }

            DatabaseSingleton.CloseConnection();
        }

        public void GetById(int id, DataGridView dataview)
        {
            SqlCommand cmd = new SqlCommand("select product.id, product.name, product.price, supplier.name as supplier, product.in_stock from Product inner join supplier on product.supplier_id = supplier.id where product.id = @Id", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataview.DataSource = dt;
            DatabaseSingleton.CloseConnection();
        }

        public void Save(Product ele)
        {
            SqlCommand cmd = new SqlCommand("insert into Product values(@Name, @Price, (select supplier.id from supplier where supplier.name = @Supplier), @inStock)", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Name", ele.Name);
            cmd.Parameters.AddWithValue("@Price", ele.Price);
            cmd.Parameters.AddWithValue("@Supplier", ele.Supplier_name);
            cmd.Parameters.AddWithValue("@inStock", ele.In_stock ? "Yes" : "No");
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

        public void Update(int id, Product newEle)
        {
            SqlCommand cmd = new SqlCommand("update Product set name = @Name, price = @Price, product.supplier_id = (select supplier.id from supplier where supplier.name = @Supplier), product.in_stock = @inStock where id = @Id ", DatabaseSingleton.GetInstance());
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", newEle.Name);
            cmd.Parameters.AddWithValue("@Price", newEle.Price);
            cmd.Parameters.AddWithValue("@Supplier", newEle.Supplier_name);
            cmd.Parameters.AddWithValue("@inStock", newEle.In_stock ? "Yes" : "No");

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

        public void GetAll(DataGridView dataView)
        {
            SqlCommand cmd = new SqlCommand("select product.id, product.name, price, supplier.name as Supplier, in_stock from product\r\ninner join supplier on product.supplier_id = supplier.id;", DatabaseSingleton.GetInstance());
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
