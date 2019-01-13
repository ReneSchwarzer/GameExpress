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
            var storageFolder = KnownFolders.DocumentsLibrary;
            var folder = await storageFolder.CreateFolderAsync("Lost On Omicron", CreationCollisionOption.OpenIfExists);

            var file1 = await folder.CreateFileAsync("lostonomicron.gx", CreationCollisionOption.OpenIfExists);
            LoadProject(file1);

            var file = await folder.CreateFileAsync("lostonomicron1.gx", CreationCollisionOption.ReplaceExisting);
            SaveProject(file);
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
        public Project Project { get; private set; } = new Project();

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
