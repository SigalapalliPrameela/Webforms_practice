using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using System.Diagnostics;

namespace WebForm
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(Object Sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(">>>>Page Loaded");
            // Block access if the user has not logged in.
            if (Session["UserId"] == null ||
                Session["Role"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            SetRoleBasedPermissions();

            if (!IsPostBack)
            {
                //LoadStudents();
                
                LoadStudents();
            }

        }
        private bool IsAdmin()
        {
            return string.Equals(
                Convert.ToString(Session["Role"]),
                "Admin",
                StringComparison.OrdinalIgnoreCase);
        }

        private void SetRoleBasedPermissions()
        {
            // The last column will be our Edit/Delete command column.
            gvStudents.Columns[4].Visible = IsAdmin();
        }
        private void LoadStudents()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["StudentDBConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string query = "Select Id, Name , Age , Course from Students";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataTable table = new DataTable();

                adapter.Fill(table);
                gvStudents.DataSource = table;
                gvStudents.DataBind();
            }
        }


        protected void gvStudents_RowEditing(
    object sender,
    GridViewEditEventArgs e)
        {
            if (!IsAdmin())
            {
                e.Cancel = true;
                gvStudents.EditIndex = -1;

                lblMessage.Text = "Access denied. Only Admin can edit students.";

                SetRoleBasedPermissions();
                LoadStudents();
                return;
            }

            gvStudents.EditIndex = e.NewEditIndex;
            LoadStudents();
        }

        protected void gvStudents_RowCancelingEdit(
    object sender,
    GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;

            LoadStudents();
        }

        protected void gvStudents_RowUpdating(
    object sender,
    GridViewUpdateEventArgs e)
        {

            if (!IsAdmin())
            {
                e.Cancel = true;
                gvStudents.EditIndex = -1;
                lblMessage.Text = "Access denied. Only Admin can update students.";
                SetRoleBasedPermissions();
                LoadStudents();
                return;
            }
            int studentId =
                Convert.ToInt32(
                    gvStudents.DataKeys[e.RowIndex].Value);

            string name =
                ((TextBox)gvStudents.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            string ageText =
                ((TextBox)gvStudents.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            string course =
                ((TextBox)gvStudents.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            int age = Convert.ToInt32(ageText);

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["StudentDBConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                string query =
                    "UPDATE Students " +
                    "SET Name = @Name, Age = @Age, Course = @Course " +
                    "WHERE Id = @Id";

                SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.Add(
                    "@Name",
                    SqlDbType.NVarChar,
                    100).Value = name;

                command.Parameters.Add(
                    "@Age",
                    SqlDbType.Int).Value = age;

                command.Parameters.Add(
                    "@Course",
                    SqlDbType.NVarChar,
                    100).Value = course;

                command.Parameters.Add(
                    "@Id",
                    SqlDbType.Int).Value = studentId;

                connection.Open();

                command.ExecuteNonQuery();
            }

            gvStudents.EditIndex = -1;

            LoadStudents();
        }


        protected void ddlcourse_SelectedIndexChanged(Object sender, EventArgs e)
        {
            lblMessage.Text = "You Selected : " + ddlcourse.SelectedItem.Text;
            if (ddlcourse.SelectedValue == "C#")
            {
                lblMessage.Text += "<br/>This Course focuses on Coding through C#";
            }
            else if (ddlcourse.SelectedValue == "DA")
            {
                lblMessage.Text += "<br/>This course focuses on Analysing ";
            }
            else if (ddlcourse.SelectedValue == "Java")
            {
                lblMessage.Text += "<br/>This course focuses on Programming through Java ";
            }
            else
            {
                lblMessage.Text += "<br/>You have not selected any course , Please Select  ";

            }
        }

        protected void cvCustom_ServerValidate(Object Source, ServerValidateEventArgs args)
        {
            int age = Convert.ToInt32(args.Value);
            if (age >= 18)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }

        protected void gvStudents_RowDeleting(
    object sender,
    GridViewDeleteEventArgs e)
        {
            if (!IsAdmin())
            {
                e.Cancel = true;
                lblMessage.Text = "Access denied. Only Admin can delete students.";
                SetRoleBasedPermissions();
                LoadStudents();
                return;
            }
            int studentId =
                Convert.ToInt32(
                    gvStudents.DataKeys[e.RowIndex].Value);

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["StudentDBConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                string query =
                    "DELETE FROM Students WHERE Id = @Id";

                SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.Add(
                    "@Id",
                    SqlDbType.Int).Value = studentId;

                connection.Open();

                command.ExecuteNonQuery();
            }

            LoadStudents();
        }


        protected void btnRegister_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string connectionString =
                    ConfigurationManager
                    .ConnectionStrings["StudentDBConnection"]
                    .ConnectionString;

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    string query =
                        "INSERT INTO Students (Name, Age, Course) " +
                        "VALUES (@Name, @Age, @Course)";

                    SqlCommand command =
                        new SqlCommand(query, connection);

                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 100)
                           .Value = txtName.Text;

                    command.Parameters.Add("@Age", SqlDbType.Int)
                           .Value = Convert.ToInt32(txtage.Text);

                    command.Parameters.Add("@Course", SqlDbType.NVarChar, 100)
                           .Value = ddlcourse.SelectedItem.Text;

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                LoadStudents();

                lblMessage.Text =
                    "Student registered successfully!";


                /*
                string Name = txtName.Text; // here the id we kept in Default.aspx so we will use it here
                string age = txtage.Text;
                string Course = ddlcourse.Text; // if we write just ddlcourse.Text it will return the value
                                                // string Course = ddlcourse.SelectedItem.Text ; ----> it will return the Text like Full forms in our case
                                                // string Course = ddlCourse.SelectedValue.Text; ---> this returns C# (short forms) (values not text)

                string gender;
                if (rbMale.Checked)
                {
                    // Male
                    gender = "Male";
                }
                else if (rbFemale.Checked)
                {
                    // female
                    gender = "Female";
                }
                else
                {
                    // gender Not selected
                    gender = "Not Selected";
                }

                string selectedinterests = "";
                foreach (ListItem item in cbInterests.Items)
                {
                    if (item.Selected)
                    {
                        selectedinterests += item + "<br/> ";
                    }
                }

                if (cbterms.Checked)
                {
                    lblMessage.Text = "Welcome " + Name + "!<br/> Age: " + age + "<br/> You have successfully Registered to  " + Course + "<br/> Your interests are " + selectedinterests;

                }
                else
                {
                    lblMessage.Text = "Please Accept the terms and Conditions";
                }
                System.Diagnostics.Debug.WriteLine(">>>>> Button clciked");

                */

            }
        }


        

    }
}