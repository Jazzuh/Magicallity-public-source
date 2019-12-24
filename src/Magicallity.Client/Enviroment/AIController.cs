using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Magicallity.Client.Enviroment
{
    class AIController : ClientAccessor
    {
        private float pedDensityMult = 0.7f;
        private float weaponDamageMult = 0.5f;
        private float vehicleDensityMult = 0.5f;
        private float parkedVehDensity = 0.6f;
        private float scenarioPedDensity = 0.6f;

        public AIController(Client client) : base(client)
        {
            client.RegisterTickHandler(OnTick);
            client.RegisterTickHandler(RefreshValuesTick);
        }

        private async Task OnTick()
        {
            SetPedDensityMultiplierThisFrame(pedDensityMult);
            SetPlayerWeaponDamageModifier(PlayerId(), weaponDamageMult);
            SetPedAccuracy(Cache.PlayerPed.Handle, 20);
            SetPlayerHealthRechargeMultiplier(PlayerId(), 0.0f);
            SetVehicleDensityMultiplierThisFrame(vehicleDensityMult);
            SetParkedVehicleDensityMultiplierThisFrame(parkedVehDensity);
            SetScenarioPedDensityMultiplierThisFrame(scenarioPedDensity, scenarioPedDensity);
            
            SetGarbageTrucks(false);
            SetRandomBoats(false);
            EnableDispatchService(1, false);
            EnableDispatchService(2, false);
            EnableDispatchService(3, false);
            EnableDispatchService(4, false);
            EnableDispatchService(5, false);
            EnableDispatchService(6, false);
            EnableDispatchService(7, false);
            EnableDispatchService(8, false);
            EnableDispatchService(9, false);
            EnableDispatchService(10, false);
            EnableDispatchService(11, false);
            EnableDispatchService(12, false);
            EnableDispatchService(13, false);
            EnableDispatchService(14, false);
            EnableDispatchService(15, false);
            //ClearPlayerWantedLevel(PlayerId());
        }

        private async Task RefreshValuesTick()
        {
            pedDensityMult = GetConvarInt("mg_pedDensityMult", 100) / 100.0f;
            weaponDamageMult = GetConvarInt("mg_weaponDamageMult", 100) / 100.0f;
            vehicleDensityMult = GetConvarInt("mg_vehicleDensityMult", 50) / 100.0f;
            parkedVehDensity = GetConvarInt("mg_parkedVehDensity", 100) / 100.0f;
            scenarioPedDensity = GetConvarInt("mg_scenarioPedDensity", 100) / 100.0f;
            await BaseScript.Delay(GetConvarInt("mg_aiRefreshTime", 300000));
        }
    }
}
