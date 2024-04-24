using ClosedXML.Excel;
using Newtonsoft.Json;
using System.Data.OleDb;

namespace VVIPUser_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtbrowse_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "Excel Files|*.xlsx";
            openFileDialog1.ShowDialog();
            txtfile.Text = openFileDialog1.FileName.ToString();
            

        }

        public List<UserDetails> GetVVIPUsersData()
        {
            APICall aPI = new APICall();
            List<UserDetails> _VVIPUsers;
            List<string> User = new List<string>();

            try
            {
                string _primaGroupName = "SNOWAA_VVIP-Group_Global";
                string routepath = "/resource/Group/search?query=/";

                string queryparams = "Group[starts-with(%27DisplayName%27,%27" + _primaGroupName + "%27)]&attributes=Description,DisplayedOwner,memberAll";



                Response resultdata = new Response();
                resultdata = (Response)aPI.GetRestApiCall(routepath, queryparams);
                string Json = resultdata.Result;
                var result = JsonConvert.DeserializeObject<ApiResponseObj>(Json);
                var apiobjdata = result.GetResourcesResult;

                //var strapiobjdata = apiobjdata.ToString();
                //var serializer = new JavaScriptSerializer();
                //serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                //dynamic objdata = serializer.Deserialize(strapiobjdata, typeof(object));

                //var resultdta = objdata.Resources[0].Attributes[3].Values;

                //System.Text.StringBuilder _resources = new System.Text.StringBuilder();


                //for (var i = 0; i < resultdta.Count; i++)
                //{
                //    var item = resultdta[i].ToString();
                //    _resources.Append("'" + item.ToString() + "',");

                //    User.Add(item);
                //}
                //_resources = _resources.Remove(_resources.Length - 1, 1);

                //DAL _dal = new DAL();
                //DataTable dt = _dal.GetVVIPUserData(_resources);
                //_VVIPUsers = new List<UserDetails>();
                //UserDetails user;
                //if (dt != null)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        user = new UserDetails();
                //        user.cwid = dr["AccountName"].ToString();
                //        user.firstName = dr["FirstName"].ToString();
                //        user.lastName = dr["LastName"].ToString();
                //        user.email = dr["Email"].ToString();
                //        _VVIPUsers.Add(user);
                //    }


                //}

                return _VVIPUsers =null;



            }
            catch (Exception ex)
            {

                return _VVIPUsers = null;
            }
        }

        private void txtsubmit_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if(txtfile.Text !=String.Empty && txtfile.Text.ToLower().Contains("xls"))
            {
                try
                {
                    #region  Button Click Action
                    string excelFilePath = txtfile.Text;

                    //if (File.Exists(excelFilePath))
                    //{
                    //    FileInfo fileInfo = new FileInfo(excelFilePath);

                    //    // If you use EPPlus in a noncommercial context
                    //    // according to the Polyform Noncommercial license:
                    //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    //    using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                    //    {
                    //        ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // You can select the specific worksheet by index (0-based) or name.

                    //        int rowCount = worksheet.Dimension.Rows;
                    //        int colCount = worksheet.Dimension.Columns;

                    //        for (int row = 1; row <= rowCount; row++)
                    //        {
                    //            for (int col = 1; col <= colCount; col++)
                    //            {
                    //                var cellValue = worksheet.Cells[row, col].Text;
                    //                Console.Write(cellValue + "\t");
                    //            }
                    //            Console.WriteLine();
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    Console.WriteLine("Excel file not found");
                    //}



                    string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 12.0;";
                    OleDbConnection excelConnection = new OleDbConnection(connstring);

                    //Sheet Name
                    excelConnection.Open();
                    string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                    excelConnection.Close();
                    //End
                    OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);
                    excelConnection.Open();

                    OleDbDataReader dReader;
                    dReader = cmd.ExecuteReader();

                    //Getting the imported Users List-------------------------------
                    List<object> ImportedUsers = new List<object>();
                    List<UserDetails> userdetails = new List<UserDetails>();
                    UserDetails user;//= new UserDetails();

                    while (dReader.Read())
                    {
                        user = new UserDetails();
                        user.cwid = Convert.ToString(dReader.GetValue(0));
                        user.firstName = Convert.ToString(dReader.GetValue(1));
                        user.lastName = Convert.ToString(dReader.GetValue(2));
                        user.email = Convert.ToString(dReader.GetValue(3));


                        ImportedUsers.Add(user);
                    }

                    BAL bal = new BAL();
                    List<UserDetails> responseusers = bal.GetVVIPUsersData();
                    //------------------------------------------------------------


                    // Getting the common users details
                    if (responseusers != null)
                    {
                        List<UserDetails> Finaluserdetails = new List<UserDetails>();
                        foreach (UserDetails uservalue in ImportedUsers)
                        {
                            UserDetails UD = responseusers.Find(x => x.cwid == uservalue.cwid);
                            if (UD != null)
                            {
                                uservalue.isMatched = true;
                                Finaluserdetails.Add(uservalue);
                            }
                            else
                            {
                                uservalue.isMatched = false;
                                Finaluserdetails.Add(uservalue);
                            }
                        }

                        foreach (var obj in Finaluserdetails)
                        {
                            var gridUser = (UserDetails)obj;
                            dataGridView1.Rows.Add(gridUser.cwid, gridUser.firstName, gridUser.lastName, gridUser.email, gridUser.isMatched);

                        }

                        btndownload.Enabled = true;
                        dReader.Close();

                        excelConnection.Close();
                    }
                    //------------------------------------------------------------ 
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Some thing went wrong please contact with application team.");

                }
            }
            else
            {
                MessageBox.Show("Please select the file..!");
            }
            
        }
        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    // Export column headers
                    for (int col = 1; col <= dataGridView.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col).Value = dataGridView.Columns[col - 1].HeaderText;
                    }

                    // Export data rows
                    for (int row = 0; row < dataGridView.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataGridView.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = Convert.ToString(dataGridView.Rows[row].Cells[col].Value);
                        }
                    }

                    workbook.SaveAs(filePath);
                }
            }
            
        }

        private void btndownload_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Excel Files|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(dataGridView1, saveFileDialog1.FileName);
                MessageBox.Show("Data exported to Excel successfully!");
            }
        }

        private void btncheck_individual_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                List<object> ImportedUsers = new List<object>();
                List<UserDetails> userdetails = new List<UserDetails>();
                UserDetails user;//= new UserDetails();

                string cwids = txt_cwids.Text;
                if (cwids.Trim() != "")
                {
                    string[] strcwids = cwids.Split(',');
                    foreach (string cwid in strcwids)
                    {
                        user = new UserDetails();
                        user.cwid = cwid;
                        user.firstName = "NA";
                        user.lastName = "NA";
                        user.email = "NA";
                        ImportedUsers.Add(user);
                    }
                }

                //while (dReader.Read())
                //{
                //    user = new UserDetails();
                //    user.cwid = Convert.ToString(dReader.GetValue(0));
                //    user.firstName = Convert.ToString(dReader.GetValue(1));
                //    user.lastName = Convert.ToString(dReader.GetValue(2));
                //    user.email = Convert.ToString(dReader.GetValue(3));


                //    ImportedUsers.Add(user);
                //}

                BAL bal = new BAL();
                List<UserDetails> responseusers = bal.GetVVIPUsersData();
                //------------------------------------------------------------


                // Getting the common users details
                if (responseusers != null)
                {
                    List<UserDetails> Finaluserdetails = new List<UserDetails>();
                    foreach (UserDetails uservalue in ImportedUsers)
                    {
                        UserDetails UD = responseusers.Find(x => x.cwid == uservalue.cwid);
                        if (UD != null)
                        {
                            uservalue.isMatched = true;
                            Finaluserdetails.Add(uservalue);
                        }
                        else
                        {
                            uservalue.isMatched = false;
                            Finaluserdetails.Add(uservalue);
                        }
                    }

                    foreach (var obj in Finaluserdetails)
                    {
                        var gridUser = (UserDetails)obj;
                        dataGridView1.Rows.Add(gridUser.cwid, gridUser.firstName, gridUser.lastName, gridUser.email, gridUser.isMatched);

                    }

                    btndownload.Enabled = true;

                }
                //------------------------------------------------------------ 
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something went wrong please contact administrator");
            }
        }
    }


}
