using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;
using Vector = GameExpress.Model.Structs.Vector;

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
            get => m_source;
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
        public override Vector Size => Image != null ? new Vector(Image.Size.Width, Image.Size.Height) : new Vector();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemImage()
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Entfernen nicht mehr benötigter Ressourcen des Items
        /// </summary>
        public override void Dispose()
        {
            Image?.Dispose();
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            if (!Enable)
            {
                return;
            }

            if (Image == null)
            {
                // Bild laden
                CreateResources(pc.Graphics);
            }

            // Nichts zum zeichnen vorhanden
            if (Image == null)
            {
                return;
            }

            var matrix = new Matrix3x2
            (
                (float)pc.Matrix.M11, (float)pc.Matrix.M12,
                (float)pc.Matrix.M21, (float)pc.Matrix.M22,
                (float)pc.Matrix.M31, (float)pc.Matrix.M32
            );

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
        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public override Matrix3D GetMatrix()
        {
            return Matrix3D.Identity;
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public override Item HitTest(HitTestContext hc, Vector point)
        {
            var invert = hc.Matrix.Invert;
            var p = invert.Transform(point);

            var rect = new Rect(new Point(), Size);

            return rect.Contains(p) ? this : null;
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override Item Copy()
        {
            return Copy<ItemImage>();
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
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
                var source = Source;

                try
                {
                    var file = await StorageFile.GetFileFromPathAsync(source);

                }
                catch/* (FileNotFoundException)*/
                {
                    // Releative Datei
                    source = System.IO.Path.Combine(Project.Path, source);
                }

                return await CanvasBitmap.LoadAsync(g, source);

            }).Result;

            RaisePropertyChanged("Image");
            RaisePropertyChanged("Size");
        }

    }
}
