using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCSystem_New.Models
{
    public class Project
    {
        public int ID { get; set; }

        [DisplayName("ชื่อโครงการ")]
        public string ProjectName { get; set; }

        [DisplayName("ที่ตั้งโครงการ")]
        public string ProjectLoc { get; set; }

        [DisplayName("เจ้าของโครงการ")]
        public string ProjectOwner { get; set; }

        [DisplayName("งบประมาณ")]
        public decimal ProjectBudget { get; set; }

        [DisplayName("หัวหน้าโครงการ")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public string ProjectSupervisor { get; set; }

        [DisplayName("ความคืบหน้าโครงการ")]
        public string Progress { get; set; }

        [DisplayName("วันเริ่มโครงการ")]
        [Required(ErrorMessage = "Please enter a start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ProjectStart { get; set; }

        [DisplayName("วันสิ้นสุดโครงการ")]
        [Required(ErrorMessage = "Please enter a end date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ProjectEnd { get; set; }
    }
}