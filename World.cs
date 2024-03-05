using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public struct CraftRecipe
    {
        public List<ItemStack> RequiredItems;
        public ItemStack ResultItems;

        public CraftRecipe(List<ItemStack> requiredItems, ItemStack resultItems)
        {
            RequiredItems = requiredItems;
            ResultItems = resultItems;
        }
    }
    public class Furnace
    {
        public Inventory SmeltingInventory;
        public Inventory FuelInventory;
        public Inventory OutputInventory;

        private FurnaceOccupation f;
        public int FurnaceProgress => f.FurnaceProgress;
        public int FurnaceFuelUsed => f.FurnaceFuelUsed;
        public int FurnaceFuelMax => f.FurnaceFuelMax;

        public Furnace(Inventory smeltingInventory, Inventory fuelInventory, Inventory outputInventory, FurnaceOccupation f)
        {
            SmeltingInventory = smeltingInventory;
            FuelInventory = fuelInventory;
            OutputInventory = outputInventory;
            this.f = f;
        }
    }

    public class DirtReactor
    {
        public Inventory CoreInventory;
        public Inventory DirtInventory;
        public Inventory OutputInventory;

        public DirtReactor(Inventory coreInventory, Inventory dirtInventory, Inventory outputInventory)
        {
            CoreInventory = coreInventory;
            DirtInventory = dirtInventory;
            OutputInventory = outputInventory;
        }
    }
    public static class World
    {
        public static List<Tile> WorldSet = new List<Tile>();
        public static int[,] UndergroundMap = new int[15,9];
        public static List<TileCoordinates> LandFilledTiles = new List<TileCoordinates>();
        public static TileCoordinates NetheriteCoordinates;
        public const int XSize = 15;
        public const int YSize = 9;
        public static int TimesUsedFinale = 0;
        public static List<Control> ControlList = new List<Control>();

        public static Tile FindTile(TileCoordinates tc)
        {
            return WorldSet.Find(t => t.X == tc.X && t.Y == tc.Y);
        }

        public static Tile FindTile(Control ab)
        {
            return WorldSet.Find(t => t.AssociatedButton == ab);
        }

        public static bool OpenedChest = false;
        public static bool OpenedCoolChest = false;
        public static bool OpenedFurnace = false;
        public static bool OpenedCraftingTable = false;
        public static bool OpenedCoolCraftingTable = false;
        public static bool OpenedDirtReactor = false;
    }

    public static class Const
    {
        public const int StackAmount = 64;
        public static Image[] ItemIDToImage =
        {
            null,
            Properties.Resources.cobblestone,
            Properties.Resources.woodlog,
            Properties.Resources.stone,
            Properties.Resources.charcoal,
            Properties.Resources.coal,
            Properties.Resources.plank,
            Properties.Resources.woodenpickaxe,
            Properties.Resources.stick,
            Properties.Resources.ironore,
            Properties.Resources.ironingot,
            Properties.Resources.craftingtable,
            Properties.Resources.chest,
            Properties.Resources.stonepickaxe,
            Properties.Resources.furnacedim,
            Properties.Resources.woodenshovel,
            Properties.Resources.dirt,
            Properties.Resources.goldore,
            Properties.Resources.diamond,
            Properties.Resources.ironpickaxe,
            Properties.Resources.bedrock,
            Properties.Resources.diamondpickaxe,
            Properties.Resources.stick,
            Properties.Resources.goldingot,
            Properties.Resources.stoneshovel,
            Properties.Resources.ironshovel,
            Properties.Resources.goldenshovel,
            Properties.Resources.diamondshovel,
            Properties.Resources.goldenpickaxe,
            Properties.Resources.woodenaxe,
            Properties.Resources.stoneaxe,
            Properties.Resources.ironaxe,
            Properties.Resources.goldenaxe,
            Properties.Resources.diamondaxe,
            Properties.Resources.coolbench,
            Properties.Resources.map,
            Properties.Resources.netheritescrap,
            Properties.Resources.netheriteingot,

            Properties.Resources.darkgrayorb,
            Properties.Resources.grayorb,
            Properties.Resources.blackorb,
            Properties.Resources.brownorb,
            Properties.Resources.yelloworb,
            Properties.Resources.blueorb,

            Properties.Resources.hemispherea,
            Properties.Resources.hemisphereb,
            Properties.Resources.perforator,
            Properties.Resources.activator,

            Properties.Resources.final,

            Properties.Resources.grass,
            Properties.Resources.emerald,
            Properties.Resources.enderchest,
            Properties.Resources.dirtreactor

        };

        public static Image[] OccupationIDToImage =
        {
            Properties.Resources.grass,
            Properties.Resources.stone,
            Properties.Resources.woodlog,
            Properties.Resources.water,
            Properties.Resources.chest,
            Properties.Resources.furnacedim,
            Properties.Resources.craftingtable,
            Properties.Resources.plank,
            Properties.Resources.ironore,
            Properties.Resources.coalore,
            Properties.Resources.dirt,
            Properties.Resources.goldore,
            Properties.Resources.diamondore,
            Properties.Resources.bedrock,
            Properties.Resources.coolbench,
            Properties.Resources.netherite,
            Properties.Resources.enderchest,
            Properties.Resources.dirtreactor
        };

        public static Item[] FurnaceRecipes =
        {
            Item.None, Item.Stone, Item.Charcoal, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.IronIngot, Item.None, Item.None, Item.None, Item.None, Item.None, 
            Item.None, Item.None, Item.GoldIngot, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None, Item.None,
            Item.None, Item.None, Item.None, Item.None, Item.None, Item.NetheriteIngot, Item.None
        };
        public static int[] FurnaceDurability =
        {
            0, 0, 10, 0, 35, 35, 0, 0, 0, 0
        };

        public static CraftRecipe[] CraftingRecipes =
        {

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Wood, 2)
                },
                new ItemStack(Item.Plank, 5)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 2)
                },
                new ItemStack(Item.Stick, 4)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 4)
                },
                new ItemStack(Item.CraftingTable, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 8)
                },
                new ItemStack(Item.Chest, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Cobblestone, 12)
                },
                new ItemStack(Item.Furnace, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 5),
                    new ItemStack(Item.Stick, 7)
                },
                new ItemStack(Item.WoodenPickaxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Cobblestone, 7),
                    new ItemStack(Item.Stick, 9)
                },
                new ItemStack(Item.StonePickaxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.IronIngot, 10),
                    new ItemStack(Item.Stick, 15)
                },
                new ItemStack(Item.IronPickaxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.GoldIngot, 10),
                    new ItemStack(Item.Stick, 15)
                },
                new ItemStack(Item.GoldenPickaxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Diamond, 12),
                    new ItemStack(Item.Stick, 20)
                },
                new ItemStack(Item.DiamondPickaxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 5),
                    new ItemStack(Item.Stick, 7)
                },
                new ItemStack(Item.WoodenShovel, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Cobblestone, 7),
                    new ItemStack(Item.Stick, 9)
                },
                new ItemStack(Item.StoneShovel, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.IronIngot, 10),
                    new ItemStack(Item.Stick, 15)
                },
                new ItemStack(Item.IronShovel, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.GoldIngot, 10),
                    new ItemStack(Item.Stick, 15)
                },
                new ItemStack(Item.GoldenShovel, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Diamond, 10),
                    new ItemStack(Item.Stick, 20)
                },
                new ItemStack(Item.DiamondShovel, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 5),
                    new ItemStack(Item.Stick, 7)
                },
                new ItemStack(Item.WoodenAxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Cobblestone, 7),
                    new ItemStack(Item.Stick, 9)
                },
                new ItemStack(Item.StoneAxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.IronIngot, 10),
                    new ItemStack(Item.Stick, 15)
                },
                new ItemStack(Item.IronAxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.GoldIngot, 10),
                    new ItemStack(Item.Stick, 15)
                },
                new ItemStack(Item.GoldenAxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Diamond, 10),
                    new ItemStack(Item.Stick, 20)
                },
                new ItemStack(Item.DiamondAxe, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Diamond, 10),
                    new ItemStack(Item.Stone, 20),
                    new ItemStack(Item.GoldIngot, 10),
                    new ItemStack(Item.CraftingTable, 1)
                },
                new ItemStack(Item.CoolCraftingTable, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Dirt, 10),
                    new ItemStack(Item.Cobblestone, 15)
                },
                new ItemStack(Item.Landfill, 3)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Plank, 20),
                    new ItemStack(Item.Diamond, 1),
                    new ItemStack(Item.GoldIngot, 1)
                },
                new ItemStack(Item.Locator, 1)),

            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Bedrock, 1)
                },
                new ItemStack(Item.Bedrock, 2)),



            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Wood, 100)
                },
                new ItemStack(Item.WoodCore, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Stone, 100)
                },
                new ItemStack(Item.StoneCore, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.IronIngot, 100)
                },
                new ItemStack(Item.IronCore, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Coal, 100)
                },
                new ItemStack(Item.CoalCore, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.GoldIngot, 100)
                },
                new ItemStack(Item.GoldCore, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Diamond, 100)
                },
                new ItemStack(Item.DiamondCore, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.WoodCore, 1),
                    new ItemStack(Item.StoneCore, 1),
                    new ItemStack(Item.GoldCore, 1),
                    new ItemStack(Item.NetheriteIngot, 15)
                },
                new ItemStack(Item.HemisphereA, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.CoalCore, 1),
                    new ItemStack(Item.IronCore, 1),
                    new ItemStack(Item.DiamondCore, 1),
                    new ItemStack(Item.NetheriteIngot, 15)
                },
                new ItemStack(Item.HemisphereB, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.DiamondAxe, 1),
                    new ItemStack(Item.DiamondPickaxe, 1),
                    new ItemStack(Item.DiamondShovel, 1),
                    new ItemStack(Item.NetheriteIngot, 15),
                },
                new ItemStack(Item.Perforator, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.IronIngot, 15),
                    new ItemStack(Item.GoldIngot, 15),
                    new ItemStack(Item.Diamond, 15),
                    new ItemStack(Item.NetheriteIngot, 15),
                },
                new ItemStack(Item.Activator, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.HemisphereA, 1),
                    new ItemStack(Item.HemisphereB, 1),
                    new ItemStack(Item.Perforator, 1),
                    new ItemStack(Item.Activator, 1),
                },
                new ItemStack(Item.TheFinale, 1)),





            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Emerald, 8),
                    new ItemStack(Item.Dirt, 20),
                    new ItemStack(Item.Stone, 20),
                    new ItemStack(Item.CraftingTable, 1),
                },
                new ItemStack(Item.DirtReactor, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Emerald, 8),
                    new ItemStack(Item.GoldIngot, 5),
                    new ItemStack(Item.Chest, 1),
                },
                new ItemStack(Item.CoolChest, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Emerald, 1)
                },
                new ItemStack(Item.Dirt, 8)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Emerald, 1)
                },
                new ItemStack(Item.GoldIngot, 1)),
            new CraftRecipe( new List<ItemStack>
                {
                    new ItemStack(Item.Emerald, 1)
                },
                new ItemStack(Item.Diamond, 1)),
        };

        public static int[] ItemReceivePoints =
        {
            0, 2, 1, 5, 20, 2, 1, 0, 0, 1, 7, 0, 0, 0, 0, 0, 1, 1, 50, 0, 99999999, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 60, 150, 100, 100, 100, 100, 150, 500, 500, 100, 50, 10000, 0
        };
    }
}
