using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

using Magicallity.Server.Admin;
using Magicallity.Server.Helpers;
using Magicallity.Server.Session;
using Magicallity.Server.Permissions;
using Magicallity.Shared;
using Magicallity.Shared.Enums;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace Magicallity.Server.Jobs.EmergencyServices
{
    class DoorLockHandler : JobClass
    {
#region DoorObjects
        private List<LockedDoorModel> doors = new List<LockedDoorModel>
        {
            new LockedDoorModel { Location = new Vector3(464.0f, -992.265f, 24.9149f), LockState = true, Model = "v_ilev_ph_cellgate", InitialHeading = 0.0f, RequiredJobType = JobType.Police | JobType.EMS} ,
            new LockedDoorModel { Location = new Vector3(462.381f, -993.651f, 24.9149f), LockState = true, Model = "v_ilev_ph_cellgate", InitialHeading = -90.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(462.331f, -998.152f, 24.9149f), LockState = true, Model = "v_ilev_ph_cellgate", InitialHeading = 90.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(462.704f, -1001.92f, 24.9149f), LockState = true, Model = "v_ilev_ph_cellgate", InitialHeading = 90.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(449.698f, -986.469f, 30.689f), LockState = true, Model = "v_ilev_ph_gendoor004", InitialHeading = 90.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(447.238f, -980.630f, 30.689f), LockState = true, Model = "v_ilev_ph_gendoor002", InitialHeading = -180.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(443.97f, -989.033f, 30.689f), LockState = true, Model = "v_ilev_ph_gendoor005", InitialHeading = -180.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(445.37f, -988.705f, 30.689f), LockState = true, Model = "v_ilev_ph_gendoor005", InitialHeading = 0.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(464.126f, -1002.78f, 24.9149f), LockState = true, Model = "v_ilev_gtdoor", InitialHeading = 0.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(1855.685f, 3683.93f, 34.59282f), LockState = true, Model = "v_ilev_shrfdoor", InitialHeading = 30.0f, RequiredJobType = JobType.Police | JobType.EMS },
            new LockedDoorModel { Location = new Vector3(206.922f, -189.951f, 54.686f), LockState = true, Model = "v_ilev_ra_door1_l", InitialHeading = 340.0f, RequiredJobPermissions = "business.Hamaza.employee"},
            new LockedDoorModel { Location = new Vector3(208.980f, -190.699f, 54.686f), LockState = true, Model = "v_ilev_ra_door1_r", InitialHeading = 340.0f, RequiredJobPermissions = "business.Hamaza.employee"},
            new LockedDoorModel { Location = new Vector3(212.384f, -188.241f, 54.743f), LockState = true, Model = "xm_prop_lab_door02_r", InitialHeading = 70.0f, RequiredJobPermissions = "business.Hamaza.manager"},
            new LockedDoorModel { Location = new Vector3(212.479f, -184.688f, 54.733f), LockState = true, Model = "xm_prop_lab_door02_r", InitialHeading = 160.0f, RequiredJobPermissions = "business.Hamaza.owner"},
            //new LockedDoorModel { Location = new Vector3(464.18f, -1004.31f, 24.9152f), LockState = true, Model = "v_ilev_gtdoor" },
            //new LockedDoorModel { Location = new Vector3(252.87f, -1366.76f, 24.55f), LockState = true, Model = "v_ilev_cor_firedoor" },
            //new LockedDoorModel { Location = new Vector3(251.11f, -1365.28f, 24.55f), LockState = true, Model = "v_ilev_cor_firedoor" },
        };
#endregion

        public DoorLockHandler()
        {
            for(var i = 0; i < doors.Count; i++)
            {
                doors[i].DoorId = i + 1;
            }
        }

        public void OnCharacterLoaded(Session.Session playerSession)
        {
            var doorsToSend = doors.ToList();

            //doorsToSend.ForEach(o => o.CanOpenDoor = CanOpenDoor(playerSession, o));
            for (int i = 0; i < doorsToSend.Count; i++)
            {
                doorsToSend[i].CanOpenDoor = CanOpenDoor(playerSession, doorsToSend[i]);
            }

            playerSession.TriggerEvent("Locks.ReceiveLockStates", JsonConvert.SerializeObject(doorsToSend));
        }

        public void OnPermissionRefresh(Session.Session playerSession) => OnCharacterLoaded(playerSession);

        public bool CanOpenDoor(Session.Session playerSession, LockedDoorModel door)
        {
            if (string.IsNullOrEmpty(door.RequiredJobPermissions))
            {
                return door.RequiredJobType.HasFlag(JobHandler.GetPlayerJob(playerSession));
                //return JobHandler.GetPlayerJob(playerSession).HasFlag(door.RequiredJobType);
            }
            else
            {
                return Permissions.Permissions.HasPermissions(playerSession, door.RequiredJobPermissions);
            }
        }

        [EventHandler("Locks.RequestDoorLock")]
        private void OnReqestLockDoor([FromSource] Player source, int doorId, bool state)
        {
            Log.Info($"{source.Name} is attempting to set the state of door #{doorId} to {state}");
            var playerSession = Sessions.GetPlayer(source);
            var door = doors.FirstOrDefault(o => o.DoorId == doorId);

            if (playerSession == null || door == null || door.Location.DistanceToSquared(playerSession.Position) > 24.0f) return;

            if (CanOpenDoor(playerSession, door))
            {
                door.LockState = state;

                Log.ToClient("[Door]", $"Door  {(door.LockState ? "locked" : "unlocked")}", ConstantColours.Job, source);

                BaseScript.TriggerClientEvent("Locks.UpdateDoorState", doorId, state);
            }
        }

        [EventHandler("Locks.RequestLockStates")]
        private void OnRequestLocks([FromSource] Player source)
        {
            Log.Info($"{source.Name} is requesting locked door states");
            source.TriggerEvent("Locks.ReceiveLockStates", JsonConvert.SerializeObject(doors));
        }
    }
}
