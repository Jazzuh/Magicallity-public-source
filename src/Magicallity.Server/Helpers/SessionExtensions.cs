using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Server.Bank;
using Magicallity.Server.Models;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magicallity.Server.Helpers
{
    public static class SessionExtensions
    {
        static SessionExtensions()
        {
            Server.Instance.RegisterEventHandler("Session.UpdateLocation", new Action<Player, string>(OnRecieveLocation));
            //Server.Instance.RegisterEventHandler("Session.UpdatePositon", new Action<Player, List<object>>(OnRecievePosition));
        }

        public static async Task<string> GetLocation(this Session.Session playerSession)
        {
            var ticks = 0;
            playerSession.SetServerData("Player.Location", default(string));
            playerSession.TriggerEvent("Player.UpdateLocation");

            while (playerSession.GetServerData("Player.Location", default(string)) == default && ticks < 600)
            {
                await BaseScript.Delay(0);
                ticks++;
            }

            if (ticks >= 600) return "";

            return playerSession.GetServerData("Player.Location", "");
        }

        private static void OnRecieveLocation([FromSource] Player source, string location)
        {
            var playerSession = Server.Instance.Instances.Session.GetPlayer(source);

            playerSession.SetServerData("Player.Location", location);
        }

        /*public static async Task<Vector3> GetPosition(this Session.Session playerSession, bool updatePosition = true)
        {
        #if ONESYNC
            try
            {
                var playerPed = playerSession.GetPlayerPed();
                return CitizenFX.Core.Native.API.GetEntityCoords(playerPed);
            }
            catch (Exception e)
            {
                return Vector3.Zero;
            }
        #else
            if(updatePosition)
            {
                var ticks = 0;
                playerSession.SetServerData("Player.Position", default(Vector3));
                playerSession.TriggerEvent("Player.UpdatePosition");

                while (playerSession.GetServerData("Player.Position", default(Vector3)) == default && ticks < 600)
                {
                    await BaseScript.Delay(0);
                    ticks++;
                }

                if (ticks >= 600) return Vector3.Zero;
            }

            return playerSession.GetServerData("Player.Position", Vector3.Zero);
        #endif
        }

        private static void OnRecievePosition([FromSource] Player source, List<object> pos)
        {
            var playerSession = Server.Instance.Instances.Session.GetPlayer(source);

            playerSession.SetServerData("Player.Position", new Vector3(pos.Select(Convert.ToSingle).ToArray()));
        }

        public static PlayerInventory GetInventory(this Session.Session playerSession)
        {
            return new PlayerInventory(playerSession.GetGlobalData("Character.Inventory", ""), playerSession);
        }

        public static int GetCharId(this Session.Session playerSession)
        {
            return playerSession.GetGlobalData<int>("Character.CharID");
        }

        /*public static Dictionary<string, dynamic> GetPlayerSettings(this Session.Session playerSession)
        {
            var settings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(playerSession.GetLocalData("User.Settings", ""));
            return settings ?? new Dictionary<string, dynamic>();
        }

        public static void SetPlayerSettings(this Session.Session playerSession, Dictionary<string, dynamic> settings)
        {
            playerSession.SetLocalData("User.Settings", JsonConvert.SerializeObject(settings));
        }

        public static Dictionary<string, dynamic> GetCharacterSettings(this Session.Session playerSession)
        {
            var settings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(playerSession.GetGlobalData("Character.Settings", ""));
            return settings ?? new Dictionary<string, dynamic>();
        }

        public static void SetCharacterSettings(this Session.Session playerSession, Dictionary<string, dynamic> settings)
        {
            playerSession.SetGlobalData("Character.Settings", JsonConvert.SerializeObject(settings));
        }*/

        public static string GetCharacterName(this Session.Session playerSession)
        {
            return playerSession.GetGlobalData("Character.FirstName", "") + " " + playerSession.GetGlobalData("Character.LastName", "");
        }

        public static void UpdateChatTheme(this Session.Session playerSession, string theme)
        {
            if (theme == "default" || theme == "gtao")
            {
                var playerSettings = playerSession./*GetPlayerSettings()*/PlayerSettings;
                playerSettings["ChatTheme"] = theme;
                playerSession./*GetPlayerSettings()*/PlayerSettings = playerSettings;
                playerSession.RefreshChatTheme();

                Log.ToClient("[Info]", $"Updated chat theme to {theme}", ConstantColours.Info, playerSession.Source);
            }
        }

        public static void RefreshChatTheme(this Session.Session playerSession)
        {
            var playerSettings = playerSession./*GetPlayerSettings()*/PlayerSettings;
            var theme = playerSettings.ContainsKey("ChatTheme") ? playerSettings["ChatTheme"] : "default";

            playerSession.TriggerEvent($"chat:{(theme == "default" ? "clearThemes" : "reloadThemes")}");         
        }

        public static BankAccountModel GetBankAccount(this Session.Session playerSession, string bankId = "")
        {
            if (bankId != "")
            {
                //TODO get alternate bank accounts
                return null;
            }
            else
            {
                return Server.Instance.Get<BankHandler>().GetBank(playerSession./*GetCharId()*/CharId);
            }
        }

        public static bool IsInVehicle(this Session.Session playerSession)
        {
            return playerSession.GetServerData("Character.IsInVehicle", false);
        }

        public static Dictionary<string, string> GetEmoteBinds(this Session.Session playerSession)
        {
            var playerSettings = playerSession./*GetPlayerSettings()*/PlayerSettings;

            if(playerSettings.ContainsKey("EmoteBinds"))
            {
                if (playerSettings["EmoteBinds"].GetType() == typeof(JObject))
                    playerSettings["EmoteBinds"] = ((JObject)playerSettings["EmoteBinds"]).ToObject<Dictionary<string, string>>();
            }
            else
            {
                playerSettings["EmoteBinds"] = new Dictionary<string, string>();
            }

            return playerSettings["EmoteBinds"];
        }

        public static void AddBlip(this Session.Session playerSession, string blipName, Vector3 blipPosition, BlipOptions blipOptions = null)
        {
            playerSession.TriggerEvent("Blips.AddBlip", blipName, blipPosition.ToArray(), JsonConvert.SerializeObject(blipOptions));
        }

        public static void AddBlip(this Session.Session playerSession, string blipName, List<Vector3> blipPositions, BlipOptions blipOptions = null)
        {
            playerSession.TriggerEvent("Blips.AddBlips", blipName, blipPositions.Select(o => o.ToArray()).ToArray(), JsonConvert.SerializeObject(blipOptions));
        }

        public static void AddMarker(this Session.Session playerSession, Vector3 markerPosition, MarkerOptions markerOptions = null)
        {
            playerSession.TriggerEvent("Markers.AddMarker", markerPosition.ToArray(), JsonConvert.SerializeObject(markerOptions));
        }

        public static void AddMarker(this Session.Session playerSession, List<Vector3> markerPosition, MarkerOptions markerOptions = null)
        {
            playerSession.TriggerEvent("Markers.AddMarkers", markerPosition.Select(o => o.ToArray()).ToArray(), JsonConvert.SerializeObject(markerOptions));
        }

        public static void RemoveMarker(this Session.Session playerSession, string markerId)
        {
            playerSession.TriggerEvent("Markers.RemoveMarker", markerId);
        }

        public static void RemoveMarkers(this Session.Session playerSession, List<string> markerIds) => markerIds.ForEach(playerSession.RemoveMarker);
    }
}
