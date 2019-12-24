using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

using Magicallity.Server.Jobs;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using Newtonsoft.Json;

namespace Magicallity.Server.Vehicle
{
    public class VehicleGarageHandler : ServerAccessor
    {
        public List<GarageModel> GlobalGarages = new List<GarageModel>
        {
            new GarageModel
            {
                Name = "Public1",
                Location = new Vector3(215.246f, -791.41f, 29.8699f),
                AlternateDisplayName = "Public garage"
            },
            new GarageModel
            {
                Name = "Public2",
                Location = new Vector3(1881.36f, 3757.43f, 31.9999f),
                AlternateDisplayName = "Public garage"
            },
            new GarageModel
            {
                Name = "Public3",
                Location = new Vector3(126.467f, 6610.13f, 31.0497f),
                AlternateDisplayName = "Public garage"
            },
            new GarageModel
            {
                Name = "Public4",
                Location = new Vector3(274.11f, -330.86f, 43.92f),
                AlternateDisplayName = "Public garage"
            },
            new GarageModel
            {
                Name = "Public5",
                Location = new Vector3(-335.24f, -777.28f, 32.97f),
                AlternateDisplayName = "Public garage"
            },
            new GarageModel
            {
                Name = "Public6",
                Location = new Vector3(25.79f, -1714.82f, 28.3f),
                AlternateDisplayName = "Public garage"
            },
            new GarageModel
            {
                Name = "Impound",
                Location = new Vector3(-426.261f, -1690.44f, 18.2291f),
                BlipOptions = new BlipOptions
                {
                    Sprite = 50,
                    Colour = 1
                }
            }
        };

        private VehicleManager vehManager;
        public VehicleManager VehManager => vehManager ?? (vehManager = Server.Get<VehicleManager>());

        public VehicleGarageHandler(Server server) : base(server)
        {

        }

        public void OnCharacterLoaded(Session.Session playerSession)
        {
            playerSession.AddGarages(GlobalGarages);
        }

        [EventHandler("Vehicle.Garage.AttemptStoreVehicle")]
        private async void OnAttemptStoreVehicle([FromSource] Player source, int vehicleId, string vehicleMods/*, string garageName*/)
        {
            Log.Verbose($"Attempting to store vehicle #{vehicleId} for {source.Name}");
            try
            {
                var playerSession = Sessions.GetPlayer(source);

                if (playerSession == null) return;
                Log.Debug($"playerSession is not null");

                var garage = playerSession.GetClosestGarage();

                if (garage == null) return;
                Log.Debug($"garage is not null");

                Log.Verbose($"{source.Name} is next to garage {garage.Name} running storage checks");

                if (garage.MaxVehicles != -1) // do garage size check for this garage
                {
                    bool? canStoreVehicle = null;

                    MySQL.execute("SELECT Count(*) AS NumVehs FROM vehicle_data WHERE Garage = @garage AND CharID = @char AND VehID != @curveh", new Dictionary<string, dynamic> { { "@char", playerSession.CharId }, { "@curveh", vehicleId }, {"@garage", garage.Name} },
                        new Action<List<dynamic>>(count =>
                        {
                            Log.Debug($"Count of garage vehicles for location {garage.Name} is {count[0].NumVehs}");
                            canStoreVehicle = Convert.ToInt32(count[0].NumVehs) <= garage.MaxVehicles;
                        }));

                    var ticks = 0;
                    while (canStoreVehicle == null && ticks < 150)
                    {
                        await BaseScript.Delay(0);
                        ticks++;
                    }

                    if (canStoreVehicle != null && !(bool)canStoreVehicle)
                    {
                        playerSession.Message("[Garage]", $"You currently cannot store this vehicle here because this garage is at max capacity ({garage.MaxVehicles} vehicles)", ConstantColours.Green);
                        return;
                    }
                }

                playerSession.TriggerEvent("Vehicle.DeleteCurrentVehicle");
                playerSession.Message("[Garage]", "Storing vehicle", ConstantColours.Green);
                storeVehicle(vehicleId, vehicleMods, garage.Name);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        //[EventHandler("Vehicles.StoreOwnedVehicle")]
        private void storeVehicle(int vehicleId, string vehicleMods, string garage)
        {
            Log.Debug($"About to start storing vehicle ({vehicleId})");
            var veh = VehManager.GetVehicle(vehicleId);

            Log.Debug($"Vehicle plate -> {veh?.Plate}");
            if (veh == null) return;

            var oldMods = veh.Mods;

            veh.Mods = JsonConvert.DeserializeObject<VehicleDataModel>(vehicleMods);
            veh.Mods.VehicleFuel = oldMods.VehicleFuel;

            veh.Garage = garage;

            VehManager.SaveVehicle(veh, true);

            Log.Verbose($"Storing vehicle ({vehicleId}) at garage ({garage})");
        }
    }
}
