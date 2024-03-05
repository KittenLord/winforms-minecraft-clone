using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public enum Item
    {
        None, Cobblestone, Wood, Stone, Charcoal, Coal, Plank, WoodenPickaxe, Stick, IronOre, IronIngot, CraftingTable, Chest, StonePickaxe, Furnace, WoodenShovel, Dirt, GoldOre, Diamond, IronPickaxe,
        Bedrock, DiamondPickaxe, DebugStick, GoldIngot, StoneShovel, IronShovel, GoldenShovel, DiamondShovel, GoldenPickaxe, WoodenAxe, StoneAxe, IronAxe, GoldenAxe, DiamondAxe, CoolCraftingTable,
        Locator, NetheriteScrap, NetheriteIngot,

        StoneCore, IronCore, CoalCore, WoodCore, GoldCore, DiamondCore,
        HemisphereA, HemisphereB, Perforator, Activator,

        TheFinale,

        Landfill, Emerald, CoolChest, DirtReactor
    }

    public enum AccessLevel
    {
        Low, Mid, High
    }

    public class ItemStack
    {
        public Item Item;
        public int Count;

        public ItemStack(Item item, int count)
        {
            Item = item;
            Count = count;
        }

        public ItemStack Single => new ItemStack(Item, 1);
    }

    public class Inventory
    {


        private List<ItemStack> Items = new List<ItemStack>();
        public int Capacity { get; private set; }

        public Inventory(int capacity, AccessLevel accessLevel)
        {
            Capacity = capacity;
            AccessLevel = accessLevel;

            for (int i = 0; i < capacity; i++)
            {
                Items.Add(new ItemStack(Item.None, 0));
            }
        }

        public Inventory(int capacity, AccessLevel accessLevel, Item[] filterItems) : this(capacity, accessLevel)
        {
            FilterItems = filterItems;
            UseFilter = true;
        }

        public AccessLevel AccessLevel;

        public Item[] FilterItems = new Item[1];
        public bool UseFilter = false;

        public int Occupied => Items.Where(i => i.Item != Item.None).Count();
        public int ItemCount(Item item)
        {
            var itemStacks = Items.Where(s => s.Item == item).ToList();
            if (itemStacks.Count <= 0) return -1;
            int totalItems = 0;
            foreach (var st in itemStacks)
                totalItems += st.Count;

            return totalItems;
        }
        public int InsertItem(ItemStack stackt)
        {
            ItemStack stack = new ItemStack(stackt.Item, stackt.Count);
            if (stack.Count <= 0) return -1;
            if (stack.Item == Item.None) return -1;
            if (!FilterItems.Contains(stack.Item) && UseFilter) return -1;

            var itemStack = Items.Find(i => i.Item == stack.Item && i.Count < Const.StackAmount);

            if (itemStack == null && Occupied == Capacity) return -1;

            if (itemStack == null)
            {
                
                var emptyStack = Items.Find(s => s.Item == Item.None);
                emptyStack.Item = stack.Item;
                if(stack.Count > Const.StackAmount)
                {
                    emptyStack.Count = Const.StackAmount;
                    return InsertItem(new ItemStack(stack.Item, stack.Count - Const.StackAmount));
                }
                emptyStack.Count = stack.Count;
                return 0;
            }
            else if(Occupied == Capacity)
            {
                if(itemStack.Count + stack.Count > Const.StackAmount)
                {
                    var oldCount = itemStack.Count;
                    itemStack.Count = Const.StackAmount;
                    var ret = InsertItem(new ItemStack(stack.Item, itemStack.Count + stack.Count - Const.StackAmount));
                    if(ret < 0)
                    {
                        return Const.StackAmount - oldCount;
                    }
                }
                itemStack.Count += stack.Count;
                return 0;
            }

            if (itemStack.Count + stack.Count > Const.StackAmount)
            {
                var newInsert = itemStack.Count + stack.Count - Const.StackAmount;
                itemStack.Count = Const.StackAmount;
                return InsertItem(new ItemStack(stack.Item, newInsert));
            }
            itemStack.Count += stack.Count;

            return 0;
        }
        public int RemoveItem(ItemStack stack)
        {
            if (stack.Count <= 0) return -1;
            if (stack.Item == Item.None) return -1;

            var itemStacks = Items.Where(s => s.Item == stack.Item).ToList();
            if (itemStacks.Count <= 0) return -1;
            int totalItems = 0;
            foreach (var st in itemStacks)
                totalItems += st.Count;

            if (totalItems < stack.Count) return -1;

            foreach (var st in itemStacks)
            {
                if(stack.Count > st.Count)
                {
                    stack.Count -= st.Count;
                    st.Count = 0;
                    st.Item = Item.None;
                }
                else
                {
                    st.Count -= stack.Count;
                    if (st.Count == 0) st.Item = Item.None;

                    return 0;
                }
            }

            return -1;
        }

        public bool TransferInventories(Inventory anotherInventory, ItemStack giveaway)
        {
            if(giveaway.Count == 64)
            {

            }

            ItemStack giveawayRemove = new ItemStack(giveaway.Item, giveaway.Count);
            ItemStack giveawayInsert = new ItemStack(giveaway.Item, giveaway.Count);

            var itemsBefore = anotherInventory.ItemCount(giveaway.Item);


            if ((int)AccessLevel < (int)anotherInventory.AccessLevel) return false;

            var removeError = RemoveItem(giveawayRemove);
            if (removeError < 0)
            {
                return false;
            }

            var insertError = anotherInventory.InsertItem(giveawayInsert);
            if(anotherInventory.AccessLevel < AccessLevel)
            {
                Player.PointsCount += (anotherInventory.ItemCount(giveaway.Item) - itemsBefore) * Const.ItemReceivePoints[(int)giveaway.Item];
            }
            if(insertError < 0)
            {
                InsertItem(giveaway);
                return false;
            }
            if (insertError > 0)
            {
                InsertItem(new ItemStack(giveaway.Item, insertError));
                return false;
            }

            return true;
        }

        public bool ContainsItem(ItemStack stack)
        {
            var error = RemoveItem(stack);

            if (error >= 0) InsertItem(stack);

            return error >= 0;
        }

        public ItemStack GetSlot(int slot)
        {
            if (slot >= Capacity) return null;
            return Items[slot];
        }
    }
}
