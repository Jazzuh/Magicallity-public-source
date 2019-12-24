using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Magicallity.Client.Helpers;
using Magicallity.Client.Enums;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Locations;
using Magicallity.Client.Models;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using MenuFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magicallity.Client.UI.Store
{
    internal class StoreMenu : ClientAccessor
    {
        #region Variables
        private static Dictionary<string, dynamic> generalStoreItems = new Dictionary<string, dynamic>()
        {
            ["Food items"] = new Dictionary<string, int>()
            {
                ["Doughnut"] = 2,
                ["Beef jerky"] = 4,
                ["Chocolate"] = 2
            },
            ["Drinks"] = new Dictionary<string, int>()
            {
                ["Beer"] = 4,
                ["Vodka"] = 10
            },
            ["Healing items"] = new Dictionary<string, int>()
            {
                ["Bandage"] = 5,
                ["First aid kit"] = 20
            },
            ["Misc items"] = new Dictionary<string, int>()
            {
                ["Cigarette"] = 3,
                ["Bobby Pin"] = 25,
            }
        };

        private static Dictionary<string, dynamic> ammuNationItems = new Dictionary<string, dynamic>()
        {
            ["Weapons"] = new Dictionary<string, int>()
            {
                ["Knife"] = 30,
                ["Pistol"] = 450,
                ["Vintage Pistol"] = 550,
                ["SNS Pistol"] = 450
            },
            ["Ammunition"] = new Dictionary<string, int>()
            {
                ["Pistol ammo"] = 100
            }
        };

        private static Dictionary<string, dynamic> hardwareStoreItems = new Dictionary<string, dynamic>()
        {
            ["Items"] = new Dictionary<string, int>()
            {
                ["Hammer"] = 30,
                ["Bat"] = 45,
                ["Flashlight"] = 25,
                ["Petrol can"] = 50,
                ["Radio"] = 75,
                ["Repair kit"] = 200,
                ["Zipties"] = 50,
            }
        };

        private static Dictionary<string, dynamic> blackMarketStoreItems = new Dictionary<string, dynamic>()
        {
            ["Weapons"] = new Dictionary<string, int>()
            {
                ["Sawn-off shotgun"] = 20000,
                ["TEC-9"] = 15000,
                ["Marksman rifle"] = 75000,
                ["Switchblade"] = 150
            },
            ["Misc items"] = new Dictionary<string, int>()
            {
                ["Lockpick"] = 100,
                ["Light armour"] = 2500,
                ["Cuff lockpick"] = 500,
            },
            ["Ammunition"] = new Dictionary<string, int>()
            {
                ["Rifle ammo"] = 1200,
                ["Shotgun ammo"] = 600,
                ["SMG ammo"] = 600
            }
        };

        private static Dictionary<string, dynamic> fishingStoreItems = new Dictionary<string, dynamic>()
        {
            ["Rods"] = new Dictionary<string, int>()
            {
                ["Fishing rod"] = 500,
                ["Professional rod"] = 1500,
                ["Expert rod"] = 3000
            }
        };

        private static Dictionary<string, dynamic> clubStoreItems = new Dictionary<string, dynamic>()
        {
            ["Drinks"] = new Dictionary<string, int>()
            {
                ["Beer"] = 3,
                ["Vodka"] = 8
            }
        };

        private static Dictionary<string, dynamic> burgerStoreItems = new Dictionary<string, dynamic>()
        {
            ["Burgers / Fries"] = new Dictionary<string, int>()
            {
                ["Heart Stopper Burger"] = 6,
                ["Bleeder Burger"] = 4,
                ["Money Shot Burger"] = 7,
                ["Torpedo Burger"] = 7,
                ["Meat Free Burger"] = 5,
            },
            ["Drinks"] = new Dictionary<string, int>()
            {
                ["Lockpick"] = 100,
                ["Light armour"] = 2500,
                ["Cuff lockpick"] = 500,
            },
        };

        private Dictionary<string, Tuple<string, dynamic, MenuModel>> storeMenuData = new Dictionary<string, Tuple<string, dynamic, MenuModel>>()
        {
            ["general"] = new Tuple<string, dynamic, MenuModel>("General store", generalStoreItems, null),
            ["ammunation"] = new Tuple<string, dynamic, MenuModel>("Ammu Nation", ammuNationItems, null),
            ["hardware"] = new Tuple<string, dynamic, MenuModel>("Hardware store", hardwareStoreItems, null),
            ["blackmarket"] = new Tuple<string, dynamic, MenuModel>("Black market", blackMarketStoreItems, null),
            ["fishing"] = new Tuple<string, dynamic, MenuModel>("Fishing store", fishingStoreItems, null),
            ["club"] = new Tuple<string, dynamic, MenuModel>("Bar", clubStoreItems, null),
            ["burger"] = new Tuple<string, dynamic, MenuModel>("Burger Shot", burgerStoreItems, null)
        };
        private Dictionary<string, MenuModel> storeUIMenus = new Dictionary<string, MenuModel>();
        #endregion

        #region Init
        public StoreMenu(Client client) : base(client)
        {
            Client.RegisterEventHandler("Player.OnLoginComplete", new Action(OnLogin));
            Client.RegisterEventHandler("Player.CheckForInteraction", new Action(() =>
            {
                foreach (var i in storeUIMenus.Keys)
                {
                    if (isNearStoreType(i))
                    {
                        InteractionUI.Observer.OpenMenu(storeUIMenus[i]);
                    }
                }
            }));

            Client.RegisterEventHandler("Player.OnLoginComplete", new Action(() => Client.TriggerServerEvent("Store.RequestStoreData")));
            Client.RegisterEventHandler("Store.BuildStoreMenus", new Action<string>(OnReceiveMenus));
            Client.RegisterTickHandler(OnTick);
        }
        #endregion

        #region Methods

        private void OnReceiveMenus(string data)
        {
            var menus = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            foreach (var store in menus)
            {
                var menuObjects = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(store.Value);
                var menuData = menuObjects.ToDictionary(o => o.Key, o => (dynamic)o.Value.ToObject<Dictionary<string, int>>());

                if (store.Key == "generalStoreItems")
                {
                    generalStoreItems = menuData;
                }
                else if (store.Key == "ammuNationItems")
                {
                    ammuNationItems = menuData;
                }
                else if (store.Key == "hardwareStoreItems")
                {
                    hardwareStoreItems = menuData;
                }
                else if (store.Key == "blackMarketStoreItems")
                {
                    blackMarketStoreItems = menuData;
                }
                else if (store.Key == "fishingStoreItems")
                {
                    fishingStoreItems = menuData;
                }
                else if (store.Key == "clubStoreItems")
                {
                    clubStoreItems = menuData;
                }
                else if (store.Key == "burgerStoreItems")
                {
                    burgerStoreItems = menuData;
                }
            }

            storeMenuData = new Dictionary<string, Tuple<string, dynamic, MenuModel>>()
            {
                ["generalStoreItems"] = new Tuple<string, dynamic, MenuModel>("General store", generalStoreItems, null),
                ["ammuNationItems"] = new Tuple<string, dynamic, MenuModel>("Ammu Nation", ammuNationItems, null),
                ["hardwareStoreItems"] = new Tuple<string, dynamic, MenuModel>("Hardware store", hardwareStoreItems, null),
                ["blackMarketStoreItems"] = new Tuple<string, dynamic, MenuModel>("Black market", blackMarketStoreItems, null),
                ["fishingStoreItems"] = new Tuple<string, dynamic, MenuModel>("Fishing store", fishingStoreItems, null),
                ["clubStoreItems"] = new Tuple<string, dynamic, MenuModel>("Club", clubStoreItems, null),
                ["burgerStoreItems"] = new Tuple<string, dynamic, MenuModel>("Burger Shot", burgerStoreItems, null)
            };

            loadStoreMenus();
        }

        private void loadStoreMenus()
        {
            foreach (var i in storeMenuData)
            {
                var storeMenu = new MenuModel() { headerTitle = i.Value.Item1 };
                createStoreMenu(i.Value.Item2, storeMenu, i.Key);
                Client.Get<InteractionUI>().RegisterInteractionMenuItem(new MenuItemSubMenu
                {
                    Title = i.Value.Item1,
                    SubMenu = storeMenu
                }, () => isNearStoreType(i.Key), 500);
                storeUIMenus[i.Key] = storeMenu;
            }
        }

        private void createStoreMenu(Dictionary<string, dynamic> storeData, MenuModel parentMenu, string baseMenuName)
        {
            List<MenuItem> storeSubMenus = new List<MenuItem>();
            foreach (var i in storeData.Keys)
            {
                Dictionary<string, int> storeCategory = storeData[i];
                List<MenuItem> subMenuData = new List<MenuItem>();
                foreach (var b in storeCategory.Keys)
                    subMenuData.Add(new MenuItemStandard
                    {
                        Title = $"{b}",
                        Detail = $"(${storeCategory[b]})",
                        OnActivate = async state =>
                        {
                            //InteractionUI.Observer.CloseMenu(true);
                            var amountToUse = 1;
                            //if (Int32.TryParse(await Game.GetUserInput(4), out amountToUse))
                            //{
                                //InteractionUI.Observer.OpenMenu(InteractionUI.InteractionMenu);
                                //InteractionUI.Observer.OpenMenu(parentMenu);
                                if (amountToUse < 0)
                                    amountToUse *= -1;

                                //TriggerEvent("buyStoreItem", b, amountToUse, storeCategory[b]);
                                Client.TriggerServerEvent("Store.AttemptBuyItem", baseMenuName, i, b, amountToUse); // store, category, item, amount
                            //}
                        },
                    });
                storeSubMenus.Add(new MenuItemSubMenu
                {
                    Title = i,
                    SubMenu = new MenuModel
                    {
                        headerTitle = i,
                        menuItems = subMenuData
                    }
                });
            }
            parentMenu.menuItems = storeSubMenus;
        }

        private bool isNearStoreType(string storeType)
        {
            bool nearStore = false;
            if (storeType == "generalStoreItems")
                GeneralStores.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(1.75, 2))
                        nearStore = true;
                });
            else if(storeType == "ammuNationItems")
                AmmuNation.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(1.75, 2))
                        nearStore = true;
                });
            else if(storeType == "hardwareStoreItems")
                HardwareStores.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(1.75, 2))
                        nearStore = true;
                });
            else if(storeType == "blackMarketStoreItems")
                BlackMarket.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(1.75, 2))
                        nearStore = true;
                });
            else if (storeType == "fishingStoreItems")
                Fishing.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(1.75, 2))
                        nearStore = true;
                });
            else if(storeType == "clubStoreItems")
                Clubs.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(2, 2))
                        nearStore = true;
                });
            else if(storeType == "burgerStoreItems")
                Burger.Positions.ForEach(o =>
                {
                    if (o.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(2, 2))
                        nearStore = true;
                });
            return nearStore;
        }

        private async Task OnTick()
        {
            if (storeUIMenus.ContainsValue(InteractionUI.Observer.CurrentMenu))
            {
                bool isNearAnyStore = false;
                foreach (var i in storeUIMenus.Keys)
                {
                    if (isNearStoreType(i))
                        isNearAnyStore = true;

                    if (InteractionUI.Observer.CurrentMenu == storeUIMenus[i] && !isNearAnyStore)
                    {
                        InteractionUI.Observer.CloseMenu();
                    } 
                }
            }
        }

        private async void OnLogin()
        {
            // General store
            await BlipHandler.AddBlipAsync("General store", GeneralStores.Positions, new BlipOptions
            {
                Sprite = BlipSprite.Store
            });
            await MarkerHandler.AddMarkerAsync(GeneralStores.Positions);

            // Hardware store
            await BlipHandler.AddBlipAsync("Hardware store", HardwareStores.Positions, new BlipOptions
            {
                Sprite = BlipSprite.Repair
            });
            await MarkerHandler.AddMarkerAsync(HardwareStores.Positions);

            // Ammu nation
            await BlipHandler.AddBlipAsync("Ammu Nation", AmmuNation.Positions, new BlipOptions
            {
                Sprite = BlipSprite.AmmuNation,
                Colour = (BlipColor)6
            });
            await MarkerHandler.AddMarkerAsync(AmmuNation.Positions);

            // Fishing store
            await BlipHandler.AddBlipAsync("Fishing store", Fishing.Positions, new BlipOptions
            {
                Sprite = BlipSprite.Marina,
            });
            await MarkerHandler.AddMarkerAsync(Fishing.Positions);

            // Clubs
            await MarkerHandler.AddMarkerAsync(Clubs.Positions, new MarkerOptions
            {
                ScaleFloat = 3.0f
            });

            // Black market
            await MarkerHandler.AddMarkerAsync(BlackMarket.Positions, new MarkerOptions
            {
                Color = ConstantColours.Red
            });
        }
        #endregion
    }
}
