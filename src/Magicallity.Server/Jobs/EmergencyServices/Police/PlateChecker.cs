using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Server.Helpers;
using Magicallity.Server.Session;
using Magicallity.Server.Vehicle;
using Magicallity.Server.HTTP;
using Magicallity.Shared;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Helpers;
using Newtonsoft.Json;

namespace Magicallity.Server.Jobs.EmergencyServices.Police
{
    public class PlateChecker : JobClass
    {
        private Dictionary<string, string> vehiclePlateOwners = new Dictionary<string, string>();

        public PlateChecker()
        {
            CommandRegister.RegisterJobCommand("runplate", OnRunPlate, JobType.Police);
        }

        public async void CreateRandomName(string plate, Action<string> cb)
        {
            // TODO pick from different regions
            if(!vehiclePlateOwners.ContainsKey(plate))
            {
                try
                {
                    var webRequest = new HTTPRequest();
                    var response = await webRequest.Request("https://uinames.com/api/?amount=1&region=England", "GET", $"");
                    dynamic nameJSON = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(response.content);

                    vehiclePlateOwners[plate] = $"{nameJSON.name} {nameJSON.surname}";
                    cb($"{nameJSON.name} {nameJSON.surname}");
                }
                catch{ }
            }
            else
            {
                cb(vehiclePlateOwners[plate]);
            }
        }

        private void OnRunPlate(Command cmd)
        {
            var plate = cmd.GetArgAs(0, "");

            var vehManager = Server.Get<VehicleManager>();
            var veh = vehManager.GetVehicleByPlate(plate);

            if (veh != null && (veh.VehID < 1000000 || veh.VehID >= 1000000 && veh.RentedVehicle))
            {
                var vehicleOwner = veh.VehicleOwner;

                Log.ToClient("[Radar]", $"The plate {plate.ToUpper()} is registered to {vehicleOwner.GetCharacterName()}; DOB - {vehicleOwner.GetGlobalData("Character.DOB", "")}", ConstantColours.Dispatch, cmd.Player);
            }
            else
            {
                CreateRandomName(plate.ToUpper(), name =>
                {
                    Log.ToClient("[Radar]", $"The plate {plate.ToUpper()} is registered to {name}", ConstantColours.Dispatch, cmd.Player);
                });
            }        
        }
    }
}
