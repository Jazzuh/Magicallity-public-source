﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicallity.Server.HTTP;
using Magicallity.Server.Session;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Newtonsoft.Json;

namespace Magicallity.Server.Admin
{
    public class PlayerReporting : ServerAccessor
    {
        private const string reportHook = "https://discordapp.com/api/webhooks/372480325886017536/LRFvZOPI-7DO7nKDafm2imm5jmwYI61H8Jb0NB0QKujg3UXw9Qba4aFclX7Pr8H9_bKM";
        public PlayerReporting(Server server) : base(server)
        {
            CommandRegister.RegisterCommand("report", OnReportCommand);
        }

        private void OnReportCommand(Command cmd)
        {
            var playerToReport = cmd.GetArgAs(0, 0);
            var reportedSession = Sessions.GetPlayer(playerToReport);

            if (reportedSession == null)
            {
                Log.ToClient("[Report]", "Invalid server ID", ConstantColours.Red, cmd.Player);
                return;
            }

            cmd.Args.RemoveAt(0);
            var reportReason = string.Join(" ", cmd.Args);

            Server.Get<Admin>().SendAdminMessage("[Report]", $"{cmd.Session.PlayerName} reported {reportedSession.PlayerName} for reason {reportReason}", ConstantColours.Red);
            SendWebhookMessage(reportHook, "Report", $"**Report on {reportedSession.PlayerName}**\n```\nReported by: {cmd.Session.PlayerName}\nReporter Server ID: {cmd.Player.Handle}\nReport reason: {reportReason}\nReported Server ID: {reportedSession.ServerID}```");
            Log.ToClient("[Report]", $"You reported {reportedSession.PlayerName} for reason {reportReason}", ConstantColours.Red, cmd.Player);
        }

        private void SendWebhookMessage(string webhook, string username, string content)
        {
            var request = new HTTPRequest();
#pragma warning disable 4014
            request.Request(webhook, "POST", JsonConvert.SerializeObject(new Dictionary<string, string>
#pragma warning restore 4014
            {
                {"username", username },
                {"content", content }
            }), new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json"
            });
        }
    }
}
