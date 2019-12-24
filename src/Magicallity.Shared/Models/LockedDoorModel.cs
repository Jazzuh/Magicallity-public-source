using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Shared.Enums;

namespace Magicallity.Shared.Models
{
    public class LockedDoorModel
    {
        public int DoorId;
        public Vector3 Location;
        public bool LockState;
        public string Model;
        public float InitialHeading;
        /// <summary>
        /// Client only however needs to be set initially on the server once a player loads a character
        /// </summary>
        public bool CanOpenDoor = false;

#if SERVER
        public JobType RequiredJobType = (JobType)(-1); // None
        /// <summary>
        /// The required job groups / principals needed. If this is set then <see cref="RequiredJobType"/> will be ignored. Split by ;
        /// </summary>
        public string RequiredJobPermissions = "";
#endif
    }
}
