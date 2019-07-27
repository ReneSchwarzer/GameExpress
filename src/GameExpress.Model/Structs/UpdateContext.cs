using Windows.Foundation;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Der Kontext, indem ein Update der GameLoop ausgefürt wird
    /// </summary>
    public class UpdateContext : IContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public UpdateContext()
        {
            Time = new Structs.Time();
            Level = 1;
        }

        /// <summary>
        /// Kopier - Konstruktor
        /// </summary>
        /// <param name="uc">Der Update Kontext</param>
        public UpdateContext(UpdateContext uc)
            : this()
        {
            Designer = uc.Designer;
            Level = uc.Level + 1;
            Time = uc.Time;
            Matrix = uc.Matrix;
        }

        /// <summary>
        /// Der Updatekontext wird im Designer ausgeführt
        /// </summary>
        public bool Designer { get; set; }

        /// <summary>
        /// Tiefe
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        public Time Time { get; set; }

        /// <summary>
        /// Die 3x3 Matrix
        /// </summary>
        public Matrix3D Matrix { get; set; } = Matrix3D.Identity;

        /// <summary>
        /// Transformiert Punkte
        /// </summary>
        /// <param name="points">Array von Points</param>
        public void Transform(Point[] points)
        {
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = Matrix.Transform(points[i]);
            }
        }

        /// <summary>
        /// Transformiert ein Punkt
        /// </summary>
        /// <param name="point">Der zu transformierende Punkt</param>
        public Point Transform(Point point)
        {
            return Matrix.Transform(point);
        }

        /// <summary>
        /// Transformiert ein Punkt
        /// </summary>
        /// <param name="point">Der zu transformierende Punkt</param>
        public Vector Transform(Vector point)
        {
            return Matrix.Transform(point);
        }
    }
}
