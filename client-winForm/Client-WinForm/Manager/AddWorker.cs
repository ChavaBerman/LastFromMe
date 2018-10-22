using Client_WinForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_WinForm.Manager
{
    public partial class AddWorker : Form
    {
        
        User manager = new User();
        public AddWorker(User manager)
        {
           this.manager= manager ; 
            InitializeComponent();
            List<Status> statuses = new List<Status>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:61309/api/Status/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("GetAllstatuses").Result;
            if (response.IsSuccessStatusCode)
            {
                var departmentJson = response.Content.ReadAsStringAsync().Result;
                statuses = JsonConvert.DeserializeObject<List<Status>>(departmentJson);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            cmb_department.DataSource = statuses;
            cmb_department.DisplayMember = "StatusName";
        }

        private void cmb_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_managerName.DataSource=null;
            if (((sender as ComboBox).SelectedItem as Status).StatusName!= "TeamHead")
            {
                List<User> teamHeads = new List<User>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(@"http://localhost:61309/api/Users/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("GetAllTeamHeads").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    teamHeads = JsonConvert.DeserializeObject<List<User>>(usersJson);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
                cmb_managerName.DataSource = teamHeads;
            }
            else
            {
                List<User> managers = new List<User>() { manager };
                cmb_managerName.DataSource = managers;

            }
            cmb_managerName.DisplayMember = "UserName";
        }

        private void btn_addWorker_Click(object sender, EventArgs e)
        {
            User newUser = new User {
                UserName = txt_userName.Text,
                Password = LoginUser.sha256_hash(txt_password.Text),
                Email =txt_email.Text,
                ManagerId =(cmb_managerName.SelectedItem  as User).UserId,
                NumHoursWork=0,
                StatusId=(cmb_department.SelectedItem as Status).Id,
                
            };
            var validationContext = new ValidationContext(newUser, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(newUser, validationContext, results, true))
            {
                try
                {
                    //Post Request for Register
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:61309/api/addUser");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string user = JsonConvert.SerializeObject(newUser, Formatting.None);

                        streamWriter.Write(user);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    //Gettting response
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    //Reading response
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), ASCIIEncoding.ASCII))
                    {
                        string result = streamReader.ReadToEnd();
                        //If Register succeeded
                        if (httpResponse.StatusCode == HttpStatusCode.Created)
                        {

                         
                            Close();
                        }
                        //Printing the matching error
                        else MessageBox.Show(result);
                    }
                }
                catch (Exception exception)
                {


                }
            }
            else
            {
                MessageBox.Show( string.Join(",\n", results.Select(p => p.ErrorMessage)));  
                
            }
        }
    }
}
