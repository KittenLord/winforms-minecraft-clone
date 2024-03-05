using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class Mineable
    {
        public int ResourceCount;
        public TileCoordinates sourceCoordinates;
        public Item Resource;
        public List<Item> ToolFilter = new List<Item>();

        public Mineable(int resourceCount, TileCoordinates sourceCoordinates, Item resource)
        {
            ResourceCount = resourceCount;
            this.sourceCoordinates = sourceCoordinates;
            Resource = resource;
        }

        public Mineable(int resourceCount, TileCoordinates sourceCoordinates, Item resource, List<Item> toolFilter) : this(resourceCount, sourceCoordinates, resource)
        {
            ToolFilter = toolFilter;
        }

        public void Mine(object s, EventArgs e)
        {
            Control ab = s as Control;

            List<Item> correctTools = new List<Item>();
            int mineAmount = 1;

            if (Player.PlayerInventory.ContainsItem(new ItemStack(Item.StonePickaxe, 1)))
            {

            }

            if(ToolFilter.Count > 0)
            {
                for(int i = 0; i < 5; i++)
                {
                    var tool = Player.PlayerInventory.GetSlot(i).Item;
                    if (ToolFilter.Contains(tool))
                    {
                        correctTools.Add(tool);
                    }
                }

                if (correctTools.Count == 0 && !ToolFilter.Contains(Item.None)) return;

                foreach (var tool in correctTools)
                {
                    var amount = ToolFilter.IndexOf(tool) + 1;
                    if (amount > mineAmount) mineAmount = amount;
                }
            }

            if (ResourceCount > 0)
            {
                if(mineAmount > ResourceCount)
                {
                    mineAmount = ResourceCount;
                }

                Player.PointsCount += mineAmount * Const.ItemReceivePoints[(int)Resource];

                ResourceCount -= mineAmount;

                ab.Text = ResourceCount.ToString();
                Player.PlayerInventory.InsertItem(new ItemStack(Resource, mineAmount));

                if (ResourceCount == 0)
                {
                    World.FindTile(sourceCoordinates).Reset();
                }
            }
        }
    }
}
