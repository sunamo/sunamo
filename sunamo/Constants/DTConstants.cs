using System;
using System.Collections.Generic;
using System.Text;


    public partial class DTConstants
    {
    

        

    public static readonly string[] daysInWeekEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public static readonly string[] monthsInYearEN = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        
    public static readonly DateTime UnixFsStart = new DateTime(1970, 1, 1);
    public const long secondsInMinute = 60;
        public const long secondsInHour = secondsInMinute * 60;
        public const long secondsInDay = secondsInHour * 24;
    }

