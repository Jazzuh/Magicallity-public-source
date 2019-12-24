using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicallity.Client.Enums
{
    [Flags]
    public enum ControlModifier
    {
        Any = -1,
        None = 0,
        Ctrl = 1 << 0,
        Alt = 1 << 1,
        Shift = 1 << 2
    }
}
