using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Magicallity.Client.Jobs;
using Magicallity.Shared.Enums;

namespace Magicallity.Client.UI.Jobs
{
    internal static class VehicleLoadoutPresets
    {
        public static List<string> serviceVehicles = new List<string>
        {
            "police", "police2"
        };

        private struct ModSetting
        {
            public VehicleModType modType;
            public int modIndex;
            public bool modStatus;
        }

        private struct VehicleSetting
        {
            public Dictionary<int, int> Extras;
            public int Livery;
        }

        private static Dictionary<string, VehicleSetting> VehicleSettings = new Dictionary<string, VehicleSetting>();

        private static List<ModSetting> ModSettings = new List<ModSetting>()
        {
            new ModSetting { modType = VehicleModType.Engine, modIndex = 2, modStatus = false },
            new ModSetting { modType = VehicleModType.Brakes, modIndex = 2, modStatus = false },
            new ModSetting { modType = VehicleModType.Transmission, modIndex = 2, modStatus = false },
            new ModSetting { modType = VehicleModType.Suspension, modIndex = 3, modStatus = false },
            new ModSetting { modType = VehicleModType.Armor, modIndex = 4, modStatus = false }
        };

        static VehicleLoadoutPresets()
        {
            Client.Instance.RegisterEventHandler("Player.OnDutyStatusChange", new Action<bool>(onDuty =>
            {
                if (Client.Instance.Get<JobHandler>().OnDutyAsJob(JobType.Police))
                {
                    serviceVehicles = new List<string>
                    {
                        "othercvpi",
                        "slickcvpi",
                        "police",
                        "police11",
                        "police12",
                        "police13",
                        "police14",
                        "police15",
                        "police16",
                        "police19",
                        "police20",
                        "police5",
                        "police6",
                        "police7",
                        "policeb2",
                        "policeb3",
                        "polmav",
                        "pranger2",
                        "2015polstang",
                        "fbi",
                        "sheriff"
                    };
                }
                else if (Client.Instance.Get<JobHandler>().OnDutyAsJob(JobType.EMS))
                {
                    serviceVehicles = new List<string>
                    {
                        "ambulance",
                        "emsexplorer",
                        "emscv",
                        "emsimpala",
                        "emssuv",
                        "emsvan",
                        "EMS2",
                        "EMS3",
                        "firetruk",
                        "polmav"
                    };
                }

                EmergencyServicesVehicleMenu.RemakeHashList();
            }));
        }
    }
}