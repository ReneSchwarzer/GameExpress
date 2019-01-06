using GameExpress.Core.Items;
using GameExpress.Editor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor
{
    public static class EditorContext
    {
        public static ItemPage CreatePage(IItem item)
        {
            if (item is ItemDirectory)
            {
                return new ItemDirectoryPage(item) { Title = item.Name, Image = item.Context.Image };
            }
            else if (item is ItemVisualGeometry)
            {
                return new ItemGeometryPage(item) { Title = item.Name, Image = item.Context.Image };
            }
            else if (item is ItemVisualObject)
            {
                return new ItemObjectPage(item) { Title = item.Name, Image = item.Context.Image };
            }
            else if (item is ItemVisualAnimatedObjectState)
            {
                return new ItemObjectStatePage(item) { Title = item.Name, Image = item.Context.Image };
            }
            else if (item is ItemVisualImage)
            {
                return new ItemImagePage(item) { Title = item.Name, Image = item.Context.Image };
            }
            else if (item is ItemVisualScene)
            {
                return new ItemScenePage(item) { Title = item.Name, Image = item.Context.Image };
            }
            else if (item is ItemMap)
            {
                return new ItemMapPage(item) { Title = item.Name, Image = item.Context.Image };
            }

            return null;
        }
    }
}
