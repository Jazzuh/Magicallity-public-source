using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Helpers;
using Magicallity.Shared.Models;

namespace Magicallity.Client.Vehicles
{
    public class VehicleSeller : ClientAccessor
    {
        private Vector3 vehicleSellerLocaiton = new Vector3(-46.05f, -1080.27f, 25.99f);
        private int sellAttempts = 0;

        public VehicleSeller(Client client) : base(client)
        {
            client.RegisterEventHandler("Player.CheckForInteraction", new Action(OnInteraciton));

            MarkerHandler.AddMarker(vehicleSellerLocaiton, new MarkerOptions
            {
                ScaleFloat = 3.0f
            });
        }

        private void OnInteraciton()
        {
            var currentVeh = Cache.PlayerPed.CurrentVehicle;
            if (Cache.PlayerPed.Position.DistanceToSquared(vehicleSellerLocaiton) < 48.0f && Cache.PlayerPed.IsInVehicle() && currentVeh.Driver == Cache.PlayerPed)
            {
                var closestOwnedVeh = Client.Get<VehicleHandler>().GetClosestOwnedVehicle();

                if(closestOwnedVeh == null || currentVeh != closestOwnedVeh) return;
                
                attemptSellVehicle();
            }
        }

        private async void attemptSellVehicle()
        {
            var playerVeh = Cache.PlayerPed.CurrentVehicle;
            var vehId = playerVeh.HasDecor("Vehicle.ID") ? playerVeh.GetDecor<int>("Vehicle.ID") : -1;

            if (playerVeh.Occupants.Length > 1 || vehId == -1) return;

            if (sellAttempts == 0)
            {
                Client.TriggerServerEvent("Vehicle.ShowVehicleSellPrice", vehId);

                sellAttempts++;

#pragma warning disable 4014
                Task.Factory.StartNew(async () =>
#pragma warning restore 4014
                {
                    await BaseScript.Delay(15000);
                    sellAttempts = 0;
                });
            }
            else
            {
                sellAttempts++;

                Client.TriggerServerEvent("Vehicle.SellVehicle", vehId);

                Cache.PlayerPed.Task.LeaveVehicle();

                playerVeh.IsDriveable = false;
                playerVeh.LockStatus = VehicleLockStatus.CannotBeTriedToEnter;
                playerVeh.SetDecor("Vehicle.ID", -1);

                await BaseScript.Delay(1500);

                playerVeh.Position = Vector3.Zero;
                playerVeh.Delete();
            }
        }
    }
}
