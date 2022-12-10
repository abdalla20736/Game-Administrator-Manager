using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light_Sro_Admin_Controller.Classes
{
    public class Logger
    {
        private static Logger _instance = null;
        private ListView _logViewer;

        public static Logger getInstance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }
        public void LogSetter(ListView logviewer)
        {
            _logViewer = logviewer;
        }
        public void ConsoleWrite(string str, Color color)
        {

            string c = "";
            bool flag = false;
            for (int i = 0; i < str.Length; i++)
            {
                flag = false;
                c = c + str[i];
                if (str[i] == '/')
                {
                    string[] s = c.Split('/');

                    ListViewItem lvi1 = new ListViewItem(new[] { "[" + DateTime.Now.ToString("HH:mm:ss - dd/MM/yy") + "] ", s[0] });

                    lvi1.ForeColor = color;

                    _logViewer.Items.Add(lvi1);

                    c = "";
                    flag = true;

                }

            }
            if (flag == false)
            {

                ListViewItem lvi1 = new ListViewItem(new[] { "[" + DateTime.Now.ToString("HH:mm:ss - dd/MM/yy") + "] ", c });

                lvi1.ForeColor = color;
                
                _logViewer.Items.Add(lvi1);
                

            }

            //  listBox.SelectedIndex = -1;
            // listBox.SelectedIndex = listBox.Items.Count - 1;

        }
        public void Info(string str)
        {
            ConsoleWrite(str, Color.Black);
        }
        public void Error(string str)
        {
            ConsoleWrite(str, Color.DarkRed);
        }
        public void Warning(string str)
        {
            ConsoleWrite(str, Color.FromArgb(246, 190, 0));
        }
        public void Success(string str)
        {
            ConsoleWrite(str, Color.Green);
        }
    }
}
