using GameExpress.Context;
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
            ContextRepository.RegisterContext(typeof(ItemGame), new ItemGameContext());
            ContextRepository.RegisterContext(typeof(ItemScene), new ItemSceneContext());
            ContextRepository.RegisterContext(typeof(ItemObject), new ItemObjectContext());
            ContextRepository.RegisterContext(typeof(ItemAnimation), new ItemAnimationContext());
            ContextRepository.RegisterContext(typeof(ItemMap), new ItemMapContext());
            ContextRepository.RegisterContext(typeof(ItemMapVertext), new ItemMapVertextContext());
            ContextRepository.RegisterContext(typeof(ItemStory), new ItemStoryContext());
            ContextRepository.RegisterContext(typeof(ItemKeyFrame), new ItemKeyFrameContext());
            ContextRepository.RegisterContext(typeof(ItemKeyFrameAct), new ItemKeyFrameContext());
            ContextRepository.RegisterContext(typeof(ItemKeyFrameTweening), new ItemTweeningContext());
            ContextRepository.RegisterContext(typeof(ItemImage), new ItemImageContext());
            ContextRepository.RegisterContext(typeof(ItemSound), new ItemSoundContext());

#if DEBUG
            var appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assets = await appInstalledFolder.GetFolderAsync("Assets");
            var project = await assets.GetFolderAsync("TestProject");
            var file = await project.GetFileAsync("lostonomicron.gx");

            LoadProject(file);
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
        public ObservableCollection<ItemTreeNode> Tree => Project.Tree;

        /// <summary>
        /// Liefert oder setzt den Dateinamen des Projektes
        /// </summary>
        public string ProjectFileName { get; private set; }

    }
}
