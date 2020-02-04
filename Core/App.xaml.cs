using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ParameterSender SendPar { get; set; }
        protected override void OnStartup(StartupEventArgs e)

        {
            SendPar = new ParameterSender();
            foreach (var args in e.Args)
            {
                SendPar.ParameterList.Add(args.ToString());
            }
            try
            {
                App.Current.Properties["login"] = SendPar.ParameterList[0];
                App.Current.Properties["pass"] = SendPar.ParameterList[1];
            }
            catch { }
            base.OnStartup(e);

        }


        

    }
    }

