
using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace GameExpress.Model
{
    /// <summary>
    /// Ein Spieleprojet, welches sämtliche Daten enthällt
    /// </summary>
    public class Project : INotifyPropertyChanged
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich eine Eigenschaften geändert hat
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Liefert oder setzt den Projektbaum
        /// </summary>
        public ObservableCollection<ItemTreeNode> Tree { get; private set; } = new ObservableCollection<ItemTreeNode>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Project()
        {
        }

        /// <summary>
        /// Lädt das Projekt
        /// </summary>
        /// <param name="file">Die Datei, aus dem die Daten lelesen werden sollen</param>
        public async void LoadAsync(StorageFile file)
        {
            var serializer = new XmlSerializer(typeof(ItemGame));

            using (var stream = await file.OpenStreamForReadAsync())
            {
                var game = serializer.Deserialize(stream) as ItemGame;
                Tree.Clear();
                Tree.Add(game);
            }
        }

        /// <summary>
        /// Speichert das Projekt
        /// </summary>
        /// <param name="file">Die Datei, indem die Daten geschrieben werden sollen</param>
        public async void SaveAsync(StorageFile file)
        {
            var serializer = new XmlSerializer(typeof(ItemGame));

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                serializer.Serialize(stream, Tree.FirstOrDefault() as ItemGame);
            }
        }

        /// <summary>, 
        /// Löst das PropertyChanged-Event aus
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
