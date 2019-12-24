using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Server.Helpers;
using Magicallity.Server.Housing;
using Magicallity.Server.Session;
using Magicallity.Server.UI;
using Magicallity.Shared;

namespace Magicallity.Server.Players.Clothing
{
    public class SkinHandler : ServerAccessor
    {
        private Vector3 skinCreationLocation = new Vector3(-277.80f, -960.67f, 85.31f);

        public SkinHandler(Server server) : base(server)
        {
            server.RegisterEventHandler("Skin.UpdatePlayerSkin", new Action<Player, string>(OnSkinUpdate));
            server.RegisterEventHandler("Skin.OnFinishInitialCreation", new Action<Player>(OnFinishCreation));
        }

        public async void OnCharacterLoaded(Session.Session playerSession)
        {
            if (playerSession.GetGlobalData("Character.SkinData", "") == "")
            {
                Log.Debug($"{playerSession.PlayerName} is starting initial creation of a character");
                await playerSession.Transition(600, 1500, 600);
                playerSession.SetGlobalData("Character.Instance", playerSession.ServerID);
                playerSession.SetServerData("Character.Skin.PreviousLocation", playerSession.GetPlayerPosition());
                playerSession.SetPlayerPosition(skinCreationLocation);
                playerSession.SetPlayerHeading(331.0f);
                playerSession.TriggerEvent("Skin.StartCharacterCreation");
                
                playerSession.SetFreezeStatus(true);
            }
        }

        private void OnSkinUpdate([FromSource] Player source, string skinData)
        {
            var playerSession = Sessions.GetPlayer(source);

            if (playerSession == null) return;

            playerSession.SetGlobalData("Character.SkinData", skinData);
        }

        private async void OnFinishCreation([FromSource] Player source)
        {
            Log.Debug($"{source.Name} just finished inital creation of a character");
            var playerSession = Sessions.GetPlayer(source);

            if (playerSession == null) return;

            await playerSession.Transition(600, 1500, 600);

            if (playerSession.GetGlobalData("Character.Home", 0) != 0)
            {
                Server.Get<HousingManager>().OnCharacterLoaded(playerSession);
            }
            else
            {
                playerSession.SetPlayerPosition(playerSession.GetServerData("Character.Skin.PreviousLocation", Vector3.Zero));
            }
            playerSession.SetGlobalData("Character.Instance", 0);
            playerSession.SetFreezeStatus(false);
        }
    }
}
