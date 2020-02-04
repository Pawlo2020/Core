using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics
{
    public class DayState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime date;
        private bool enabledDate;
        private bool correctMonth;
        private bool isTodayDate;

        public bool IsTodayDate
        {
            get { return isTodayDate; }
            set
            {
                isTodayDate = value;
                //if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsTodayDate"));
            }
        }

        public bool CorrectMonth
        {
            get { return correctMonth; }
            set
            {
                correctMonth = value;
                //if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CorrectMonth"));
            }
        }

        public bool EnabledDate
        {
            get { return enabledDate; }
            set { enabledDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EnabledDate"));
            }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }



    }
}
