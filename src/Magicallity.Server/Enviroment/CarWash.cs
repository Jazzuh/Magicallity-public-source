using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

using Magicallity.Server.Bank;
using Magicallity.Server.Helpers;
using Magicallity.Server.Session;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;

namespace Magicallity.Server.Enviroment
{
    public class CarWash : ServerAccessor
    {
        private List<Vector3> carWashLocaitons = new List<Vector3>
        {
            new Vector3(28.548f, -1391.905f, 28.5f),
            new Vector3(1026.75f, 2660.65f, 38.95f),
            new Vector3(-699.62f, -933.64f, 18.31f),
        };

        private int carWashPrice = 10;

        public CarWash(Server server) : base(server)
        {

        }

        public void OnCharacterLoaded(Session.Session playerSession)
        {
            playerSession.AddBlip("Car wash", carWashLocaitons, new BlipOptions
            {
                Sprite = 100
            });
            playerSession.AddMarker(carWashLocaitons, new MarkerOptions
            {
                ScaleFloat = 3.0f,
                ColorArray = ConstantColours.Yellow.ToArray()
            });
        }

        [EventHandler("Player.OnInteraction")]
        private void OnInteraction([FromSource] Player source)
        {
            var playerSession = Sessions.GetPlayer(source);

            if(playerSession == null) return;

            var playerPos = playerSession.Position;
            if (playerSession.IsInVehicle() && carWashLocaitons.Any(o => o.DistanceToSquared(playerPos) < 32.0f))
            {
                var payHandler = Server.Get<PaymentHandler>();

                if (payHandler.CanPayForItem(playerSession, carWashPrice))
                {
                    payHandler.PayForItem(playerSession, carWashPrice, "car wash");
                    Log.ToClient("[Car wash]", $"You paid ${carWashPrice} to wash your car", ConstantColours.Store, source);

                    source.TriggerEvent("Vehicle.WashVehicle");
                }
            }
        }
    }
}
