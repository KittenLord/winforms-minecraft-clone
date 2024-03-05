using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class IronOreOccupation : Mineable, IOccupation
    {
        public OccupationType Type => OccupationType.IronOre;
        public IronOreOccupation(int resourceCount, TileCoordinates sourceCoordinates, Item resource) : base(resourceCount, sourceCoordinates, resource)
        {
        }
        public IronOreOccupation(int resourceCount, TileCoordinates sourceCoordinates, Item resource, List<Item> filter) : base(resourceCount, sourceCoordinates, resource, filter)
        {
        }
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
