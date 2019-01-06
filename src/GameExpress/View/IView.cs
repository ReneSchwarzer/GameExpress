using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameExpress.Controller;

namespace GameExpress.View
{
    /// <summary>
    /// View-Schnittstelle
    /// </summary>
    public interface IView<TController> where TController : IController
    {
        /// <summary>
        /// Verknüfpft den Controller mit der View
        /// </summary>
        /// <param name="controller">Der zugehörige Controller</param>
        void SetController(TController controller);
    }
}
