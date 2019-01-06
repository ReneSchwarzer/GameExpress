using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace GameExpress.Core
{
    /// <summary>
    /// Basisklasse eines Projektes
    /// </summary>
    [XmlRoot("gameeditor")]
    public sealed class Project : IProject
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Project()
        {
            
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public void Init()
        {
            RegisterItemContext(typeof(ItemRoot), new ItemRootContext());
            RegisterItemContext(typeof(ItemDirectory), new ItemDirectoryContext());
            RegisterItemContext(typeof(ItemVisualImage), new ItemVisualImageContext());
            RegisterItemContext(typeof(ItemVisualObject), new ItemVisualObjectContext());
            RegisterItemContext(typeof(ItemVisualAnimatedObjectState), new ItemVisualAnimatedObjectStateContext());
            RegisterItemContext(typeof(ItemVisualGeometryRectangele), new ItemVisualGeometryRectangeleContext());
            RegisterItemContext(typeof(ItemMap), new ItemMapContext());
            RegisterItemContext(typeof(ItemMapVertext), new ItemMapVertextContext());
            RegisterItemContext(typeof(ItemMapMesh), new ItemMapMeshContext());
            RegisterItemContext(typeof(ItemVisualInstance), new ItemVisualInstanceContext());
            RegisterItemContext(typeof(ItemVisualKeyFrame), new ItemVisualKeyFrameContext());
            
            RootItem = ItemContextList.GetItemContext(typeof(ItemRoot)).ItemFactory() as ItemRoot;
        }

        /// <summary>
        /// Registriert ein Kontextobjekt
        /// </summary>
        /// <param name="type">Das zu registrierende Objekt</param>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public void RegisterItemContext(Type type, ItemContext context)
        {
            ItemContextList.RegisterItemContext(type, context);
        }

        /// <summary>
        /// Sucht ein Kontext anhand des Item-Typs
        /// </summary>
        /// <param name="type">der Typ</param>
        /// <returns>Der Kontext</returns>
        public IItemContext GetItemContext(Type type)
        {
            return ItemContextList.GetItemContext(type);
        }

        /// <summary>
        /// Liefert die Kontextliste
        /// </summary>
        [XmlIgnore]
        public static ItemContextList ItemContextList { get; } = new ItemContextList();

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        [XmlElement("project")]
        public ItemRoot RootItem { get; set; }
    }
}
