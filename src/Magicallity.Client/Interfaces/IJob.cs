using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicallity.Client.Interfaces
{
    interface IJob
    {
        void StartJob();
        void EndJob();
        void GiveJobPayment();
        Task JobTick();
    }
}
