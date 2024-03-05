using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class CoalOreOccupation : Mineable, IOccupation
    {
        public CoalOreOccupation(int resourceCount, TileCoordinates sourceCoordinates, Item resource, List<Item> toolFilter) : base(resourceCount, sourceCoordinates, resource, toolFilter)
        {
        }

        public OccupationType Type => OccupationType.CoalOre;
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

            if (IsUnderground && ResourceCount <= 0)
            {
                World.FindTile(ab).ForceSetOccupation(new BedrockOccupation(), true);
            }
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
            base.sourceCoordinates = tc;
        }
    }
}
