using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Models;
using Magicallity.Shared;
using Magicallity.Shared.Models;
using Newtonsoft.Json;

namespace Magicallity.Client.Jobs.Criminal.Drugs
{
    public class Drugs : ClientAccessor
    {
        private DrugSelling selling;

        internal List<Drug> currentDrugs = new List<Drug>();

        public Drugs(Client client) : base(client)
        {
            client.RegisterEventHandler("Drugs.ReceiveDrugs", new Action<List<object>>(OnRecieveDrugs));
            selling = new DrugSelling(this);

            client.TriggerServerEvent("Drugs.RequestDrugs");
        }

        private void OnRecieveDrugs(List<object> drugList)
        {
            drugList.ForEach(o =>
            {
                var drug = JsonConvert.DeserializeObject<Drug>(o.ToString());
                Log.Debug($"Recieved a drug of {drug.HarvestDrugName}");
                currentDrugs.Add(drug);

                if (drug.HarvestDrugName != "Bud") return;
                BlipHandler.AddBlip("Marijuana", drug.HarvestLocations[0], new BlipOptions
                {
                    Sprite = BlipSprite.Marijuana,
                    Colour = (BlipColor)2
                });
            });
        }
    }
}
