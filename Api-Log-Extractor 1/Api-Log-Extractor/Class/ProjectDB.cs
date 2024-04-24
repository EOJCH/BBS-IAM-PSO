using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ApiData_Extractor
{
    public class ProjectDB
    {
        string userid = ConfigurationManager.AppSettings["UserId"];
        string dbPassword = ConfigurationManager.AppSettings["DBPassword"];
        
        public DataTable GetExecute(string query,string dbinstance,string database)
        {
            try
            {
                string strcon = @"Server = " + dbinstance + "; Database = "+database+"; User Id = " + userid + "; Password = " + dbPassword + "";

                SqlConnection con = new SqlConnection();
                //Console.WriteLine("Connecting Database to retrive user details");
                //con.ConnectionString = "Data Source=BY-SQL0546\\I0546;Initial Catalog = OmadaReporting; Integrated Security = True";  // For Windows Authendication
                
                con.ConnectionString = strcon;
                con.Open();

                using (con)
                {
                   // Console.WriteLine("Connection Established");
                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    var _dataTable = new DataTable();
                    _dataTable.Load(dataReader);
                    con.Close();
                   // Console.WriteLine("Connection Closed");

                    return _dataTable;
                }
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}