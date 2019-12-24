using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Emotes;
using static CitizenFX.Core.Native.API;
using Magicallity.Client.Helpers;
using Magicallity.Client.Player;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Newtonsoft.Json;

namespace Magicallity.Client.Vehicles
{
    public class VehicleRepairKit : ClientAccessor
    {
        public VehicleRepairKit(Client client) : base(client)
        {
            client.RegisterEventHandler("Vehicle.StartRepair", new Action(OnVehicleRepair));
        }

        private async void OnVehicleRepair()
        {
            var closeVeh = GTAHelpers.GetClosestVehicle();

            if (closeVeh != null)
            {
                Log.ToChat("[Inventory]", "Repairing vehicle", ConstantColours.Inventory);
                EmoteManager.playerAnimations["mechanic"].PlayFullAnim();

                await BaseScript.Delay(new Random((int)DateTime.Now.Ticks).Next(3500, 7500));

                var playerInv = await LocalSession.GetInventory();
                if (closeVeh.Position.DistanceToSquared(Game.PlayerPed.Position) < Math.Pow(3, 2) && playerInv.HasItem("RepKit"))
                {
                    if (closeVeh.EngineHealth < 150)
                        closeVeh.EngineHealth = 150;

                    if (closeVeh.BodyHealth < 150)
                        closeVeh.BodyHealth = 150;

                    SetVehicleTyreFixed(closeVeh.Handle, 0);
                    SetVehicleTyreFixed(closeVeh.Handle, 1);
                    SetVehicleTyreFixed(closeVeh.Handle, 2);
                    SetVehicleTyreFixed(closeVeh.Handle, 3);
                    SetVehicleTyreFixed(closeVeh.Handle, 4);
                    SetVehicleTyreFixed(closeVeh.Handle, 5);

                    Log.ToChat("[Inventory]", "Vehicle repaired", ConstantColours.Inventory);

                    Client.TriggerServerEvent("Inventory.AddInvItem", JsonConvert.SerializeObject(playerInv.GetItem("RepKit")), -1);
                }
                else
                {
                    Log.ToChat("[Inventory]", "You moved to far away from the vehicle", ConstantColours.Inventory);
                }

                EmoteManager.playerAnimations["mechanic"].End(Game.PlayerPed);
            }
        }
    }
}
