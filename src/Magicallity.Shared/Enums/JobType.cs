using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicallity.Shared.Enums
{
    [Flags]
    public enum JobType
    {
        Civillian = 1,
        Police = 2,
        EMS = 4,
        Mechanic = 8,
        Tow = 16,
        Taxi = 32,
        Delivery = 64,
        Realtor = 128
    }
}
