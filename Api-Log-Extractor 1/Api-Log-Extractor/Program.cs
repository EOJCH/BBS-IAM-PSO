using ApiData_Extractor;

using Microsoft.VisualBasic.FileIO;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;
namespace ApiData_Extractor
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=========================================================================================================");
                Console.WriteLine("Prima API Log Extractor...");
                Console.WriteLine("=========================================================================================================");
                Console.WriteLine("Available months data");
                string _folderpath = ConfigurationManager.AppSettings["Logfilepath"];
                string _selectemonth = "";
                Console.WriteLine(_folderpath);
                string[] _monthFolders = Directory.GetDirectories(_folderpath);

                string monthslist = "";
                List<int> monthsdata = new List<int>();
                foreach (string _months in _monthFolders)
                {
                    string[] _filedata = _months.Split('\\');
                    string _monthname = _filedata[_filedata.Length - 1].ToString();
                    _selectemonth = _monthname;
                    if (_monthname.Length < 3)
                    {
                        monthsdata.Add(Convert.ToInt32(_monthname));

                        monthslist = monthslist + Getmonth(Convert.ToInt32(_monthname)) + ":" + _monthname + " , ";
                    }

                }
                Console.WriteLine("Available Months :" + monthslist);
                Console.WriteLine("Please Select The Available Month :");
                string _selectedmont = Console.ReadLine();

                if (monthsdata.Contains(Convert.ToInt32(_selectedmont)))
                {
                    Console.WriteLine("For Day wise report type: D ");
                    Console.WriteLine("For Month wise report type: M");
                    Console.WriteLine("=========================================================================================================");
                    Console.WriteLine("Please Enter Your Choice :");
                    string choice = Console.ReadLine();
                    if (choice.ToUpper() == "D")
                    {
                        Console.WriteLine("Log File Available Dates in the Month:" + Getmonth(Convert.ToInt32(_selectedmont)));
                        Console.WriteLine("=========================================================================================================");
                        string folderpath = ConfigurationManager.AppSettings["Logfilepath"] + _selectedmont + "\\";
                        string[] files = Directory.GetFiles(folderpath);
                        if (files.Length > 0)
                        {
                            string datelist = "";
                            List<int> dates = new List<int>();
                            foreach (string filepath in files)
                            {
                                string[] filedata = filepath.Split('\\');
                                string[] filedate = filedata[filedata.Length - 1].Split('.');
                                int date = int.Parse(filedate[0].Substring(filedate[0].Length - 2));
                                dates.Add(date);
                                datelist = datelist + date + " , ";
                            }
                            datelist = datelist.Substring(0, datelist.Length - 1);

                            Console.WriteLine(datelist.ToString());

                            Console.WriteLine("=========================================================================================================");

                            Console.WriteLine("Please provide the date in the  above available dates");
                            string selecteddate = Console.ReadLine();
                            if (dates.Contains(Convert.ToInt32(selecteddate)))
                            {
                                Console.WriteLine("Log file selected for the process for the date of:" + selecteddate);
                                Console.WriteLine("Process Started");
                                DataExtraction de = new DataExtraction();
                                de.GetDaywiseData(selecteddate, _selectedmont);
                            }
                            else
                            {
                                Console.WriteLine("Selected date log file is not available");
                                Console.WriteLine("Please provide the date in the  above available dates");
                                string selecteddate1 = Console.ReadLine();
                                if (dates.Contains(Convert.ToInt32(selecteddate1)))
                                {
                                    Console.WriteLine("Log file selected for the process for the date of:" + selecteddate1);
                                    Console.WriteLine("Process Started");
                                    DataExtraction de = new DataExtraction();
                                    de.GetDaywiseData(selecteddate1, _selectedmont);
                                }
                                else
                                { Console.WriteLine("Selected date log file is not available Please restart the app again"); }
                            }


                        }
                        else
                        {
                            Console.WriteLine("There no fies available in selected month please cehck adn try again..");
                        }



                    }
                    else if (choice.ToUpper() == "M")
                    {
                        Console.WriteLine("Preparing Monthly report for the month : " + DateTime.Now.ToString("MMMM"));
                        Console.WriteLine("Process Started");
                        DataExtraction de = new DataExtraction();
                        de.GetMontlyData(_selectedmont);
                        Console.WriteLine("Process completed");
                        string r = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Entry Please re run the application");
                    Console.WriteLine("=========================================================================================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                var op = Console.ReadLine();
                //throw;
            }



        }
        public static string Getmonth(int monthid)
        {
            switch (monthid)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";

                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    return "Invalid";

            }

        }


    }
    class DataExtraction
    {
        public void GetMontlyData(string selectedmonth)
        {
            string _year = System.DateTime.Now.ToString("yyyy");

            int _month = int.Parse(System.DateTime.Now.ToString("MM"));
            int _date = int.Parse(System.DateTime.Now.ToString("dd"));
            Console.WriteLine("Reading the folder for the available log files");
            string folderpath = ConfigurationManager.AppSettings["Logfilepath"] + selectedmonth + "\\";
            string[] files = Directory.GetFiles(folderpath);
            List<int> Dates = new List<int>();

            foreach (string filepath in files)
            {
                string[] filedata = filepath.Split('\\');
                string[] filedate = filedata[filedata.Length-1].Split('.');
                int date = int.Parse(filedate[0].Substring(filedate[0].Length - 2));
                Dates.Add(date);
            }


            DataTable dttotal = new DataTable();
            int counter = 0;
            Console.WriteLine("Reading the log files");
            int filecount = files.Length;
            foreach (string filepath in files)
            {
                DataTable dtdata = getData(filepath);
                dttotal.Merge(dtdata);
                counter++;
                double percent = (Convert.ToDouble(counter) / Convert.ToDouble(files.Length)) * 100;

                Console.Write(" \r{0}%   ", "File Generation Progress :" + Math.Round(percent, 0));
            }
            Console.WriteLine("");

            DataTable result = alignTableData(dttotal);
            CreateCSV(result, "Monthly", DateTime.Now.ToString("MMM"), selectedmonth);

        }
        public void GetDaywiseData(string date, string selectedmonth)
        {

            string _year = ConfigurationManager.AppSettings["Year"];//System.DateTime.Now.ToString("yy");
            string _month = ""; 
            System.DateTime.Now.ToString("MM");

            int year = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"));
            int month = Convert.ToInt32(System.DateTime.Now.ToString("MM"));

            if (date.Length == 1)
            {
                date = "0" + date;

            }
            string folderpath = ConfigurationManager.AppSettings["Logfilepath"] + selectedmonth + "\\";
            if (selectedmonth.Length == 1)
            {
                _month = "0" + selectedmonth;
            }
            else
            {
                _month =  selectedmonth;
            }

            string filename = folderpath + "u_ex" + _year + "" + _month + "" + date + ".log";//"u_ex230222";
            string filepath = filename;

            DataTable dtresult = GetDataByDate(filepath);
            CreateCSV(dtresult, "Day", date, selectedmonth);

        }
        private DataTable getData(string path)
        {
            int MaxRequestCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxRequestCount"]);

            DataTable dtdata = new DataTable();
            string[] filedata = path.Split('\\');
            string[] filename = filedata[filedata.Length - 1].ToString().Split('.');
            string strFileDate = filename[0].Replace("u_ex", "");
            try
            {
                DataTable dt = new DataTable();
                if (System.IO.File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        for (int i = 0; i < 15; i++)
                        {
                            dt.Columns.Add(i.ToString());
                        }
                        while ((line = sr.ReadLine()) != null)
                        {

                            if (!line.StartsWith("#"))
                            {
                                string[] strdata = line.Split(' ');
                                DataRow row = dt.NewRow();
                                int cellcount = 0;
                                foreach (var item in strdata)
                                {
                                    row[cellcount] = item;
                                    cellcount++;
                                }
                                dt.Rows.Add(row);
                            }

                        }
                    }

                    var query = from row in dt.AsEnumerable()
                                group row by row.Field<string>("7") into sales
                                orderby sales.Count()
                                select new
                                {
                                    Name = sales.Key,
                                    CountOfClients = sales.Count()
                                };
                    #region Processing Data
                    string strQuerydata = "";
                    foreach (var strVals in query)
                    {
                        if (strVals.CountOfClients > MaxRequestCount)
                        {
                            string[] col = null;
                            if (strVals.Name.Contains(@"\"))
                            {
                                col = strVals.Name.ToString().Split('\\');
                                strQuerydata = strQuerydata + "'" + col[1].ToString() + "',";
                            }
                            else
                            {
                                strQuerydata = strQuerydata + "'" + strVals.Name + "',";
                            }

                        }

                    }

                    strQuerydata = strQuerydata.Remove(strQuerydata.Length - 1);
                    string excludedcwids = ConfigurationManager.AppSettings["ExcludeCwids"];
                    string[] excluCwids = excludedcwids.Split(',');
                    DataTable dtretrivedData = new DataTable();
                    dtretrivedData = BindGrid(strQuerydata);
                    System.Data.DataTable DtTable = new DataTable();
                    if (dtretrivedData != null)
                    {

                        DtTable.Columns.Add("UserID");
                        DtTable.Columns.Add("Last Name");
                        DtTable.Columns.Add("First Name");
                        DtTable.Columns.Add("Email");
                        DtTable.Columns.Add("Requests");
                        DtTable.Columns.Add("Date");

                        foreach (var salesman in query)
                        {
                            if (salesman.CountOfClients > MaxRequestCount)
                            {

                                string[] col = null;
                                if (salesman.Name.Contains(@"\"))
                                {
                                    col = salesman.Name.ToString().Split('\\');
                                    DataRow[] dr = dtretrivedData.Select("AccountName='" + col[1].ToUpper() + "'");
                                    if (!excluCwids.Contains(col[1].ToUpper()))
                                    {
                                        foreach (DataRow row in dr)
                                        {
                                            DataRow row1 = DtTable.NewRow();
                                            row1["UserID"] = row["AccountName"].ToString();
                                            row1["Last Name"] = row["LastName"].ToString();
                                            row1["First Name"] = row["FirstName"].ToString();

                                            if (row["Email"].ToString() == "") { row1["Email"] = "NA"; }
                                            else { row1["Email"] = row["Email"].ToString(); }

                                            row1["Requests"] = salesman.CountOfClients;
                                            row1["Date"] = strFileDate;

                                            DtTable.Rows.Add(row1);
                                        }
                                    }


                                }
                                else
                                {
                                    if (!excluCwids.Contains(salesman.Name.ToUpper()))
                                    {
                                        DataRow[] dr = dtretrivedData.Select("AccountName='" + salesman.Name.ToUpper() + "'");

                                        foreach (DataRow row in dr)
                                        {
                                            DataRow row1 = DtTable.NewRow();
                                            row1["UserID"] = row["AccountName"].ToString();
                                            row1["Last Name"] = row["LastName"].ToString();
                                            row1["First Name"] = row["FirstName"].ToString();

                                            if (row["Email"].ToString() == "") { row1["Email"] = "NA"; }
                                            else { row1["Email"] = row["Email"].ToString(); }

                                            row1["Requests"] = salesman.CountOfClients;
                                            row1["Date"] = strFileDate;
                                            DtTable.Rows.Add(row1);

                                        }
                                    }

                                }

                            }

                        }

                    }
                    return DtTable;
                    #endregion
                }
                else { return dtdata; }

            }
            catch (Exception ex)
            {
                //throw;
                return dtdata;
            }

        }
        private DataTable BindGrid(string queryParams)
        {

            BAL _bal = new BAL();
            DataTable dataTable = _bal.GetApiUsersData(queryParams);
            return dataTable;


        }



        private DataTable alignTableData(DataTable dtusersData)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("UserID");
            dtResult.Columns.Add("Last Name");
            dtResult.Columns.Add("First Name");
            dtResult.Columns.Add("Email");
            dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["UserID"] };
            for (int i = 1; i < 32; i++)
            {
                if (i < 10)
                {
                    dtResult.Columns.Add("0" + i.ToString());
                }
                else
                {
                    dtResult.Columns.Add(i.ToString());
                }

            }

            foreach (DataRow dr in dtusersData.Rows)
            {
                string date = dr["Date"].ToString();
                string day = date.Substring(date.Length - 2);
                DataRow dr1 = dtResult.Rows.Find(dr["UserID"].ToString());
                DataRow rows = dtResult.AsEnumerable().FirstOrDefault(r => Convert.ToString(r["UserID"]) == dr["UserID"].ToString());

                if (rows != null)
                {
                    if (dr["Requests"].ToString() == "")
                    {
                        rows[day] = "0";
                    }
                    else
                    {
                        rows[day] = Convert.ToString(dr["Requests"]);//dr.ItemArray[4]
                    }

                }
                else
                {
                    DataRow dtr = dtResult.NewRow();
                    dtr["UserID"] = dr["UserID"].ToString();
                    dtr["Last Name"] = dr["Last Name"].ToString();
                    dtr["First Name"] = dr["First Name"].ToString();
                    dtr["Email"] = dr["Email"].ToString();
                    if (dr["Requests"].ToString() == "")
                    {
                        dtr[day] = "0";
                    }
                    else
                    {
                        dtr[day] = Convert.ToString(dr[4]);//dr.ItemArray[4]
                    }
                    dtResult.Rows.Add(dtr);

                }
            }
            foreach (DataRow dr in dtResult.Rows)
            {
                for (int i = 0; i < dtResult.Columns.Count; i++)
                {

                    string Cellval = Convert.ToString(dr[i]);
                    if (Cellval == "")
                    {
                        dr[i] = "0";
                    }
                }
            }


            return dtResult;
        }
        private DataTable GetDataByDate(string path)
        {
            DataTable dt = new DataTable();
            DataTable dtresult = new DataTable();
            StringBuilder sb = new StringBuilder();
            dtresult.Columns.Add("CWID");
            dtresult.Columns.Add("Request Type");
            dtresult.Columns.Add("Query");
            dtresult.Columns.Add("RequestTime");
            dtresult.Columns.Add("End Point");
            dtresult.Columns.Add("SourceIp");
            dtresult.Columns.Add("Status");
            dtresult.Columns.Add("Time");
            try
            {


                if (System.IO.File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        for (int i = 0; i < 15; i++)
                        {
                            dt.Columns.Add(i.ToString());
                        }
                        int counter = 0;
                        while ((line = sr.ReadLine()) != null)
                        {

                            if (!line.StartsWith("#"))
                            {
                                string[] strdata = line.Split(' ');
                                DataRow row = dt.NewRow();
                                int cellcount = 0;
                                foreach (var item in strdata)
                                {
                                    row[cellcount] = item;
                                    cellcount++;
                                }
                                dt.Rows.Add(row);
                            }
                            counter++;

                            Console.Write(" \r{0}   ", "Reading the log file lines :" + counter);
                        }

                        Console.WriteLine("");
                        Console.WriteLine("Loading the lines..");
                        int counter1 = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            try
                            {
                                string CWID, ReqTime, Query, requesttype, Endpoint, Status, sourceip, Timetaken = "";

                                string[] uid = dr[7].ToString().Split('\\');
                                if (uid.Length > 1)
                                {
                                    CWID = Convert.ToString(uid[1]);
                                }
                                else
                                {
                                    CWID = uid[0].ToString();
                                }


                                requesttype = dr[3].ToString();
                                string[] endpoints = dr[4].ToString().Split('/');

                                if (endpoints.Length > 0)
                                {
                                    Endpoint = endpoints[endpoints.Length - 2].ToString() + "/" + endpoints[endpoints.Length - 1].ToString();
                                }
                                else
                                {
                                    Endpoint = endpoints[0].ToString();
                                }

                                ReqTime = dr[1].ToString();
                                Query = dr[5].ToString();
                                Status = dr[11].ToString();
                                sourceip = dr[8].ToString();//Need to cehck
                                Timetaken = dr[14].ToString();
                                DataRow dtr = dtresult.NewRow();

                                dtr["CWID"] = CWID;
                                dtr["Query"] = Query;
                                dtr["RequestTime"] = ReqTime;
                                dtr["Request Type"] = requesttype;
                                dtr["End Point"] = Endpoint;
                                dtr["SourceIp"] = sourceip;


                                dtr["Status"] = Status;
                                dtr["Time"] = Timetaken;
                                dtresult.Rows.Add(dtr);
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                            counter1++;
                            double percent = (Convert.ToDouble(counter1) / Convert.ToDouble(dt.Rows.Count)) * 100;

                            Console.Write(" \r{0}%   ", "Processing the data :" + Math.Round(percent, 0));

                        }
                    }
                    Console.WriteLine("");
                    return (dtresult);
                }
                else
                {
                    return (dtresult);
                }
            }
            catch (Exception ex)
            {
                return (dtresult);
            }

        }

        public void CreateCSV(DataTable dataTable, string type, string date, string month)
        {
            Console.WriteLine("Creating CSV File Type :" + type);

            string root = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\APILog_Output\\" + System.DateTime.Now.ToString("dd-MM-yyyy") + "\\ApiLog_File\\";

            if (!Directory.Exists(root.Trim()))
            {
                Directory.CreateDirectory(root.Trim());
            }
            var lines = new List<string>();

            string[] columnNames = dataTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);
            string createdfilepath = root + type + "_" + Program.Getmonth(Convert.ToInt32(month)) + "_" + date + "_" + System.DateTime.Now.ToString("ddMMyyyymmhhss") + ".csv";
            var valueLines = dataTable.AsEnumerable()
                .Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);

            File.WriteAllLines(createdfilepath, lines);

            Console.WriteLine("CSV file created successfully in path :" + createdfilepath);

        }

    }
}
