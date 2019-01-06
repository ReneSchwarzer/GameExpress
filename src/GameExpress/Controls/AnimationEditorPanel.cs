using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;

namespace GameExpress.Controls
{
    /// <summary>
    /// Editor für animierte Objekte
    /// </summary>
    public class AnimationEditorPanel : EditorPanel 
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public AnimationEditorPanel()
        {
            DefaultStyleKey = typeof(EditorPanel);
        }

        /// <summary>
        /// Wird beim Anwenden des Templates (EditorPanel.xaml-Datei) aufgerufen
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Time = 0;

            // Eigenschaft GridVisibilityProperty hat sich geändert
            RegisterPropertyChangedCallback(TimeProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Neuzeichnen erforderlich
                Content?.Invalidate();
            }));
        }

        /// <summary>
        /// Erstellt die Ressourcen
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnCreateResources(CanvasCreateResourcesEventArgs args)
        {
            Parallel.Invoke(() => { Item.CreateResources(Content); });
        }

        /// <summary>
        /// Zeichnet das Item
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnDrawContent(CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out bool infinty);

            // Zeichne Hintergrund
            OnDrawBackground(args);

            Item.Update(new UpdateContext()
            {
                Designer = true,
                Time = new Time(Time)

            });

            Item.Presentation(new PresentationContext(args.DrawingSession)
            {
                Designer = true,
                Time = new Time(Time),
                Matrix = Matrix3D.Identity * Matrix3D.Translation(viewRect.Left, viewRect.Top) * Matrix3D.Scaling(Zoom / 100f, Zoom / 100f)
            });
        }

        /// <summary>
        /// Liefert oder setzt die Animationszeit
        /// </summary>
        public ulong Time
        {
            get { return (ulong)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("TimeProperty", typeof(ulong), typeof(AnimationEditorPanel), new PropertyMetadata(0));

    }
}
