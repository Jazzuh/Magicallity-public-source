using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicallity.Client.Helpers;
using Magicallity.Client.Jobs;
using Magicallity.Client.Property;
using Magicallity.Client.Vehicles;
using Magicallity.Shared.Enums;
using MenuFramework;

namespace Magicallity.Client.UI.Inventory.Menus
{
    public sealed class VehicleInventoryMenu : InventoryMenu
    {
        public override string MenuHeaderTitle { get; set; } = "Vehicle Inventory";
        public override string ItemDescription { get; set; } = "This vehicle has {0} of this item with a weight of {1}";

        public VehicleInventoryMenu(Shared.Inventory targetInv)
            : base(targetInv)
        {

        }

        public VehicleInventoryMenu(string invString)
            : base(invString)
        {

        }

        public override bool CanViewMenu() => !Client.Instance.Get<PropertyHandler>().IsNearPropertyStorage();

        public override Action<MenuItemSubMenu> GetOnActivateFunction()
        {
            return item =>
            {
                RefreshMenu("");
                var closeOwnedVeh = Client.Instance.Get<JobHandler>().OnDutyAsJob(JobType.Police) ? GTAHelpers.GetClosestVehicle(3.0f, o => o.HasDecor("Vehicle.ID")) : Client.Instance.Get<VehicleHandler>().GetClosestVehicleWithKeys();
                if (closeOwnedVeh != null)
                {
                    Client.Instance.TriggerServerEvent("Inventory.RequestInventory", closeOwnedVeh.GetDecor<int>("Vehicle.ID"));
                }
            };
        }

        protected override List<InventoryItemInteractable> getMenuInteractables()
        {
            return new List<InventoryItemInteractable>
            {
                InventoryMenuInteractions.TakeFromVehItem
            };
        }
    }
}
