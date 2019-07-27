using GameExpress.Model.Structs;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnet ein Item, dass es aus Klicks reagiert
    /// </summary>
    public interface IItemClickable
    {
        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        Item HitTest(HitTestContext hc, Vector point);
    }
}
