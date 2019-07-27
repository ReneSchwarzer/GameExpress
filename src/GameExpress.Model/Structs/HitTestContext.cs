namespace GameExpress.Model.Structs
{
    public class HitTestContext : UpdateContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HitTestContext()
        {
            Time = new Structs.Time();
            Level = 1;
        }

        /// <summary>
        /// Kopier - Konstruktor
        /// </summary>
        /// <param name="hc">Der HitTest-Kontext</param>
        public HitTestContext(HitTestContext hc)
            : this()
        {
            Designer = hc.Designer;
            Level = hc.Level + 1;
            Time = hc.Time;
            Matrix = hc.Matrix;
        }
    }
}
