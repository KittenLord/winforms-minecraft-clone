using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gridproject
{
    public class DirtOccupation : Mineable, IOccupation
    {
        public DirtOccupation(int resourceCount, TileCoordinates sourceCoordinates, Item resource, List<Item> toolFilter) : base(resourceCount, sourceCoordinates, resource, toolFilter)
        {
        }

        public OccupationType Type => OccupationType.Dirt;
        public bool IsUnderground { get; set; }

        public new TileCoordinates sourceCoordinates { get; private set; }

        public void Load(Control ab)
        {
            try
            {
                ab.Click -= Mine;
            }
            catch { }
            ab.Text = ResourceCount.ToString();

            ab.Click += Mine;
        }

        public void Reset(Control ab)
        {
            var tile = World.FindTile(ab);
            IOccupation occupation = null;
            var coordinates = tile.TileCoordinates;
            int rarity = World.UndergroundMap[coordinates.X, coordinates.Y];

            int resources = new Random().Next(10, 30);

            switch (rarity)
            {
                case 0:
                    occupation = new StoneSourceOccupation(resources, sourceCoordinates, Item.Cobblestone, new List<Item> { Item.WoodenPickaxe, Item.StonePickaxe, Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    break;
                case 1:
                    occupation = new IronOreOccupation(resources, sourceCoordinates, Item.IronOre, new List<Item> { Item.StonePickaxe, Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    break;
                case 2:
                    occupation = new CoalOreOccupation(resources, sourceCoordinates, Item.Coal, new List<Item> { Item.StonePickaxe, Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    break;
                case 3:
                    occupation = new GoldOreOccupation(resources + 20, sourceCoordinates, Item.GoldOre, new List<Item> { Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    break;
                case 4:
                    occupation = new DiamondOreOccupation(resources, sourceCoordinates, Item.Diamond, new List<Item> { Item.IronPickaxe, Item.DiamondPickaxe, Item.GoldenPickaxe, Item.Perforator });
                    break;
                case 5:
                    occupation = new NetheriteOccupation(resources + 80, sourceCoordinates, Item.NetheriteScrap, new List<Item> { Item.DiamondPickaxe, Item.Perforator });
                    break;
                default:
                    break;
            }


            tile.ForceSetOccupation(occupation, true);
            tile.Occupation.IsUnderground = true;
        }

        public void SetCoordinates(TileCoordinates tc)
        {
            sourceCoordinates = tc;
        }
    }
}
