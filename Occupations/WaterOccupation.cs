using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class WaterOccupation : IOccupation
    {
        public OccupationType Type => OccupationType.Water;

        public TileCoordinates sourceCoordinates { get; private set; }
        public bool IsUnderground { get; set; }

        public void Load(Control ab)
        {
            ab.Text = "";
        }

        public void Reset(Control ab)
        {

        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
