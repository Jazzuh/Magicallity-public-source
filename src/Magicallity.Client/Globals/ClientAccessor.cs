using CitizenFX.Core;
using Magicallity.Client.Jobs;
using Magicallity.Client.Session;

namespace Magicallity.Client
{
    public abstract class ClientAccessor : BaseScript
    {
        protected Client Client { get; }
        protected Session.Session LocalSession => Client.LocalSession;
        //protected PlayerList Players => Client.PlayerList;

        private SessionManager sessionManager;
        public SessionManager Sessions => sessionManager ?? (sessionManager = Client.Get<SessionManager>());

        private JobHandler jobHandler;
        public JobHandler JobHandler => jobHandler ?? (jobHandler = Client.Get<JobHandler>());

        protected ClientAccessor(Client client)
        {
            Client = client;
        }
    }
}
