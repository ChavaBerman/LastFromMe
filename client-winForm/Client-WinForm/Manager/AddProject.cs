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
    public partial class AddProject : Form
    {
        List<User> addedUsers = new List<User>();

        public AddProject()
        {
            InitializeComponent();
            HttpClient client = new HttpClient();
            List<User> teamHeads = new List<User>();
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
            cmb_TeamHeads.DataSource = teamHeads;
            cmb_TeamHeads.DisplayMember = "userName";
        }

        private void cmb_TeamHeads_SelectedIndexChanged(object sender, EventArgs e)
        {
            Added_Workers.Items.Clear();
            addedUsers.Clear();
            List<User> allowedWorkers = new List<User>();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(@"http://localhost:61309/api/Users/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"GetAllowedWorkers/{((sender as ComboBox).SelectedItem as User).UserId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var usersJson = response.Content.ReadAsStringAsync().Result;
                allowedWorkers = JsonConvert.DeserializeObject<List<User>>(usersJson);
                cmb_workers.DataSource = allowedWorkers;
                cmb_workers.DisplayMember = "userName";
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private void cmb_workers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (addedUsers.Find(p => p.UserId == ((sender as ComboBox).SelectedItem as User).UserId) == null)
            {
                addedUsers.Add((sender as ComboBox).SelectedItem as User);
                Added_Workers.Items.Add(((sender as ComboBox).SelectedItem as User).UserName);
            }
        }


        private void Added_Workers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            foreach (ListViewItem item in Added_Workers.SelectedItems)
            {
                addedUsers.RemoveAt(Added_Workers.Items.IndexOf(item));
                Added_Workers.Items.Remove(item);
                
            }
        }

        private void btn_add_project_Click(object sender, EventArgs e)
        {
            Project newProject = new Project {
                ProjectName = txt_projectName.Text,
                CustomerName = txt_CustomerName.Text,
                IdManager = ((cmb_TeamHeads as ComboBox).SelectedItem as User).UserId,
                workers = addedUsers,
                DevHours = DevHours.Value,
                UIUXHours=UIUXHours.Value,
                QAHours =QAHours.Value,
                DateBegin=date_begin.Value,
                DateEnd=date_end.Value
            };

            var validationContext = new ValidationContext(newProject, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(newProject, validationContext, results, true))
            {
                try
                {
                    //Post Request for Register
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:61309/api/Projects");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string user = JsonConvert.SerializeObject(newProject, Formatting.None);

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

                            MessageBox.Show("added successfully");
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
                MessageBox.Show(string.Join(",\n", results.Select(p => p.ErrorMessage)));

            }

        }
    }
}
