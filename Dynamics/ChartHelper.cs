using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dynamics
{
    class ChartHelper : EventClass
    {
        DateTime Today;
        List<DateTime> WList;
        List<string> WeekList;
        List<int> Counter;
        int currentDayNO;
        int maxvalue;
        double singleTaskValue;
        Dynamics.Projects.WeekViewChart.WeekViewUC _Chart;

        public ChartHelper(string JsonPath, Dynamics.Projects.WeekViewChart.WeekViewUC Chart) : base(JsonPath)
        {
            _Chart = Chart;
            Today = DateTime.Today;
            currentDayNO = (int)Today.DayOfWeek;
            FillDays();
        }

        void FillDays()
        {
            _Chart.MONDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));
            _Chart.TUEDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));
            _Chart.WEDDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));
            _Chart.THUDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));
            _Chart.FRIDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));
            _Chart.SATDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));
            _Chart.SUNDayColor = (Brush)(new BrushConverter().ConvertFrom("#FF5F27CD"));

            switch (currentDayNO - 1)
            {
                case 0:
                    _Chart.MONDayColor = Brushes.Red;
                    break;

                case 1:
                    _Chart.TUEDayColor = Brushes.Red;
                    break;
                case 2:
                    _Chart.WEDDayColor = Brushes.Red;
                    break;
                case 3:
                    _Chart.THUDayColor = Brushes.Red;
                    break;
                case 4:
                    _Chart.FRIDayColor = Brushes.Red;
                    break;
                case 5:
                    _Chart.SATDayColor = Brushes.Red;
                    break;
                case 6:
                    _Chart.SUNDayColor = Brushes.Red;
                    break;

            }
        }

        public void CountWeekTask()
        {
            WeekList = new List<string>();
            Counter = new List<int>();
            DateTime monday = Today.AddDays(-currentDayNO + 1);
            DateTime sunday = monday.AddDays(6);

            WList = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();

            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\CountWeekTasks.py");
            Scope.SetVariable("PATH", _JsonPath);
            for (int i = 0; i < WList.Count; i++)
            {
                WeekList.Add(WList[i].ToShortDateString());
            }
            Scope.SetVariable("WeekList", WeekList);
            Source.Execute(Scope);
            Counter = Scope.GetVariable("WeekTasks");
        }

        public void SetTasksValues()
        {
            CountWeekTask();
            maxvalue = Counter.Max();
            try
            {
                singleTaskValue = 124 / maxvalue;
            }
            catch
            {
                singleTaskValue = 0;
            }
            //MON
            _Chart.RMON.Height = singleTaskValue * Counter[0];

            //TUE
            _Chart.RTUE.Height = singleTaskValue * Counter[1];

            //WED
            _Chart.RWED.Height = singleTaskValue * Counter[2];

            //THU
            _Chart.RTHU.Height = singleTaskValue * Counter[3];

            //FRI
            _Chart.RFRI.Height = singleTaskValue * Counter[4];

            //SAT
            _Chart.RSAT.Height = singleTaskValue * Counter[5];

            //SUN
            _Chart.RSUN.Height = singleTaskValue * Counter[6];
        }


    }
}
