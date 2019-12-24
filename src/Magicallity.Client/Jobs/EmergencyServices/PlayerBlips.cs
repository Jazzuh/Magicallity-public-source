using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Shared;
using Magicallity.Shared.Attributes;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Loader;

namespace Magicallity.Client.Jobs.EmergencyServices
{
    public class PlayerBlips : JobClass
    {
        private Dictionary<JobType, BlipColor> jobColours = new Dictionary<JobType, BlipColor>
        {
            {JobType.EMS, BlipColor.Red},
            {JobType.Police, BlipColor.Blue}
        };
        private Dictionary<int, Blip> currentDutyBlips = new Dictionary<int, Blip>();

        public PlayerBlips()
        {

        }

        [EventHandler("Player.OnDutyStatusChange")]
        private void OnDutyStateChange(bool state)
        {
            if (jobColours.ContainsKey(JobHandler.GetPlayerJob()))
            {
                if (state)
                {
                    Log.Verbose($"Gone on duty adding duty blips tick");
                    Client.RegisterTickHandler(BlipHandlerTick);
                }
                else
                {
                    Log.Verbose($"Gone off duty removing duty blips tick");
                    Client.DeregisterTickHandler(BlipHandlerTick);
                    foreach (var kvp in currentDutyBlips)
                    {
                        kvp.Value.Delete();
                    }
                    currentDutyBlips.Clear();
                }
            }
        }

        private async Task BlipHandlerTick()
        {
            foreach (var player in Sessions.PlayerList)
            {
                var playerJob = JobHandler.GetPlayerJob(player);

                if(!jobColours.TryGetValue(playerJob, out var blipColour)) continue;

                var playerPed = player.Player.Character;
                var hasBlip = currentDutyBlips.TryGetValue(player.ServerID, out var blip);

                if (hasBlip && !playerPed.IsInVehicle())
                {
                    Log.Verbose($"Removing duty blip for {player.Player.Name}");
                    blip.Delete();
                    currentDutyBlips.Remove(player.ServerID);
                }

                if (!hasBlip && playerPed.IsInVehicle() && playerPed.CurrentVehicle.ClassType == VehicleClass.Emergency)
                {
                    Log.Verbose($"Creating duty blip for {player.Player.Name}");
                    var newBlip = playerPed.CurrentVehicle.AttachBlip();
                    newBlip.Name = $"{player.GetGlobalData("Character.FirstName", "")} {player.GetGlobalData("Character.LastName", "")}";
                    newBlip.Color = blipColour;
                    currentDutyBlips[player.ServerID] = newBlip;
                }
            }
        }
    }
}
