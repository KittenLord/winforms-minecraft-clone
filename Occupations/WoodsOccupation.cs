using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class WoodsOccupation : Mineable, IOccupation
    {
        public WoodsOccupation(int resourceCount, TileCoordinates sourceCoordinates, Item resource) : base(resourceCount, sourceCoordinates, resource)
        {
        }

        public WoodsOccupation(int resourceCount, TileCoordinates sourceCoordinates, Item resource, List<Item> filter) : base(resourceCount, sourceCoordinates, resource, filter)
        {
        }


        public OccupationType Type => OccupationType.Woods;
        public bool IsUnderground { get; set; }

        public new TileCoordinates sourceCoordinates { get; private set; }

        public void Load(Control ab)
        {
            Reset(ab);
            ab.Text = ResourceCount.ToString();

            ab.Click += Mine;
        }

        public void Reset(Control ab)
        {
            ab.Click -= Mine;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
