using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    public class StatusChangeEventArgs : EventArgs, IDisposable
    {
        public string Text { get; set; }
        public bool IsClosed { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text"></param>
        public StatusChangeEventArgs(string text)
        {
            IsClosed = false;
            Text = text;
        }

        /// <summary>
        /// Schließt die Statusmeldung ab
        /// </summary>
        public void Close()
        {
            IsClosed = true;
        }

        /// <summary>
        /// Wird vor dem zerstören aufgerufen
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
