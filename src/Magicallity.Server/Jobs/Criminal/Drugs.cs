using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Server.Bank;
using Magicallity.Server.Helpers;
using Magicallity.Server.Players;
using Magicallity.Server.Session;
using Magicallity.Shared;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using Newtonsoft.Json;

namespace Magicallity.Server.Jobs.Criminal
{
    public class Drugs : ServerAccessor
    {
        private List<Drug> drugs = new List<Drug>
        {
            new Drug
            {
                HarvestDrugName = "Bud",
                HarvestLocations =  new List<Vector3>
                {
                    new Vector3(1368.03f, 4306.94f, 38.10f)
                   // new Vector3(-334.07f, -999.02f, 30.38f)
                   // new Vector3(2303.82f, 4856.08f, 41.81f)
                },
                HarvestTime = 2500,
                HarvestInteractionText = "collect",
                ProcessDrugName = "Weed baggie",
                ProcessLocations = new List<Vector3>
                {
                    new Vector3(606.26f, -462.29f, 24.74f)
                    //new Vector3(-320.29f, -960.51f, 31.08f)
                    //new Vector3(883.44f, -1048.76f, 33.01f)
                },
                ProcessTime = 3000,
                ProcessInteractionText = "process",
                SellPriceMin = 150,
                SellPriceMax = 350
            },
            new Drug
            {
                HarvestDrugName = "Methamphetamine",
                HarvestLocations =  new List<Vector3>
                {
                    new Vector3(2332.69f, 3070.17f, 48.57f)
                    //new Vector3(57.94f, 3692.08f, 39.92f)
                    //new Vector3(124.98f, -420.32f, 41.06f)
                },
                HarvestTime = 2500,
                HarvestInteractionText = "collect",
                ProcessDrugName = "Meth Rock",
                ProcessLocations = new List<Vector3>
                {
                    new Vector3(65.67f, 6663.82f, 31.79f)
                    //new Vector3(956.56f, -195.78f, 73.15f)
                    //new Vector3(18.34f, -391.98f, 39.51f)
                },
                ProcessTime = 3000,
                ProcessInteractionText = "process",
                SellPriceMin = 150,
                SellPriceMax = 400,
            },
            new Drug
            {
                HarvestDrugName = "Unprocessed Cocaine",
                HarvestLocations =  new List<Vector3>
                {
                    new Vector3(1004.94f, -1210.99f, 25.29f)
                    //new Vector3(175.36f, -1687.2f, 29.56f)
                    //new Vector3(90.92f, -206.16f, 54.49f)
                },
                HarvestTime = 2500,
                HarvestInteractionText = "collect",
                ProcessDrugName = "Cocaine baggie",
                ProcessLocations = new List<Vector3>
                {
                    new Vector3(-916.84f, 156.44f, 63.46f)
                    //new Vector3(-1168.863f, -2020.754f, 12.16055f)
                    //new Vector3(173.14f, -61.05f, 68.22f)
                },
                ProcessTime = 3000,
                ProcessInteractionText = "process",
                SellPriceMin = 150,
                SellPriceMax = 450
            }
        };
        private List<string> drugsSerialized = new List<string>();

        private JobHandler JobHandler => Server.Get<JobHandler>();

        public Drugs(Server server) : base(server)
        {
            server.RegisterEventHandler("Player.OnInteraction", new Action<Player, bool>(OnInteraction));
            server.RegisterEventHandler("Drugs.RequestDrugs", new Action<Player>(OnRequestDrugs));
            server.RegisterEventHandler("Drugs.AttemptSellDrug", new Action<Player, string>(OnSellDrugs));
            drugs.ForEach(o =>
            {
                drugsSerialized.Add(JsonConvert.SerializeObject(o));
            });
        }

        public Drug GetDrugData(string drugName)
        {
            return drugs.FirstOrDefault(o => o.ProcessDrugName == drugName) ?? drugs.FirstOrDefault(o => o.HarvestDrugName == drugName);
        }

        public void GetClosestLocation(Vector3 currentLocation, out Drug closeLocation, out Vector3 closestVector, out bool isHarvest)
        {
            var location = drugs.FirstOrDefault(o => o.HarvestLocations.Any(loc => loc.DistanceToSquared(currentLocation) < 15.0f));

            if (location != null)
            {
                closeLocation = location;
                closestVector = location.HarvestLocations.Where(loc => loc.DistanceToSquared(currentLocation) < 15.0f).ElementAt(0);
                isHarvest = true;
                return;
            }

            location = drugs.FirstOrDefault(o => o.ProcessLocations.Any(loc => loc.DistanceToSquared(currentLocation) < 15.0f));
    
            closeLocation = location;
            closestVector = location?.ProcessLocations.Where(loc => loc.DistanceToSquared(currentLocation) < 15.0f).ElementAt(0) ?? Vector3.Zero;
            isHarvest = false;
        }

