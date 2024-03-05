using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class GrassOccupation : IOccupation
    {
        public OccupationType Type => OccupationType.Grass;
        public TileCoordinates sourceCoordinates { get; set; }
        public bool IsUnderground { get; set; }

        public GrassOccupation()
        {

        }

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
