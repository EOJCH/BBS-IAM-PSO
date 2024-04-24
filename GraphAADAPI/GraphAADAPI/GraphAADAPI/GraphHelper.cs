using Azure.Core;
using Azure.Identity;
using LanguageExt.Pipes;
using LanguageExt.TypeClasses;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ExternalConnectors;
using System.Data.SqlClient;
using System.Globalization;

class GraphHelper
{
    // Settings object
    private static Settings? _settings;
    // App-ony auth token credential
    private static ClientSecretCredential? _clientSecretCredential;
    // Client configured with app-only authentication
    private static GraphServiceClient? _appClient;

    public static void InitializeGraphForAppOnlyAuth(Settings settings)
    {
        _settings = settings;

        // Ensure settings isn't null
        _ = settings ??
            throw new System.NullReferenceException("Settings cannot be null");

        _settings = settings;

        if (_clientSecretCredential == null)
        {
            _clientSecretCredential = new ClientSecretCredential(
                _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
        }

        if (_appClient == null)
        {
            _appClient = new GraphServiceClient(_clientSecretCredential,
                // Use the default scope, which will request the scopes
                // configured on the app registration
                new[] { "https://graph.microsoft.com/.default" });
        }
    }
    public static async Task<string> GetAppOnlyTokenAsync()
    {
        // Ensure credential isn't null
        _ = _clientSecretCredential ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

        // Request token with given scopes
        var context = new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" });
        //var context = new TokenRequestContext(new[] { "https://graph.microsoft.com/v1.0/users?" });

        var response = await _clientSecretCredential.GetTokenAsync(context);
        return response.Token;
    }
    public static Task<UserCollectionResponse?> GetUsersAsync()
    {
        // Ensure client isn't null
        _ = _appClient ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");


        return _appClient.Users.GetAsync((config) =>
        //return _appClient.Users[""].GetAsync((config)=> 
        {
            //config.QueryParameters.Filter = "startswith(OnPremisesSamAccountName,'MVVSE')";
            //startsWith(userPrincipalName, 'username')") 

            // config.QueryParameters.Count = true;
            // config.QueryParameters.Filter = "identities / any(c: c / issuerAssignedId eq 'MVVSE' and c / issuer eq 'MVVSE')";
            config.QueryParameters.Top = 999;
           // config.QueryParameters.Filter = "imAddresses/any(i:i eq '_Chris_Turner@bayer.com')";

            config.QueryParameters.Select = new[] { "displayName", "id", "onPremisesSamAccountName", "mail", "signInActivity", "lastPasswordChangeDateTime" };
            // Get at most 25 results
           // var c = config.QueryParameters.Count.Value.ToString();

            // Sort by display name
            // config.QueryParameters.Orderby = new[] { "displayName" };
        });
    }
    public async static Task MakeGraphCallAsync()
    {
        // INSERT YOUR CODE HERE     
       
        try
        {
            SqlConnection SQLConnection = new SqlConnection();
            SQLConnection.ConnectionString = "Data Source = BY-IAM-IDENTITY1.bayer.cnb,52560; Initial Catalog =IAM_HELPER; "
                             + "Integrated Security=true;";

            string query = String.Empty;
            string query2 = "Truncate table tbl_180AAD";


            SQLConnection.Open();
            SqlCommand myCommand1 = new SqlCommand(query2, SQLConnection);
            myCommand1.ExecuteNonQuery();
            var messages = await _appClient.Users
            .GetAsync((config) =>
            {
                config.QueryParameters.Top = 999;
                config.QueryParameters.Filter = "accountEnabled eq true";
                //config.QueryParameters.Filter = "identities/any(c:c/issuerAssignedId eq 'j.smith@yahoo.com' and c/issuer eq 'My B2C tenant')";
                config.QueryParameters.Select = new[] { "displayName", "id", "onPremisesSamAccountName", "mail", "signInActivity", "lastPasswordChangeDateTime" };
            });
            string temp;
            while (messages?.Value != null)
            {
                //foreach (var message in messages.Value)
                //{
                //    Console.WriteLine(message);
                //    Console.WriteLine($"User: {message.DisplayName ?? "NO NAME"}");
                //    Console.WriteLine($"  ID: {message.Id}");
                //    Console.WriteLine($" sAMAccountName: {message.OnPremisesSamAccountName ?? "empty"}");


                //    Console.WriteLine($"  Email: {message.Mail ?? "NO EMAIL"}");

                //    if (message.SignInActivity != null)
                //    {
                //        Console.WriteLine($"  SignIn: {message.SignInActivity.LastSignInDateTime}");

                //    }
                //    if (message.LastPasswordChangeDateTime != null)
                //    {
                //        Console.WriteLine($" Password Last Set: {message.LastPasswordChangeDateTime.Value}");
                //    }
                //}

                // If OdataNextLink has a value, there is another page
                if (!string.IsNullOrEmpty(messages.OdataNextLink))
                {
                    // Pass the OdataNextLink to the WithUrl method
                    // to request the next page
                    messages = await _appClient.Users
                        .WithUrl(messages.OdataNextLink)
                        .GetAsync();
                }
                else
                {
                    // No more results, exit loop
                    break;
                }
                foreach (var user in messages.Value)
                {
                    //user.SignInActivity.LastSignInDateTime;

                    if (user.SignInActivity != null && user.LastPasswordChangeDateTime != null)
                    {
                        string dispNm = user.DisplayName.ToString();
                        string usrid = user.Id.ToString();
                        string OnpremSamAccount = null;
                        if (user.OnPremisesSamAccountName!=null)
                        {
                            OnpremSamAccount = user.OnPremisesSamAccountName.ToString();
                        }
                        string email = null;
                        if (user.Mail!= null)
                        email= user.Mail.ToString();
                        string AADLastlogin = user.SignInActivity.LastSignInDateTime.ToString();
                        string AADPwdLastSet = user.LastPasswordChangeDateTime.ToString();
                        string DummyDateValue = null;
                        string DummyDateValue2 = null;
                        if (AADLastlogin != null || AADPwdLastSet!=null)
                        {
                            DummyDateValue = AADLastlogin.Split()[0];
                            DummyDateValue2 = AADPwdLastSet.Split()[0];
                        }
                        DateTime date =DateTime.Now;
                        DateTime dt = DateTime.Now;
                        if (DummyDateValue != "")
                        {
                             date= DateTime.ParseExact(DummyDateValue, "M/d/yyyy", CultureInfo.InvariantCulture);
                            
                        }

                        if(DummyDateValue2!="")
                            dt = DateTime.ParseExact(DummyDateValue2, "M/d/yyyy", CultureInfo.InvariantCulture);


                        AADLastlogin = date.ToString("dd.MM.yyyy");
                        AADPwdLastSet = dt.ToString("dd.MM.yyyy");
                        try
                        {
                            query = "Insert into tbl_180AAD values (null, '" + usrid + "','" + OnpremSamAccount + "' , '" + email + "','" + AADLastlogin + "','" + AADPwdLastSet + "')";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error getting users: {ex.Message}");
                            Console.WriteLine(dispNm);
                        }
                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, SQLConnection);

                            myCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error getting users: {ex.Message}");
                            Console.WriteLine(dispNm);
                        }
                    }
                    else
                    {
                        string dispNm = user.DisplayName.ToString();
                        string usrid = user.Id.ToString();
                        string OnpremSamAccount = null;
                        if (user.OnPremisesSamAccountName != null)
                        {
                            OnpremSamAccount = user.OnPremisesSamAccountName.ToString();
                        }


                        //string email = user.Mail.ToString();
                        string email = null;
                        if (user.Mail != null)
                            email = user.Mail.ToString();
                        // string AADLastlogin = user.SignInActivity.LastSignInDateTime.ToString();
                        // string AADPwdLastSet = user.LastPasswordChangeDateTime.ToString();
                        // query = "Insert into tbl_180AAD values ('" + user.DisplayName + "', '" + user.Id + "' ,'" + user.OnPremisesSamAccountName + "','" + user.Mail + "',null,null)";
                        try
                        {

                            query = "Insert into tbl_180AAD values (null, '" + usrid + "','" + OnpremSamAccount + "' , '" + email + "',null,null)";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error getting users: {ex.Message}");
                            Console.WriteLine(dispNm);
                        }

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, SQLConnection);

                            myCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error getting users: {ex.Message}");
                            Console.WriteLine(dispNm);
                        }
                    }

                    

                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error getting users: {ex.Message}");
        }
    }
    public async static Task GetUsers()
    {
        
           // var users =
            //  .Request()
            //.Filter("startswith(displayName,'Eric')")
            //.Select("displayName,signInActivity")
            //.GetAsync();
            var result = await _appClient.Users.GetAsync((config) =>   //Users.GetAsync((requestConfiguration) =>
            {
                //config.QueryParameters.Select = new string[] { "displayName", "id" };
                //config.QueryParameters.Filter = "identities/any(c:c/issuerAssignedId eq 'j.smith@yahoo.com' and c/issuer eq 'My B2C tenant')";
                config.QueryParameters.Select = new[] { "displayName", "id", "onPremisesSamAccountName", "mail", "signInActivity", "lastPasswordChangeDateTime" };


            });

        var result1 = await _appClient.Users.GetAsync((config) =>
        {
           // requestConfiguration.QueryParameters.Filter = "startswith(displayName,'Eric')";
            config.QueryParameters.Select = new string[] { "displayName", "signInActivity" };
        });

        //var users = await _appClient.Users.Request().GetAsync();


    }
}