using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Controls
{
    public class EventArgsChangedPage : EventArgs
    {
        public TreeViewPathCollection OldPath { get; set; }
        public TreeViewPathCollection NewPath { get; set; }
    }
}
