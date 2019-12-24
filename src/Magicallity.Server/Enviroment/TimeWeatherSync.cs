using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Server.Enums;
using Magicallity.Shared;

namespace Magicallity.Server.Enviroment
{
    public class TimeWeatherSync : ServerAccessor
    {
        public bool FreezeTime = false;
        public int CurrentHour { get; private set; } = 12;
        public int CurrentMinute { get; private set; } = 0;

        public string CurrentWeather { get; set; } = "ExtraSunny";
        private List<string> weatherTypes = new List<string>
        {
            "ExtraSunny",
            "Clear",
            "Clouds",
            "Smog",
            "Foggy",
            "Overcast",
            //"Raining",
            //"ThunderStorm",
            "Clearing",
            "Neutral",
        };
        private Random rand => new Random((int)DateTime.Now.Ticks);

        public TimeWeatherSync(Server server) : base(server)
        {
            server.RegisterEventHandler("Sync.RequestTime", new Action<Player>(OnRequestTime));
            server.RegisterEventHandler("Sync.RequestWeather", new Action<Player>(OnRequestWeather));
            server.RegisterTickHandler(TimeTick);
            server.RegisterTickHandler(WeatherTick);

            CommandRegister.RegisterAdminCommand("freezetime", OnFreezeTime, AdminLevel.SuperAdmin);
            CommandRegister.RegisterAdminCommand("time", OnSetTime, AdminLevel.SuperAdmin);
            CommandRegister.RegisterAdminCommand("weather", OnSetWeather, AdminLevel.Admin);
        }

        private void OnRequestTime([FromSource] Player source)
        {
            source.TriggerEvent("Sync.ReceiveTime", CurrentHour, CurrentMinute);
        }

        private void OnRequestWeather([FromSource] Player source)
        {
            source.TriggerEvent("Sync.ReceiveWeather", CurrentWeather);
        }

        private async Task TimeTick()
        {
            if (!FreezeTime)
            {
                CurrentMinute++;

                if (CurrentMinute > 59)
                {
                    CurrentMinute = 0;
                    CurrentHour++;

                    if (CurrentHour > 23)
                    {
                        CurrentHour = 0;
                    }
                }
            }

            if (CurrentHour >= 6 && CurrentHour < 21) // day
            {
                await BaseScript.Delay(12000);
            }
            else // night
            {
                await BaseScript.Delay(8000);
            }

            BaseScript.TriggerClientEvent("Sync.ReceiveTime", CurrentHour, CurrentMinute);
        }

        private async Task WeatherTick()
        {
            await BaseScript.Delay(rand.Next(120000, 1200000));

            CurrentWeather = weatherTypes[rand.Next(0, weatherTypes.Count - 1)];
            Log.Verbose($"Automatically setting weather to {CurrentWeather}");

            BaseScript.TriggerClientEvent("Sync.ReceiveWeather", CurrentWeather);
        }

        private void OnFreezeTime(Command cmd)
        {
            var freezeArg = cmd.GetArgAs(0, "");
 
            if (freezeArg == "true")
                FreezeTime = true;
            else if (freezeArg == "false")
                FreezeTime = false;
            else
                FreezeTime = !FreezeTime;

            Log.Info($"Time has been {(FreezeTime ? "frozen" : "unfrozen")}");
        }

        private void OnSetTime(Command cmd)
        {
            var hour = cmd.GetArgAs(0, 0);
            var minute = cmd.GetArgAs(1, 0);

            if (hour > 23 || hour < 0)
                hour = 0;

            if (minute > 59 || minute < 0)
                minute = 0;

            CurrentHour = hour;
            CurrentMinute = minute;

            BaseScript.TriggerClientEvent("Sync.ReceiveTime", CurrentHour, CurrentMinute);
        }

        private void OnSetWeather(Command cmd)
        {
            var weatherString = cmd.GetArgAs(0, "");

            if (weatherString == "" || !weatherTypes.Contains(weatherString))
                CurrentWeather = weatherTypes[rand.Next(0, weatherTypes.Count - 1)];
            else
                CurrentWeather = weatherString;

            Log.Info($"Set weather to {CurrentWeather}");
            BaseScript.TriggerClientEvent("Sync.ReceiveWeather", CurrentWeather);
        }
    }
}
