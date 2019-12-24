using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Magicallity.Client.Locations
{
    public static class TransitLocations
    {
        /*public static List<Vector3> TrainStations = new List<Vector3>
        {
            new Vector3(-848.418f, -133.194f, 28.185f), //Portola dr
            new Vector3(-278.963f, -304.992f, 18.29f), //Dorset Dr
            new Vector3(-911.47f, -2352.18f, -3.50752f), //Airport
            new Vector3(-215.204f, -1031.78f, 30.1406f), //Alta St
            new Vector3(-1342.59f, -508.607f, 23.2694f), //Bahama Mamas
            new Vector3(-466.044f, -703.759f, 20.0319f), //San Andreas Ave/Little Seoul
            new Vector3(282.002f, -1201.83f, 38.9029f), //Olympic Fwy/Adams Apple
        };*/

        public static Dictionary<string, Vector3> TrainStations = new Dictionary<string, Vector3>
        {
            {
                "Portola dr", new Vector3(-848.418f, -133.194f, 28.185f)
            }, 

            {
                "Dorset Dr", new Vector3(-278.963f, -304.992f, 18.29f)
            }, //Dorset Dr

            {
               "Airport", new Vector3(-911.47f, -2352.18f, -3.50752f)
            }, //Airport
            {
               "Alta St", new Vector3(-215.204f, -1031.78f, 30.1406f)
            }, //Alta St
            {
               "Bahama Mamas", new Vector3(-1369.93f, -528.11f, 30.31f)
            }, //Bahama Mamas
            {
               "San Andreas Ave/Little Seoul", new Vector3(-466.044f, -703.759f, 20.0319f)
            }, //San Andreas Ave/Little Seoul
            {
                "Olympic Fwy/Adams Apple", new Vector3(282.002f, -1201.83f, 38.9029f)
            }, //Olympic Fwy/Adams Apple
        };


        public static Dictionary<string, Vector3> BusStops = new Dictionary<string, Vector3>
        {
            { "Prison", new Vector3(1928.9f, 2617.86f, 46.2795f) }, //Prison
            { "Sandy near SO", new Vector3(1899.63f, 3705.46f, 32.7619f) }, //Sandy near SO
            { "Paleto", new Vector3(-221.75f,6172f,31.4004f) }, //Paleto
            { "Inseno Rd", new Vector3(-3032.78f, 582.046f, 7.80207f) }, //Inseno Rd
            { "Marathon Ave/Bahama Mamas", new Vector3(-1476.23f, -634.597f, 30.5863f) }, //Marathon Ave/Bahama Mamas
            { "Airport", new Vector3(-1059.74f, -2544.76f, 20.1693f) }, //Airport
            { "Popular south of Capitol", new Vector3(822.443f, -1638.56f, 30.3933f) }, //Popular south of Capitol
            { "Vespucci near Alta", new Vector3(-251.557f, -885.976f, 30.6793f) }, //Vespucci near Alta
            { "Clinton Ave", new Vector3(635.934f, 200.138f, 97.0695f) }, //Clinton Ave west of Elgin (vinewood)
            { "Mirror Park", new Vector3(1032.1f, -760.944f, 57.8988f) }, //Mirror Park near Parking Garage
            { "Innocence/Popular", new Vector3(775.836f, -1755.01f, 29.514f) }, //Innocence/Popular
            { "Bay City Ave/Invention Ct", new Vector3(-1214.76f, -1220.8f, 7.637f) }, //Bay City Ave/Invention Ct
            { "Rt 68 gas station", new Vector3(-2564.05f, 2316.99f, 33.2157f) }, //Rt 68 gas station near Zancudo
            { "Cable Car station", new Vector3(-758.471f, 5587.53f, 36.7062f) }, //Cable Car station west of Paleto
            { "Joshua Rd", new Vector3(318.845f, 2624.59f, 44.4666f) }, //68Joshua Rd
            { "Parsons Rehabilitation Center", new Vector3(-1397.12f, 738.51f, 182.93f) }, 
        };
    }
}
