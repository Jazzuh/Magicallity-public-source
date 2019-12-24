using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Server.Bank;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using Newtonsoft.Json;

namespace Magicallity.Server.Enviroment
{
    public class Stores : ServerAccessor
    {
#region Fields
        private static Dictionary<string, dynamic> generalStoreItems = new Dictionary<string, dynamic>()
        {
            ["Food items"] = new Dictionary<string, int>()
            {
                ["Beef Jerky"] = 4,
                ["Soup"] = 4,
                ["Frozen Pizza"] = 4,
                ["Frozen Peas"] = 4,
                ["Pasta"] = 4,
                ["Chocolate"] = 2,
                ["Doughnut"] = 2,
                ["EgoChaser Bar"] = 2,
                ["Meteorite Bar"] = 2,
                ["Zebra Bar"] = 3
            },
            ["Fruits / Vegetables"] = new Dictionary<string, int>()
            {
                ["Orange"] = 1,
                ["Pineapple"] = 2,
                ["Pear"] = 2,
                ["Banana"] = 1,
                ["Apple"] = 1
            },
            ["Drinks"] = new Dictionary<string, int>()
            {
                ["Beer"] = 7,
                ["Coffee"] = 7,
                ["Sprunk Can"] = 4,
                ["eCola Can"] = 4,
                ["Apple Juice"] = 2,
                ["Silver Claw"] = 2,
                ["Orange Juice"] = 2
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
                ["Ball"] = 8
            }
        };

        private static Dictionary<string, dynamic> ammuNationItems = new Dictionary<string, dynamic>()
        {
            ["Melee"] = new Dictionary<string, int>()
            {
                ["Knife"] = 30,
                ["Hammer"] = 25,
                ["Baseball Bat"] = 40,
                ["Golf Club"] = 50,
                ["Crowbar"] = 50,
                ["Nightstick"] = 100,
                ["Dagger"] = 30,
                ["Hatchet"] = 120,
                ["Pool Cue"] = 100,
                ["Wrench"] = 60
            },
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
                ["Sawn-off shotgun"] = 50000,
                ["TEC-9"] = 40000,
                ["Switchblade"] = 150,
                ["Broken Bottle"] = 50,
                ["Machete"] = 150,
                ["Battle Axe"] = 140
            },
            ["Misc items"] = new Dictionary<string, int>()
            {
                ["Lockpick"] = 100,
                ["Light armour"] = 2500,
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
            }
        };

        private static Dictionary<string, dynamic> clubStoreItems = new Dictionary<string, dynamic>()
        {
            ["Drinks"] = new Dictionary<string, int>()
            {
                ["Beer"] = 3,
                ["Red Wine"] = 6,
                ["White Wine"] = 7,
                ["Brandy"] = 5,
                ["Rum"] = 5,
                ["Gin"] = 8,
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
                ["Steak Fries"] = 3,
                ["Curly Fries"] = 3,
                ["Fries"] = 2
            },
            ["Drinks"] = new Dictionary<string, int>()
            {
                ["Sprunk Can"] = 4,
                ["eCola Can"] = 4
            },
        };

        private Dictionary<string, dynamic> itemDicts = new Dictionary<string, dynamic>
        {
            {"generalStoreItems", generalStoreItems },
            {"ammuNationItems", ammuNationItems },
            {"hardwareStoreItems", hardwareStoreItems },
            {"blackMarketStoreItems", blackMarketStoreItems },
            {"fishingStoreItems",  fishingStoreItems},
            {"clubStoreItems", clubStoreItems },
            {"burgerStoreItems", burgerStoreItems }
        };

        private string _cachedItemsForClient;
        private string itemsForClient
        {
            get
            {
                if (string.IsNullOrEmpty(_cachedItemsForClient))
                {
                    _cachedItemsForClient = JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        {"generalStoreItems", JsonConvert.SerializeObject(generalStoreItems) },
                        {"ammuNationItems", JsonConvert.SerializeObject(ammuNationItems) },
                        {"hardwareStoreItems", JsonConvert.SerializeObject(hardwareStoreItems) },
                        {"blackMarketStoreItems", JsonConvert.SerializeObject(blackMarketStoreItems) },
                        {"fishingStoreItems",  JsonConvert.SerializeObject(fishingStoreItems)},
                        {"clubStoreItems", JsonConvert.SerializeObject(clubStoreItems) },
                        {"burgerStoreItems", JsonConvert.SerializeObject(burgerStoreItems) }
                    });
                }

                return _cachedItemsForClient;
            }
        }
        
        #endregion

        public Stores(Server server) : base(server)
        {
            server.RegisterEventHandler("Store.RequestStoreData", new Action<Player>(OnStoreRequest));
            server.RegisterEventHandler("Store.AttemptBuyItem", new Action<Player, string, string, string, int>(OnBuyItem));
        }

        private void OnBuyItem([FromSource] Player source, string store, string category, string item, int itemAmount)
        {
            var playerSession = Server.Instances.Session.GetPlayer(source);
            if(playerSession == null) return;

            int itemPrice = itemDicts[store][category][item];

            Log.Info($"{store}->{category}->{item}");
            var payHandler = Server.Get<PaymentHandler>();
            if(payHandler.CanPayForItem(playerSession, itemPrice, itemAmount))
            {
                var playerInv = new PlayerInventory(playerSession.GetGlobalData("Character.Inventory", ""), playerSession);
                if(playerInv.CanStoreItem(item, itemAmount))
                {
                    payHandler.PayForItem(playerSession, itemPrice * itemAmount, $"buying {itemAmount}x {item}");

                    if (item.Contains("ammo"))
                    {
                        itemAmount = 50;
                    }
                    playerInv.AddItem(item, itemAmount);
                    Log.ToClient("[Store]", $"You just bought {itemAmount}x {item}", ConstantColours.Store, source);
                }
                else
                {
                    Log.ToClient("[Inventory]", "You do not have enough space to carry these item(s)", ConstantColours.Inventory, source);
                }
            }
            else
            {
                Log.ToClient("[Bank]", "You don't have enough money to but this item", ConstantColours.Bank, source);
            }
        }

        private void OnStoreRequest([FromSource] Player source)
        {
            /*var storeData = new Dictionary<string, dynamic>();
            var storeFields = GetType().GetProperties().Where(o => o.Name.Contains("Items"));
            storeFields.ToList().ForEach(o =>
            {
                Log.Info(o.Name);
                storeData.Add(o.Name, o.GetValue(this));
            });
            var storeData = new Dictionary<string, string>
            {
                {"generalStoreItems", JsonConvert.SerializeObject(generalStoreItems) },
                {"ammuNationItems", JsonConvert.SerializeObject(ammuNationItems) },
                {"hardwareStoreItems", JsonConvert.SerializeObject(hardwareStoreItems) },
                {"blackMarketStoreItems", JsonConvert.SerializeObject(blackMarketStoreItems) },
                {"fishingStoreItems",  JsonConvert.SerializeObject(fishingStoreItems)},
                {"clubStoreItems", JsonConvert.SerializeObject(clubStoreItems) }
            };*/

            source.TriggerEvent("Store.BuildStoreMenus", itemsForClient);
        }
    }
}
