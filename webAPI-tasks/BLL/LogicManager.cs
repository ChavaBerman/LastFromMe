
using DAL;
using BOL;
using BOL.Convertors;
using BOL.HelpModel;
using BOL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BLL
{
    public  class LogicManager
    {

        public static List<User> GetAllUsers()
        {
            string query = $"SELECT * FROM task.user JOIN task.status ON task.user.IdStatus=task.status.IdStatus ;";

            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(ConvertorUser.convertDBtoUser(reader));
                }
                return users;
            };

            return DBAccess.RunReader(query, func);
        }

        public static List<User> GetAllWorkersByTeamId(int id)
        {
            string query = $"SELECT * FROM task.user JOIN task.status ON task.user.IdStatus=task.status.IdStatus WHERE managerId={id};";

            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(ConvertorUser.convertDBtoUser(reader));
                }
                return users;
            };

            return DBAccess.RunReader(query, func);
        }
        public static List<Project> GetProjectsUser(int id)
        {
            string query = $"SELECT * FROM task.projectworker WHERE workerId={id} ;";
            List<Project> projects = new List<Project>();
            Func<MySqlDataReader, List<PresentDay>> func = (reader) => {
                List<PresentDay> projectsWorker = new List<PresentDay>();
                while (reader.Read())
                {
                    projectsWorker.Add(ConvertorsProjectWorkercs.convertDBtoProjectWorker(reader));
                }
                return projectsWorker;
            };

            List<PresentDay> ProjectWorker= DBAccess.RunReader(query, func);
            foreach (var item in ProjectWorker)
            {
                projects.Add( LogicProjects.GetProjectDetails(item.ProjectId));
            }

            return projects;
        }

        public static List<User> GetAllowedWorkers(int teamHeadId)
        {
            string query = $"SELECT * FROM task.user JOIN task.status ON task.user.IdStatus=task.status.IdStatus WHERE task.status.statusname!='TeamHead' and task.status.statusname!='Manager' and task.user.managerId!={teamHeadId};";

            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(ConvertorUser.convertDBtoUser(reader));
                }
                return users;
            };

            return DBAccess.RunReader(query, func);
        }

        public static List<User> GetAllTeamHeads()
        {
            string query = $"SELECT * FROM task.user JOIN task.status ON task.user.IdStatus=task.status.IdStatus WHERE task.status.statusname='TeamHead';";

            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(ConvertorUser.convertDBtoUser(reader));
                }
                return users;
            };

            return DBAccess.RunReader(query, func);
        }

        public static User GetUserDetails(int id)
        {
            string query = $"SELECT * FROM task.user JOIN task.status ON user.IdStatus=status.IdStatus WHERE id={id}";
                 List<User> users = new List<User>();
            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> projectsWorker = new List<User>();
                while (reader.Read())
                {
                    projectsWorker.Add(ConvertorUser.convertDBtoUser(reader));
                }
                return projectsWorker;
            };
            List<User> users1 = DBAccess.RunReader(query, func); 
            return users1.Count>0? users1[0] as User : null;
        }

        public static User GetUserDetailsByPassword(string password,string userName)
        {
            //TODO:לעשות פונקציה שבודקת תווים מיוחדים עי סטור ופונקציה
            string query = $"SELECT * FROM task.user JOIN task.status ON user.IdStatus=status.IdStatus  WHERE password='{password}' and username='{userName}'";

            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(ConvertorUser.convertDBtoUser(reader));
                }
                return users;
            };

           List<User> usersLogin= DBAccess.RunReader(query, func);
            if (usersLogin != null && usersLogin.Count > 0)
            {
                
                return usersLogin[0];
            }
            return null;
 
        }

        public static bool RemoveUser(int id)
        {
            string query = $"DELETE FROM task.user WHERE id={id}";
            return DBAccess.RunNonQuery(query) == 1;
        }
        //-------------------------------------------

        public static bool UpdateUser(User user)
        {
            string query = $"UPDATE task.user SET userName='{user.UserName}',password='{user.Password}',email='{user.Email}',IdStatus={user.StatusId}  ,totalhours={user.NumHoursWork},managerId={user.ManagerId},userComputer='{user.UserComputer}'  WHERE idUser={user.UserId} ";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool AddUser(User user)
        {
            
            //TODO:איזה דפרטמנט
            string query = $"INSERT INTO `task`.`user`(`userName`,`password`,`email`,`IdStatus`,`totalhours`,`managerId`,`userComputer`) VALUES('{user.UserName}','{user.Password}','{user.Email}',{user.StatusId},{user.NumHoursWork},{user.ManagerId},'{user.UserComputer}'); ";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static User GetUserDetailsComputerUser(string computerUser)
        {
            string query = $"USE task;SELECT * FROM task.user JOIN task.status ON user.IdStatus=status.IdStatus WHERE userComputer='{computerUser}'";

            Func<MySqlDataReader, List<User>> func = (reader) => {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(ConvertorUser.convertDBtoUser(reader));
                    
                }
                return users;
            };

            List<User> usersLogin = DBAccess.RunReader(query, func);
            if (usersLogin != null && usersLogin.Count > 0)
            {
               
                return usersLogin[0];
            }
            return null;
        }
    }

   
}