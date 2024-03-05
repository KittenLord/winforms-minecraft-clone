using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace gridproject
{


    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            SetupWorld();

            GenerateWorld();

            ConstantUpdating();

            //Player.PlayerInventory.InsertItem(new ItemStack(Item.CoolCraftingTable, 1));
            //Player.PlayerInventory.InsertItem(new ItemStack(Item.HemisphereA, 64));
            //Player.PlayerInventory.InsertItem(new ItemStack(Item.HemisphereB, 36));
            //Player.PlayerInventory.InsertItem(new ItemStack(Item.Perforator, 64));
            //Player.PlayerInventory.InsertItem(new ItemStack(Item.Locator, 1));
        }

        public void SetupWorld()
        {
            World.ControlList.AddRange(worldHolder.Controls.OfType<Control>());

            for (int x = 0; x < World.XSize; x++)
            {
                for (int y = 0; y < World.YSize; y++)
                {
                    var control = World.ControlList.Find(t => Math.Abs(t.Location.X - x * 38) < 8 && Math.Abs(t.Location.Y - y * 41) < 4);
                    Tile tile = new Tile(control, x, y);
                    World.WorldSet.Add(tile);
                }
            }
        }

        public void GenerateWorld()
        {
            foreach (var tile in World.WorldSet)
            {
                tile.SetOccupation(new GrassOccupation());
            }

            Random r = new Random();

            int forestStartX = r.Next(0, 3);
            int forestEndX = r.Next(12, 15);
            int forestStartY = r.Next(0, 2);
            int forestEndY = r.Next(7, 9);

            for (int x = forestStartX; x < forestEndX; x++)
            {
                for (int y = forestStartY; y < forestEndY; y++)
                {
                    if (r.Next(0, 11) <= 2)
                    {
                        var tile = World.FindTile(new TileCoordinates(x, y));
                        tile.SetOccupation(OccupationType.Woods, r.Next(50, 130));
                    }
                }
            }

            int stoneDepositAX = r.Next(0, 15);
            int stoneDepositAY = r.Next(0, 9);
            int stoneDepositBX = r.Next(0, 15);
            int stoneDepositBY = r.Next(0, 9);
            int ironDepositAX = r.Next(0, 15);
            int ironDepositAY = r.Next(0, 9);
            int ironDepositBX = r.Next(0, 15);
            int ironDepositBY = r.Next(0, 9);
            int coalDepositAX = r.Next(0, 15);
            int coalDepositAY = r.Next(0, 9);
            int waterSpotAX = r.Next(0, 15);
            int waterSpotAY = r.Next(0, 9);

            int diamondSpotAX = r.Next(0, 15);
            int diamondSpotAY = r.Next(0, 9);

            int goldSpotAX = r.Next(0, 15);
            int goldSpotAY = r.Next(0, 9);
            int goldSpotBX = r.Next(0, 15);
            int goldSpotBY = r.Next(0, 9);

            int ironuSpotAX = r.Next(0, 15);
            int ironuSpotAY = r.Next(0, 9);
            int coaluSpotBX = r.Next(0, 15);
            int coaluSpotBY = r.Next(0, 9);

            int[] closeNeighborOffsetX =
            {
                -1, 0, 1, -1, 1, -1, 0, 1
            };
            int[] closeNeighborOffsetY =
            {
                -1, -1, -1, 0, 0, 1, 1, 1
            };
            int[] bigNeighborOffsetX =
            {
                -1, 0, -1, -2, -1, 0, 1, 2, -2, -1, 1, 2, -2, -1, 0, 1, 2, -1, 0, 1
            };
            int[] bigNeighborOffsetY =
            {
                -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2
            };

            GenerateUndergroundSpot(ironuSpotAX, ironuSpotAY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.IronOre, 85, r);
            GenerateUndergroundSpot(coaluSpotBX, coaluSpotBY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.CoalOre, 85, r);

            GenerateUndergroundSpot(goldSpotAX, goldSpotAY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.GoldOre, 85, r);
            GenerateUndergroundSpot(goldSpotBX, goldSpotBY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.GoldOre, 85, r);

            GenerateSpot(stoneDepositAX, stoneDepositAY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.StoneSource, 90, 80, 170, r);
            GenerateUndergroundSpot(stoneDepositAX, stoneDepositAY, closeNeighborOffsetX, closeNeighborOffsetY, OccupationType.DiamondOre, 90, r);

            GenerateSpot(stoneDepositBX, stoneDepositBY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.StoneSource, 90, 80, 170, r);
            GenerateUndergroundSpot(stoneDepositBX, stoneDepositBY, closeNeighborOffsetX, closeNeighborOffsetY, OccupationType.DiamondOre, 90, r);
            GenerateUndergroundSpot(diamondSpotAX, diamondSpotAY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.DiamondOre, 65, r);


            GenerateSpot(ironDepositAX, ironDepositAY, closeNeighborOffsetX, closeNeighborOffsetY, OccupationType.IronOre, 90, 80, 170, r);
            GenerateSpot(ironDepositBX, ironDepositBY, closeNeighborOffsetX, closeNeighborOffsetY, OccupationType.IronOre, 90, 80, 170, r);


            GenerateSpot(waterSpotAX, waterSpotAY, closeNeighborOffsetX, closeNeighborOffsetY, OccupationType.Water, 90, r);


            GenerateSpot(coalDepositAX, coalDepositAY, bigNeighborOffsetX, bigNeighborOffsetY, OccupationType.CoalOre, 90, 70, 100, r);

            World.FindTile(new TileCoordinates(r.Next(0, 15), r.Next(0, 9))).SetOccupation(OccupationType.GoldOre, 45);

            World.NetheriteCoordinates = new TileCoordinates(r.Next(0, 15), r.Next(0, 9));

            World.UndergroundMap[World.NetheriteCoordinates.X, World.NetheriteCoordinates.Y] = 5;
            if (World.FindTile(World.NetheriteCoordinates).Occupation.Type == OccupationType.Water)
                World.FindTile(World.NetheriteCoordinates).SetOccupation(OccupationType.Grass);
        }

        public void GenerateSpot(int xCentre, int yCentre, int[] offsetListX, int[] offsetListY, OccupationType occupation, int percent, int min, int max, Random r)
        {
            World.FindTile(new TileCoordinates(xCentre, yCentre)).SetOccupation(occupation, r.Next(min, max));
            for (int i = 0; i < offsetListX.Length; i++)
            {
                var tile = World.FindTile(new TileCoordinates(xCentre + offsetListX[i], yCentre + offsetListY[i]));
                if (tile == null) continue;
                if (r.Next(0, 100) <= percent)
                {
                    tile.SetOccupation(occupation, r.Next(min, max));
                }
            }
        }
        public void GenerateSpot(int xCentre, int yCentre, int[] offsetListX, int[] offsetListY, OccupationType occupation, int percent, Random r)
        {
            World.FindTile(new TileCoordinates(xCentre, yCentre)).SetOccupation(occupation);
            for (int i = 0; i < offsetListX.Length; i++)
            {
                var tile = World.FindTile(new TileCoordinates(xCentre + offsetListX[i], yCentre + offsetListY[i]));
                if (tile == null) continue;
                if (r.Next(0, 100) <= percent)
                {
                    tile.SetOccupation(occupation);
                }
            }
        }

        public void GenerateUndergroundSpot(int xCentre, int yCentre, int[] offsetListX, int[] offsetListY, OccupationType occupationValue, int percent, Random r)
        {
            int occupation = 0;
            switch (occupationValue)
            {
                case OccupationType.StoneSource:
                    occupation = 0;
                    break;
                case OccupationType.IronOre:
                    occupation = 1;
                    break;
                case OccupationType.CoalOre:
                    occupation = 2;
                    break;
                case OccupationType.GoldOre:
                    occupation = 3;
                    break;
                case OccupationType.DiamondOre:
                    occupation = 4;
                    break;
                default:
                    return;
            }

            World.UndergroundMap[xCentre, yCentre] = occupation;
            for(int i = 0; i < offsetListX.Length; i++)
            {
                int x = xCentre + offsetListX[i];
                int y = yCentre + offsetListY[i];
                if (x < 0 || x >= 15 || y < 0 || y >= 9) continue;

                if(r.Next(0, 100) <= percent)
                {
                    World.UndergroundMap[x, y] = occupation;
                }
            }
        }


        private async void PlayerInteract(object sender, EventArgs e)
        {
            World.OpenedChest = false;

            Item interactiveItem = Player.PlayerInventory.GetSlot(Player.SelectedSlot).Item;

            var tile = World.FindTile((Control)sender);

            //MessageBox.Show(tile.Occupation.ToString());

            if (BreakingBlocksCheck.Checked)
            {
                OccupationType[] breakable =
                {
                    OccupationType.Chest, OccupationType.CraftingTable, OccupationType.Furnace, OccupationType.CoolCraftingTable
                };

                if (breakable.Contains(tile.Occupation.Type))
                {
                    tile.Reset();

                    await Task.Delay(50);
                    tile.Reset();

                    World.OpenedChest = false;
                    World.OpenedCraftingTable = false;
                    World.OpenedCoolCraftingTable = false;
                    World.OpenedFurnace = false;
                    Player.SelectedInventory = null;
                    Player.SelectedFurnace = null;
                }

                return;
            }

            switch (interactiveItem)
            {
                case Item.None:
                    break;
                case Item.Cobblestone:
                    break;
                case Item.Wood:
                    break;
                case Item.Stone:
                    break;
                case Item.Charcoal:
                    break;
                case Item.Coal:
                    break;
                case Item.Plank:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.Plank);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.Plank, 1));
                    }
                    break;
                case Item.WoodenPickaxe:
                    break;
                case Item.Stick:
                    break;
                case Item.IronOre:
                    break;
                case Item.IronIngot:
                    break;
                case Item.CraftingTable:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.CraftingTable);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.CraftingTable, 1));
                    }
                    break;
                case Item.Chest:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.Chest);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.Chest, 1));
                    }
                    break;
                case Item.StonePickaxe:
                    break;
                case Item.Furnace:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.Furnace);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.Furnace, 1));
                    }
                    break;
                case Item.Dirt:
                    break;
                case Item.GoldOre:
                    break;
                case Item.Diamond:
                    break;
                case Item.IronPickaxe:
                    break;
                case Item.Bedrock:
                    break;
                case Item.DiamondPickaxe:
                    break;
                case Item.DebugStick:
                    MessageBox.Show($"Type: {tile.Occupation.Type}\n" +
                                    $"X: {tile.X} Y: {tile.Y}");
                    break;
                case Item.GoldIngot:
                    break;
                case Item.WoodenShovel:
                case Item.StoneShovel:
                case Item.IronShovel:
                case Item.GoldenShovel:
                case Item.DiamondShovel:
                case Item.Perforator:
                    if (tile.IsEmpty && !World.LandFilledTiles.Contains(tile.TileCoordinates))
                    {
                        tile.SetOccupation(OccupationType.Dirt, new Random().Next(13, 30), true);
                    }
                    break;
                case Item.GoldenPickaxe:
                    break;
                case Item.WoodenAxe:
                    break;
                case Item.StoneAxe:
                    break;
                case Item.IronAxe:
                    break;
                case Item.GoldenAxe:
                    break;
                case Item.DiamondAxe:
                    break;
                case Item.CoolCraftingTable:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.CoolCraftingTable);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.CoolCraftingTable, 1));
                    }
                    break;
                case Item.Locator:
                    MessageBox.Show($"Netherite is hidden at:\n" +
                                    $"X: {World.NetheriteCoordinates.X} Y: {World.NetheriteCoordinates.Y}\n" +
                                    $"(Counting from top left corner)", "Locator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Player.PlayerInventory.RemoveItem(new ItemStack(Item.Locator, 1));
                    break;
                case Item.NetheriteScrap:
                    break;
                case Item.NetheriteIngot:
                    break;
                case Item.StoneCore:
                    break;
                case Item.IronCore:
                    break;
                case Item.CoalCore:
                    break;
                case Item.WoodCore:
                    break;
                case Item.GoldCore:
                    break;
                case Item.DiamondCore:
                    break;
                case Item.HemisphereA:
                    break;
                case Item.HemisphereB:
                    break;
                case Item.Activator:
                    break;
                case Item.TheFinale:
                    Player.PlayerInventory.RemoveItem(new ItemStack(Item.TheFinale, 1));

                    World.TimesUsedFinale++;
                    finaleUses.Text = $"The Finale uses: {World.TimesUsedFinale}";

                    var result = MessageBox.Show($"Congrats! You've beaten this abomination of a game!\n\n" +
                                                 $"Yes - Destroy your world\n" +
                                                 $"No - Continue playing", "Congratulations", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        EndGame();
                    }
                    else
                    {
                        Player.PlayerInventory.InsertItem(new ItemStack(Item.Emerald, 64));
                        if(World.TimesUsedFinale == 1)
                        {
                            coolCraftList.Items.Add("Dirt Reactor");
                            coolCraftList.Items.Add("Cool Chest");
                            coolCraftList.Items.Add("Dirt (8)");
                            coolCraftList.Items.Add("Gold");
                            coolCraftList.Items.Add("Diamond");
                        }
                    }

                    World.FindTile(World.NetheriteCoordinates).ForceSetOccupation(new NetheriteOccupation(100, World.NetheriteCoordinates, Item.NetheriteScrap, new List<Item> { Item.DiamondPickaxe, Item.Perforator }), true, true);

                    break;
                case Item.Landfill:
                    if (tile.Occupation.Type == OccupationType.Bedrock || tile.Occupation.Type == OccupationType.Water)
                    {
                        tile.SetOccupation(OccupationType.Grass);
                        World.LandFilledTiles.Add(tile.TileCoordinates);
                    }
                    break;
                case Item.Emerald:
                    break;
                case Item.CoolChest:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.CoolChest);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.CoolChest, 1));
                    }
                    break;
                case Item.DirtReactor:
                    if (tile.IsEmpty)
                    {
                        tile.SetOccupation(OccupationType.DirtReactor);
                        Player.PlayerInventory.RemoveItem(new ItemStack(Item.DirtReactor, 1));
                    }
                    break;
            }
        }

        public async void EndGame()
        {
            foreach (var tile in World.ControlList)
            {
                tile.Visible = false;
                tile.Enabled = false;
                await Task.Delay(15);
            }

            World.OpenedCoolCraftingTable = false;
            World.OpenedFurnace = false;
            World.OpenedCraftingTable = false;
            World.OpenedChest = false;

            MessageBox.Show("Boom", "Boom", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public async void ConstantUpdating()
        {
            slotSelection1.Visible = true;
            slotSelection2.Visible = false;
            slotSelection3.Visible = false;
            slotSelection4.Visible = false;
            slotSelection5.Visible = false;

            while (true)
            {
                await Task.Delay(100);
                UpdateInventory();
                UpdateChest();
                UpdateFurnace();
                UpdateCraftingTable();
                UpdateCoolCraftingTable();
                UpdateCoolChest();
                UpdateReactor();
                pointsLabel.Text = $"Points: {Player.PointsCount}";
            }
        }

        #region Updaters
        public void UpdateInventory()
        {
            InventorySlot1.BackgroundImage = Const.ItemIDToImage[(int)Player.PlayerInventory.GetSlot(0).Item];
            InventorySlot1.Text = Player.PlayerInventory.GetSlot(0).Count.ToString();
            if (InventorySlot1.Text == "0") InventorySlot1.Text = "";

            InventorySlot2.BackgroundImage = Const.ItemIDToImage[(int)Player.PlayerInventory.GetSlot(1).Item];
            InventorySlot2.Text = Player.PlayerInventory.GetSlot(1).Count.ToString();
            if (InventorySlot2.Text == "0") InventorySlot2.Text = "";

            InventorySlot3.BackgroundImage = Const.ItemIDToImage[(int)Player.PlayerInventory.GetSlot(2).Item];
            InventorySlot3.Text = Player.PlayerInventory.GetSlot(2).Count.ToString();
            if (InventorySlot3.Text == "0") InventorySlot3.Text = "";

            InventorySlot4.BackgroundImage = Const.ItemIDToImage[(int)Player.PlayerInventory.GetSlot(3).Item];
            InventorySlot4.Text = Player.PlayerInventory.GetSlot(3).Count.ToString();
            if (InventorySlot4.Text == "0") InventorySlot4.Text = "";

            InventorySlot5.BackgroundImage = Const.ItemIDToImage[(int)Player.PlayerInventory.GetSlot(4).Item];
            InventorySlot5.Text = Player.PlayerInventory.GetSlot(4).Count.ToString();
            if (InventorySlot5.Text == "0") InventorySlot5.Text = "";
        }
        public void UpdateChest()
        {
            chestHolder.Visible = World.OpenedChest;
            chestHolder.Enabled = World.OpenedChest;

            if (World.OpenedChest)
            {
                ChestSlot1.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(0).Item];
                ChestSlot1.Text = Player.SelectedInventory.GetSlot(0).Count.ToString();
                if (ChestSlot1.Text == "0") ChestSlot1.Text = "";

                ChestSlot2.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(1).Item];
                ChestSlot2.Text = Player.SelectedInventory.GetSlot(1).Count.ToString();
                if (ChestSlot2.Text == "0") ChestSlot2.Text = "";

                ChestSlot3.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(2).Item];
                ChestSlot3.Text = Player.SelectedInventory.GetSlot(2).Count.ToString();
                if (ChestSlot3.Text == "0") ChestSlot3.Text = "";
            }
        }
        public void UpdateCoolChest()
        {
            coolChestPanel.Visible = World.OpenedCoolChest;
            coolChestPanel.Enabled = World.OpenedCoolChest;

            if (World.OpenedCoolChest)
            {
                coolChestSlot1.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(0).Item];
                coolChestSlot1.Text = Player.SelectedInventory.GetSlot(0).Count.ToString();
                if (coolChestSlot1.Text == "0") coolChestSlot1.Text = "";

                coolChestSlot2.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(1).Item];
                coolChestSlot2.Text = Player.SelectedInventory.GetSlot(1).Count.ToString();
                if (coolChestSlot2.Text == "0") coolChestSlot2.Text = "";

                coolChestSlot3.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(2).Item];
                coolChestSlot3.Text = Player.SelectedInventory.GetSlot(2).Count.ToString();
                if (coolChestSlot3.Text == "0") coolChestSlot3.Text = "";

                coolChestSlot4.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(3).Item];
                coolChestSlot4.Text = Player.SelectedInventory.GetSlot(3).Count.ToString();
                if (coolChestSlot4.Text == "0") coolChestSlot4.Text = "";

                coolChestSlot5.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedInventory.GetSlot(4).Item];
                coolChestSlot5.Text = Player.SelectedInventory.GetSlot(4).Count.ToString();
                if (coolChestSlot5.Text == "0") coolChestSlot5.Text = "";
            }
        }
        public void UpdateFurnace()
        {
            FurnaceHolder.Visible = World.OpenedFurnace;
            FurnaceHolder.Enabled = World.OpenedFurnace;

            if (World.OpenedFurnace)
            {
                FurnaceSmeltingSlot.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedFurnace.SmeltingInventory.GetSlot(0).Item];
                FurnaceSmeltingSlot.Text = Player.SelectedFurnace.SmeltingInventory.GetSlot(0).Count.ToString();
                if (FurnaceSmeltingSlot.Text == "0") FurnaceSmeltingSlot.Text = "";

                FurnaceFuelSlot.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedFurnace.FuelInventory.GetSlot(0).Item];
                FurnaceFuelSlot.Text = Player.SelectedFurnace.FuelInventory.GetSlot(0).Count.ToString();
                if (FurnaceFuelSlot.Text == "0") FurnaceFuelSlot.Text = "";

                FurnaceOutputSlot.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedFurnace.OutputInventory.GetSlot(0).Item];
                FurnaceOutputSlot.Text = Player.SelectedFurnace.OutputInventory.GetSlot(0).Count.ToString();
                if (FurnaceOutputSlot.Text == "0") FurnaceOutputSlot.Text = "";

                FurnaceProgress.Value = Player.SelectedFurnace.FurnaceProgress % 120;

                float fueluse = Player.SelectedFurnace.FurnaceFuelUsed;
                float fuelmax = Player.SelectedFurnace.FurnaceFuelMax;
                if (fuelmax == 0) fuelmax = 1;
                FurnaceFuelBar.Value = (int)(fueluse / fuelmax * 100);
            }
        }
        public void UpdateReactor()
        {
            reactorPanel.Visible = World.OpenedDirtReactor;
            reactorPanel.Enabled = World.OpenedDirtReactor;

            if (World.OpenedDirtReactor)
            {
                reactorCoreSlot.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedDirtReactor.CoreInventory.GetSlot(0).Item];
                reactorCoreSlot.Text = Player.SelectedDirtReactor.CoreInventory.GetSlot(0).Count.ToString();
                if (reactorCoreSlot.Text == "0") reactorCoreSlot.Text = "";

                reactorDirtSlot.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedDirtReactor.DirtInventory.GetSlot(0).Item];
                reactorDirtSlot.Text = Player.SelectedDirtReactor.DirtInventory.GetSlot(0).Count.ToString();
                if (reactorDirtSlot.Text == "0") reactorDirtSlot.Text = "";

                reactorOutputSlot.BackgroundImage = Const.ItemIDToImage[(int)Player.SelectedDirtReactor.OutputInventory.GetSlot(0).Item];
                reactorOutputSlot.Text = Player.SelectedDirtReactor.OutputInventory.GetSlot(0).Count.ToString();
                if (reactorOutputSlot.Text == "0") reactorOutputSlot.Text = "";
            }
        }
        public void UpdateCraftingTable()
        {
            CraftingTableHolder.Visible = World.OpenedCraftingTable;
            CraftingTableHolder.Enabled = World.OpenedCraftingTable;
        }
        public void UpdateCoolCraftingTable()
        {
            CoolCraftingTableHolder.Visible = World.OpenedCoolCraftingTable;
            CoolCraftingTableHolder.Enabled = World.OpenedCoolCraftingTable;
        }
        #endregion


        public void SelectInventorySlot(int slot)
        {
            slotSelection1.Visible = slot == 0;
            slotSelection2.Visible = slot == 1;
            slotSelection3.Visible = slot == 2;
            slotSelection4.Visible = slot == 3;
            slotSelection5.Visible = slot == 4;

            Player.SelectedSlot = slot;
            if (Player.SelectedInventory == null) return;

            var stack = new ItemStack(Player.PlayerInventory.GetSlot(slot).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.PlayerInventory.GetSlot(slot).Count;
            Player.PlayerInventory.TransferInventories(Player.SelectedInventory, stack).ToString();
        }

        public CraftRecipe CopyCraft(CraftRecipe craftCopy)
        {
            CraftRecipe craft = new CraftRecipe();
            craft.RequiredItems = new List<ItemStack>();

            foreach (var req in craftCopy.RequiredItems)
            {
                craft.RequiredItems.Add(new ItemStack(req.Item, req.Count));
            }
            craft.ResultItems = new ItemStack(craftCopy.ResultItems.Item, craftCopy.ResultItems.Count);

            return craft;
        }

        public void CraftItem(CraftRecipe craft)
        {

            foreach (var req in craft.RequiredItems)
            {
                if (!Player.PlayerInventory.ContainsItem(req))
                {
                    MessageBox.Show("You do not have required items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            foreach (var req in craft.RequiredItems)
            {
                Player.PlayerInventory.RemoveItem(req);
            }

            var error = Player.PlayerInventory.InsertItem(craft.ResultItems);
            if (error < 0)
            {
                MessageBox.Show("You do not have enough space in your inventory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                foreach (var req in craft.RequiredItems)
                {
                    Player.PlayerInventory.InsertItem(req);
                }
                return;
            }
            Player.PointsCount += craft.ResultItems.Count * Const.ItemReceivePoints[(int)craft.ResultItems.Item];
        }



        #region Inventory slots

        private void InventorySlot1Interact(object sender, EventArgs e)
        {
            SelectInventorySlot(0);
        }
        private void InventorySlot2Interact(object sender, EventArgs e)
        {
            SelectInventorySlot(1);
        }
        private void InventorySlot3Interact(object sender, EventArgs e)
        {
            SelectInventorySlot(2);
        }
        private void InventorySlot4Interact(object sender, EventArgs e)
        {
            SelectInventorySlot(3);
        }
        private void InventorySlot5Interact(object sender, EventArgs e)
        {
            SelectInventorySlot(4);
        }
        #endregion

        #region Chest slots
        private void ChestSlot1Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(0).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }

        private void ChestSlot2Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(1).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(1).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }

        private void ChestSlot3Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(2).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(2).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }
        #endregion

        #region Cool chest slots
        private void CoolChestSlot1Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(0).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }

        private void CoolChestSlot2Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(1).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(1).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }

        private void CoolChestSlot3Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(2).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(2).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }

        private void CoolChestSlot4Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(3).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(3).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }

        private void CoolChestSlot5Interact(object sender, EventArgs e)
        {
            var stack = new ItemStack(Player.SelectedInventory.GetSlot(4).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(4).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }
        #endregion

        #region Furnace slots
        bool ssc = false, fsc = false;
        private void FurnaceSmeltingSlot_Click(object sender, EventArgs e)
        {
            if (!ssc)
            { 
                fsc = false;
                ssc = true;
                Player.SelectedInventory = Player.SelectedFurnace.SmeltingInventory;
                return;
            }
            else
            {
                Player.SelectedInventory = Player.SelectedFurnace.SmeltingInventory;
                var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
                if (Control.ModifierKeys == Keys.Shift)
                    stack.Count = Player.SelectedInventory.GetSlot(0).Count;
                Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
            }
        }

        private void FurnaceFuelSlot_Click(object sender, EventArgs e)
        {
            if (!fsc)
            {
                ssc = false;
                fsc = true;
                Player.SelectedInventory = Player.SelectedFurnace.FuelInventory;
                return;
            }
            else
            {
                Player.SelectedInventory = Player.SelectedFurnace.FuelInventory;
                var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
                if (Control.ModifierKeys == Keys.Shift)
                    stack.Count = Player.SelectedInventory.GetSlot(0).Count;
                Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
            }
        }

        private void FurnaceOutputSlot_Click(object sender, EventArgs e)
        {
            ssc = false;
            fsc = false;
            Player.SelectedInventory = Player.SelectedFurnace.OutputInventory;

            var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(0).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }
        #endregion


        #region Reactor slots
        bool rcsc = false, rdsc = false;
        private void ReactorCoreSlot_Click(object sender, EventArgs e)
        {
            if (!rcsc)
            {
                rdsc = false;
                rcsc = true;
                Player.SelectedInventory = Player.SelectedDirtReactor.CoreInventory;
                return;
            }
            else
            {
                Player.SelectedInventory = Player.SelectedDirtReactor.CoreInventory;
                var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
                if (Control.ModifierKeys == Keys.Shift)
                    stack.Count = Player.SelectedInventory.GetSlot(0).Count;
                Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
            }
        }

        private void ReactorDirtSlot_Click(object sender, EventArgs e)
        {
            if (!rdsc)
            {
                rcsc = false;
                rdsc = true;
                Player.SelectedInventory = Player.SelectedDirtReactor.DirtInventory;
                return;
            }
            else
            {
                Player.SelectedInventory = Player.SelectedDirtReactor.DirtInventory;
                var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
                if (Control.ModifierKeys == Keys.Shift)
                    stack.Count = Player.SelectedInventory.GetSlot(0).Count;
                Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
            }
        }

        private void ReactorOutputSlot_Click(object sender, EventArgs e)
        {
            rcsc = false;
            rdsc = false;
            Player.SelectedInventory = Player.SelectedDirtReactor.OutputInventory;

            var stack = new ItemStack(Player.SelectedInventory.GetSlot(0).Item, 1);
            if (Control.ModifierKeys == Keys.Shift)
                stack.Count = Player.SelectedInventory.GetSlot(0).Count;
            Player.SelectedInventory.TransferInventories(Player.PlayerInventory, stack);
        }
        private void ReactorSynthesizeButtonClick(object sender, EventArgs e)
        {
            var r = Player.SelectedDirtReactor;
            var coreType = r.CoreInventory.GetSlot(0).Item;
            Item resourceType = Item.None;
            if(coreType == Item.None)
            {
                MessageBox.Show("Insert a Core into the top slot and at least 2 dirt into the left one, then press \"Synthesize\" to produce an item", "Reactor is empty", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                return;
            }
            if (!r.DirtInventory.ContainsItem(new ItemStack(Item.Dirt, 2)))
            {
                MessageBox.Show("Insert a Core into the top slot and at least 2 dirt into the left one, then press \"Synthesize\" to produce an item", "Reactor is empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            switch (coreType)
            {
                case Item.StoneCore:
                    resourceType = Item.Stone;
                    break;
                case Item.WoodCore:
                    resourceType = Item.Wood;
                    break;
                case Item.IronCore:
                    resourceType = Item.IronIngot;
                    break;
                case Item.CoalCore:
                    resourceType = Item.Coal;
                    break;
                case Item.GoldCore:
                    resourceType = Item.GoldIngot;
                    break;
                case Item.DiamondCore:
                    resourceType= Item.Diamond;
                    break;
            }
            if((resourceType != r.OutputInventory.GetSlot(0).Item && r.OutputInventory.GetSlot(0).Item != Item.None) || r.OutputInventory.GetSlot(0).Count >= Const.StackAmount)
            {
                MessageBox.Show("Output slot is full", "Reactor is filled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Player.SelectedDirtReactor.DirtInventory.RemoveItem(new ItemStack(Item.Dirt, 2));
            Player.SelectedDirtReactor.OutputInventory.InsertItem(new ItemStack(resourceType, 1));
        }
        #endregion

        #region Crafting table
        private void NewCraftSelected(object sender, EventArgs e)
        {
            CraftingTableSlot1.BackgroundImage = null;
            CraftingTableSlot2.BackgroundImage = null;
            CraftingTableSlot3.BackgroundImage = null;
            CraftingTableSlot4.BackgroundImage = null;
            previewCraftSlot.BackgroundImage = null;
            CraftingTableSlot1.Text = "";
            CraftingTableSlot2.Text = "";
            CraftingTableSlot3.Text = "";
            CraftingTableSlot4.Text = "";

            var craft = Const.CraftingRecipes[CraftingTableListCombo.SelectedIndex];

            previewCraftSlot.BackgroundImage = Const.ItemIDToImage[(int)craft.ResultItems.Item];

            CraftingTableSlot1.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[0].Item];
            CraftingTableSlot1.Text = craft.RequiredItems[0].Count == 0 ? "" : craft.RequiredItems[0].Count.ToString();

            if (craft.RequiredItems.Count < 2) return;
            CraftingTableSlot2.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[1].Item];
            CraftingTableSlot2.Text = craft.RequiredItems[1].Count == 0 ? "" : craft.RequiredItems[1].Count.ToString();

            if (craft.RequiredItems.Count < 3) return;
            CraftingTableSlot3.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[2].Item];
            CraftingTableSlot3.Text = craft.RequiredItems[2].Count == 0 ? "" : craft.RequiredItems[2].Count.ToString();

            if (craft.RequiredItems.Count < 4) return;
            CraftingTableSlot4.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[3].Item];
            CraftingTableSlot4.Text = craft.RequiredItems[3].Count == 0 ? "" : craft.RequiredItems[3].Count.ToString();
        }

        private void CraftItemButtonClick(object sender, EventArgs e)
        {
            if (CraftingTableListCombo.SelectedIndex < 0) return;

            CraftRecipe craft = CopyCraft(Const.CraftingRecipes[CraftingTableListCombo.SelectedIndex]);

            CraftItem(craft);
        }
        #endregion

        #region Cool crafting table
        private void CoolNewCraftSelected(object sender, EventArgs e)
        {
            coolSlot1.BackgroundImage = null;
            coolSlot2.BackgroundImage = null;
            coolSlot3.BackgroundImage = null;
            coolSlot4.BackgroundImage = null;
            coolPreviewButton.BackgroundImage = null;
            coolSlot1.Text = "";
            coolSlot2.Text = "";
            coolSlot3.Text = "";
            coolSlot4.Text = "";

            var craft = Const.CraftingRecipes[coolCraftList.SelectedIndex + 21];


            coolPreviewButton.BackgroundImage = Const.ItemIDToImage[(int)craft.ResultItems.Item];

            coolSlot1.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[0].Item];
            coolSlot1.Text = craft.RequiredItems[0].Count == 0 ? "" : craft.RequiredItems[0].Count.ToString();

            if (craft.RequiredItems.Count < 2) return;
            coolSlot2.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[1].Item];
            coolSlot2.Text = craft.RequiredItems[1].Count == 0 ? "" : craft.RequiredItems[1].Count.ToString();

            if (craft.RequiredItems.Count < 3) return;
            coolSlot3.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[2].Item];
            coolSlot3.Text = craft.RequiredItems[2].Count == 0 ? "" : craft.RequiredItems[2].Count.ToString();

            if (craft.RequiredItems.Count < 4) return;
            coolSlot4.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[3].Item];
            coolSlot4.Text = craft.RequiredItems[3].Count == 0 ? "" : craft.RequiredItems[3].Count.ToString();
        }

        private void CoolCraftItemButtonClick(object sender, EventArgs e)
        {
            if (coolCraftList.SelectedIndex < 0) return;

            CraftRecipe craft = CopyCraft(Const.CraftingRecipes[coolCraftList.SelectedIndex + 21]);

            CraftItem(craft);
        }
        #endregion

        #region Pocket crafting
        private void PocketCraftSelected(object sender, EventArgs e)
        {
            var craft = Const.CraftingRecipes[PocketCraftListCombo.SelectedIndex];

            PocketCraftSlot1.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[0].Item];
            PocketCraftSlot1.Text = craft.RequiredItems[0].Count == 0 ? "" : craft.RequiredItems[0].Count.ToString();

            if (craft.RequiredItems.Count < 2) return;
            PocketCraftSlot2.BackgroundImage = Const.ItemIDToImage[(int)craft.RequiredItems[1].Item];
            PocketCraftSlot2.Text = craft.RequiredItems[1].Count == 0 ? "" : craft.RequiredItems[1].Count.ToString();
        }



        private void PocketCraftItemButtonClick(object sender, EventArgs e)
        {
            if(Control.ModifierKeys == Keys.Alt)
            {
                consoleEnterKey.Visible = true;
                consoleTextBox.Visible = true;
            }

            if (PocketCraftListCombo.SelectedIndex < 0) return;

            CraftRecipe craft = CopyCraft(Const.CraftingRecipes[PocketCraftListCombo.SelectedIndex]);

            CraftItem(craft);
        }
        #endregion

        #region Closers
        private void CloseFurnace(object sender, EventArgs e)
        {
            World.OpenedFurnace = false;
            Player.SelectedFurnace = null;
            Player.SelectedInventory = null;
        }
        private void CloseReactor(object sender, EventArgs e)
        {
            World.OpenedDirtReactor = false;
            Player.SelectedDirtReactor = null;
            Player.SelectedInventory = null;
        }

        private void CloseCraftingTable(object sender, EventArgs e)
        {
            World.OpenedCraftingTable = false;
        }
        private void CloseCoolCraftingTable(object sender, EventArgs e)
        {
            World.OpenedCoolCraftingTable = false;
        }

        private void CloseChest(object sender, EventArgs e)
        {
            World.OpenedChest = false;
            Player.SelectedInventory = null;
        }

        private void CloseCoolChest(object sender, EventArgs e)
        {
            World.OpenedCoolChest = false;
            Player.SelectedInventory = null;
        }
        #endregion



        private void consoleEnterKey_Click(object sender, EventArgs e)
        {
            string command = consoleTextBox.Text;
            consoleTextBox.Text = "";

            var commandParams = command.Split(' ');
            switch (commandParams[0])
            {
                case "give":
                    Player.PlayerInventory.InsertItem(new ItemStack((Item)Convert.ToInt32(commandParams[1]), Convert.ToInt32(commandParams[2])));
                    break;
                default:
                    break;
            }
        }

        private void WarnBreakCheck(object sender, EventArgs e)
        {
            if (BreakingBlocksCheck.Checked)
                MessageBox.Show("If you destroy a building using the \"breaking mode\", all resources inside the building will be lost! Be careful!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
