using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagement
{
    public partial class Library : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-MJ6TO16\PERSONAL_LIBRARY;Initial Catalog=dbLibrary;Integrated Security=True");// Enter the connection string for the sql server here
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) {
                btnDelete.Enabled = false;
                FillGridView();
            }
            ;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            hfBookID.Value = "";
            txtAuthor.Text = txtPublisher.Text = txtTitle.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand sqlCmd = new SqlCommand("BooksCreateOrUpdate", conn); // put the name of the stored procedure here, followed by the name of the sql connection
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@bookID",(hfBookID.Value==""?0:Convert.ToInt32(hfBookID.Value)));
            sqlCmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@author", txtAuthor.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@publisherName", txtPublisher.Text.Trim());
            sqlCmd.ExecuteNonQuery();
            conn.Close();
            Clear();

            if (hfBookID.Value == "")
                lblSuccessMessage.Text = "Saved Successfully";
            else lblSuccessMessage.Text = "Updated Successfully";
            FillGridView();
        }
        void FillGridView()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("BooksViewAll", conn);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            conn.Close();
            gvLibrary.DataSource = dt;
            gvLibrary.DataBind();
        }
    }
}