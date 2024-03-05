using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public enum OccupationType
    {
        Grass, StoneSource, Woods, Water, Chest, Furnace, CraftingTable, Plank, IronOre, CoalOre, Dirt, GoldOre, DiamondOre, Bedrock, CoolCraftingTable, Netherite, CoolChest, DirtReactor
    }

    public interface IOccupation
    {
        OccupationType Type { get; }
        TileCoordinates sourceCoordinates { get; }
        bool IsUnderground { get; set; }
        void SetCoordinates(TileCoordinates tc);
        void Load(Control ab);
        void Reset(Control ab);
    }
}
