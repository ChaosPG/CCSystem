using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CCSystem_New.Models
{
    public class MaterialList
    {
        public int ID { get; set; }

        [DisplayName("ชื่อโครงการ")]
        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        [Display(Name = "ชื่อโครงการ")]
        public virtual Project Project { get; set; }

        [DisplayName("วัสดุปูพื้น 1")]
        public int Floor_01_ID { get; set; }

        [ForeignKey("Floor_01_ID")]
        [Display(Name = "วัสดุปูพื้น 1")]
        public virtual Floor_Material_01 Floor_Material_01 { get; set; }

        [DisplayName("จำนวน")]
        public int Material_01_quantity { get; set; }

        [DisplayName("วัสดุปูพื้น 2")]
        public int Floor_02_ID { get; set; }

        [ForeignKey("Floor_02_ID")]
        [Display(Name = "วัสดุปูพื้น 2")]
        public virtual Floor_Material_02 Floor_Material_02 { get; set; }

        [DisplayName("จำนวน")]
        public int? Material_02_quantity { get; set; }

        [DisplayName("วัสดุทั่วไป 1")]
        public int General_01_ID { get; set; }

        [ForeignKey("General_01_ID")]
        [Display(Name = "วัสดุทั่วไป 1")]
        public virtual General_Material_01 General_Material_01 { get; set; }

        [DisplayName("จำนวน")]
        public int? Material_03_quantity { get; set; }


        [DisplayName("วัสดุทั่วไป 2")]
        public int General_02_ID { get; set; }

        [ForeignKey("General_02_ID")]
        [Display(Name = "วัสดุทั่วไป 2")]
        public virtual General_Material_02 General_Material_02 { get; set; }

        [DisplayName("จำนวน")]
        public int? Material_04_quantity { get; set; }

        [DisplayName("วัสดุมุงหลังคา 1")]
        public int Roof_01_ID { get; set; }

        [ForeignKey("Roof_01_ID")]
        [Display(Name = "วัสดุมุงหลังคา 1")]
        public virtual Roof_Material_01 Roof_Material_01 { get; set; }

        [DisplayName("จำนวน")]
        public int? Material_05_quantity { get; set; }

        [DisplayName("วัสดุมุงหลังคา 2")]
        public int Roof_02_ID { get; set; }

        [ForeignKey("Roof_02_ID")]
        [Display(Name = "วัสดุมุงหลังคา 2")]
        public virtual Roof_Material_02 Roof_Material_02 { get; set; }

        [DisplayName("จำนวน")]
        public int? Material_06_quantity { get; set; }

        [DisplayName("ราคารวมวัสดุ")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal MaterialCost { get; set; }
    }
}