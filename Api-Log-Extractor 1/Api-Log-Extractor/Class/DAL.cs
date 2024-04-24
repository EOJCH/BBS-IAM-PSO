using System.Data;

namespace ApiData_Extractor
{
    public class DAL
    {
        ProjectDB _dbconnection;
        public DataTable GetVVIPUserData(System.Text.StringBuilder resources)
        {
            _dbconnection = new ProjectDB();
            string _dbinstance = "BY-SQL0546\\I0546";
            string _database = "OmadaReporting";
            try
            {
                
                string query = @"SELECT [DisplayName],[FirstName],[LastName],[AccountName],[Email] FROM [OmadaReporting].[dbo].[vwCurrentPerson] where ObjectID in ("+ resources + ")";
                DataTable dataTable = _dbconnection.GetExecute(query, _dbinstance,_database);
                return dataTable;

            }
            catch (Exception)
            {

                return null;
            }
            

        }
        public DataTable GetApiUserData(string resources)
        {
            _dbconnection = new ProjectDB();
            string _dbinstance = "BY-SQL0546\\I0546";
            string _database = "OmadaReporting";
            try
            {

                string query = @"SELECT [DisplayName],[FirstName],[LastName],[AccountName],[Email] FROM [OmadaReporting].[dbo].[vwCurrentPerson] where AccountName in (" + resources + ")";
                DataTable dataTable = _dbconnection.GetExecute(query, _dbinstance,_database);
                return dataTable;

            }
            catch (Exception)
            {

                return null;
            }


        }

        public DataTable GetRoleEngineData()
        {
            _dbconnection = new ProjectDB();
            string _dbinstance = "BY-SQL0374\\I0374";
            string _database = "FIMService";
            try
            {

                string query = @"select distinct o.ObjectID,oDN.ValueString As ObjectDisplayName, oROPoU.ValueString As RolePoU, count(*) as amount
                                from fim.Objects  as o with(nolock)
                                inner join fim.ObjectTypeInternal  oti with(nolock) on o.ObjectTypeKey = oti.[Key] and oti.Name NOT IN ('Request')
                                inner join fim.BindingInternal  bi with(nolock) on bi.ObjectTypeKey = o.ObjectTypeKey                                                    
                                inner join fim.AttributeInternal  ai with(nolock) on ai.[Key] = bi.AttributeKey and ai.Name IN ('ObjectType')
                                inner join fim.ObjectValueDateTime  cd with(nolock) on o.ObjectKey = cd.ObjectKey and cd.AttributeKey = 53
                                left join fim.ObjectValueString  oDN with(nolock) on o.ObjectKey = oDN.ObjectKey and oDN.AttributeKey = 66
                                left join fim.ObjectValueString  oROPoU with(nolock) on o.ObjectKey = oROPoU.ObjectKey and oROPoU.AttributeKey = 32464
                                where 1=1 and oti.Name IN ('RoleHierarchy') group by  o.ObjectID, oDN.ValueString, oROPoU.ValueString having count(*) > 1";
                DataTable dataTable = _dbconnection.GetExecute(query, _dbinstance,_database);
                return dataTable;

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}