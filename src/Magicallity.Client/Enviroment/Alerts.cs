using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;

namespace Magicallity.Client.Enviroment
{
    public class Alerts : ClientAccessor
    {
        private float maxMessageDistance = 45.0f;

        public Alerts(Client client) : base(client)
        {
            client.RegisterEventHandler("Alerts.SendMeMessage", new Action<int, string>(OnMeMessage));
            client.RegisterEventHandler("Alerts.SendDoMessage", new Action<int, string>(OnDoMessage));
        }

        private void OnMeMessage(int targetPlayer, string message)
        {
            var sourcePlayerChar = Client.PlayerList.FirstOrDefault(o => o.ServerId == targetPlayer)?.Character;

            if(sourcePlayerChar == null) return;

            if(Game.PlayerPed.Position.DistanceToSquared(sourcePlayerChar.Position) <= maxMessageDistance)    
                Log.ToChat("", $"^6{message}");
        }

        private void OnDoMessage(int targetPlayer, string message)
        {
            var sourcePlayerChar = Client.PlayerList.FirstOrDefault(o => o.ServerId == targetPlayer)?.Character;

            if (sourcePlayerChar == null) return;

            if (Game.PlayerPed.Position.DistanceToSquared(sourcePlayerChar.Position) <= maxMessageDistance)
                Log.ToChat("Action:", $"{message}", ConstantColours.Do);
        }
    }
}
