using System.Collections.Generic;
using System.Drawing;
using CitizenFX.Core;
using Magicallity.Client.Jobs.Bases;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Models;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;

namespace Magicallity.Client.Jobs.Civillian.Delivery
{
    public sealed class GoPostalDelivery : DeliveryJob
    {
        public GoPostalDelivery()
        {
            VehicleSpawnLocation = new Vector3(-312.197f, -1028.32f, 29.3851f);
            MarkerLocation = new Vector3(-313.591f, -1032.38f, 29.5351f);
            VehicleSpawnHeading = 80.0f;
            DeliveryLocations = new List<Vector3>
            {
                new Vector3(822.008f, -2143.18f, 28.8315f),
                new Vector3(851.084f, -1010.56f, 28.6684f),
                new Vector3(-669.031f, -952.288f, 21.1993f),
                new Vector3(-1300.12f, -384.517f, 36.5631f),
                new Vector3(241.541f, -43.0412f, 69.7362f),
                new Vector3(2554.91f, 288.592f, 108.461f),
                new Vector3(-1131.3f, 2698.17f, 18.8004f),
                new Vector3(1697.34f, 3744.89f, 34.0315f),
                new Vector3(-318.431f, 6082.66f, 31.4622f),
                new Vector3(-3186.73f, 1085.29f, 20.8402f),
            };
            DrawMarker();
            CreateBlip();
        }

        protected override void CreateBlip()
        {
            BlipHandler.AddBlip("Postal depot", MarkerLocation, new BlipOptions
            {
                Sprite = BlipSprite.GTAOMission
            });
        }
    }
}
