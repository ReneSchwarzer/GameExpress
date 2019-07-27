using Windows.Foundation;

namespace GameExpress.Model.Structs
{
    public interface IContext
    {
        /// <summary>
        /// Der Kontext wird im Designer ausgeführt
        /// </summary>
        bool Designer { get; }

        /// <summary>
        /// Tiefe
        /// </summary>
        int Level { get; }

        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        Time Time { get; }

        /// <summary>
        /// Die 3x3 Matrix
        /// </summary>
        Matrix3D Matrix { get; set; }

        /// <summary>
        /// Transformiert Punkte
        /// </summary>
        /// <param name="points">Array von Points</param>
        void Transform(Point[] points);

        /// <summary>
        /// Transformiert ein Punkt
        /// </summary>
        /// <param name="point">Der zu transformierende Punkt</param>
        Point Transform(Point point);

        /// <summary>
        /// Transformiert ein Punkt
        /// </summary>
        /// <param name="point">Der zu transformierende Punkt</param>
        Vector Transform(Vector point);
    }
}
