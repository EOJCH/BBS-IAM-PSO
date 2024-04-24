using System;
using System.Configuration;
using System.IO;
using System.Net.Http;


namespace ApiData_Extractor
{
    public class APICall
    {
      
        public object GetRestApiCall(string routepath,string queryparams)
        {
            Response result;
            try
            {
                string apiurl = ConfigurationManager.AppSettings["PrimaRestApiUrl"];
                string userid = ConfigurationManager.AppSettings["UserId"];
                string password = ConfigurationManager.AppSettings["ApiPassword"];
                //string userid ="EHGLM";
                //string password ="Sudheer#9666088099";

                HttpMessageHandler handler = new HttpClientHandler()
                {
                };

                var url = apiurl + routepath + queryparams;
                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url),
                    Timeout = new TimeSpan(0, 2, 0)
                };

                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
  
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(userid+":"+ password);
                string val = System.Convert.ToBase64String(plainTextBytes);
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                string content = string.Empty;

                using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().Result, System.Text.Encoding.UTF8))
                {
                    content = stream.ReadToEnd();
                    var Json = content.ToString();
                    
                    result = new Response();
                    result.Statuscode = "200";
                    result.Message = "Success";
                    result.Error = "";
                    result.Result = Json;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = new Response();
                result.Statuscode = "400";
                result.Message ="Error";
                result.Error = ex.Message.ToString();
                result.Result = "";
                return result;

            }
          

        }
        
       
       
     

    }

}