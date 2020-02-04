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
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dynamics
{
    /// <summary>
    /// Logika interakcji dla klasy Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        //DateTime CurrentData;
        List<string> WeekList { get; set; }
        List<DayState> DayList { get; set; }
        List<string> months = new List<string> { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
        int item;
        //public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(Calendar));
        //public event EventHandler<DayChangedEventArgs> DayChanged;

        //public DateTime CurrentDate
        //{
        //    get { return (DateTime)GetValue(CurrentDateProperty); }
        //    set { SetValue(CurrentDateProperty, value); }
        //}
        private object FindItemControl(ItemsControl itemsControl, string controlName, object item)
        {
            ContentPresenter container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
            container.ApplyTemplate();
            return container.ContentTemplate.FindName(controlName, container);
        }
        ItemsControl IC;
        EventClass EVHelper;
        Dynamics.Projects.Calendar_page _main;
        public Calendar(EventClass IPHelper, string JsonPath, Dynamics.Projects.Calendar_page main)
        {
            _main = main;
            //CurrentData = DateTime.Today;


            InitializeComponent();

            EVHelper = new EventClass(JsonPath);
            
            

            comboMonth.ItemsSource = months;
            for (int i = -50; i < 50; i++)
            {
                comboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }
            int MNumber = Convert.ToInt32(DateTime.Today.Month.ToString());
            comboMonth.SelectedItem = months[MNumber - 1];
            comboYear.SelectedItem = DateTime.Today.Year;

           
            WeekList = new List<string>() { "PN", "WT", "ŚR", "CZ", "PT", "SO", "ND"};
            
            foreach (string element in WeekList)
            {
                ItemC.Items.Add(element);
            }

            DayList = new List<DayState>();
            
            comboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            comboYear.SelectionChanged += (o, e) => RefreshCalendar();

        }


        public void TakeDates(DateTime TargetDate)
        {
            DayList.Clear();

            //Calculating

            DateTime d = new DateTime(TargetDate.Year, TargetDate.Month, 1);
            int offset = DayOfWeekNumber(d.DayOfWeek);
            if (offset != 1) d = d.AddDays(-offset - 6);

            for (int i = 1; i <= 42; i++)
            {
                DayState Daystate = new DayState { Date = d, EnabledDate = true, CorrectMonth = TargetDate.Month == d.Month };
                //Daystate.PropertyChanged += Day_Changed;
                if (DateTime.Today == Daystate.Date)
                {
                    Daystate.IsTodayDate = true;
                }
                if (TargetDate.Month != Daystate.Date.Month)
                {
                    Daystate.CorrectMonth = false;
                }
                DayList.Add(Daystate);
                d = d.AddDays(1);






            }
            foreach (DayState element in DayList)
            {

                DayControl.Items.Add(element);

                DockPanel Panel = (DockPanel)FindItemControl(DayControl, "DOCK", element);
                if (Panel != null)
                {
                    foreach (object child in Panel.Children)
                    {
                        string childname = null;
                        if (child is FrameworkElement)
                        {
                            childname = (child as FrameworkElement).Name;
                            if (childname == "EventContainer")
                            {
                                IC = (ItemsControl)child;
                                EVHelper.GetEvents(IC, element.Date);
                            }
                        }
                    }
                }



            }

        }
            

        private static int DayOfWeekNumber(DayOfWeek dow)
        {
            return Convert.ToInt32(dow.ToString("D"));
        }

        private void RefreshCalendar()
        {
            DayControl.Items.Clear();
            if (comboYear.SelectedItem == null) return;
            if (comboMonth.SelectedItem == null) return;

            int year = (int)comboYear.SelectedItem;

            int month = comboMonth.SelectedIndex + 1;

            DateTime targetDate = new DateTime(year, month, 1);
            DateText.Text = months[month-1] + ", " + year;
            item = month - 1;
            TakeDates(targetDate);

            

            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int MNumber = Convert.ToInt32(DateTime.Today.Month.ToString());
            comboMonth.SelectedItem = months[MNumber - 1];
            comboYear.SelectedItem = DateTime.Today.Year;
            DateText.Text = comboMonth.SelectedItem.ToString() + ", " + comboYear.SelectedItem.ToString();
        }

        

        private void DayControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCalendar();
        }

        private void NextMonthBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (item != 11)
                {
                    comboMonth.SelectedItem = months[item + 1];
                }
                else
                {
                    comboMonth.SelectedItem = months[0];
                    item = Convert.ToInt32(comboYear.SelectedItem.ToString());
                    comboYear.SelectedItem = item + 1;
                }
            }
            catch { }
        }

        private void PreviousMonthBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (item != 0)
                {
                    comboMonth.SelectedItem = months[item - 1];
                }
                else
                {
                    comboMonth.SelectedItem = months[11];
                    item = Convert.ToInt32(comboYear.SelectedItem.ToString());
                    comboYear.SelectedItem = item - 1;
                }
            }
            catch { }
        }

        private void CalBut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DAYVIEW");
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
    
}
