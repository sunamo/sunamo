using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


    //Declare a class that inherits from ToolStripControlHost.
    public class ToolStripDropDownButtonMonthCalendar : ToolStripDropDownButton
    {
        public ToolStripMonthCalendar mc = new ToolStripMonthCalendar();

        public ToolStripDropDownButtonMonthCalendar()
        {
            mc.DateChanged += new DateRangeEventHandler(mc_DateChanged);
            DropDownItems.Add(mc);
        }

        void mc_DateChanged(object sender, DateRangeEventArgs e)
        {
            OnDateChanged(sender, e);
        }

        public Day FirstDayOfWeek
        {
            get
            {
                return mc.FirstDayOfWeek;
            }
            set { mc.FirstDayOfWeek = value; }
        }

        public int MaxSelectionDays
        {
            set
            {
                //mc.MaxSelectionCount = value;
                mc.MaxSelectionDays = value;
            }
        }

        // Declare the DateChanged event.
        public event DateRangeEventHandler DateChanged;

        // Raise the DateChanged event.
        private void OnDateChanged(object sender, DateRangeEventArgs e)
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }
    }
