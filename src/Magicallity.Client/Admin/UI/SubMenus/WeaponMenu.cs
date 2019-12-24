using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Admin.Interfaces;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using MenuFramework;
using Newtonsoft.Json;

namespace Magicallity.Client.Admin.UI.SubMenus
{
    internal class WeaponMenu : MenuModel, IAdminMenu
    {
        public WeaponMenu()
        {
            headerTitle = "Weapons";
            foreach (var weapon in Enum.GetValues(typeof(WeaponHash)).Cast<WeaponHash>())
            {
                var weaponItem = InventoryItems.GetInvItemData($"WEAPON_{weapon.ToString().ToLower()}");

                if(weaponItem != null)
                {
                    menuItems.Add(new WeaponSubItem(weaponItem));
                }
            }

            menuItems = menuItems.OrderBy(o => o.Title).ToList();
        }

        public MenuItem GetSubMenu()
        {
            return new MenuItemSubMenu
            {
                Title = "Weapons",
                SubMenu = this
            };
        }
    }

    class WeaponSubItem : MenuItemStandard
    {
        public WeaponSubItem(inventoryItem weapon)
        {
            Title = weapon.itemName;

            OnActivate = item =>
            {
                Client.Instance.TriggerServerEvent("Inventory.AddInvItem", JsonConvert.SerializeObject(weapon), 1);
            };
        }
    }
}
