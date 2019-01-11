using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.View
{
    /// <summary>
    /// Hilfsklasse
    /// </summary>
    public static class ViewHelper
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf das Hauptfenster
        /// </summary>
        public static MainPage MainPage { get; set; }

        /// <summary>
        /// Wechselt die Eigenschaftsseite
        /// </summary>
        /// <param name="item">Das Item</param>
        public static void ChangePropertyPage(object item)
        {
            if (item is Item)
            {
                ChangePropertyPage(item as Item);
            }
        }

        /// <summary>
        /// Wechselt die Eigenschaftsseite
        /// </summary>
        /// <param name="item">Das Item</param>
        public static void ChangePropertyPage(Item item)
        {
            if (item is ItemGame)
            {
                MainPage?.ChangePropertyPage(typeof(GamePropertyPage), item);
            }
            else if (item is ItemScene)
            {
                MainPage?.ChangePropertyPage(typeof(ScenePropertyPage), item);
            }
            else if (item is ItemObject)
            {
                MainPage?.ChangePropertyPage(typeof(ObjectPropertyPage), item);
            }
            else if (item is ItemMap)
            {
                MainPage?.ChangePropertyPage(typeof(MapPropertyPage), item);
            }
            else if (item is ItemStory)
            {
                MainPage?.ChangePropertyPage(typeof(StoryPropertyPage), item);
            }
            else if (item is ItemKeyFrame)
            {
                MainPage?.ChangePropertyPage(typeof(KeyFramePropertyPage), item);
            }
            else if (item is ItemImage)
            {
                MainPage?.ChangePropertyPage(typeof(ImagePropertyPage), item);
            }
            else if (item is ItemSound)
            {
                MainPage?.ChangePropertyPage(typeof(SoundPropertyPage), item);
            }
            else
            {
                MainPage?.ChangePropertyPage(null, item);
            }

        }
    }
}
