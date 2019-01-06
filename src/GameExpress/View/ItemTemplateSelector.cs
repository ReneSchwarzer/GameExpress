using GameExpress.Model.Item;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameExpress.View
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate GameTemplate { get; set; }
        public DataTemplate SceneTemplate { get; set; }
        public DataTemplate ObjectTemplate { get; set; }
        public DataTemplate MapTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate SoundTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item.GetType() == typeof(ItemGame)) return GameTemplate;
            else if (item.GetType() == typeof(ItemScene)) return SceneTemplate;
            else if (item.GetType() == typeof(ItemObject)) return ObjectTemplate;
            else if (item.GetType() == typeof(ItemMap)) return MapTemplate;
            else if (item.GetType() == typeof(ItemImage)) return ImageTemplate;
            else if (item.GetType() == typeof(ItemSound)) return SoundTemplate;

            return DefaultTemplate;
        }
    }
}
