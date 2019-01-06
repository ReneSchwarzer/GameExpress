using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich eine Eigenschaften geändert hat
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialisiert das ViewModel
        /// </summary>
        public virtual void InitAsync()
        {
        }

        /// <summary>
        /// Löst das PropertyChanged-Event aus
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
