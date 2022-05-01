using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CCSystem_New.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [DisplayName("ชื่อโครงการ")]
        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        [DisplayName("ชื่อพนักงาน")]
        public string EmpName { get; set; }

        [DisplayName("ตำแหน่ง")]
        public string EmpPosition { get; set; }

        [DisplayName("แผนก")]
        public string EmpDepartment { get; set; }

        [DisplayName("เงินเดือน")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal EmpSalary { get; set; }

    }
}