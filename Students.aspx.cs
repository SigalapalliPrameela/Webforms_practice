
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebForm
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only logged-in users can view the student list.
            if (Session["UserId"] == null ||
                Session["Role"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Load the list only on the first page request.
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["StudentDBConnection"]
                .ConnectionString;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                string query =
                    "SELECT Id, Name, Age, Course FROM Students";

                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(query, connection))
                {
                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    gvStudentsList.DataSource = table;
                    gvStudentsList.DataBind();
                }
            }
        }
    }
}
