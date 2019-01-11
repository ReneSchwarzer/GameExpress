using GameExpress.Model;
using GameExpress.Model.Item;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;

namespace GameExpress.ViewModel
{
    public class ViewModelMain : ViewModel
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ViewModelMain()
        {
            
        }

        /// <summary>
        /// Initialisiert das ViewModel
        /// </summary>
        public override async void InitAsync()
        {
#if DEBUG
            //// Testdaten
            //var items = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets", "Items");

            //var game = new ItemGame() { Name = "Lost on Omicron", Note = "Adventurespiel" };
            //var scene = new ItemScene() { Name = "Szene 1", Alpha = 255 };
            //var ufo = new ItemImage() { Name = "UFO", Source = Path.Combine(items, "ufo.png"), Alpha = 255 };
            //var sputnik = new ItemImage() { Name = "Sputnik", Source = Path.Combine(items, "sputnik.png"), Alpha = 255 };
            //var omicron = new ItemImage() { Name = "Omicron", Source = Path.Combine(items, "omicron.png"), Alpha = 255 };
            //var scene1Backgraund = new ItemImage() { Name = "Hintergrund", Source = Path.Combine(items, "scene1.png"), Alpha = 255 };
            //var boom = new ItemSound() { Name = "Boom", Source = Path.Combine(items, "boom.mp3") };
            //var flyingUfo = new ItemObject() { Name = "Fliegendes UFO", Alpha = 255 };

            //Project.Tree.Add(game);

            //game.Children.Add(scene);
            //game.Children.Add(flyingUfo);
            //flyingUfo.Children.Add(ufo);
            //game.Children.Add(sputnik);
            //game.Children.Add(omicron);
            //game.Children.Add(boom);
            //scene.Children.Add(scene1Backgraund);

            var storageFolder = KnownFolders.DocumentsLibrary;
            var folder = await storageFolder.CreateFolderAsync("Lost On Omicron", CreationCollisionOption.OpenIfExists);

            
            
            var file1 = await folder.CreateFileAsync("lostonomicron1.gx", CreationCollisionOption.OpenIfExists);
            LoadProject(file1);

            var file = await folder.CreateFileAsync("lostonomicron.gx", CreationCollisionOption.ReplaceExisting);
            SaveProject(file);
            //Thread.Sleep(5000);
#endif
        }

        /// <summary>
        /// Lädt das Projekt
        /// </summary>
        /// <param name="file">Der Dateiname inklusive Pfad</param>
        public void LoadProject(StorageFile file)
        {
            Project?.LoadAsync(file);

            ProjectFileName = file.Path;
        }

        /// <summary>
        /// Speichert das Projekt
        /// </summary>
        /// <param name="file">Der Dateiname inklusive Pfad</param>
        public void SaveProject(StorageFile file)
        {
            Project?.SaveAsync(file);

            ProjectFileName = file.Path;
        }

        /// <summary>
        /// Liefert oder setzt das Project
        /// </summary>
        private Project Project { get; set; } = new Project();

        /// <summary>
        /// Liefert oder setzt den Projektbaum
        /// </summary>
        public ObservableCollection<ItemTreeNode> Tree { get { return Project.Tree; } }

        /// <summary>
        /// Liefert oder setzt den Dateinamen des Projektes
        /// </summary>
        public string ProjectFileName { get; private set; }

    }
}
