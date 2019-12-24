using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace Magicallity.Client.Enviroment
{
    public class SoundController : ClientAccessor
    {
        public SoundController(Client client) : base(client)
        {
            client.RegisterEventHandler("Sound.PlaySoundFrontend", new Action<string, string>(PlayFrontentSound));
        }

        public void PlayFrontentSound(string audioName, string audioRef)
        {
            PlaySoundFrontend(-1, audioName, audioRef, false);
        }
    }
}
