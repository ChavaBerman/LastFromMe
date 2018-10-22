﻿
using BOL.Models;
using BOL.Validations;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Project
    {

        public Project()
        {

            //HoursForDepartment = new List<HourForDepartment>();
            PresentsDayUser = new List<PresentDay>();
            workers = new List<User>();
        }

        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(15, ErrorMessage = "ProjectName grade than 15 chars"), MinLength(2, ErrorMessage = "ProjectName less than 2 chars")]
        [UniqueProjectName]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "CustomerName is required")]
        [MaxLength(15, ErrorMessage = "CustomerName grade than 15 chars"), MinLength(2, ErrorMessage = "CustomerName less than 2 chars")]
        public string CustomerName { get; set; }



        [Required(ErrorMessage = "DateBegin is required")]
        [ValidDateTimeBegin]
        public MySqlDateTime DateBegin { get; set; }


        [Required(ErrorMessage = "DateEnd is required")]
        [ValidDateTimeEnd]
        public MySqlDateTime DateEnd { get; set; }

        public bool IsFinish { get; set; } = false;

        public int IdManager { get; set; }

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public decimal DevHours { get; set; }

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public decimal QAHours { get; set; }

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public decimal UIUXHours { get; set; }


        //-------------------------
        public User Manager { get; set; }



        public List<PresentDay> PresentsDayUser { get; set; }

        public List<User> workers { get; set; }

    }
}
