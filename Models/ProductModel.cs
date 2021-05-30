using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POS_ASP.Models;
using System.Data;
using System.Data.SqlClient;
using POS_ASP.Server;
namespace POS_ASP.Models
{
    public class ProductModel
    {
        Server.Server server = new Server.Server();
        public String productID { get; set; } = "";
        public String productName { get; set; } = "";
        public String SellingPrice { get; set; } = "";
        public String CostPrice { get; set; } = "";
        public bool isValid { get; set; } = false;
        public String ErrorMessage { get; set; } = "";



        /// <summary>
        /// REGISTER PRODUCT INTO THE DATABASE;
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RegisterProduct(ProductModel model)
        {
            try
            {
                server.command = new SqlCommand("INSERT INTO Products(ProductID,ProductName,SellingPrice,CostPrice) VALUES(@pid,@pname,@psp,@pcp)", server.Connection());
                server.command.Parameters.Add("@pid", SqlDbType.VarChar).Value = model.productID;
                server.command.Parameters.Add("@pname", SqlDbType.VarChar).Value = model.productName;
                server.command.Parameters.Add("@psp", SqlDbType.VarChar).Value = model.SellingPrice;
                server.command.Parameters.Add("@pcp", SqlDbType.VarChar).Value = model.CostPrice;

                server.OpenConnection();
                server.command.ExecuteNonQuery();
                server.CloseConnection();
                return "Item successfully added";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        /// <summary>
        /// GET ALL DATA IN THE PRODUCTS TABLE
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllProduct()
        {
            server.table = new DataTable();
            server.adapter = new SqlDataAdapter();
            try
            {
                server.command = new SqlCommand("SELECT * FROM Products", server.Connection());
                server.adapter.SelectCommand = server.command;
                server.adapter.Fill(server.table);
                return server.table;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return server.table;
            }
        }

        public DataTable GetProductByProductID(ProductModel model)
        {
            server.table = new DataTable();
            server.adapter = new SqlDataAdapter();
            try
            {
                server.command = new SqlCommand("SELECT * FROM Products WHERE ProductID=@id", server.Connection());
                server.command.Parameters.Add("@id", SqlDbType.VarChar).Value = model.productID;
                server.adapter.SelectCommand = server.command;
                server.adapter.Fill(server.table);
                return server.table;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return server.table;
            }
        }

        /// <summary>
        /// LOAD PRODUCT DETAILS
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public ProductModel LoadProductInfo(DataTable table)
        {
            ProductModel model = new ProductModel();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    model.productID = dataRow["ProductID"].ToString();
                    model.productName = dataRow["ProductName"].ToString();
                    model.SellingPrice = dataRow["SellingPrice"].ToString();
                    model.CostPrice = dataRow["CostPrice"].ToString();
                    model.isValid = true;
                    model.ErrorMessage = "";
                   
                }
                return model;
            }
            else
            {
                model.ErrorMessage = "Product not found";
                model.isValid = false;
                return model;
            }
        }

        /// <summary>
        /// GENERATE THE PRODUCT ID
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public String GenerateProductId(DataTable table)
        {
            if (table.Rows.Count >= 10)
            {
                return string.Format("PID{0}", (table.Rows.Count + 1));
            }
            else
            {
                return string.Format("PID0{0}", (table.Rows.Count + 1));

            }
        }


        /// <summary>
        /// FORMAT THE PRODUCT ID;
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductModel FormatProductDetails(string pid,ProductModel model)
        {
            model.productID = pid;
            return model;
        }
    }
}