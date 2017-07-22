using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignSheet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm pForm = new MainForm();
            Application.AddMessageFilter(new KeyboardMessageFilter(pForm));
            Application.Run(pForm);
        }

        public class KeyboardMessageFilter : IMessageFilter
        {
            private MainForm form;

            public KeyboardMessageFilter(MainForm form)
            {
                this.form = form;
            }

            public bool PreFilterMessage(ref Message mesg)
            {
                if (mesg.Msg == 0x100)
                {
                    if ((int)mesg.WParam == (int)Keys.F11)
                    {
                        form.ToggleFullscreen();
                    }
                }
                return false;
            }
        }
    }
}
