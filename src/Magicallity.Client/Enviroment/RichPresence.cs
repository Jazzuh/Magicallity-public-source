using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace Magicallity.Client.Enviroment
{
    class RichPresence : ClientAccessor
    {
        public RichPresence(Client client) : base(client)
        {
            SetDiscordAppId("505555372229263380");
            SetDiscordRichPresenceAsset("magic-logo");
            //SetRichPresence("64 players");

            client.RegisterEventHandler("Presence.SetPlayerCount", new Action<int>(count => SetRichPresence($"{count} players")));
        }
    }
}
