using GameExpress.Model.Item;
using GameExpress.SelectionFrames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameExpress.Context
{
    public interface IItemContext
    {
        /// <summary>
        /// Liefert die Eigenschaftsseite
        /// </summary>
        Type Property { get; }

        /// <summary>
        /// Liefert die Bearbeitungsseite
        /// </summary>
        Type Page { get; }

        /// <summary>
        /// Liefert den Auswahlrahmen
        /// </summary>
        /// <param name="item">Der zum Item gehörende Auswahlrahmen</param>
        /// <return>Der Auswahlrahmen</return>
        ISelectionFrame SelectionFrameFactory(Item item);

        /// <summary>
        /// Liefert das Icon des Items aus der FontFamily Segoe MDL2 Assets
        /// </summary>
        string Symbol { get; }
    }
}
