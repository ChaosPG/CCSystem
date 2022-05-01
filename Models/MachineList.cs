using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSystem_New.Models
{
    public class MachineList
    {
        public int ID { get; set; }

        [DisplayName("ชื่อโครงการ")]
        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        [DisplayName("ชื่อโครงการ")]
        public virtual Project Project { get; set; }

        [DisplayName("รถปูยาง")]
        public int MA_ID { get; set; }

        [ForeignKey("MA_ID")]
        [DisplayName("รถปูยาง")]
        public virtual Machine_Asphalt Machine_Asphalt { get; set; }

        [DisplayName("จำนวน")]
        public int Machine_01_Unit { get; set; }

        [DisplayName("รถดัน")]
        public int? MB_ID { get; set; }

        [ForeignKey("MB_ID")]
        [DisplayName("รถดัน")]
        public virtual Machine_Bulldozer Machine_Bulldozer { get; set; }

        [DisplayName("จำนวน")]
        public int? Machine_02_Unit { get; set; }

        [DisplayName("รถเครน")]
        public int? MC_ID { get; set; }

        [ForeignKey("MC_ID")]
        [DisplayName("รถเครน")]
        public virtual Machine_Crane Machine_Crane { get; set; }

        [DisplayName("จำนวน")]
        public int? Machine_03_Unit { get; set; }

        [DisplayName("รถแบคโฮ")]
        public int? ME_ID { get; set; }

        [ForeignKey("ME_ID")]
        [DisplayName("รถแบคโฮ")]
        public virtual Machine_Excavator Machine_Excavator { get; set; }

        [DisplayName("จำนวน")]
        public int? Machine_04_Unit { get; set; }

        [DisplayName("รถโฟล์คลิฟท์")]
        public int? MF_ID { get; set; }

        [ForeignKey("MF_ID")]
        [DisplayName("รถโฟล์คลิฟท์")]
        public virtual Machine_Folklift Machine_Folklift { get; set; }

        [DisplayName("จำนวน")]
        public int? Machine_05_Unit { get; set; }

        [DisplayName("รถบด")]
        public int? MR_ID { get; set; }

        [ForeignKey("MR_ID")]
        [DisplayName("รถบด")]
        public virtual Machine_Roller Machine_Roller { get; set; }

        [DisplayName("จำนวน")]
        public int? Machine_06_Unit { get; set; }

        [DisplayName("รถตัก")]
        public int? MW_ID { get; set; }

        [ForeignKey("MW_ID")]
        [DisplayName("รถตัก")]
        public virtual Machine_WheelLoader Machine_WheelLoader { get; set; }

        [DisplayName("จำนวน")]
        public int? Machine_07_Unit { get; set; }

        [DisplayName("ค่าเช่าทั้งหมด")]
        public decimal MachineRental { get; set; }
    }
}