using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Education.Program_Management
{
   



    public class ProgramClass
    {
        private class Item
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public Item(string name, string value)
            {
                Name = name;
                Value = value;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        



        Profile_Management.UserCredentials UserAccount;
        List<string> ProgramList;
        public Dictionary<string, List<string>> DLIST = new Dictionary<string, List<string>>();
        public string IdentityProgram { get; set; }
        public string Type { get; set; }
        public string DateStart { get; set; }
        public string DateFinish { get; set; }
        public string Level { get; set; }
        public string Direction { get; set; }

        public ProgramClass(Profile_Management.UserCredentials UA)
        {
            UserAccount = UA;
        }
        public ProgramClass()
        {

        }

        public void UpdateProgramList(ComboBox List)
        {
            List.Items.Clear();
            using (Database_Handler.EduDB GetList = new Database_Handler.EduDB())
            {
                ProgramList = GetList.GetRowList($"SELECT ID FROM LEARNING_PROGRAM ORDER BY ID DESC",$"Data Source = {UserAccount.ServiceDataPath}");

                DLIST.Clear();
                DLIST.Add("EID", GetList.GetRowList($"SELECT ID FROM LEARNING_PROGRAM ORDER BY ID DESC " , $"Data Source = {UserAccount.ServiceDataPath}"));
                DLIST.Add("TYPE", GetList.GetRowList($"SELECT TYPE FROM LEARNING_PROGRAM ORDER BY TYPE DESC ", $"Data Source = {UserAccount.ServiceDataPath}"));
                DLIST.Add("START", GetList.GetRowList($"SELECT START FROM LEARNING_PROGRAM ORDER BY START DESC ", $"Data Source = {UserAccount.ServiceDataPath}"));
                DLIST.Add("FINISH", GetList.GetRowList($"SELECT FINISH FROM LEARNING_PROGRAM ORDER BY FINISH DESC ", $"Data Source = {UserAccount.ServiceDataPath}"));
                DLIST.Add("LEVEL", GetList.GetRowList($"SELECT LEVEL FROM LEARNING_PROGRAM ORDER BY LEVEL DESC ", $"Data Source = {UserAccount.ServiceDataPath}"));
                DLIST.Add("DIRECTION", GetList.GetRowList($"SELECT DIRECTION FROM LEARNING_PROGRAM ORDER BY DIRECTION DESC ", $"Data Source = {UserAccount.ServiceDataPath}"));

                string[] Row = new string[DLIST.Count];

                try
                {
                    int j = 0;
                    for (int i = 0; i < ProgramList.Count; i++)
                    {
                        j = 0;
                        if (DLIST.TryGetValue("EID", out ProgramList))
                        {
                            Row[j] = ProgramList[i];
                        }
                        j++;
                        if (DLIST.TryGetValue("TYPE", out ProgramList))
                        {
                            Row[j] = ProgramList[i];
                        }
                        j++;
                        if (DLIST.TryGetValue("START", out ProgramList))
                        {
                            Row[j] = ProgramList[i];
                        }
                        j++;
                        if (DLIST.TryGetValue("FINISH", out ProgramList))
                        {
                            Row[j] = ProgramList[i];
                        }
                        j++;
                        if (DLIST.TryGetValue("LEVEL", out ProgramList))
                        {
                            Row[j] = ProgramList[i];
                        }
                        j++;
                        if (DLIST.TryGetValue("DIRECTION", out ProgramList))
                        {
                            Row[j] = ProgramList[i];
                        }

                        if (String.IsNullOrWhiteSpace(Row[4]))
                        {
                            if (Row[1] == "ELEMENTARY")
                            {
                                List.Items.Add(new Item("Szkoła podstawowa " + Row[2] + "-" + Row[3], Row[0]));
                            }else if (Row[1] == "MIDDLE")
                            {
                                List.Items.Add(new Item("Liceum " + Row[2] + "-" + Row[3], Row[0]));

                            }else if (Row[1] == "TECH")
                            {
                                List.Items.Add(new Item("Technikum " + Row[2] + "-" + Row[3], Row[0]));

                            }
                        }else if (Row[1] == "BRANCH")
                        {
                            List.Items.Add(new Item("Szkoła branżowa " + Row[2] + "-" + Row[3] + " " + Row[4] + ", stopień", Row[0]));

                        }else if (Row[1] == "STUDY")
                        {
                            List.Items.Add(new Item("Studia wyższe " + Row[2] + "-" + Row[3] + " " + Row[4] + ", stopień," + " " + Row[5], Row[0]));

                        }
                    }

                }
                catch { }
            }
        }
        }
    }

