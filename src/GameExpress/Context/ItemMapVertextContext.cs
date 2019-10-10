using GameExpress.Model.Item;
using GameExpress.SelectionFrames;
using GameExpress.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameExpress.Context
{
    public class ItemMapVertextContext : IItemContext
    {
        /// <summary>
        /// Liefert die Eigenschaftsseite
        /// </summary>
        public Type Property => typeof(MapVertextPropertyPage);

        /// <summary>
        /// Liefert die Bearbeitungsseite
        /// </summary>
        public Type Page => typeof(MapPage);

        /// <summary>
        /// Liefert den Auswahlrahmen
        /// </summary>
        /// <param name="item">Der zum Item gehörende Auswahlrahmen</param>
        /// <return>Der Auswahlrahmen</return>
        public ISelectionFrame SelectionFrameFactory(Item item)
        {
            return null;
        }

        /// <summary>
        /// Liefert das Icon des Items aus der FontFamily Segoe MDL2 Assets
        /// </summary>
        public virtual string Symbol => "\uE139";
    }
}
