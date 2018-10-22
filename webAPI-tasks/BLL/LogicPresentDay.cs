using DAL;
using BOL;
using BOL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class LogicPresentDay
    {

        public static List<PresentDay> GetAllPresents()
        {
            string query = $"SELECT * FROM task.PresentDayUser;";
            //TODO:לגמור לעדכן
            Func<MySqlDataReader, List<PresentDay>> func = (reader) =>
            {
                List<PresentDay> presentDays = new List<PresentDay>();
                while (reader.Read())
                {
                    presentDays.Add(new PresentDay
                    {
                        ProjectId=reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TimeBegin=reader.GetDateTime(2),
                        TimeEnd=reader.GetDateTime(3),
                        sumHoursDay=reader.GetInt32(4)
                    });
                }
                return presentDays;
            };

            return DBAccess.RunReader(query, func);
        }

        public static PresentDay GetPresent(int idWorker,int idProject)
        {
            string query = $"SELECT Name FROM task.PresentDayUser WHERE projectid={idProject} and workerid={idWorker}";
            
           

            Func<MySqlDataReader, List<PresentDay>> func = (reader) =>
            {
                List<PresentDay> presentDays = new List<PresentDay>();
                while (reader.Read())
                {
                    presentDays.Add(new PresentDay
                    {
                        ProjectId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TimeBegin = reader.GetDateTime(2),
                        TimeEnd = reader.GetDateTime(3),
                        sumHoursDay = reader.GetInt32(4)
                    });
                }
                return presentDays;
            };

            return DBAccess.RunReader(query, func)[0];
        }
        public static PresentDay GetPresentByWorkerId(int idWorker)
        {
            string query = $"SELECT Name FROM task.PresentDay WHERE  workerid={idWorker}";



            Func<MySqlDataReader, List<PresentDay>> func = (reader) =>
            {
                List<PresentDay> presentDays = new List<PresentDay>();
                while (reader.Read())
                {
                    presentDays.Add(new PresentDay
                    {
                        ProjectId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TimeBegin = reader.GetDateTime(2),
                        TimeEnd = reader.GetDateTime(3),
                        sumHoursDay = reader.GetInt32(4)
                    });
                }
                return presentDays;
            };

            return DBAccess.RunReader(query, func)[0];
        }

        public static bool RemovePresent(int id)
        {
            string query = $"DELETE FROM task.PresentDay WHERE presentid={id}";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool UpdatePresent(PresentDay present)
        {
            string query = $"UPDATE task.PresentDay SET timeBegin='{present.TimeBegin}',timeEnd='{present.TimeEnd}',projectId={present.ProjectId} ,workerid={present.UserId}";
            return DBAccess.RunNonQuery(query) == 1;
        }

        public static bool AddPresent(PresentDay presentDay)
        {
            //TODO:לעדכן את סך השעות שהעובד עבד
            string query = $"INSERT INTO `task`.`PresentDay`(`timeBegin`,`timeEnd`,`projectId`,`workerid`) VALUES('{presentDay.TimeBegin}','{presentDay.TimeEnd}',{presentDay.ProjectId},{presentDay.UserId}); ";
            return DBAccess.RunNonQuery(query) == 1;
        }
    }
}
