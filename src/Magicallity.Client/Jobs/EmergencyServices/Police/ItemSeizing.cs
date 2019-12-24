using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicallity.Client.Helpers;
using Magicallity.Shared;
using Magicallity.Shared.Enums;

namespace Magicallity.Client.Jobs.EmergencyServices.Police
{
    public class ItemSeizing : JobClass
    {
        public ItemSeizing()
        {
            CommandRegister.RegisterCommand("seizevehitems|seizevehicleitems", OnSeizeVehItems);
        }

        private void OnSeizeVehItems(Command cmd)
        {
            if (JobHandler.OnDutyAsJob(JobType.Police))
            {
                var closeVeh = GTAHelpers.GetClosestVehicle(3.0f, o => o.HasDecor("Vehicle.ID"));

                if (closeVeh != null)
                {
                    Client.TriggerServerEvent("Items.SeizeVehicleItems", closeVeh.GetDecor<int>("Vehicle.ID"), string.Join(" ", cmd.Args));
                }
            }
        }
    }
}
