using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LocationDisplay.Miscellaneous;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.PageControllers
{
    public class HistoryController
    {
        public String AgentFilter { get; set; }                     = String.Empty;
        public ControllerDateTime initialDateTime { get; set; }     = new ControllerDateTime( new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
        public ControllerDateTime finalDateTime { get; set; }       = new ControllerDateTime( new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0));

        [Range(typeof(bool), "false", "false",
            ErrorMessage = "Data initiala nu poate fi mai mare decat data finala")]
        public Boolean dateTimeError { get; set; } = false;
        
        public void CheckDate()
        {
            dateTimeError = initialDateTime.dateTime > finalDateTime.dateTime;
        }
    }
}
