using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using Magicallity.Client.Helpers;
using Magicallity.Client.Player;
using Magicallity.Client.UI.Classes;
using Magicallity.Shared;
using Magicallity.Shared.Attributes;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Loader;
using Font = CitizenFX.Core.UI.Font;

namespace Magicallity.Client.UI.Vehicle
{
    public class VehicleDisplay : ClientAccessor
    {
        private float leftBarOnePercent;
        private float rightBarOnePercent;

        private float currentFuelLevel = 80.0f;

        public ScreenText SpeedIndicator;
        public ScreenText MPHText;
        private Rect speedIndicatedBackground;

        private ScreenText fuelIndicator;
        private ScreenText fuelText;
        private Rect fuelIndicatorBackground;

        /*private Rect leftBoxOverlay;
        private Rect leftBoxBackground;

        private Rect rightBoxOverlay;
        private Rect rightBoxBackground;*/

        private ScreenText compassDirection;
        private ScreenText streetDisplay;
        private ScreenText crossingDisplay;
        /*= new ScreenText("0 MPH", 966, 1013, 0.4f, () =>
            {
                SpeedIndicator.Caption = $"{Math.Floor(Cache.PlayerPed.CurrentVehicle.Speed * 2.23694)} MPH";

                return Task.FromResult(0);
            }, Color.FromArgb(255, 255, 255), Font.ChaletLondon);*/
        public VehicleDisplay(Client client) : base(client)
        {
            speedIndicatedBackground = new Rect(255, 1022, 88, 32, null, Color.FromArgb(150, 0, 0, 0), true);
            SpeedIndicator = new ScreenText("0", 211, 1000, 0.64f, async () =>
            {
                SpeedIndicator.Caption = $"{Math.Floor(Cache.PlayerPed.CurrentVehicle.Speed * 2.23694)}";
            }, Color.FromArgb(255, 255, 255), Font.ChaletComprimeCologne, Alignment.Left);
            MPHText = new ScreenText("mph", 255, 1014, 0.4f, async () => {}, Color.FromArgb(255, 255, 255), Font.ChaletComprimeCologne, Alignment.Left);

            fuelIndicatorBackground = new Rect(255, 979, 88, 32, null, Color.FromArgb(150, 0, 0, 0), true);
            fuelIndicator = new ScreenText("0", 211, 956, 0.64f, null, Color.FromArgb(255, 255, 255), Font.ChaletComprimeCologne, Alignment.Left);
            fuelText = new ScreenText("fuel", 255, 970, 0.4f, async () => { }, Color.FromArgb(255, 255, 255), Font.ChaletComprimeCologne, Alignment.Left);

            client.RegisterTickHandler(RadarTick);

            /*client.RegisterEventHandler("baseevents:enteredVehicle", new Action<int, int, string>((veh, seat, name) => {
                client.RegisterTickHandler(VehicleTick);
            }));
            client.RegisterEventHandler("baseevents:leftVehicle", new Action<int, int, string>((veh, seat, name) => {
                client.DeregisterTickHandler(VehicleTick);
            }));*/

            compassDirection = new ScreenText("S", 28, 810, 0.9f, async () =>
            {
                compassDirection.Caption = GetCardinalDirection();
            }, Color.FromArgb(255, 255, 255), Font.ChaletLondon, Alignment.Left, true, true);

            streetDisplay = new ScreenText($"~b~{GetCrossingName(Cache.PlayerPed.Position)[0]}~w~ in ~y~{World.GetZoneLocalizedName(Cache.PlayerPed.Position)}", 85, 820, 0.29f, async () =>
            {
                streetDisplay.Caption = $"~b~{GetCrossingName(Cache.PlayerPed.Position)[0]}~w~ in ~y~{World.GetZoneLocalizedName(Cache.PlayerPed.Position)}";
            }, Color.FromArgb(255, 255, 255), Font.ChaletLondon, Alignment.Left, true, true); 

            crossingDisplay = new ScreenText($"Crossing ~y~{GetCrossingName(Cache.PlayerPed.Position)[1]}", 85, 844, 0.29f, async () =>
            {
                var crossingName = GetCrossingName(Cache.PlayerPed.Position)[1];
                if (crossingName != "")
                    crossingDisplay.Caption = $"Crossing ~y~{GetCrossingName(Cache.PlayerPed.Position)[1]}";
                else
                    crossingDisplay.Caption = "";
            }, Color.FromArgb(255, 255, 255), Font.ChaletLondon, Alignment.Left, true, true);
        }

        public List<string> GetCrossingName(Vector3 position)
        {
            uint streetName = 0;
            uint crossingRoad = 0;
            GetStreetNameAtCoord(position.X, position.Y, position.Z, ref streetName, ref crossingRoad);
            
            return new List<string>
            {
                GetStreetNameFromHashKey(streetName),
                GetStreetNameFromHashKey(crossingRoad)
            };
        }

        public string GetCardinalDirection()
        {
            float h = Game.PlayerPed.Heading;
            if (h >= 315f || h < 45f) return "N";
            else if (h >= 45f && h < 135f) return "W";
            else if (h >= 135f && h < 225f) return "S";
            else if (h >= 225f && h < 315f) return "E";
            else return "N";
        }

        [DynamicTick(TickUsage.InVehicle)]
        private async Task VehicleTick()
        {
            if (CinematicMode.InCinematicMode) return;

            var playerVeh = Cache.PlayerPed.CurrentVehicle;

            if (playerVeh == null) return;

            if (playerVeh.Driver == Cache.PlayerPed)
            {
                if (playerVeh.ClassType == VehicleClass.Cycles) return;

                var fuelLevel = playerVeh.HasDecor("Vehicle.Fuel") ? Math.Round(playerVeh.GetDecor<float>("Vehicle.Fuel")).ToString() : "100";

                fuelIndicator.Caption = fuelLevel;
            }

            speedIndicatedBackground.DrawTick();
            SpeedIndicator.DrawTick();
            MPHText.DrawTick();

            fuelIndicatorBackground.DrawTick();
            fuelIndicator.DrawTick();
            fuelText.DrawTick();

            compassDirection.DrawTick();
            streetDisplay.DrawTick();
            crossingDisplay.DrawTick();
        }

        private async Task RadarTick()
        {
            if (Cache.PlayerPed.IsInVehicle() && Cache.PlayerPed.CurrentVehicle.ClassType != VehicleClass.Cycles && !CinematicMode.InCinematicMode)
            {
                DisplayRadar(true);
                SetRadarBigmapEnabled(false, true);
            }
            else
            {
                DisplayRadar(false);
                SetRadarBigmapEnabled(true, true);

                DisplayRadar(false);
                SetRadarBigmapEnabled(false, true);
            }
        }
    }
}

