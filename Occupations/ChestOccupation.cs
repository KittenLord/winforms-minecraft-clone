using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class ChestOccupation : IOccupation
    {
        public OccupationType Type => OccupationType.Chest;

        public TileCoordinates sourceCoordinates { get; private set; }

        public bool IsUnderground { get; set; } = false;



        public Inventory ChestInventory { get; set; } = new Inventory(3, AccessLevel.Low);


        public void OpenChest(object s, EventArgs e)
        {
            Player.SelectedInventory = ChestInventory;
            Player.SelectedFurnace = null;
            Player.SelectedDirtReactor = null;
            World.OpenedChest = true;
            World.OpenedFurnace = false;
            World.OpenedCraftingTable = false;
            World.OpenedCoolCraftingTable = false;
            World.OpenedCoolChest = false;
            World.OpenedDirtReactor = false;
        }

        public void Load(Control ab)
        {
            ab.Click += OpenChest;
        }

        public void Reset(Control ab)
        {
            ab.Click -= OpenChest;
            if (Player.SelectedInventory == ChestInventory) Player.SelectedInventory = null;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
