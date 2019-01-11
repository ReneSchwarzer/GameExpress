using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using System.Xml.Serialization;
using Windows.Foundation;
using System;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;

namespace GameExpress.Model.Item
{
    [XmlType("image")]
    public class ItemImage : ItemGraphics
    {
        /// <summary>
        /// Die Bildquelle
        /// </summary>
        private string m_source = string.Empty;

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        [XmlIgnore]
        public CanvasBitmap Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Bildquelle (Pfad+Dateiname)
        /// </summary>
        [XmlElement("source")]
        public string Source
        {
            get { return m_source; }
            set
            {
                m_source = value;
                Image = null;

                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public override Size Size
        {
            get
            {
                return Image != null ? Image.Size : new Size();
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemImage()
        {
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(PresentationContext pc)
        {
            if (Image == null)
            {
                // Bild laden
                CreateResources(pc.Graphics);
            }

            // Nichts zum zeichnen vorhanden
            if (Image == null) return;

            var matrix = new Matrix3x2
            (
                (float)pc.Matrix.M11, (float)pc.Matrix.M12, 
                (float)pc.Matrix.M21, (float)pc.Matrix.M22,
                (float)pc.Matrix.M31, (float)pc.Matrix.M32
            );

            if (pc.Level > 1)
            {
                matrix.Translation = new Vector2(Hotspot.X * -1, Hotspot.Y * -1);
            }

            var transform = pc.Graphics.Transform;
            pc.Graphics.Transform = matrix;

            // Alphawert umrechnen
            var opacity = Alpha / 255f;

            base.Presentation(pc);

            // Punkte transformieren
            var origin = pc.Transform(new Point());

            var effect = (ICanvasImage)Image;

            // Unschärfe
            if (Blur > 0)
            {
                var blur = new GaussianBlurEffect
                {
                    BlurAmount = Blur / 2f,
                    Source = effect
                };

                effect = blur;
            }

            // Gamma
            if (Gamma > 0)
            {
                var gamma = new GammaTransferEffect()
                {
                    RedAmplitude = 1 + Gamma / 15f,
                    BlueAmplitude = 1 + Gamma / 15f,
                    GreenAmplitude = 1 + Gamma / 15f,
                    Source = effect
                };

                effect = gamma;
            }

            // Effekte anwenden und Blid zeichnen
            pc.Graphics.DrawImage
            (
                effect,
                0, 0,
                new Rect(new Point(), Image.Size),
                opacity,
                CanvasImageInterpolation.Linear
            );

            pc.Graphics.Transform = transform;

            // Hotspot zeichnen
            DrawHotspot(pc);
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemImage;
            copy.Source = Source;

            return copy as T;
        }

        /// <summary>
        /// Lädt das Bild aus der gegebenen Quelle
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public override void CreateResources(ICanvasResourceCreator g)
        {
           Image = Task.Run(async () => 
           {
               return await CanvasBitmap.LoadAsync(g, Source);
           }).Result;
        }
        
    }
}
