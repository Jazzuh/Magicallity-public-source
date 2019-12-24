using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Helpers;
using Magicallity.Shared;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Helpers;

namespace Magicallity.Client.Jobs.EmergencyServices
{
    public class SharedEmergencyItems : JobClass
    {
        public SharedEmergencyItems()
        {
            Client.RegisterEventHandler("Job.FixVehicle", new Action(OnFixVehicle));
            Client.RegisterEventHandler("Job.SetVehicleExtra", new Action<string, string>(OnSetVehicleExtra));
            Client.RegisterEventHandler("Player.OnDutyStatusChange", new Action<bool>(OnDutyChange));
        }

        public void OnDutyChange(bool state)
        {
            if (state && JobHandler.GetPlayerJob() == JobType.Police)
            {
                Game.PlayerPed.Armor = 100;
            }
        }

        [EventHandler("Job.DeleteVehicle")]
        private void OnDeleteVehicle(bool impound)
        {
            var closeVeh = GTAHelpers.GetClosestVehicle();

            if (closeVeh != null)
            {
                if (closeVeh.HasDecor("Vehicle.ID") /*&& JobHandler.OnDutyAsJob(JobType.Police)*/)
                {
                    var vehId = closeVeh.GetDecor<int>("Vehicle.ID");
                    if (vehId < 1000000 && JobHandler.OnDutyAsJob(JobType.Police))
                    {
                        if(impound)
                        {
                            Client.TriggerServerEvent("Vehicle.ImpoundVehicle", vehId);
                        }
                        closeVeh.Delete();
                    }
                    else if (vehId > 1000000)
                    {
                        closeVeh.Delete();
                    }
                }
                else
                {
                    closeVeh.Delete();
                }
            }
        }

        private void OnFixVehicle()
        {
            var closeVeh = GTAHelpers.GetClosestVehicle();

            if (closeVeh != null)
            {
                closeVeh.Repair();

                Log.ToChat("[Job]", "Fixed vehicle", ConstantColours.Job);
            }
        }

        private void OnSetVehicleExtra(string extra, string state)
        {
            var playerVeh = Game.PlayerPed.CurrentVehicle;

            if (playerVeh != null && playerVeh.ClassType == VehicleClass.Emergency)
            {
                var enableExtra = state == "true";

                if (extra == "all")
                {
                    for (var i = 0; i < 50; i++)
                    {
                        playerVeh.ToggleExtra(i, enableExtra);
                    }
                }
                else
                {
                    playerVeh.ToggleExtra(Convert.ToInt32(extra), enableExtra);
                }
            }
        }
    }
}