        private void OnRequestDrugs([FromSource] Player source)
        {
            source.TriggerEvent("Drugs.ReceiveDrugs", drugsSerialized);
        }

        private void OnInteraction([FromSource] Player source, bool isInVehicle)
        {
            var playerSession = Sessions.GetPlayer(source);
            if (playerSession == null || isInVehicle) return;

            if(!playerSession.GetServerData("Drug.IsInteracting", false))
            {
                playerSession.SetServerData("Drug.IsInteracting", true);
                GetClosestLocation(playerSession.Position, out var drugData, out var closeVector, out var isHarvest);

                if (drugData != null)
                {
                    var drugName = isHarvest ? drugData.HarvestDrugName : drugData.ProcessDrugName;
                    var playerInv = playerSession./*GetInventory()*/Inventory;
                    if (isHarvest && playerInv.CanStoreItem(drugName, 1) || !isHarvest && playerInv.HasItem(drugData.HarvestDrugName))
                    {
                        Log.Info($"{source.Name} is close to a {drugName} point at position {closeVector} starting the stuff");
                        Log.ToClient("[Drugs]", $"You begin to {(isHarvest ? drugData.HarvestInteractionText : drugData.ProcessInteractionText)} {drugData.HarvestDrugName}", ConstantColours.Criminal, source);
                        beginDrugInteraction(playerSession, drugData, closeVector, isHarvest);
                    }
                    else
                    {
                        Log.ToClient("[Drugs]", $"You cannot hold anymore {drugName}", ConstantColours.Criminal, playerSession.Source);
                        playerSession.SetServerData("Drug.IsInteracting", false);
                    }
                }
                else
                {
                    playerSession.SetServerData("Drug.IsInteracting", false);
                }
            }
        }

        private int GetPoliceMult(int currentValue, float multiplier)
        {
            var policeOnDuty = JobHandler.GetPlayersOnJob(JobType.Police);

            if (policeOnDuty.Count >= CitizenFX.Core.Native.API.GetConvarInt("mg_drugsRequiredPolice", 2))
                return currentValue;
            else
                return (int)Math.Ceiling(currentValue * multiplier);
        }

        private async void beginDrugInteraction(Session.Session playerSession, Drug drugData, Vector3 closeVector, bool isHarvest)
        {
            var drugName = isHarvest ? drugData.HarvestDrugName : drugData.ProcessDrugName;
            var interactionTime = GetPoliceMult(isHarvest ? drugData.HarvestTime : drugData.ProcessTime, 1.5f);

            var remainingTime = interactionTime;
            while (true)
            {
                await BaseScript.Delay(250);
                remainingTime -= 250;

                if (remainingTime <= 0)
                {
                    var playerInv = playerSession./*GetInventory()*/Inventory;
                    if(isHarvest && playerInv.CanStoreItem(drugName, 1) || !isHarvest && playerInv.HasItem(drugData.HarvestDrugName))
                    {
                        Log.ToClient("[Drugs]", $"You just got 1 {drugName}", ConstantColours.Criminal, playerSession.Source);
                        if (!isHarvest)
                        {
                            playerInv.AddItem(drugData.HarvestDrugName, -1);
                        }
                        playerInv.AddItem(drugName, 1);
                    }
                    else
                    {
                        Log.ToClient("[Drugs]", $"You cannot hold anymore {drugName}", ConstantColours.Criminal, playerSession.Source);
                        playerSession.SetServerData("Drug.IsInteracting", false);
                        return;
                    }

                    remainingTime = interactionTime;
                }
                else
                {
                    var playerPos = playerSession.GetPlayerPosition();

                    if (playerPos.DistanceToSquared(closeVector) > 15.0f || playerSession.IsInVehicle())
                    {
                        Log.ToClient("[Drugs]", "You moved to far away from the drug area", ConstantColours.Criminal, playerSession.Source);
                        playerSession.SetServerData("Drug.IsInteracting", false);
                        return;
                    }
                }
            }
        }

        private void OnSellDrugs([FromSource] Player source, string drug)
        {
            var playerSession = Sessions.GetPlayer(source);
            if (playerSession == null) return;

            var drugData = GetDrugData(drug);
            var playerInv = playerSession./*GetInventory()*/Inventory;

            if (playerInv.HasItem(drug))
            {
                var invItem = playerInv.GetItem(drug);
                var baseSellAmount = GetPoliceMult(drugData.SellPrice, 0.25f);
                var amountToSell = new Random().Next(1, invItem.itemAmount > 3 ? 3 : invItem.itemAmount);

                Log.ToClient("[Drugs]", $"You just sold ^2{amountToSell}^0 {drug}(s) for ^2${baseSellAmount * amountToSell}^0", ConstantColours.Criminal, source);
                Server.Get<PaymentHandler>().UpdatePlayerCash(playerSession, baseSellAmount * amountToSell, $"selling {drug}");
                playerInv.AddItem(invItem, -amountToSell);
            }
        }
    }
}
