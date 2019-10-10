using GameExpress.Context;
using GameExpress.Model;
using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            // Kontext ermitteln
            var context = ContextRepository.FindContext(item);

            MainPage?.ChangePropertyPage(context?.Property, item);
        }

        /// <summary>
        /// Wechselt die Ansichtsseite
        /// </summary>
        /// <param name="item">Das Item</param>
        public static void ChangePage(Item item)
        {
            // Kontext ermitteln
            var context = ContextRepository.FindContext(item);

            if (context.Page != null)
            {
                MainPage?.ChangePage(context.Page, item);
            }

            ChangePropertyPage(item);
        }

        /// <summary>
        /// Liefert das aktuelle Projekt
        /// </summary>
        public static Project Project => MainPage.Model.Project;

        /// <summary>
        /// Liefert das Symbol zu einem Item
        /// </summary>
        /// <param name="item">Das Item</param>
        /// <returns>Das Symbol</returns>
        public static string GetSymbol(Item item)
        {
            var context = ContextRepository.FindContext(item);

            if (item is ItemStory story)
            {
                if (ContextRepository.FindContext(story.Instance?.Instance) is IItemContext c)
                {
                    return c.Symbol;
                }
            }
            else if (context != null)
            {
                return context.Symbol;
            }

            return "\uE18A";
        }
    }
}
