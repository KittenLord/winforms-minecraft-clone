using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{


    public class FurnaceOccupation : IOccupation
    {
        public OccupationType Type => OccupationType.Furnace;

        public TileCoordinates sourceCoordinates { get; private set; }
        public bool IsUnderground { get; set; } = false;

        public Inventory SmeltingInventory { get; set; } = new Inventory(1, AccessLevel.Low);
        public Inventory FuelInventory { get; set; } = new Inventory(1, AccessLevel.Low, new Item[] { Item.Wood, Item.Charcoal, Item.Coal });
        public Inventory OutputInventory { get; set; } = new Inventory(1, AccessLevel.High);
        public int FurnaceProgress { get; set; } = 0;
        public int FurnaceFuelUsed { get; set; } = 0;
        public int FurnaceFuelMax { get; set; } = 0;


        public void OpenFurnace(object s, EventArgs e)
        {
            //Player.SelectedInventory = ChestInventory;
            World.OpenedChest = false;
            World.OpenedFurnace = true;
            World.OpenedCraftingTable = false;
            World.OpenedCoolCraftingTable = false;

            Player.SelectedFurnace = new Furnace(SmeltingInventory, FuelInventory, OutputInventory, this);
            Player.SelectedInventory = null;
            Player.SelectedDirtReactor = null;
            World.OpenedCoolChest = false;
            World.OpenedDirtReactor = false;
        }


        public async void FurnaceCycle(Control ab)
        {
            while (true)
            {
                await Task.Delay(30);
                if (World.FindTile(ab).Occupation.Type != OccupationType.Furnace)
                    return;
                ab.BackgroundImage = Properties.Resources.furnacedim;
                await Task.Delay(50);
                if(FuelInventory.GetSlot(0).Count > 0 && Const.FurnaceRecipes[(int)SmeltingInventory.GetSlot(0).Item] != Item.None)
                {
                    bool hasFuel = FuelInventory.GetSlot(0).Count > 0;
                    bool hasItem = SmeltingInventory.GetSlot(0).Count > 0;

                    ab.BackgroundImage = Properties.Resources.furnacelit;

                    var fuel = FuelInventory.GetSlot(0).Item;
                    var energy = 0;

                    do
                    {
                        if (energy <= 0)
                        {
                            fuel = FuelInventory.GetSlot(0).Item;
                            FuelInventory.RemoveItem(FuelInventory.GetSlot(0).Single);
                            energy += Const.FurnaceDurability[(int)fuel];
                        }
                        var smeltingItem = SmeltingInventory.GetSlot(0).Item;

                        if(FurnaceProgress == 0)
                            SmeltingInventory.RemoveItem(SmeltingInventory.GetSlot(0).Single);

                        if(fuel != 0)
                            FurnaceFuelMax = Const.FurnaceDurability[(int)fuel];
                        FurnaceFuelUsed = energy;

                        while (energy > 0)
                        {
                            bool breaking = false;
                            energy--;
                            FurnaceFuelUsed = energy;
                            FurnaceProgress += 20;
                            await Task.Delay(1100);
                            if (FurnaceProgress == 120)
                            {
                                OutputInventory.InsertItem(new ItemStack(Const.FurnaceRecipes[(int)smeltingItem], 1));
                                FurnaceProgress = 0;
                                hasItem = SmeltingInventory.GetSlot(0).Count > 0;
                                if (!hasItem) breaking = true;
                                break;
                            }
                            if (breaking)
                            {
                                break;
                            }
                        }
                        hasFuel = FuelInventory.GetSlot(0).Count > 0 || energy > 0;
                        hasItem = SmeltingInventory.GetSlot(0).Count > 0;
                    } while (hasFuel && hasItem);

                    FurnaceProgress = 0;
                    FurnaceFuelUsed = 0;
                }
            }
        }


        public void Load(Control ab)
        {
            //Reset(ab);
            ab.Click += OpenFurnace;

            FurnaceCycle(ab);
        }

        public void Reset(Control ab)
        {
            ab.Click -= OpenFurnace;

            Player.SelectedFurnace = null;
            Player.SelectedInventory = null;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
