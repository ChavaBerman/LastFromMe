using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_WinForm.Models
{
    class Task
    {
        public int IdTask { get; set; }

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int ReservingHours { get; set; }

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int GivenHours { get; set; }
        public int IdUser { get; set; }
        public int IdProject { get; set; }
    }
}
