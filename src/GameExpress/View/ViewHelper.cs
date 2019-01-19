using GameExpress.Model;
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

        /// <summary>
        /// Wechselt die Ansichtsseite
        /// </summary>
        /// <param name="item">Das Item</param>
        public static void ChangePage(Item item)
        {
            if (item is ItemGame)
            {
                MainPage?.ChangePage(typeof(GamePage), item);
            }
            else if (item is ItemScene)
            {
                MainPage?.ChangePage(typeof(ObjectPage), item);
            }
            else if (item is ItemObject)
            {
                MainPage?.ChangePage(typeof(ObjectPage), item);
            }
            else if (item is ItemMap)
            {
                MainPage?.ChangePage(typeof(MapPage), item);
            }
            else if (item is ItemImage)
            {
                MainPage?.ChangePage(typeof(ImagePage), item);
            }
            else if (item is ItemSound)
            {
                MainPage?.ChangePage(typeof(SoundPage), item);
            }

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

        /// <summary>
        /// Liefert das aktuelle Projekt
        /// </summary>
        public static Project Project
        {
            get
            {
                return MainPage.Model.Project;
            }
        }
    }
}
