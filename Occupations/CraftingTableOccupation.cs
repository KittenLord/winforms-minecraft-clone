using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class CraftingTableOccupation : IOccupation
    {
        public OccupationType Type => OccupationType.CraftingTable;

        public TileCoordinates sourceCoordinates { get; private set; }
        public bool IsUnderground { get; set; }

        public void OpenCraftingTable(object s, EventArgs e)
        {
            Player.SelectedInventory = null;
            Player.SelectedFurnace = null;
            Player.SelectedDirtReactor = null;
            World.OpenedChest = false;
            World.OpenedFurnace = false;
            World.OpenedCraftingTable = true;
            World.OpenedCoolCraftingTable = false;
            World.OpenedCoolChest = false;
            World.OpenedDirtReactor = false;

        }

        public void Load(Control ab)
        {
            ab.Click += OpenCraftingTable;
        }

        public void Reset(Control ab)
        {
            ab.Click -= OpenCraftingTable;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
