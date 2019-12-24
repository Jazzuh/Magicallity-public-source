using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Magicallity.Client.Helpers;
using Magicallity.Shared.Helpers;

namespace Magicallity.Client.Jobs.EmergencyServices.Police
{
    public class Jail : ClientAccessor
    {
        private float[] previousPosition;
        private Vector3 previousPositionFull;
        private float[][] prisonPolygon =
        {
            new float[] {1767.076f, 2557.332f},
            new float[] {1781.502f, 2563.654f},
            new float[] {1777.738f, 2536.071f},
            new float[] {1758.665f, 2501.655f},
            new float[] {1714.147f, 2479.080f},
            new float[] {1689.400f, 2460.822f},
            new float[] {1645.425f, 2479.439f},
            new float[] {1607.681f, 2492.532f},
            new float[] {1593.306f, 2546.877f},
            new float[] {1606.972f, 2576.762f},
            new float[] {1779.412f, 2570.443f}
        };
        private Vector3 enterPrisonLocation = new Vector3(1676.277f, 2536.605f, 45.565f);
        private Vector3 exitPrisonLocation = new Vector3(1846f, 2586f, 46f);
        private float exitPrisonHeading = 265f;
        private float enterPrisonHeading = 120f;

        public Jail(Client client) : base(client)
        {
            client.RegisterEventHandler("Jail.SetJailState", new Action<bool>(OnSetJail)); 
        }

        private void OnSetJail(bool putInJail)
        {
            if (putInJail)
            {
                Client.RegisterTickHandler(JailTick);
                previousPosition = new float[] { Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y };
                previousPositionFull = Game.PlayerPed.Position;
                TriggerServerEvent("InteractSound_SV:PlayWithinDistance", 1, "jailed", 0.05);
                Client.TriggerServerEvent("InteractSound_SV:PlayWithinDistance", 1, "jailed", 0.05);
            }
            else
            {
                Client.DeregisterTickHandler(JailTick);
                Game.PlayerPed.Position = exitPrisonLocation;
                Game.PlayerPed.Heading = exitPrisonHeading;
            }
        }

        private async Task JailTick()
        {
            float[] currentPosition = { Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y };
            if (!PolygonCollision.Contains(prisonPolygon, currentPosition))
            {
                if (PolygonCollision.Contains(prisonPolygon, previousPosition))
                {
                    Game.PlayerPed.PositionNoOffset = previousPositionFull;
                    //await BaseScript.Delay(50);
                    return;
                }
                else
                {
                    Game.PlayerPed.IsPositionFrozen = true;
                    API.RequestCollisionAtCoord(enterPrisonLocation.X, enterPrisonLocation.Y, enterPrisonLocation.Z);
                    Game.PlayerPed.PositionNoOffset = enterPrisonLocation;
                    Game.PlayerPed.Heading = enterPrisonHeading;
                    while (!API.HasCollisionLoadedAroundEntity(Game.PlayerPed.Handle))
                        await BaseScript.Delay(0);

                    Game.PlayerPed.IsPositionFrozen = false;
                }
            }
            Game.PlayerPed.Weapons.RemoveAll();
            previousPosition = currentPosition;
            previousPositionFull = Game.PlayerPed.Position;
            //await BaseScript.Delay(50);
        }
    }
}
