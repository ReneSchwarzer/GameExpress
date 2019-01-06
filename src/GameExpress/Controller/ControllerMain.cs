using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using GameExpress.Adventure.Items;
using GameExpress.Controls;
using GameExpress.Core;
using GameExpress.Core.Items;
using GameExpress.Editor;
using GameExpress.Editor.Pages;
using GameExpress.Model;
using GameExpress.View;
using static GameExpress.Editor.Pages.ItemPage;

namespace GameExpress.Controller
{
    /// <summary>
    /// Contoller für das Hauptfenster
    /// </summary>
    public class ControllerMain : IControllerMain
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich Namenseigenschaften geändert haben
        /// </summary>
        public event EventHandler<TreeViewPathCollection> ChangedProjectTree;

        /// <summary>
        /// Event zum Mitteilen, dass sich das aktive Item geändert hat
        /// </summary>
        public event EventHandler<ChangeActiveItemEventArgs> ChangeActiveItemEvent;

        /// <summary>
        /// Die zugehörige View
        /// </summary>
        protected IView<IControllerMain> View { get; private set; }

        /// <summary>
        /// Das zugehörige Model
        /// </summary>
        protected IModelMain Model { get; private set; }

        /// <summary>
        /// Liefert oder setzt die letzte Position des Hauptfensters
        /// </summary>
        public Point LastWindowsPos
        {
            get { return Model.LastWindowsPos; }
            set { Model.LastWindowsPos = value; }
        }

        /// <summary>
        /// Liefert oder setzt die letzte Größe des Hauptfensters
        /// </summary>
        public Size LastWindowSize
        {
            get { return Model.LastWindowSize; }
            set { Model.LastWindowSize = value; }
        }

        /// <summary>
        /// Liefert oder setzt den letzten Status des Hauptfensters
        /// </summary>
        public FormWindowState LastWindowState
        {
            get { return Model.LastWindowState; }
            set { Model.LastWindowState = value; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="view">Die View</param>
        /// <param name="model">Das Modell</param>
        public ControllerMain(IView<IControllerMain> view, IModelMain model)
        {
            View = view;
            Model = model;

            View.SetController(this);
        }

        /// <summary>
        /// Erstellt ein Projekt
        /// </summary>
        public IProject CreateProject()
        {
            Model.Project = new Project();

            Model.Project.RootItem = new Core.Items.ItemRoot();

#if DEBUG
            Model.Project.RootItem.Name = "Adventure";
            Model.Project.RootItem.Width = 640;
            Model.Project.RootItem.Height = 480;

            var scene = new ItemScene1();
            Model.Project.RootItem.AddChild(scene);

            scene.AddChild(new ItemImageBackgroundScene1());
            scene.AddChild(new ItemMap1());

            var newItem = new ItemDirectory();
            Model.Project.RootItem.AddChild(newItem);

            var objectItem = new ItemVisualObject();
            objectItem.Name = "Objekt";
            newItem.AddChild(objectItem);
            objectItem.AddChild(new ItemObjectState1());
            objectItem.AddChild(new ItemObjectState2());

            newItem.AddChild(new ItemImageUfo());
            newItem.AddChild(new ItemImageSputnik());
            newItem.AddChild(new ItemImageOmicron());
            newItem.AddChild(new ItemImageFlower1());
            newItem.AddChild(new ItemImageFlower2());
            newItem.AddChild(new ItemImageFlower3());
            newItem.AddChild(new ItemImageFlower4());

            var geometryRectangeleItem = new ItemVisualGeometryRectangele();
            geometryRectangeleItem.Name = "Rechteck";
            geometryRectangeleItem.FrontColor = Color.Red;
            geometryRectangeleItem.BackColor = Color.Yellow;
            geometryRectangeleItem.Width = 100;
            geometryRectangeleItem.Heigt = 100;
            geometryRectangeleItem.Background = new SolidBrush(Color.YellowGreen);
            geometryRectangeleItem.Transparency = new GameExpress.Core.Structs.Transparency(Color.Transparent, false);
            geometryRectangeleItem.Hotspot = new Point(50, 50);

            //geometryRectangeleItem.Size = new Size(100, 100);
            geometryRectangeleItem.Transparency = new GameExpress.Core.Structs.Transparency(Color.Magenta, true);
            newItem.AddChild(geometryRectangeleItem);
#endif
            return Model.Project;
        }

        /// <summary>
        /// Liefert den Projektbaum
        /// </summary>
        public ICollection<TreeViewPathCollection> ProjectTree
        {
            get
            {
                var list = new List<TreeViewPathCollection>();

                foreach (Item p in Model.Project.RootItem?.GetPreOrder().Where(x => x.Context != null && !x.Context.Hidden))
                {
                    var page = EditorContext.CreatePage(p as IItem);
                    var path = new TreeViewPathCollection
                    (
                        p.GetPath().Select
                        (
                            x => new TreeViewPathItem
                            (
                                (x as IItem).GUID.ToString(),
                                (x as IItem).Name,
                                (x as IItem).Context?.Image
                            )
                            {
                                Page = page,
                                Tag = p
                            }
                        )
                    );

                    if (page != null)
                    {
                        page.ChangeActiveItemEvent += (s, e) => { ChangeActiveItemEvent?.Invoke(s, e); };
                        p.NameChanged += (s, e) => 
                        {
                            path.Last().Name = e.Item.Name;
                            path.Page.Title = e.Item.Name;
                            ChangedProjectTree?.Invoke(s, path);
                        };
                    }

                    list.Add(path);
                }

                return list;
            }
        }

        /// <summary>
        /// Lädt das Projekt
        /// </summary>
        /// <param name="file">Der Dateiname inklusive Pfad</param>
        public void LoadProject(string file)
        {
            var s = new XmlSerializer(typeof(Project));
            using (var reader = new StreamReader(file))
            {
                var project = s.Deserialize(reader);
                Model.Project = project as Project;
            }
        }

        /// <summary>
        /// Speichert das Projekt
        /// </summary>
        /// <param name="file">Der Dateiname inklusive Pfad</param>
        public void SaveProject(string file)
        {
            var project = Model.Project;
                
            var s = new XmlSerializer(typeof(Project));
            using (var writer = new StreamWriter(file))
            {
                s.Serialize(writer, project as Project);
            }
        }
    }
}
