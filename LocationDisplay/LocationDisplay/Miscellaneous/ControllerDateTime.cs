using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.Miscellaneous
{
    public class ControllerDateTime
    {
        public DateTime date { get; set; } = new DateTime();
        public DateTime time { get; set; } = new DateTime();
        public DateTime dateTime {
            get => new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            set => date = (time = value);
        }
        public ControllerDateTime(DateTime dt)
        {
            date = time = dateTime = dt; 
        }
    }
}
