using CompanyProject8209web.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompanyProject8209web.employee
{
    public partial class survey : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populdateCboCountry();
                populdaterblGender();
            }
        }

        protected void populdateCboCountry()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select countryId, country from country";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            cblCountry.DataValueField = "countryId";
            cblCountry.DataTextField = "country";
            cblCountry.DataSource = dr;
            cblCountry.DataBind();

        }
        protected void populdaterblGender()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select genderId, gender from gender";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            rblGender.DataValueField = "genderId";
            rblGender.DataTextField = "gender";
            rblGender.DataSource = dr;
            rblGender.DataBind();
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string selectedCountry = "";    // declare variable to capture selected items from cbl 
            for (int i = 0; i < cblCountry.Items.Count; i++)    // you need to loop checkBoxList to capture the selected values 
            {
                if (cblCountry.Items[i].Selected)
                    selectedCountry += cblCountry.Items[i].Text + ",";// capture the value only
                                                                      // selectedCountry += cblCountry.Items[i].Text + ",";   // capture the Text instead of the value 
            }

            //lblOutput.Text =  " selected :  "  +selectedCountry.TrimEnd(',');
            //  lblOutput.ForeColor = System.Drawing.Color.Green;
            // lblOutput.ForeColor = System.Drawing.Color.Green;

            string selectedGender = "";   // declare variable to capture selected items from cbl 
            for (int i = 0; i < rblGender.Items.Count; i++)    // you need to loop checkBoxList to capture the selected values 
            {
                if (rblGender.Items[i].Selected)
                    selectedGender += rblGender.Items[i].Text + ","; // capture the value only
                                                                     // selectedGender += rblGender.Items[i].Text + ",";   // capture the Text instead of the value 
            }

            if (!string.IsNullOrEmpty(selectedCountry) && !string.IsNullOrEmpty(selectedGender))
            {
                Label1.Text = "Thank you! You have selected: <br/>" +
                                 "Countries: " + selectedCountry.TrimEnd(',') + "<br/>" +
                                 "Gender: " + selectedGender.TrimEnd(',');
                Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                // If nothing is selected, display a "Try Again" message
                Label1.Text = "Try again! You must select a country and a gender.";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        //  lblOutput.Text = " selected :  " + selectedGender.TrimEnd(',');
       // lblOutput.Text = "Countries selected: " + selectedCountry.TrimEnd(',') + "<br/>" +
                  //    "Gender selected: " + selectedGender.TrimEnd(',');
            //lblOutput.ForeColor = System.Drawing.Color.Green;


        }
    }
