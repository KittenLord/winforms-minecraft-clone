using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gridproject
{
    public static class Player
    {
        public static Inventory PlayerInventory { get; set; } = new Inventory(5, AccessLevel.Low);

        public static Inventory SelectedInventory { get; set; }
        public static Furnace SelectedFurnace { get; set; }
        public static DirtReactor SelectedDirtReactor { get; set; }

        public static int PointsCount { get; set; }

        public static int SelectedSlot { get; set; }

        public static bool IsBreaking { get; set; }
    }
}
