using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Magicallity.Client.Helpers
{
    public class VehicleList : IEnumerable<int>
    {
        private int currentHandle;

        public IEnumerator<int> GetEnumerator()
        {
            try
            {
                OutputArgument OutArgEntity = new OutputArgument();
                currentHandle = Function.Call<int>((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("FIND_FIRST_VEHICLE")), OutArgEntity);
                yield return OutArgEntity.GetResult<int>();
                while (Function.Call<bool>((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("FIND_NEXT_VEHICLE")), currentHandle, OutArgEntity))
                {
                    yield return OutArgEntity.GetResult<int>();
                }
            } finally { }
            Function.Call((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("END_FIND_VEHICLE")), currentHandle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /*~VehicleList()
        {
            Function.Call((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("END_FIND_VEHICLE")), currentHandle);
        }*/
    }

    public class PedList : IEnumerable<int>
    {
        private int currentHandle;

        public IEnumerator<int> GetEnumerator()
        {
            try
            {
                OutputArgument OutArgEntity = new OutputArgument();
                currentHandle = Function.Call<int>((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("FIND_FIRST_PED")), OutArgEntity);
                yield return OutArgEntity.GetResult<int>();
                while (Function.Call<bool>((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("FIND_NEXT_PED")), currentHandle, OutArgEntity))
                {
                    yield return OutArgEntity.GetResult<int>();
                }
            } finally { }
            Function.Call((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("END_FIND_PED")), currentHandle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /*~PedList()
        {
            Function.Call((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("END_FIND_PED")), currentHandle);
        }*/
    }

    public class ObjectList : IEnumerable<int>
    {
        private int currentHandle;

        public IEnumerator<int> GetEnumerator()
        {
            try
            {
                OutputArgument OutArgEntity = new OutputArgument();
                currentHandle = Function.Call<int>((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("FIND_FIRST_OBJECT")), OutArgEntity);
                yield return OutArgEntity.GetResult<int>();
                while (Function.Call<bool>((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("FIND_NEXT_OBJECT")), currentHandle, OutArgEntity))
                {
                    yield return OutArgEntity.GetResult<int>();
                }
            } finally { }
            Function.Call((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("END_FIND_OBJECT")), currentHandle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /*~ObjectList()
        {
            Function.Call((CitizenFX.Core.Native.Hash)((uint)CitizenFX.Core.Game.GenerateHash("END_FIND_OBJECT")), currentHandle);
        }*/
    }
}
