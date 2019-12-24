using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Helpers;
using Magicallity.Client.Jobs.EmergencyServices.Police;
using Magicallity.Client.Player;
using Magicallity.Client.UI.Classes;
using Magicallity.Shared.Enums;
using static CitizenFX.Core.Native.API;

namespace Magicallity.Client.Jobs.EmergencyServices.EMS
{
    public class Death : ClientAccessor
    {
        private Vector3 hospitalLocation = new Vector3(340.5515f, -1396.2421f, 32.5093f);
        private WarpPoint pillboxElevatorPoint = new WarpPoint(new Vector3(324.74f, -598.63f, 43.29f), new Vector3(355.68f, -596.38f, 28.77f));

        private static ScreenText deathString = new ScreenText("You have 600 seconds until respawn (/911 [message] for help)", 960, 540, 0.5f, async () =>
        {
            var playerSession = Client.LocalSession;
            var remainingTime = playerSession.GetLocalData("Death.DeathTimer", 600);
            if (remainingTime > 0)
                deathString.Caption = $"You have {remainingTime} seconds until respawn (/911 [message] for help)";
            else
                deathString.Caption = $"You are now able to respawn with /respawn but it will cost you ${playerSession.GetLocalData("Death.RespawnCost", 3000)}";

            await BaseScript.Delay(0);
        });

        public Death(Client client) : base(client)
        {
            client.RegisterEventHandler("Death.StartDeathThread", new Action(OnStartDeath));
            client.RegisterEventHandler("Death.EndDeathThread", new Action(OnEndDeath));
            client.RegisterEventHandler("Death.RespawnPlayer", new Action(OnPlayerRespawn));
            client.RegisterEventHandler("Death.FindReviveTarget", new Action(OnFindRevive));
            client.RegisterTickHandler(EnablePVPTick);
        }

        [EventHandler("Player.Heal")]
        private void OnHealPlayer()
        {
            Game.PlayerPed.Health = Game.PlayerPed.MaxHealth;
        }

        private void OnStartDeath()
        {
            var playerPed = Game.PlayerPed;
            ResurrectPed(playerPed.Handle);
            NetworkResurrectLocalPlayer(playerPed.Position.X, playerPed.Position.Y, playerPed.Position.Z, playerPed.Heading, true, false);
            Client.RegisterTickHandler(DeathTick);
        }

        private async void OnEndDeath()
        {
            var playerPed = Game.PlayerPed;
            Client.DeregisterTickHandler(DeathTick);
            playerPed.Task.ClearAllImmediately();
            ResurrectPed(playerPed.Handle);
            NetworkResurrectLocalPlayer(playerPed.Position.X, playerPed.Position.Y, playerPed.Position.Z, playerPed.Heading, true, false);
            await BaseScript.Delay(0);
            playerPed.Ragdoll(1000);
            playerPed.IsInvincible = false;
            playerPed.MaxHealth = 100;
            playerPed.Health = 100;
        }

        private void OnPlayerRespawn()
        {
            OnEndDeath();
            Game.PlayerPed.Position = hospitalLocation;
            Game.PlayerPed.Heading = 47.0f;
        }

        private async Task DeathTick()
        {
            var playerPed = Cache.PlayerPed;
            playerPed.IsInvincible = true;
            if(Client.Get<Arrest>().GetVehState(LocalSession) == VehState.OutVeh)
            {
                playerPed.CanRagdoll = true;
                playerPed.Ragdoll(30000);
            }
            Client.Get<Arrest>().DisableActions();
            Game.DisableControlThisFrame(1, Control.MoveUpOnly);
            Game.DisableControlThisFrame(1, Control.MoveDownOnly);
            Game.DisableControlThisFrame(1, Control.MoveLeftOnly);
            Game.DisableControlThisFrame(1, Control.MoveRightOnly);
            /*if(playerPed.IsInVehicle() && playerPed.CurrentVehicle.Driver == playerPed)
            {
                playerPed.Task.ClearAllImmediately();
                playerPed.Task.LeaveVehicle(LeaveVehicleFlags.WarpOut);
            }*/

            if(CinematicMode.InCinematicMode) return;
            deathString.DrawTick();
        }

        private void OnFindRevive()
        {
            var closestPlayer = GTAHelpers.GetClosestPlayer(4.0f);
            
            if(closestPlayer != null)
                Client.TriggerServerEvent("Death.SendReviveTarget", closestPlayer.ServerId);
        }

        private async Task EnablePVPTick()
        {
            await BaseScript.Delay(5000);
            Client.PlayerList.ToList().ForEach(o =>
            {
                SetCanAttackFriendly(o.Character.Handle, true, true);
                NetworkSetFriendlyFireOption(true);
            });
        }
    }
}
