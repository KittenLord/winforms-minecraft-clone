using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public struct TileCoordinates
    {
        public int X;
        public int Y;
        public TileCoordinates(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
    }
    public class Tile
    {
        public Control AssociatedButton;
        public int X { get; private set; }
        public int Y { get; private set; }
        public TileCoordinates TileCoordinates { get; private set; }
        public IOccupation Occupation { get; private set; }

        public bool IsEmpty => Occupation.Type == OccupationType.Grass;

        public Tile(Control ab, int x, int y)
        {
            AssociatedButton = ab;
            X = x;
            Y = y;
            TileCoordinates = new TileCoordinates(x, y);
        }
        public void Load()
        {
            Occupation.Load(AssociatedButton);
            AssociatedButton.BackgroundImage = Const.OccupationIDToImage[(int)Occupation.Type];
        }

        public void Reset()
        {
            Occupation.Reset(AssociatedButton);
            if(!Occupation.IsUnderground)
                SetOccupation(new GrassOccupation());
        }

        public void SetOccupation(IOccupation occupation)
        {
            
            Occupation = occupation;
            Occupation.SetCoordinates(TileCoordinates);
            Load();

        }

        public void SetOccupation(OccupationType occupation, bool isUnderground = false)
        {
            Reset();

            switch (occupation)
            {
                case OccupationType.Grass:
                    Occupation = new GrassOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.Water:
                    Occupation = new WaterOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.Chest:
                    Occupation = new ChestOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.Furnace:
                    Occupation = new FurnaceOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.CraftingTable:
                    Occupation = new CraftingTableOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.Plank:
                    Occupation = new PlankOccupation(TileCoordinates, new ItemStack(Item.Plank, 1));
                    Load();
                    break;
                case OccupationType.CoolCraftingTable:
                    Occupation = new CoolCraftingTableOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.CoolChest:
                    Occupation = new CoolChestOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                case OccupationType.DirtReactor:
                    Occupation = new DirtReactorOccupation();
                    Occupation.SetCoordinates(TileCoordinates);
                    Load();
                    break;
                default:
                    break;
            }
            Occupation.IsUnderground = isUnderground;
        }

        public void SetOccupation(OccupationType occupation, int durability, bool isUnderground = false)
        {
            Reset();

            

            switch (occupation)
            {
                case OccupationType.StoneSource:

                    Occupation = new StoneSourceOccupation(durability, TileCoordinates, Item.Cobblestone, new List<Item> { Item.WoodenPickaxe, Item.StonePickaxe, Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    Load();

                    break;
                case OccupationType.Woods:

                    Occupation = new WoodsOccupation(durability, TileCoordinates, Item.Wood, new List<Item> { Item.None, Item.WoodenAxe, Item.StoneAxe, Item.IronAxe, Item.DiamondAxe, Item.GoldenAxe, Item.Perforator });
                    Load();

                    break;
                case OccupationType.IronOre:

                    Occupation = new IronOreOccupation(durability, TileCoordinates, Item.IronOre, new List<Item> { Item.StonePickaxe, Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    Load();

                    break;
                case OccupationType.CoalOre:

                    Occupation = new CoalOreOccupation(durability, TileCoordinates, Item.Coal, new List<Item> { Item.StonePickaxe, Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    Load();

                    break;
                case OccupationType.Dirt:

                    Occupation = new DirtOccupation(durability, TileCoordinates, Item.Dirt, new List<Item> { Item.WoodenShovel, Item.StoneShovel, Item.IronShovel, Item.DiamondShovel, Item.GoldenShovel, Item.Perforator });
                    Load();

                    break;
                case OccupationType.GoldOre:

                    Occupation = new GoldOreOccupation(durability, TileCoordinates, Item.GoldOre, new List<Item> { Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    Load();

                    break;
                case OccupationType.DiamondOre:

                    Occupation = new GoldOreOccupation(durability, TileCoordinates, Item.Diamond, new List<Item> { Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe });
                    Load();

                    break;
            }

            Occupation.IsUnderground = isUnderground;
        }

        public void ForceSetOccupation(IOccupation occupation, bool underground = false, bool removeAllEvents = false)
        {
            if (removeAllEvents)
            {
                try
                {
                    AssociatedButton.Click -= (Occupation as ChestOccupation).OpenChest;
                }
                catch { }

                try
                {
                    AssociatedButton.Click -= (Occupation as FurnaceOccupation).OpenFurnace;
                }
                catch { }

                //try
                //{
                //    AssociatedButton.Click -= (Occupation as Mineable).Mine;
                //}
                //catch { }
            }
            occupation.IsUnderground = underground;
            Occupation = occupation;
            Occupation.SetCoordinates(TileCoordinates);
            Load();
        }
    }
}
