using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CCSystem_New.Models
{
    public class Machine_Bulldozer
    {
        public int ID { get; set; }

        [DisplayName("ชื่อเครื่องจักร")]
        public string MachineName { get; set; }

        [DisplayName("ค่าเช่าเครื่องจักร")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal MachineRentalPrice { get; set; }
    }
}