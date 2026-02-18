using CompanyProject8209web.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompanyProject8209web.employee
{
    public partial class employeeInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateDdlCountry();
                populateDdlGender();
                populateGvEmpolyee();
                lblOutput.Text = "";
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for Export to Excel
        }

        protected void populateGvEmpolyee()
        {
            CRUD mycrud = new CRUD();
            string mySql = @"select * from v_employee";
            SqlDataReader dr = mycrud.getDrPassSql(mySql);
            gvEmployee.DataSource = dr;
            gvEmployee.DataBind();
        }

        protected void populateDdlCountry()
        {
            CRUD mycrud = new CRUD();
            string mySql = @"Select countryId, country from country";
            SqlDataReader dr = mycrud.getDrPassSql(mySql);

            DdlCountry.DataValueField = "countryId";
            DdlCountry.DataTextField = "country";
            DdlCountry.DataSource = dr;
            DdlCountry.DataBind();
        }

        protected void populateDdlGender()
        {
            CRUD mycrud = new CRUD();
            string mySql = @"select genderId, gender from gender";
            SqlDataReader dr = mycrud.getDrPassSql(mySql);

            DdlGender.DataValueField = "genderId";
            DdlGender.DataTextField = "gender";
            DdlGender.DataSource = dr;
            DdlGender.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(txtfName.Text))
            {
                lblOutput.Text = "⚠️ Please fill First Name.";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtfName.Focus();
                return;
            }

            try
            {
                CRUD myCrud = new CRUD();

                string mySql = @"INSERT INTO employee (fName, lName, salary, phoneNumber, countryId, genderId)
                                 VALUES (@fName, @lName, @salary, @phoneNumber, @countryId, @genderId)";

                Dictionary<string, object> myPra = new Dictionary<string, object>();
                myPra.Add("@fName", txtfName.Text);
                myPra.Add("@lName", txtlName.Text);

                // ✅ FIX: salary must come from txtSalary
                myPra.Add("@salary", txtSalary.Text);

                // phoneNumber as text (if DB column is int, large numbers will overflow)
                myPra.Add("@phoneNumber", txtPhoneNumber.Text);

                myPra.Add("@countryId", DdlCountry.SelectedValue);
                myPra.Add("@genderId", DdlGender.SelectedValue);

                int rtn = myCrud.InsertUpdateDelete(mySql, myPra);

                if (rtn >= 1)
                {
                    lblOutput.Text = "✅ Insert operation successful.";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                    populateGvEmpolyee();
                }
                else
                {
                    lblOutput.Text = "❌ Insert operation failed.";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToLower().Contains("overflow"))
                {
                    lblOutput.Text = "⚠️ Phone number is too large for the database field. Please change the column type to VARCHAR/BIGINT.";
                    lblOutput.ForeColor = System.Drawing.Color.DarkRed;
                }
                else
                {
                    lblOutput.Text = "❌ Database error: " + ex.Message;
                    lblOutput.ForeColor = System.Drawing.Color.DarkRed;
                }
            }
            catch (Exception ex)
            {
                lblOutput.Text = "❌ Unexpected error: " + ex.Message;
                lblOutput.ForeColor = System.Drawing.Color.DarkRed;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int empId;
            if (!int.TryParse(txtEmployeeId.Text, out empId))
            {
                lblOutput.Text = "⚠️ Please enter a valid Employee ID.";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"DELETE employee WHERE employeeId = @employeeId";

                Dictionary<string, object> myPra = new Dictionary<string, object>();
                myPra.Add("@employeeId", empId);

                int rtn = myCrud.InsertUpdateDelete(mySql, myPra);

                if (rtn >= 1)
                {
                    lblOutput.Text = "✅ Delete operation successful.";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                    populateGvEmpolyee();
                }
                else
                {
                    lblOutput.Text = "❌ Delete operation failed.";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (SqlException ex)
            {
                lblOutput.Text = "❌ Database error: " + ex.Message;
                lblOutput.ForeColor = System.Drawing.Color.DarkRed;
            }
            catch (Exception ex)
            {
                lblOutput.Text = "❌ Unexpected error: " + ex.Message;
                lblOutput.ForeColor = System.Drawing.Color.DarkRed;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int empId;
            if (!int.TryParse(txtEmployeeId.Text, out empId))
            {
                lblOutput.Text = "⚠️ Please enter a valid Employee ID.";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"UPDATE employee
                                 SET fName=@fName, lName=@lName, salary=@salary, phoneNumber=@phoneNumber,
                                     countryId=@countryId, genderId=@genderId
                                 WHERE employeeId=@employeeId";

                Dictionary<string, object> myPra = new Dictionary<string, object>();
                myPra.Add("@employeeId", empId);
                myPra.Add("@fName", txtfName.Text);
                myPra.Add("@lName", txtlName.Text);

                // ✅ FIX: salary from txtSalary
                myPra.Add("@salary", txtSalary.Text);

                myPra.Add("@phoneNumber", txtPhoneNumber.Text);
                myPra.Add("@countryId", DdlCountry.SelectedValue);
                myPra.Add("@genderId", DdlGender.SelectedValue);

                int rtn = myCrud.InsertUpdateDelete(mySql, myPra);

                if (rtn >= 1)
                {
                    lblOutput.Text = "✅ Update operation successful.";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                    populateGvEmpolyee();
                }
                else
                {
                    lblOutput.Text = "❌ Update operation failed.";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToLower().Contains("overflow"))
                {
                    lblOutput.Text = "⚠️ Phone number is too large for the database field. Please change the column type to VARCHAR/BIGINT.";
                    lblOutput.ForeColor = System.Drawing.Color.DarkRed;
                }
                else
                {
                    lblOutput.Text = "❌ Database error: " + ex.Message;
                    lblOutput.ForeColor = System.Drawing.Color.DarkRed;
                }
            }
            catch (Exception ex)
            {
                lblOutput.Text = "❌ Unexpected error: " + ex.Message;
                lblOutput.ForeColor = System.Drawing.Color.DarkRed;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            populateGvEmpolyee();
        }

        public static void ExportGridToExcel(GridView myGv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "";

            string FileName = "ExportedReport_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

            myGv.GridLines = GridLines.Both;
            myGv.HeaderStyle.Font.Bold = true;
            myGv.RenderControl(htmltextwrtter);

            HttpContext.Current.Response.Write(strwritter.ToString());
            HttpContext.Current.Response.End();
        }

        protected void btnExportToE_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(gvEmployee);
        }

        protected void populateForm_Click(object sender, EventArgs e)
        {
            int PK = int.Parse((sender as LinkButton).CommandArgument);

            string mySql = @"select employeeId,fName,lName,salary,phoneNumber,countryId,genderId
                             from employee
                             where employeeId = @employeeId";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@employeeId", PK);

            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtEmployeeId.Text = dr["employeeId"].ToString();
                        txtfName.Text = dr["fName"].ToString();
                        txtlName.Text = dr["lName"].ToString();
                        txtSalary.Text = dr["salary"].ToString();
                        txtPhoneNumber.Text = dr["phoneNumber"].ToString();

                        // ✅ FIX: use SelectedValue
                        DdlCountry.SelectedValue = dr["countryId"].ToString();
                        DdlGender.SelectedValue = dr["genderId"].ToString();
                    }
                }
            }
        }
    }
}