using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class DirtReactorOccupation : IOccupation
    {
        public OccupationType Type => OccupationType.DirtReactor;

        public TileCoordinates sourceCoordinates { get; private set; }
        public bool IsUnderground { get; set; } = false;

        public Inventory CoreInventory { get; set; } = new Inventory(1, AccessLevel.Low, new Item[] { Item.CoalCore, Item.DiamondCore, Item.GoldCore, Item.IronCore, Item.StoneCore, Item.WoodCore });
        public Inventory DirtInventory { get; set; } = new Inventory(1, AccessLevel.Low, new Item[] { Item.Dirt });
        public Inventory OutputInventory { get; set; } = new Inventory(1, AccessLevel.High);


        public void OpenDirtReactor(object s, EventArgs e)
        {
            //Player.SelectedInventory = ChestInventory;
            World.OpenedChest = false;
            World.OpenedFurnace = false;
            World.OpenedCraftingTable = false;
            World.OpenedCoolCraftingTable = false;

            Player.SelectedFurnace = null;
            Player.SelectedInventory = null;
            Player.SelectedDirtReactor = new DirtReactor(CoreInventory, DirtInventory, OutputInventory);
            World.OpenedCoolChest = false;
            World.OpenedDirtReactor = true;
        }


        


        public void Load(Control ab)
        {
            //Reset(ab);
            ab.Click += OpenDirtReactor;
        }

        public void Reset(Control ab)
        {
            ab.Click -= OpenDirtReactor;

            Player.SelectedFurnace = null;
            Player.SelectedInventory = null;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
