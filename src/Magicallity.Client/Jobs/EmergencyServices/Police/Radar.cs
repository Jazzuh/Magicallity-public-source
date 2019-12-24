using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using Magicallity.Client.Helpers;
using Magicallity.Client.Player.Controls;
using Magicallity.Client.UI.Classes;
using Magicallity.Shared;
using Magicallity.Shared.Attributes;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Loader;
using Font = CitizenFX.Core.UI.Font;

namespace Magicallity.Client.Jobs.EmergencyServices.Police
{
    public class Radar : JobClass
    {
        private bool radarEnabled = false;
        private bool radarFrozen = false;
        private ScreenText frontVehText = new ScreenText("", 828, 963, 0.4f, null, Color.FromArgb(255, 255, 255), Font.ChaletLondon, Alignment.Center);
        private ScreenText backVehText = new ScreenText("", 828, 1006, 0.4f, null, Color.FromArgb(255, 255, 255), Font.ChaletLondon, Alignment.Center);

        public Radar()
        {
            Client.RegisterEventHandler("Police.ToggleRadar", new Action(ToggleRadar));
            CommandRegister.RegisterCommand("radar", cmd =>
            {
                ToggleRadar();
            });
        }

        public void ToggleRadar()
        {
            radarEnabled = !radarEnabled;

            Log.ToChat("[Police]", $"Radar {(radarEnabled ? "enabled" : "disabled")}", ConstantColours.Police);
        }

        private Vehicle getRadarVehicle(float distance)
        {
            var playerVeh = Game.PlayerPed.CurrentVehicle;
            var baseOffset = GetOffsetFromEntityInWorldCoords(playerVeh.Handle, 0.0f, 1.0f, 1.0f);
            var captureOffset = GetOffsetFromEntityInWorldCoords(playerVeh.Handle, 0.0f, distance, 0.0f);
            /*var rayHandle = StartShapeTestCapsule(baseOffset.Z, baseOffset.Y, baseOffset.Z, captureOffset.X, captureOffset.Y, captureOffset.Z, 3.0f, 10, playerVeh.Handle, 7);

            bool hitEntity = false;
            Vector3 endCoords = Vector3.Zero;
            Vector3 surfaceNormal = Vector3.Zero;
            int entityHit = 0;
            var rayResult = GetShapeTestResult(rayHandle, ref hitEntity, ref endCoords, ref surfaceNormal, ref entityHit);*/
            var data = World.RaycastCapsule(baseOffset, captureOffset, 3.0f, (IntersectOptions)10, playerVeh);

            return data.DitHitEntity ? data.HitEntity as Vehicle : null;
        }

        [DynamicTick(TickUsage.InVehicle)]
        private async Task RadarTick()
        {
            if (Cache.PlayerPed.CurrentVehicle.ClassType != VehicleClass.Emergency || !JobHandler.OnDutyAsJob(JobType.Police)) return;

            var frontVeh = getRadarVehicle(105.0f);
            var backVeh = getRadarVehicle(-105.0f);

            if(radarEnabled)
            {
                if (frontVeh != null && !radarFrozen)
                {
                    frontVehText.Caption = $"F: {frontVeh.LocalizedName} | {frontVeh.Mods.LicensePlate} | {Math.Round(frontVeh.Speed * 2.23694f)} MPH";
                }

                if (backVeh != null && !radarFrozen)
                {
                    backVehText.Caption = $"B: {backVeh.LocalizedName} | {backVeh.Mods.LicensePlate} | {Math.Round(backVeh.Speed * 2.23694f)} MPH";
                }

                if (Input.IsControlJustPressed(Control.FrontendRdown))
                {
                    radarFrozen = !radarFrozen;

                    frontVehText.Color = radarFrozen ? ConstantColours.Green : Color.FromArgb(255, 255, 255);
                    backVehText.Color = radarFrozen ? ConstantColours.Green : Color.FromArgb(255, 255, 255);
                }

                frontVehText.DrawTick();
                backVehText.DrawTick();
            }

            if (Input.IsControlJustPressed(Control.Detonate))
            {
                if (frontVeh != null)
                {
                    ExecuteCommand($"runplate {frontVeh.Mods.LicensePlate}");
                }
            }
        }
    }
}
