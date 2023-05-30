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

namespace test
{
    public partial class Form1 : Form
    {

        // connection
        string strCon = "server=.\\SQLEXPRESS;database=NahidDB;trusted_connection=true";

        SqlConnection con;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DispalyCompanyID();
        }

        void DispalyCompanyID()
        {

            // check the connection state 
            con = new SqlConnection(strCon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // sql query
            string sqlQry = "select CompanyName from CompanyDetails";

            // data adapter
            SqlDataAdapter DA = new SqlDataAdapter(sqlQry, con);

            // data set
            DataSet ds = new DataSet();


            try
            {
                DA.Fill(ds);
                this.cboCompanyName.DataSource = ds.Tables[0];
                this.cboCompanyName.DisplayMember = "companyName";

            }

            catch (Exception ex)
            {
                this.lblResult.Text = ex.Message;

            }

            finally
            {
                con.Close();
                DA.Dispose();

            }

        


    }

        private void bttnShow_Click(object sender, EventArgs e)
        {

            // connection
            con = new SqlConnection(strCon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            // sql query
            string sql = "select * from carDetails ca inner join CompanyDetails Com on ca.CompanyID = com.CompanyID where companyName='" + cboCompanyName.Text.Replace("'", "''") + "'  ";



            // make adapter
            SqlDataAdapter da = new SqlDataAdapter(sql, con);


            DataSet ds = new DataSet();


            try
            {
                da.Fill(ds, "com");

                this.dataGridView1.DataSource = ds;
                this.dataGridView1.DataMember = "com";
            }

            catch (Exception ex)
            {
                this.lblResult.Text = ex.Message;
            }

            finally
            {
                con.Close();
                da.Dispose();
                ds.Dispose();
            }

            this.lblResult.Text = this.dataGridView1.Rows.Count - 1 + " Record(s) Found...!";



        }
    }
    
}
