using System.Data;

namespace ApiData_Extractor
{

    public class BAL
    {
        APICall aPI;
     
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

    }

}