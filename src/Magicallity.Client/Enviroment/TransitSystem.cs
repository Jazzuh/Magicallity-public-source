using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Magicallity.Client.Helpers;
using Magicallity.Client.Models;
using Magicallity.Client.UI;
using Magicallity.Client.UI.Classes;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using MenuFramework;

namespace Magicallity.Client.Enviroment
{
    public class TransitSystem : ClientAccessor
    {
        private bool isTravelling = false;
        private MenuModel trainMenu = new MenuModel { headerTitle = "Train stations" };
        private MenuModel busMenu = new MenuModel { headerTitle = "Bus stations" };
        private ScreenText countdownTimer = new ScreenText("15", 920, 50, 0.75f);
        private int waitTime = 45;

        public TransitSystem(Client client) : base(client)
        {
            client.RegisterEventHandler("Player.OnLoginComplete", new Action(OnLogin));
            client.RegisterEventHandler("Player.CheckForInteraction", new Action(OnInteract));
        }

        private async void OnLogin()
        {
#pragma warning disable 4014
            await BlipHandler.AddBlipAsync("Train station", Locations.TransitLocations.TrainStations.Values.ToList(), new BlipOptions
            {
                Sprite = BlipSprite.Lift
            });

            MarkerHandler.AddMarkerAsync(Locations.TransitLocations.TrainStations.Values.ToList(), new MarkerOptions
            {
                ScaleFloat = 2.0f,
                zOffset = -0.94f
            });

            await BlipHandler.AddBlipAsync("Bus stop", Locations.TransitLocations.BusStops.Values.ToList(), new BlipOptions
            {
                Sprite = BlipSprite.VinewoodTours
            });
            MarkerHandler.AddMarkerAsync(Locations.TransitLocations.BusStops.Values.ToList(), new MarkerOptions
            {
                ScaleFloat = 2.0f,
                zOffset = -0.94f
            });
#pragma warning restore 4014

            buildUIElements();
        }

        private void buildUIElements()
        {
            foreach (var loc in Locations.TransitLocations.TrainStations)
            {
                var interactItem = new MenuItemStandard
                {
                    Title = loc.Key,
                    OnActivate = item =>
                    {
                        checkCanGoToLocation(Locations.TransitLocations.TrainStations.Values.ToList(), loc.Value, "train");
                    }
                };
                trainMenu.menuItems.Add(interactItem);
            }

            foreach (var loc in Locations.TransitLocations.BusStops)
            {
                var interactItem = new MenuItemStandard
                {
                    Title = loc.Key,
                    OnActivate = item =>
                    {
                        checkCanGoToLocation(Locations.TransitLocations.BusStops.Values.ToList(), loc.Value, "bus");
                    }
                };
                busMenu.menuItems.Add(interactItem);
            }

            Client.Get<InteractionUI>().RegisterInteractionMenuItem(new MenuItemSubMenu
            {
                Title = "Train station",
                SubMenu = trainMenu
            }, () => IsNearLocation(Locations.TransitLocations.TrainStations.Values.ToList()), 600);

            Client.Get<InteractionUI>().RegisterInteractionMenuItem(new MenuItemSubMenu
            {
                Title = "Bus station",
                SubMenu = busMenu
            }, () => IsNearLocation(Locations.TransitLocations.BusStops.Values.ToList()), 600);
        }

        private async void checkCanGoToLocation(List<Vector3> allLocations, Vector3 newPos, string transitType)
        {
            var currentLocation = getClosestLocation(allLocations);
            var canPay = await LocalSession.CanPayForItem(API.GetConvarInt($"mg_{transitType}TransitCost", transitType == "bus" ? 25 : 50));

            if (currentLocation != Vector3.Zero && canPay && !isTravelling)
            {
                isTravelling = true;
                await goToLocation(currentLocation, newPos, transitType);
                isTravelling = false;
            }
            else if (!canPay)
            {
                Log.ToChat("[Bank]", "You cannot afford to do this", ConstantColours.Bank);
            }
            else if (currentLocation == Vector3.Zero)
            {
                Log.ToChat("[Transit]", $"You moved to far away from the {transitType} station", ConstantColours.Blue);
            }
        }

        private async Task goToLocation(Vector3 currentPosition, Vector3 newPos, string transitType)
        {
            countdownTimer.Draw();

            Log.ToChat("[Transit]", $"Currently waiting for transport this will take a little bit", ConstantColours.Blue);
            var currentSecond = 0;
            var playerPed = Cache.PlayerPed;
            do
            {
                countdownTimer.Caption = $"{waitTime - currentSecond}";
                if (playerPed.Position.DistanceToSquared(currentPosition) > Math.Pow(3, 2) || playerPed.IsInVehicle())
                {
                    Log.ToChat("[Transit]", $"You moved to far away from the {transitType} station", ConstantColours.Blue);
                    countdownTimer.StopDraw();
                    return;
                }
                await BaseScript.Delay(1000);
                currentSecond++;
            } while (currentSecond <= waitTime);

            if (playerPed.Position.DistanceToSquared(currentPosition) < Math.Pow(3, 2))
            {
                await playerPed.TeleportToLocation(newPos, true);
                var cost = API.GetConvarInt($"mg_{transitType}TransitCost", transitType == "bus" ? 25 : 50);
                Client.TriggerServerEvent("Payment.PayForItem", cost, $"{transitType} station");
                Log.ToChat("[Bank]", $"You paid ${cost} for this {transitType} ride", ConstantColours.Bank);
            }
            else
            {
                Log.ToChat("[Transit]", $"You moved to far away from the {transitType} station", ConstantColours.Blue);
            }

            countdownTimer.StopDraw();
        }

        //TODO move somehwre else
        public bool IsNearLocation(Vector3 pos, float maxDist = 3.0f)
        {
            return Cache.PlayerPed.Position.DistanceToSquared(pos) < Math.Pow(maxDist, 2);
        }

        public bool IsNearLocation(List<Vector3> positions, float maxDist = 3.0f)
        {
            foreach(var pos in positions)
                if (IsNearLocation(pos))
                    return true;

            return false;
        }

        private Vector3 getClosestLocation(List<Vector3> locations)
        {
            foreach (var loc in locations)
            {
                if (IsNearLocation(loc))
                    return loc;
            }

            return Vector3.Zero;
        }

        private void OnInteract()
        {
            if(IsNearLocation(Locations.TransitLocations.TrainStations.Values.ToList()))
                InteractionUI.Observer.OpenMenu(trainMenu);
            else if (IsNearLocation(Locations.TransitLocations.BusStops.Values.ToList()))
                InteractionUI.Observer.OpenMenu(busMenu);
        }
    }
}
