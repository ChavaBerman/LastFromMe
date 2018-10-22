using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using System.Windows.Forms;
using Client_WinForm.Models;

namespace Client_WinForm
{
    public partial class ManagementTaskLogin : Form
    {
        public ManagementTaskLogin()
        {
            InitializeComponent();
            txt_password.PasswordChar = '*';

        }

        private void btn_enter_Click(object sender, EventArgs e)
        {

            LoginUser loginUser = new LoginUser { UserName = txt_userName.Text, Password = LoginUser.sha256_hash(txt_password.Text) };
            var validationContext = new ValidationContext(loginUser, null, null);
            var results = new List<ValidationResult>();
            User user = new User();
            if (Validator.TryValidateObject(loginUser, validationContext, results, true))
            {

                try
                {
                    //Post Request for Login
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:61309/api/LoginByPassword");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string userStr = JsonConvert.SerializeObject(loginUser, Formatting.None);

                        streamWriter.Write(userStr);
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
                            user = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(obj));
                            if (cb_rememberUser.Checked)
                            {
                                user.UserComputer = Environment.MachineName;
                                try
                                {
                                    httpWebRequest = (HttpWebRequest)WebRequest.Create($@"http://localhost:61309/api/Users");
                                    httpWebRequest.ContentType = "application/json";
                                    httpWebRequest.Method = "PUT";
                                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                    {
                                        dynamic currentUser = user;
                                        string currentUserNameString = Newtonsoft.Json.JsonConvert.SerializeObject(currentUser, Formatting.None);
                                        streamWriter.Write(currentUserNameString);
                                        streamWriter.Flush();
                                        streamWriter.Close();
                                    }
                                    //Get response
                                    httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                    //Read response
                                    using (var streamReaderPUT = new StreamReader(httpResponse.GetResponseStream(), ASCIIEncoding.ASCII))
                                    {
                                        string resultPUT = streamReaderPUT.ReadToEnd();
                                        //If request succeeded
                                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                                        {
                                           
                                           
                                        }
                                        //Print the matching error
                                        else MessageBox.Show(result);

                                    }
                                }
                                catch (Exception exception)
                                {
                                    MessageBox.Show(exception.Message);
                                }
                            }

                        }
                        //Printing the matching error from server
                        else MessageBox.Show(result);
                    }
                    switch (user.statusObj.StatusName)
                    {
                        case "Manager":
                            Manager.ManagerMainScreen managerMainScreen = new Manager.ManagerMainScreen(user);

                            managerMainScreen.Show();
                            Close();
                            break;
                        case "TeamHead":
                            TeamHeadScreen TeamHeadScreen = new TeamHeadScreen(user);



                            TeamHeadScreen.Show();
                            Close();
                            break;

                        default:
                            WorkerScreen workerScreen = new WorkerScreen(user);

                            workerScreen.Show();
                            Close();
                            break;

                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("TODO:bad request");

                }
            }


        }

    }
}
