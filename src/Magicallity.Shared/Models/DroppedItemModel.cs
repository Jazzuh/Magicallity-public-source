using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Shared;

#if SERVER
using Magicallity.Server.Enviroment;
#endif

namespace Magicallity.Server.Shared.Models
{
    public class DroppedItemModel
    {
#if SERVER
        public DroppedItemModel(DroppedServerItem item)
            : this(item.ItemId, item.ItemData, item.ItemPos){ }
#endif

        public DroppedItemModel()
        {

        }

        public DroppedItemModel(int itemId, inventoryItem invItem, Vector3 pos)
        {
            ItemId = itemId;
            ItemName = invItem.itemName;
            ItemPos = pos;
            ItemAmount = invItem.itemAmount;
        }

        public int ItemId;
        public string ItemName;
        public Vector3 ItemPos;
        public bool PosEdited = false;
        public int ItemAmount;
    }
}
