using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gridproject
{
    public class Breakable
    {
        public TileCoordinates sourceCoordinates;
        public ItemStack Drop;
        public List<Item> ToolFilter = new List<Item>();

        public Breakable(TileCoordinates sourceCoordinates, ItemStack drop)
        {
            this.sourceCoordinates = sourceCoordinates;
            Drop = drop;
        }

        public Breakable(TileCoordinates sourceCoordinates, ItemStack resource, List<Item> toolFilter) : this(sourceCoordinates, resource)
        {
            ToolFilter = toolFilter;
        }

        public void Break(object s, EventArgs e)
        {
            bool pass = false;

            if (ToolFilter.Count > 0)
            {
                foreach (Item tool in ToolFilter)
                {
                    if (Player.PlayerInventory.ContainsItem(new ItemStack(tool, 1)))
                        pass = true;
                }
            }
            else
                pass = true;

            if (!pass) return;

            Player.PlayerInventory.InsertItem(Drop);

            World.FindTile(sourceCoordinates).Reset();
        }
    }
}
