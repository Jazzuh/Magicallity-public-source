using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Shared.Helpers;

namespace Magicallity.Client.Enviroment
{
    public class Warps : ClientAccessor
    {
        public Warps(Client client) : base(client)
        {
            var dirtyMoneyWarp = new WarpPoint(new Vector3(2818.56f, 1463.43f, 24.74f), new Vector3(1118.71f, -3193.71f, -41.35f), ConstantColours.Red);
            var sandyCellWarp = new WarpPoint(new Vector3(1848.86f, 3689.84f, 33.35f), new Vector3(1827.66f, 3687.15f, 26.55f), ConstantColours.Yellow);
        }
    }
}
