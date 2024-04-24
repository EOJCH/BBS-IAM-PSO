using LanguageExt;
using Microsoft.Graph.Models;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Globalization;
using System.Text;


StringBuilder sb = new StringBuilder();

Console.WriteLine(".NET Code to fetch User attributes \n");

var settings = Settings.LoadSettings();

// Initialize Graph
InitializeGraph(settings);

int choice = -1;

//while (choice != 0)
//{
//    Console.WriteLine("Please choose one of the following options:");
//    Console.WriteLine("0. Exit");
//    Console.WriteLine("1. Display access token");
//    Console.WriteLine("2. List users");
//    Console.WriteLine("3. Make a Graph call");

//    try
//    {
//        choice = int.Parse(Console.ReadLine() ?? string.Empty);
//    }
//    catch (System.FormatException)
//    {
//        // Set to invalid value
//        choice = -1;
//    }

//    switch (choice)
//    {
//        case 0:
//            // Exit the program
//            Console.WriteLine("Goodbye...");
//            break;
//        case 1:
//            // Display access token
//            await DisplayAccessTokenAsync();
//            break;
//        case 2:
//            // List users
//            await ListUsersAsync();
//            break;
//        case 3:
//            // Run any Graph code
//            await MakeGraphCallAsync();
//            break;
//        default:
//            Console.WriteLine("Invalid choice! Please try again.");
//            break;
//    }
//}

await MakeGraphCallAsync();

void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForAppOnlyAuth(settings);
}

async Task DisplayAccessTokenAsync()
{
    try
    {
        var appOnlyToken = await GraphHelper.GetAppOnlyTokenAsync();
        Console.WriteLine($"App-only token: {appOnlyToken}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting app-only access token: {ex.Message}");
    }
}

async Task ListUsersAsync()
{
    try
    {
        var userPage = await GraphHelper.GetUsersAsync();
       //var users = await GraphHelper.Request().GetAsync();

        if (userPage?.Value == null)
        {
            Console.WriteLine("No results returned.");
            return;
        }

        // Output each users's details

        while (userPage != null)
        {
            foreach (var user in userPage.Value)
            {
                Console.WriteLine($"User: {user.DisplayName ?? "NO NAME"}");
                Console.WriteLine($"  ID: {user.Id}");
                Console.WriteLine($" sAMAccountName: {user.OnPremisesSamAccountName ?? "empty"}");


                Console.WriteLine($"  Email: {user.Mail ?? "NO EMAIL"}");

                if (user.SignInActivity != null)
                {
                    Console.WriteLine($"  SignIn: {user.SignInActivity.LastSignInDateTime}");

                }

                Console.WriteLine($" Password Last Set: {user.LastPasswordChangeDateTime.Value}");
                //Console.WriteLine($"Account status: {user.AccountEnabled.Value}");

            }

            if (!string.IsNullOrEmpty(userPage.OdataNextLink))
            {
                // Pass the OdataNextLink to the WithUrl method
                // to request the next page
               // userPage = await GraphHelper.GetUsersAsync()
                 //   .WithUrl(messages.OdataNextLink)
                 //   .GetAsync();
            }
            else
            {
                // No more results, exit loop
                break;
            }
        }
                

        SqlConnection SQLConnection = new SqlConnection();
        SQLConnection.ConnectionString = "Data Source = BY-IAM-IDENTITY1.bayer.cnb,52560; Initial Catalog =IAM_HELPER; "
                         + "Integrated Security=true;";

        string query = String.Empty;
        string query2 = "Truncate table tbl_180AAD";
       
        
        SQLConnection.Open();
        SqlCommand myCommand1 = new SqlCommand(query2, SQLConnection);
        myCommand1.ExecuteNonQuery();

        foreach (var user in userPage.Value)
        {
            //user.SignInActivity.LastSignInDateTime;
            
            if (user.SignInActivity != null && user.LastPasswordChangeDateTime!=null)
            {
                string AADLastlogin = user.SignInActivity.LastSignInDateTime.ToString();
                string AADPwdLastSet = user.LastPasswordChangeDateTime.ToString();
                string DummyDateValue=null;
                string DummyDateValue2=null;
                //if (AADLastlogin != null)
                //{
                //     DummyDateValue = AADLastlogin.Split()[0];
                //     DummyDateValue2 = AADLastlogin.Split()[0];
                //}
                                

                //DateTime date = DateTime.ParseExact(DummyDateValue, "M/d/yyyy", CultureInfo.InvariantCulture);
               // DateTime dt = DateTime.ParseExact(DummyDateValue2, "M/d/yyyy", CultureInfo.InvariantCulture);

                //AADLastlogin = date.ToString("dd.MM.yyyy");
                //AADPwdLastSet = dt.ToString("dd.MM.yyyy");

                query = "Insert into tbl_180AAD values ('" + user.DisplayName + "', '" + user.Id + "','" + user.OnPremisesSamAccountName + "' , '" + user.Mail + "','" + AADLastlogin + "','" + AADPwdLastSet+"')";
            }
            else
            {
                query = "Insert into tbl_180AAD values ('" + user.DisplayName + "', '" + user.Id + "' ,'" + user.OnPremisesSamAccountName + "','" + user.Mail + "',null,null)";
            }
            
            
            SqlCommand myCommand = new SqlCommand(query, SQLConnection);
           // myCommand1.ExecuteNonQuery();
            myCommand.ExecuteNonQuery();
            
        }
        SQLConnection.Close();



        var moreAvailable = !string.IsNullOrEmpty(userPage.OdataNextLink);

        Console.WriteLine($"\n More users available? {moreAvailable}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting users: {ex.Message}");
    }
}

async Task MakeGraphCallAsync()
{
    await GraphHelper.MakeGraphCallAsync();
}