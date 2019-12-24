using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicallity.Shared;
using Magicallity.Shared.Models;
using Newtonsoft.Json;

namespace Magicallity.Client.Helpers
{
    public static class SessionExtensions
    {
        public static async Task<PlayerInventory> GetInventory(this Session.Session playerSession, bool updateInv = true)
        {
            if(updateInv)
                await playerSession.UpdateData("Character.Inventory");

            return new PlayerInventory(playerSession.GetGlobalData("Character.Inventory", ""), playerSession);
        }

        public static Dictionary<string, dynamic> GetPlayerSettings(this Session.Session playerSession)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(playerSession.GetLocalData("User.Settings", ""));
        }
    }
}
