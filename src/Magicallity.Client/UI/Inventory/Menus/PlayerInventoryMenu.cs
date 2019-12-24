using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Helpers;
using Magicallity.Client.Property;
using Magicallity.Client.Session;
using Magicallity.Client.Vehicles;
using MenuFramework;

namespace Magicallity.Client.UI.Inventory.Menus
{
    public sealed class PlayerInventoryMenu : InventoryMenu
    {
        public override string MenuHeaderTitle { get; set; } = "Player Inventory";

        public PlayerInventoryMenu(Shared.Inventory targetInv)
            : base(targetInv)
        {

        }

        public PlayerInventoryMenu(string invString)
            : base(invString)
        {

        }

        public override Action<MenuItemSubMenu> GetOnActivateFunction()
        {
            return item =>
            {
                refreshPlayerInventory();
            };
        }

        protected override List<InventoryItemInteractable> getMenuInteractables()
        {
            var actions = new List<InventoryItemInteractable>
            {
                InventoryMenuInteractions.UseItem,
                InventoryMenuInteractions.DropItem,
            };

            if (Client.Instance.Get<VehicleHandler>().GetClosestVehicleWithKeys() != null)
            {
                actions.Add(InventoryMenuInteractions.PutInVehItem);
            }

            if (Client.Instance.Get<PropertyHandler>().IsNearPropertyStorage())
            {
                actions.Add(InventoryMenuInteractions.PutInStorageItem);
            }

            return actions;
        }

        private void refreshPlayerInventory()
        {
            var playerSession = Client.Instance.Get<SessionManager>().GetPlayer(Game.Player);
            RefreshMenu(playerSession.GetGlobalData("Character.Inventory", ""));
        }
    }
}
