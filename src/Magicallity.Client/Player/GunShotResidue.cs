using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicallity.Shared;
using Magicallity.Shared.Attributes;
using Magicallity.Shared.Enums;

namespace Magicallity.Client.Player
{
    public class GunShotResidue : ClientAccessor
    {
        public GunShotResidue(Client client) : base(client)
        {

        }

        [DynamicTick(TickUsage.Shooting)]
        private async Task AddResidueTick()
        {
            
        }
    }
}
