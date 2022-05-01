using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCSystem_New.Models
{
    public class Floor_Material_01
    {
        public int ID { get; set; }
        
        [DisplayName("ชื่อวัสดุ")]
        public string MaterialName { get; set; }

        [DisplayName("ราคาวัสดุ")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal MaterialPrice { get; set; }
    }
}