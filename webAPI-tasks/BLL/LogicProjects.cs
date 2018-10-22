using DAL;
using BOL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LogicProjects
    {

        public static List<Project> GetAllProjects()
        {
            string query = $"SELECT * FROM task.project;";

            Func<MySqlDataReader, List<Project>> func = (reader) =>
            {
                List<Project> projects = new List<Project>();
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        ProjectId = reader.GetInt32(0),
                        ProjectName = reader.GetString(1),
                        CustomerName = reader.GetString(2),
                        QAHours = reader.GetInt32(3),
                        UIUXHours = reader.GetInt32(4),
                        DevHours = reader.GetInt32(5),
                        DateBegin = reader.GetMySqlDateTime(6),
                        DateEnd = reader.GetMySqlDateTime(7),
                        IdManager = reader.GetInt32(8),
                        IsFinish = reader.GetBoolean(9)
                    });
                }
                return projects;
            };

            return DBAccess.RunReader(query, func);
        }



        public static Project GetProjectDetails(string projectName)
        {
            string query = $"SELECT * FROM task.project WHERE name={projectName}";
            Func<MySqlDataReader, List<Project>> func = (reader) =>
            {
                List<Project> projects = new List<Project>();
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        ProjectId = reader.GetInt32(0),
                        ProjectName = reader.GetString(1),
                        CustomerName = reader.GetString(2),
                        QAHours = reader.GetInt32(3),
                        UIUXHours = reader.GetInt32(4),
                        DevHours = reader.GetInt32(5),
                        DateBegin = reader.GetMySqlDateTime(6),
                        DateEnd = reader.GetMySqlDateTime(7),
                        IdManager = reader.GetInt32(8),
                        IsFinish = reader.GetBoolean(9)

                    });
                }
                return projects;
            };
            List<Project> proj = DBAccess.RunReader(query, func);
            if (proj != null && proj.Count > 0)
            {

                return proj[0];
            }
            return null;
          

        }


        public static Project GetProjectDetails(int projectId)
        {
            string query = $"SELECT * FROM task.project WHERE projectId={projectId}";
            Func<MySqlDataReader, List<Project>> func = (reader) =>
            {
                List<Project> projects = new List<Project>();
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        ProjectId = reader.GetInt32(0),
                        ProjectName = reader.GetString(1),
                        CustomerName = reader.GetString(2),
                        QAHours = reader.GetInt32(3),
                        UIUXHours = reader.GetInt32(4),
                        DevHours = reader.GetInt32(5),
                        DateBegin = reader.GetMySqlDateTime(6),
                        DateEnd = reader.GetMySqlDateTime(7),
                        IdManager = reader.GetInt32(8),
                        IsFinish = reader.GetBoolean(9),
                    });
                }
                return projects;
            };

            return (DBAccess.RunReader(query, func).Count() != 0 ? DBAccess.RunReader(query, func)[0] : null);

        }




        public static bool RemoveProject(string projectName)
        {
            //  int projectId = GetProjectDetails(projectName).Id;
            //  string query = $"DELETE FROM task.projectworker WHERE projectid={projectId}";
            //if(DBAccess.RunNonQuery(query)!=1)
            //      return false;
            //  query = $"DELETE FROM task.projectworker WHERE projectid={projectId}";
            //  if (DBAccess.RunNonQuery(query) != 1)
            //      return false;
            string query = $"DELETE FROM task.hourfordepartment WHERE Name={projectName}";
            return DBAccess.RunNonQuery(query) == 1;
        }



        //public static bool UpdateProject(Project project)
        //{
        //    string query = $"UPDATE task.project SET numHour='{project.numHourForProject}',name='{project.ProjectName}',dateBegin={project.DateBegin} ,dateEnd={project.DateEnd} ,isFinish='{project.IsFinish}',customerName='{project.CustomerName}'  WHERE id={project.ProjectId} ";
        //    return DBAccess.RunNonQuery(query) == 1;
        //}
        public static bool AddWorkerToProject(int projectId, List<User> workers)
        {

            foreach (var item in workers)
            {
                string query = $"INSERT INTO `task`.`projectworker`(`projectId`,`workerId`)VALUES({ projectId},{ item.UserId});";
                if (DBAccess.RunNonQuery(query) != 1)
                    return false;
            }
            return true;
        }


        public static bool AddProject(Project project)
        {
            //TODO:איזה דפרטמנט
     
            string query = $"INSERT INTO `task`.`project`(`name`,`startdate`,`Enddate`,`isFinished`,`customerName`,`DevHours`,`QAHours`,`UIUXHours`,`teamheadId`) VALUES('{project.ProjectName}','{project.DateBegin}','{project.DateEnd}',{project.IsFinish},'{project.CustomerName}',{project.DevHours},{project.QAHours},{project.UIUXHours},{project.IdManager}); ";
            if (DBAccess.RunNonQuery(query) == 1)
            {
                Project currentProject = GetProjectDetails(project.ProjectName);
                List<User> workers = new List<User>();
                workers = LogicManager.GetAllWorkersByTeamId(project.IdManager);
                workers.AddRange(project.workers);
                foreach (var item in workers)
                {
                    query = $"INSERT INTO `task`.`task`(`reservingHours`,`givenHours`,`idProject`,`idUser`)VALUES(0,0,{currentProject.ProjectId},{item.UserId });";
                    DBAccess.RunNonQuery(query);
                    
                }
                return true;
            }
            return false;

        }
    }
}
