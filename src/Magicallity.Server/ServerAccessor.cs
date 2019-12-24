using CitizenFX.Core;
using Magicallity.Server.Jobs;
using Magicallity.Server.Session;

namespace Magicallity.Server
{
    public abstract class ServerAccessor : BaseScript
    {
        protected Server Server { get; }
        protected dynamic MySQL => Server.MySQL;
        //protected PlayerList Players => Server.PlayerList;

        private SessionManager sessionManager;
        public SessionManager Sessions => sessionManager ?? (sessionManager = Server.Get<SessionManager>());

        private JobHandler jobHandler;
        public JobHandler JobHandler => jobHandler ?? (jobHandler = Server.Get<JobHandler>());

        protected ServerAccessor(Server server)
        {
            Server = server;
        }
    }
}