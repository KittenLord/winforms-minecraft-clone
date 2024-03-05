using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class PlankOccupation : Breakable, IOccupation
    {
        public PlankOccupation(TileCoordinates sourceCoordinates, ItemStack drop) : base(sourceCoordinates, drop)
        {
        }

        public OccupationType Type => OccupationType.Plank;

        public new TileCoordinates sourceCoordinates { get; private set; }
        public bool IsUnderground { get; set; }

        public void Load(Control ab)
        {
            Reset(ab);
            ab.Click += Break;
        }

        public void Reset(Control ab)
        {
            ab.Click -= Break;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
