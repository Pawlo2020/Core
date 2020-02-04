using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamics.Projects.WeekViewChart
{
    /// <summary>
    /// Logika interakcji dla klasy WeekViewUC.xaml
    /// </summary>
    public partial class WeekViewUC : UserControl
    {
        #region DayColors

        public static readonly DependencyProperty MONDayColorProperty = DependencyProperty.Register("MONDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush MONDayColor
        {
            get { return (Brush)this.GetValue(MONDayColorProperty); }
            set { this.SetValue(MONDayColorProperty, value); }
        }

        public static readonly DependencyProperty TUEDayColorProperty = DependencyProperty.Register("TUEDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush TUEDayColor
        {
            get { return (Brush)this.GetValue(TUEDayColorProperty); }
            set { this.SetValue(TUEDayColorProperty, value); }
        }

        public static readonly DependencyProperty WEDDayColorProperty = DependencyProperty.Register("WEDDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush WEDDayColor
        {
            get { return (Brush)this.GetValue(WEDDayColorProperty); }
            set { this.SetValue(WEDDayColorProperty, value); }
        }


        public static readonly DependencyProperty THUDayColorProperty = DependencyProperty.Register("THUDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush THUDayColor
        {
            get { return (Brush)this.GetValue(THUDayColorProperty); }
            set { this.SetValue(THUDayColorProperty, value); }
        }

        public static readonly DependencyProperty FRIDayColorProperty = DependencyProperty.Register("FRIDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush FRIDayColor
        {
            get { return (Brush)this.GetValue(FRIDayColorProperty); }
            set { this.SetValue(FRIDayColorProperty, value); }
        }

        public static readonly DependencyProperty SATDayColorProperty = DependencyProperty.Register("SATDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush SATDayColor
        {
            get { return (Brush)this.GetValue(FRIDayColorProperty); }
            set { this.SetValue(SATDayColorProperty, value); }
        }

        public static readonly DependencyProperty SUNDayColorProperty = DependencyProperty.Register("SUNDayColor", typeof(Brush), typeof(WeekViewUC));
        public Brush SUNDayColor
        {
            get { return (Brush)this.GetValue(SUNDayColorProperty); }
            set { this.SetValue(SUNDayColorProperty, value); }
        }
        #endregion



        public WeekViewUC()
        {
            InitializeComponent();
            

        }
    }
}
