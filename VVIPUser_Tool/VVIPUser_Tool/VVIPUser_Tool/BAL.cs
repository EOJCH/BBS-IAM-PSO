using MiNET.Worlds;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.RegularExpressions;
using DataTable = System.Data.DataTable;


namespace VVIPUser_Tool
{

    public class BAL
    {
        APICall aPI;
        public List<UserDetails> GetVVIPUsersData()
        {
            aPI = new APICall();
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

                var strapiobjdata = apiobjdata.ToString();
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                dynamic objdata = serializer.Deserialize<object>(strapiobjdata.ToString());

                // dynamic objdata = serializer.Deserialize(strapiobjdata, typeof(object));

                var resultdta = objdata.Resources[0].Attributes[3].Values;

                System.Text.StringBuilder _resources = new System.Text.StringBuilder();

           
                int i = 0;
                string final = "";
                foreach (object it in resultdta)
                {
             
                    if(i==3)
                    {
                        
                        string jobj = it.ToString();
                        jobj = jobj.Replace("[", "");
                        jobj = jobj.Replace("]", "");
                       

                        String[] data = jobj.Split(',');
                       
                        foreach (string s in data)
                        {
                            string pattern = "\"";
                            string replace = "";
                            string result1 = Regex.Replace(s, pattern, replace, RegexOptions.IgnoreCase);
                           
                            _resources.Append("'" + result1 + "',");
                        }
                    }
                    i++;
         
                }
                
                _resources = _resources.Remove(_resources.Length - 1, 1);

                DAL _dal = new DAL();
                DataTable dt = _dal.GetVVIPUserData(_resources);
                _VVIPUsers = new List<UserDetails>();
                UserDetails user;
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        user = new UserDetails();
                        user.cwid = dr["AccountName"].ToString();
                        user.firstName = dr["FirstName"].ToString();
                        user.lastName = dr["LastName"].ToString();
                        user.email = dr["Email"].ToString();
                        _VVIPUsers.Add(user);
                    }


                }

                return _VVIPUsers;



            }
            catch (Exception ex)
            {

                return _VVIPUsers = null;
            }
        }
        public DataTable GetApiUsersData(string _resources)
        {
            DAL _dal = new DAL();
            DataTable dt = _dal.GetApiUserData(_resources);
            return dt;
        }
        public DataTable GetRoleEngineData()
        {
            DAL _dal = new DAL();
            DataTable dt = _dal.GetRoleEngineData();
            return dt;
        }
    }
    struct ApiResponseObj
    {
        public object GetResourcesResult { get; set; }

        public object Resources { get; set; }
    }
    class responce
    {
        string Count;
        string ErrorMessage;
        string Error;
        object Members;


    }

}