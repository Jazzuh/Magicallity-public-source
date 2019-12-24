using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Models;
using Magicallity.Client.UI.Vehicle;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Housing;
using Magicallity.Shared.Models;
using Newtonsoft.Json;

namespace Magicallity.Client.Housing
{
    public class Housing : ClientAccessor
    {
        private List<Blip> currentHouseBlips = new List<Blip>();

        public Housing(Client client) : base(client)
        {
            client.RegisterEventHandler("Housing.SetGarageLocation", new Action<Vector3>(OnReceiveGarageLocation));
            client.RegisterEventHandler("Housing.ToggleHouseBlips", new Action<string>(OnToggleHouseBlips));
        }

        private void OnReceiveGarageLocation(Vector3 garagePos)
        {
            Client.Get<VehicleGarageMenu>().AddGarage("Home", garagePos);
            MarkerHandler.AddMarker(garagePos, new MarkerOptions
            {
                Color = Color.FromArgb(255, 69, 0),
                ScaleFloat = 1.5f
            });

            garagePos.Z += 0.5f;
            BlipHandler.AddBlip("Home garage", garagePos, new BlipOptions
            {
                IsShortRange = true,
                Sprite = BlipSprite.Garage
            });
        }

        private void OnToggleHouseBlips(string ownedHouseString)
        {
            var ownedHouses = JsonConvert.DeserializeObject<List<HousingDataModel>>(ownedHouseString);

            if (currentHouseBlips.Count == 0)
            {
                foreach (var house in HousingLocations.Locations)
                {
                    var houseBlip = World.CreateBlip(house.EntranceLocation);
                    houseBlip.Sprite = BlipSprite.Safehouse;
                    houseBlip.Scale = 0.8f;

                    if (ownedHouses.Any(o => o.HouseId == house.HouseId))
                    {
                        houseBlip.Color = BlipColor.Red;
                    }
                    currentHouseBlips.Add(houseBlip);
                }
            }
            else
            {
                currentHouseBlips.ForEach(o => o.Delete());
                currentHouseBlips.Clear();
            }
        }
    }
}
