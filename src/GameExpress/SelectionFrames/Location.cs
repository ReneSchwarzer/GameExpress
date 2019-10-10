using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.SelectionFrames
{
    public enum Location
    {
        None, // Ohne feste Position
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
        /// <summary>
        /// Verwendet den HotSpot als Position
        /// </summary>
        HotSpot,
        /// <summary>
        /// Der Schwerpunkt
        /// </summary>
        Center,
        /// <summary>
        /// Vom Eigentümer defenierte Position
        /// </summary>
        Owner
    }
}
