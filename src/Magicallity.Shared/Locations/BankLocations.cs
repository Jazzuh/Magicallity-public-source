using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Magicallity.Shared.Locations
{
    public static class BankLocations
    {
        public static Dictionary<string, Vector3> Positions = new Dictionary<string, Vector3>
        {
            ["PillboxMain"] = new Vector3(149.8642f, -1040.2297f, 28.4f),
            ["PillboxVault"] = new Vector3(146.7505f, -1044.8401f, 28.3778f),

            ["PaletoMain"] = new Vector3(-113.5642f, 6469.3960f, 30.7f),
            ["PaletoVault"] = new Vector3(-103.7352f, 6477.9512f, 30.6267f),

            ["GreatOceanMain"] = new Vector3(-2963.234f, 482.989f, 14.8f),
            ["GreatOceanVault"] = new Vector3(-2957.642f, 481.828f, 14.697f),

            ["Route68Main"] = new Vector3(1175.14f, 2706.39f, 37.394f),
            ["Route68Vault"] = new Vector3(1176.6f, 2711.61f, 37.3978f),
        };
    }
}
