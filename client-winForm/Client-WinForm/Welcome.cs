using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Client_WinForm.Models;
using System.Windows.Forms;

namespace Client_WinForm
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            IsMdiContainer = true;
            try
            {
                //Post Request for Login
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:61309/api/LoginByComputerUser");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string ComputerUser = JsonConvert.SerializeObject(Environment.MachineName, Formatting.None);

                    streamWriter.Write(ComputerUser);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                //Gettting response
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                //Reading response
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    string result = streamReader.ReadToEnd();
                    //If Login succeeded
                    if (httpResponse.StatusCode == HttpStatusCode.Created)
                    {

                        dynamic obj = JsonConvert.DeserializeObject(result);
                        User user = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(obj));
                        switch (user.statusObj.StatusName)
                        {
                            case "Manager":
                                Manager.ManagerMainScreen managerMainScreen = new Manager.ManagerMainScreen(user);
                                
                                managerMainScreen.Show();
                                break;
                            case "TeamHead":
                                TeamHeadScreen TeamHeadScreen = new TeamHeadScreen(user);
                          
                                TeamHeadScreen.Show();
                                break;

                            default:
                                WorkerScreen workerScreen = new WorkerScreen(user);
                                
                                workerScreen.Show();
                                break;

                        }

                    }
                    //Printing the matching error
                    else MessageBox.Show(result);
                }
            }
            catch (Exception exception)
            {
                ManagementTaskLogin managementTaskLogin = new ManagementTaskLogin();
                managementTaskLogin.MdiParent = this;
                managementTaskLogin.Show();

            }
            InitializeComponent();
        }

    }
}
