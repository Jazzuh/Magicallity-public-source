using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Magicallity.Client.Enviroment;
using Magicallity.Client.Models;
using Magicallity.Client.Session;
using Magicallity.Client.UI;
using Magicallity.Shared;
using Magicallity.Shared.Helpers;
using Magicallity.Shared.Models;
using MenuFramework;
using Newtonsoft.Json;

namespace Magicallity.Client.Player.Clothing
{
    internal class TattooModel
    {
        public string collection;
        public string tattooName;
        public string displayName;
        public string maleHashName;
        public string femaleHashName;
        public string zone;
    }

    /*
     * "Name": "TAT_BB_018",
    "LocalizedName": "Ship Arms",
    "HashNameMale": "MP_Bea_M_Back_000",
    "HashNameFemale": "",
    "Zone": "ZONE_TORSO",
    "ZoneID": 0,
    "Price": 7250*/
     

    internal class Tattoos : ClientAccessor
    {
        List<TattooModel> tattooModels = new List<TattooModel>()
        {
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_000",
                displayName = "Turbulence",
                maleHashName = "MP_Airraces_Tattoo_000_M",
                femaleHashName = "MP_Airraces_Tattoo_000_F",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_001",
                displayName = "Pilot Skull",
                maleHashName = "MP_Airraces_Tattoo_001_M",
                femaleHashName = "MP_Airraces_Tattoo_001_F",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_002",
                displayName = "Winged Bombshell",
                maleHashName = "MP_Airraces_Tattoo_002_M",
                femaleHashName = "MP_Airraces_Tattoo_002_F",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_003",
                displayName = "Toxic Trails",
                maleHashName = "MP_Airraces_Tattoo_003_M",
                femaleHashName = "MP_Airraces_Tattoo_003_F",
                zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_004",
                displayName = "Balloon Pioneer",
                maleHashName = "MP_Airraces_Tattoo_004_M",
                femaleHashName = "MP_Airraces_Tattoo_004_F",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_005",
                displayName = "Parachute Belle",
                maleHashName = "MP_Airraces_Tattoo_005_M",
                femaleHashName = "MP_Airraces_Tattoo_005_F",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_006",
                displayName = "Bombs Away",
                maleHashName = "MP_Airraces_Tattoo_006_M",
                femaleHashName = "MP_Airraces_Tattoo_006_F",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpairraces_overlays",
                tattooName = "TAT_AR_007",
                displayName = "Eagle Eyes",
                maleHashName = "MP_Airraces_Tattoo_007_M",
                femaleHashName = "MP_Airraces_Tattoo_007_F",
                zone = "ZONE_TORSO"
            },

            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_018",
                displayName = "Ship Arms",
                maleHashName = "MP_Bea_M_Back_000",
                femaleHashName = "",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_019",
                displayName = "Tribal Hammerhead",
                maleHashName = "MP_Bea_M_Chest_000",
                femaleHashName = "",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_020",
                displayName = "Tribal Shark",
                maleHashName = "MP_Bea_M_Chest_001",
                femaleHashName = "",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_021",
                displayName = "Pirate Skull",
                maleHashName = "MP_Bea_M_Head_000",
                femaleHashName = "",
                zone = "ZONE_HEAD"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_022",
                displayName = "Surf LS",
                maleHashName = "MP_Bea_M_Head_001",
                femaleHashName = "",
                zone = "ZONE_HEAD"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_031",
                displayName = "Shark",
                maleHashName = "MP_Bea_M_Head_002",
                femaleHashName = "",
                zone = "ZONE_HEAD"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_027",
                displayName = "Tribal Star",
                maleHashName = "MP_Bea_M_Lleg_000",
                femaleHashName = "",
                zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_025",
                displayName = "Tribal Tiki Tower",
                maleHashName = "MP_Bea_M_Rleg_000",
                femaleHashName = "",
                zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_026",
                displayName = "Tribal Sun",
                maleHashName = "MP_Bea_M_RArm_000",
                femaleHashName = "",
                zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_024",
                displayName = "Tiki Tower",
                maleHashName = "MP_Bea_M_LArm_000",
                femaleHashName = "",
                zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_017",
                displayName = "Mermaid L.S.",
                maleHashName = "MP_Bea_M_LArm_001",
                femaleHashName = "",
                zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_028",
                displayName = "Little Fish",
                maleHashName = "MP_Bea_M_Neck_000",
                femaleHashName = "",
                zone = "ZONE_HEAD"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_029",
                displayName = "Surfs Up",
                maleHashName = "MP_Bea_M_Neck_001",
                femaleHashName = "",
                zone = "ZONE_HEAD"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_030",
                displayName = "Vespucci Beauty",
                maleHashName = "MP_Bea_M_RArm_001",
                femaleHashName = "",
                zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_023",
                displayName = "Swordfish",
                maleHashName = "MP_Bea_M_Stom_000",
                femaleHashName = "",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_032",
                displayName = "Wheel",
                maleHashName = "MP_Bea_M_Stom_001",
                femaleHashName = "",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_003",
                displayName = "Rock Solid",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Back_000",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_001",
                displayName = "Hibiscus Flower Duo",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Back_001",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_005",
                displayName = "Shrimp",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Back_002",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_012",
                displayName = "Anchor",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Chest_000",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_013",
                displayName = "Anchor",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Chest_001",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_000",
                displayName = "Los Santos Wreath",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Chest_002",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_006",
                displayName = "Love Dagger",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_RSide_000",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_007",
                displayName = "School of Fish",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_RLeg_000",
                zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_015",
                displayName = "Tribal Fish",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_RArm_001",
                zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_008",
                displayName = "Tribal Butterfly",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Neck_000",
                zone = "ZONE_HEAD"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_011",
                displayName = "Sea Horses",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Should_000",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_004",
                displayName = "Catfish",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Should_001",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_014",
                displayName = "Swallow",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Stom_000",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_009",
                displayName = "Hibiscus Flower",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Stom_001",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_010",
                displayName = "Dolphin",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_Stom_002",
                zone = "ZONE_TORSO"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_002",
                displayName = "Tribal Flower",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_LArm_000",
                zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
                collection = "mpbeach_overlays",
                tattooName = "TAT_BB_016",
                displayName = "Parrot",
                maleHashName = "",
                femaleHashName = "MP_Bea_F_LArm_001",
                zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_000",
            displayName = "Demon Rider",
            maleHashName = "MP_MP_Biker_Tat_000_M",
            femaleHashName = "MP_MP_Biker_Tat_000_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_001",
            displayName = "Both Barrels",
            maleHashName = "MP_MP_Biker_Tat_001_M",
            femaleHashName = "MP_MP_Biker_Tat_001_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_002",
            displayName = "Rose Tribute",
            maleHashName = "MP_MP_Biker_Tat_002_M",
            femaleHashName = "MP_MP_Biker_Tat_002_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_003",
            displayName = "Web Rider",
            maleHashName = "MP_MP_Biker_Tat_003_M",
            femaleHashName = "MP_MP_Biker_Tat_003_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_004",
            displayName = "Dragon's Fury",
            maleHashName = "MP_MP_Biker_Tat_004_M",
            femaleHashName = "MP_MP_Biker_Tat_004_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_005",
            displayName = "Made In America",
            maleHashName = "MP_MP_Biker_Tat_005_M",
            femaleHashName = "MP_MP_Biker_Tat_005_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_006",
            displayName = "Chopper Freedom",
            maleHashName = "MP_MP_Biker_Tat_006_M",
            femaleHashName = "MP_MP_Biker_Tat_006_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_007",
            displayName = "Swooping Eagle",
            maleHashName = "MP_MP_Biker_Tat_007_M",
            femaleHashName = "MP_MP_Biker_Tat_007_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_008",
            displayName = "Freedom Wheels",
            maleHashName = "MP_MP_Biker_Tat_008_M",
            femaleHashName = "MP_MP_Biker_Tat_008_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_009",
            displayName = "Morbid Arachnid",
            maleHashName = "MP_MP_Biker_Tat_009_M",
            femaleHashName = "MP_MP_Biker_Tat_009_F",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_010",
            displayName = "Skull Of Taurus",
            maleHashName = "MP_MP_Biker_Tat_010_M",
            femaleHashName = "MP_MP_Biker_Tat_010_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_011",
            displayName = "R.I.P. My Brothers",
            maleHashName = "MP_MP_Biker_Tat_011_M",
            femaleHashName = "MP_MP_Biker_Tat_011_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_012",
            displayName = "Urban Stunter",
            maleHashName = "MP_MP_Biker_Tat_012_M",
            femaleHashName = "MP_MP_Biker_Tat_012_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_013",
            displayName = "Demon Crossbones",
            maleHashName = "MP_MP_Biker_Tat_013_M",
            femaleHashName = "MP_MP_Biker_Tat_013_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_014",
            displayName = "Lady Mortality",
            maleHashName = "MP_MP_Biker_Tat_014_M",
            femaleHashName = "MP_MP_Biker_Tat_014_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_015",
            displayName = "Ride or Die",
            maleHashName = "MP_MP_Biker_Tat_015_M",
            femaleHashName = "MP_MP_Biker_Tat_015_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_016",
            displayName = "Macabre Tree",
            maleHashName = "MP_MP_Biker_Tat_016_M",
            femaleHashName = "MP_MP_Biker_Tat_016_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_017",
            displayName = "Clawed Beast",
            maleHashName = "MP_MP_Biker_Tat_017_M",
            femaleHashName = "MP_MP_Biker_Tat_017_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_018",
            displayName = "Skeletal Chopper",
            maleHashName = "MP_MP_Biker_Tat_018_M",
            femaleHashName = "MP_MP_Biker_Tat_018_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_019",
            displayName = "Gruesome Talons",
            maleHashName = "MP_MP_Biker_Tat_019_M",
            femaleHashName = "MP_MP_Biker_Tat_019_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_020",
            displayName = "Cranial Rose",
            maleHashName = "MP_MP_Biker_Tat_020_M",
            femaleHashName = "MP_MP_Biker_Tat_020_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_021",
            displayName = "Flaming Reaper",
            maleHashName = "MP_MP_Biker_Tat_021_M",
            femaleHashName = "MP_MP_Biker_Tat_021_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_022",
            displayName = "Western Insignia",
            maleHashName = "MP_MP_Biker_Tat_022_M",
            femaleHashName = "MP_MP_Biker_Tat_022_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_023",
            displayName = "Western MC",
            maleHashName = "MP_MP_Biker_Tat_023_M",
            femaleHashName = "MP_MP_Biker_Tat_023_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_024",
            displayName = "Live to Ride",
            maleHashName = "MP_MP_Biker_Tat_024_M",
            femaleHashName = "MP_MP_Biker_Tat_024_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_025",
            displayName = "Good Luck",
            maleHashName = "MP_MP_Biker_Tat_025_M",
            femaleHashName = "MP_MP_Biker_Tat_025_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_026",
            displayName = "American Dream",
            maleHashName = "MP_MP_Biker_Tat_026_M",
            femaleHashName = "MP_MP_Biker_Tat_026_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_027",
            displayName = "Bad Luck",
            maleHashName = "MP_MP_Biker_Tat_027_M",
            femaleHashName = "MP_MP_Biker_Tat_027_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_028",
            displayName = "Dusk Rider",
            maleHashName = "MP_MP_Biker_Tat_028_M",
            femaleHashName = "MP_MP_Biker_Tat_028_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_029",
            displayName = "Bone Wrench",
            maleHashName = "MP_MP_Biker_Tat_029_M",
            femaleHashName = "MP_MP_Biker_Tat_029_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_030",
            displayName = "Brothers For Life",
            maleHashName = "MP_MP_Biker_Tat_030_M",
            femaleHashName = "MP_MP_Biker_Tat_030_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_031",
            displayName = "Gear Head",
            maleHashName = "MP_MP_Biker_Tat_031_M",
            femaleHashName = "MP_MP_Biker_Tat_031_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_032",
            displayName = "Western Eagle",
            maleHashName = "MP_MP_Biker_Tat_032_M",
            femaleHashName = "MP_MP_Biker_Tat_032_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_033",
            displayName = "Eagle Emblem",
            maleHashName = "MP_MP_Biker_Tat_033_M",
            femaleHashName = "MP_MP_Biker_Tat_033_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_034",
            displayName = "Brotherhood of Bikes",
            maleHashName = "MP_MP_Biker_Tat_034_M",
            femaleHashName = "MP_MP_Biker_Tat_034_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_035",
            displayName = "Chain Fist",
            maleHashName = "MP_MP_Biker_Tat_035_M",
            femaleHashName = "MP_MP_Biker_Tat_035_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_036",
            displayName = "Engulfed Skull",
            maleHashName = "MP_MP_Biker_Tat_036_M",
            femaleHashName = "MP_MP_Biker_Tat_036_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_037",
            displayName = "Scorched Soul",
            maleHashName = "MP_MP_Biker_Tat_037_M",
            femaleHashName = "MP_MP_Biker_Tat_037_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_038",
            displayName = "FTW",
            maleHashName = "MP_MP_Biker_Tat_038_M",
            femaleHashName = "MP_MP_Biker_Tat_038_F",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_039",
            displayName = "Gas Guzzler",
            maleHashName = "MP_MP_Biker_Tat_039_M",
            femaleHashName = "MP_MP_Biker_Tat_039_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_040",
            displayName = "American Made",
            maleHashName = "MP_MP_Biker_Tat_040_M",
            femaleHashName = "MP_MP_Biker_Tat_040_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_041",
            displayName = "No Regrets",
            maleHashName = "MP_MP_Biker_Tat_041_M",
            femaleHashName = "MP_MP_Biker_Tat_041_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_042",
            displayName = "Grim Rider",
            maleHashName = "MP_MP_Biker_Tat_042_M",
            femaleHashName = "MP_MP_Biker_Tat_042_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_043",
            displayName = "Ride Forever",
            maleHashName = "MP_MP_Biker_Tat_043_M",
            femaleHashName = "MP_MP_Biker_Tat_043_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_044",
            displayName = "Ride Free",
            maleHashName = "MP_MP_Biker_Tat_044_M",
            femaleHashName = "MP_MP_Biker_Tat_044_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_045",
            displayName = "Ride Hard Die Fast",
            maleHashName = "MP_MP_Biker_Tat_045_M",
            femaleHashName = "MP_MP_Biker_Tat_045_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_046",
            displayName = "Skull Chain",
            maleHashName = "MP_MP_Biker_Tat_046_M",
            femaleHashName = "MP_MP_Biker_Tat_046_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_047",
            displayName = "Snake Bike",
            maleHashName = "MP_MP_Biker_Tat_047_M",
            femaleHashName = "MP_MP_Biker_Tat_047_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_048",
            displayName = "STFU",
            maleHashName = "MP_MP_Biker_Tat_048_M",
            femaleHashName = "MP_MP_Biker_Tat_048_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_049",
            displayName = "These Colors Don't Run",
            maleHashName = "MP_MP_Biker_Tat_049_M",
            femaleHashName = "MP_MP_Biker_Tat_049_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_050",
            displayName = "Unforgiven",
            maleHashName = "MP_MP_Biker_Tat_050_M",
            femaleHashName = "MP_MP_Biker_Tat_050_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_051",
            displayName = "Western Stylized",
            maleHashName = "MP_MP_Biker_Tat_051_M",
            femaleHashName = "MP_MP_Biker_Tat_051_F",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_052",
            displayName = "Biker Mount",
            maleHashName = "MP_MP_Biker_Tat_052_M",
            femaleHashName = "MP_MP_Biker_Tat_052_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_053",
            displayName = "Muffler Helmet",
            maleHashName = "MP_MP_Biker_Tat_053_M",
            femaleHashName = "MP_MP_Biker_Tat_053_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_054",
            displayName = "Mum",
            maleHashName = "MP_MP_Biker_Tat_054_M",
            femaleHashName = "MP_MP_Biker_Tat_054_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_055",
            displayName = "Poison Scorpion",
            maleHashName = "MP_MP_Biker_Tat_055_M",
            femaleHashName = "MP_MP_Biker_Tat_055_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_056",
            displayName = "Bone Cruiser",
            maleHashName = "MP_MP_Biker_Tat_056_M",
            femaleHashName = "MP_MP_Biker_Tat_056_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_057",
            displayName = "Laughing Skull",
            maleHashName = "MP_MP_Biker_Tat_057_M",
            femaleHashName = "MP_MP_Biker_Tat_057_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_058",
            displayName = "Reaper Vulture",
            maleHashName = "MP_MP_Biker_Tat_058_M",
            femaleHashName = "MP_MP_Biker_Tat_058_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_059",
            displayName = "Faggio",
            maleHashName = "MP_MP_Biker_Tat_059_M",
            femaleHashName = "MP_MP_Biker_Tat_059_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbiker_overlays",
            tattooName = "TAT_BI_060",
            displayName = "We Are The Mods!",
            maleHashName = "MP_MP_Biker_Tat_060_M",
            femaleHashName = "MP_MP_Biker_Tat_060_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_005",
            displayName = "Cash is King",
            maleHashName = "MP_Buis_M_Neck_000",
            femaleHashName = "",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_006",
            displayName = "Bold Dollar Sign",
            maleHashName = "MP_Buis_M_Neck_001",
            femaleHashName = "",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_007",
            displayName = "Script Dollar Sign",
            maleHashName = "MP_Buis_M_Neck_002",
            femaleHashName = "",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_008",
            displayName = "$100",
            maleHashName = "MP_Buis_M_Neck_003",
            femaleHashName = "",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_003",
            displayName = "$100 Bill",
            maleHashName = "MP_Buis_M_LeftArm_000",
            femaleHashName = "",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_004",
            displayName = "All-Seeing Eye",
            maleHashName = "MP_Buis_M_LeftArm_001",
            femaleHashName = "",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_009",
            displayName = "Dollar Skull",
            maleHashName = "MP_Buis_M_RightArm_000",
            femaleHashName = "",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_010",
            displayName = "Green",
            maleHashName = "MP_Buis_M_RightArm_001",
            femaleHashName = "",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_011",
            displayName = "Refined Hustler",
            maleHashName = "MP_Buis_M_Stomach_000",
            femaleHashName = "",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_001",
            displayName = "Rich",
            maleHashName = "MP_Buis_M_Chest_000",
            femaleHashName = "",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_002",
            displayName = "$$$",
            maleHashName = "MP_Buis_M_Chest_001",
            femaleHashName = "",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_000",
            displayName = "Makin' Paper",
            maleHashName = "MP_Buis_M_Back_000",
            femaleHashName = "",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_002",
            displayName = "High Roller",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Chest_000",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_003",
            displayName = "Makin' Money",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Chest_001",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_004",
            displayName = "Love Money",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Chest_002",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_011",
            displayName = "Diamond Back",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Stom_000",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_012",
            displayName = "Santo Capra Logo",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Stom_001",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_013",
            displayName = "Money Bag",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Stom_002",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_000",
            displayName = "Respect",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Back_000",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_001",
            displayName = "Gold Digger",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Back_001",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_007",
            displayName = "Val-de-Grace Logo",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Neck_000",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_008",
            displayName = "Money Rose",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_Neck_001",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_009",
            displayName = "Dollar Sign",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_RArm_000",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_005",
            displayName = "Greed is Good",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_LArm_000",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_006",
            displayName = "Single",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_LLeg_000",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpbusiness_overlays",
            tattooName = "TAT_BUS_F_010",
            displayName = "Diamond Crown",
            maleHashName = "",
            femaleHashName = "MP_Buis_F_RLeg_000",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_000",
            displayName = "Thor & Goblin",
            maleHashName = "MP_Christmas2017_Tattoo_000_M",
            femaleHashName = "MP_Christmas2017_Tattoo_000_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_001",
            displayName = "Viking Warrior",
            maleHashName = "MP_Christmas2017_Tattoo_001_M",
            femaleHashName = "MP_Christmas2017_Tattoo_001_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_002",
            displayName = "Kabuto",
            maleHashName = "MP_Christmas2017_Tattoo_002_M",
            femaleHashName = "MP_Christmas2017_Tattoo_002_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_003",
            displayName = "Native Warrior",
            maleHashName = "MP_Christmas2017_Tattoo_003_M",
            femaleHashName = "MP_Christmas2017_Tattoo_003_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_004",
            displayName = "Tiger & Mask",
            maleHashName = "MP_Christmas2017_Tattoo_004_M",
            femaleHashName = "MP_Christmas2017_Tattoo_004_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_005",
            displayName = "Ghost Dragon",
            maleHashName = "MP_Christmas2017_Tattoo_005_M",
            femaleHashName = "MP_Christmas2017_Tattoo_005_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_006",
            displayName = "Medusa",
            maleHashName = "MP_Christmas2017_Tattoo_006_M",
            femaleHashName = "MP_Christmas2017_Tattoo_006_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_007",
            displayName = "Spartan Combat",
            maleHashName = "MP_Christmas2017_Tattoo_007_M",
            femaleHashName = "MP_Christmas2017_Tattoo_007_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_008",
            displayName = "Spartan Warrior",
            maleHashName = "MP_Christmas2017_Tattoo_008_M",
            femaleHashName = "MP_Christmas2017_Tattoo_008_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_009",
            displayName = "Norse Rune",
            maleHashName = "MP_Christmas2017_Tattoo_009_M",
            femaleHashName = "MP_Christmas2017_Tattoo_009_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_010",
            displayName = "Spartan Shield",
            maleHashName = "MP_Christmas2017_Tattoo_010_M",
            femaleHashName = "MP_Christmas2017_Tattoo_010_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_011",
            displayName = "Weathered Skull",
            maleHashName = "MP_Christmas2017_Tattoo_011_M",
            femaleHashName = "MP_Christmas2017_Tattoo_011_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_012",
            displayName = "Tiger Headdress",
            maleHashName = "MP_Christmas2017_Tattoo_012_M",
            femaleHashName = "MP_Christmas2017_Tattoo_012_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_013",
            displayName = "Katana",
            maleHashName = "MP_Christmas2017_Tattoo_013_M",
            femaleHashName = "MP_Christmas2017_Tattoo_013_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_014",
            displayName = "Celtic Band",
            maleHashName = "MP_Christmas2017_Tattoo_014_M",
            femaleHashName = "MP_Christmas2017_Tattoo_014_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_015",
            displayName = "Samurai Combat",
            maleHashName = "MP_Christmas2017_Tattoo_015_M",
            femaleHashName = "MP_Christmas2017_Tattoo_015_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_016",
            displayName = "Odin & Raven",
            maleHashName = "MP_Christmas2017_Tattoo_016_M",
            femaleHashName = "MP_Christmas2017_Tattoo_016_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_017",
            displayName = "Feather Sleeve",
            maleHashName = "MP_Christmas2017_Tattoo_017_M",
            femaleHashName = "MP_Christmas2017_Tattoo_017_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_018",
            displayName = "Muscle Tear",
            maleHashName = "MP_Christmas2017_Tattoo_018_M",
            femaleHashName = "MP_Christmas2017_Tattoo_018_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_019",
            displayName = "Strike Force",
            maleHashName = "MP_Christmas2017_Tattoo_019_M",
            femaleHashName = "MP_Christmas2017_Tattoo_019_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_020",
            displayName = "Medusa's Gaze",
            maleHashName = "MP_Christmas2017_Tattoo_020_M",
            femaleHashName = "MP_Christmas2017_Tattoo_020_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_021",
            displayName = "Spartan & Lion",
            maleHashName = "MP_Christmas2017_Tattoo_021_M",
            femaleHashName = "MP_Christmas2017_Tattoo_021_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_022",
            displayName = "Spartan & Horse",
            maleHashName = "MP_Christmas2017_Tattoo_022_M",
            femaleHashName = "MP_Christmas2017_Tattoo_022_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_023",
            displayName = "Samurai Tallship",
            maleHashName = "MP_Christmas2017_Tattoo_023_M",
            femaleHashName = "MP_Christmas2017_Tattoo_023_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_024",
            displayName = "Dragon Slayer",
            maleHashName = "MP_Christmas2017_Tattoo_024_M",
            femaleHashName = "MP_Christmas2017_Tattoo_024_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_025",
            displayName = "Winged Serpent",
            maleHashName = "MP_Christmas2017_Tattoo_025_M",
            femaleHashName = "MP_Christmas2017_Tattoo_025_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_026",
            displayName = "Spartan Skull",
            maleHashName = "MP_Christmas2017_Tattoo_026_M",
            femaleHashName = "MP_Christmas2017_Tattoo_026_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_027",
            displayName = "Molon Labe",
            maleHashName = "MP_Christmas2017_Tattoo_027_M",
            femaleHashName = "MP_Christmas2017_Tattoo_027_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_028",
            displayName = "Spartan Mural",
            maleHashName = "MP_Christmas2017_Tattoo_028_M",
            femaleHashName = "MP_Christmas2017_Tattoo_028_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2017_overlays",
            tattooName = "TAT_H27_029",
            displayName = "Cerberus",
            maleHashName = "MP_Christmas2017_Tattoo_029_M",
            femaleHashName = "MP_Christmas2017_Tattoo_029_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_000",
            displayName = "Skull Rider",
            maleHashName = "MP_Xmas2_M_Tat_000",
            femaleHashName = "MP_Xmas2_F_Tat_000",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_001",
            displayName = "Spider Outline",
            maleHashName = "MP_Xmas2_M_Tat_001",
            femaleHashName = "MP_Xmas2_F_Tat_001",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_002",
            displayName = "Spider Color",
            maleHashName = "MP_Xmas2_M_Tat_002",
            femaleHashName = "MP_Xmas2_F_Tat_002",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_003",
            displayName = "Snake Outline",
            maleHashName = "MP_Xmas2_M_Tat_003",
            femaleHashName = "MP_Xmas2_F_Tat_003",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_004",
            displayName = "Snake Shaded",
            maleHashName = "MP_Xmas2_M_Tat_004",
            femaleHashName = "MP_Xmas2_F_Tat_004",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_005",
            displayName = "Carp Outline",
            maleHashName = "MP_Xmas2_M_Tat_005",
            femaleHashName = "MP_Xmas2_F_Tat_005",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_006",
            displayName = "Carp Shaded",
            maleHashName = "MP_Xmas2_M_Tat_006",
            femaleHashName = "MP_Xmas2_F_Tat_006",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_007",
            displayName = "Los Muertos",
            maleHashName = "MP_Xmas2_M_Tat_007",
            femaleHashName = "MP_Xmas2_F_Tat_007",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_008",
            displayName = "Death Before Dishonor",
            maleHashName = "MP_Xmas2_M_Tat_008",
            femaleHashName = "MP_Xmas2_F_Tat_008",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_009",
            displayName = "Time To Die",
            maleHashName = "MP_Xmas2_M_Tat_009",
            femaleHashName = "MP_Xmas2_F_Tat_009",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_010",
            displayName = "Electric Snake",
            maleHashName = "MP_Xmas2_M_Tat_010",
            femaleHashName = "MP_Xmas2_F_Tat_010",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_011",
            displayName = "Roaring Tiger",
            maleHashName = "MP_Xmas2_M_Tat_011",
            femaleHashName = "MP_Xmas2_F_Tat_011",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_012",
            displayName = "8 Ball Skull",
            maleHashName = "MP_Xmas2_M_Tat_012",
            femaleHashName = "MP_Xmas2_F_Tat_012",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_013",
            displayName = "Lizard",
            maleHashName = "MP_Xmas2_M_Tat_013",
            femaleHashName = "MP_Xmas2_F_Tat_013",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_014",
            displayName = "Floral Dagger",
            maleHashName = "MP_Xmas2_M_Tat_014",
            femaleHashName = "MP_Xmas2_F_Tat_014",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_015",
            displayName = "Japanese Warrior",
            maleHashName = "MP_Xmas2_M_Tat_015",
            femaleHashName = "MP_Xmas2_F_Tat_015",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_016",
            displayName = "Loose Lips Outline",
            maleHashName = "MP_Xmas2_M_Tat_016",
            femaleHashName = "MP_Xmas2_F_Tat_016",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_017",
            displayName = "Loose Lips Color",
            maleHashName = "MP_Xmas2_M_Tat_017",
            femaleHashName = "MP_Xmas2_F_Tat_017",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_018",
            displayName = "Royal Dagger Outline",
            maleHashName = "MP_Xmas2_M_Tat_018",
            femaleHashName = "MP_Xmas2_F_Tat_018",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_019",
            displayName = "Royal Dagger Color",
            maleHashName = "MP_Xmas2_M_Tat_019",
            femaleHashName = "MP_Xmas2_F_Tat_019",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_020",
            displayName = "Time's Up Outline",
            maleHashName = "MP_Xmas2_M_Tat_020",
            femaleHashName = "MP_Xmas2_F_Tat_020",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_021",
            displayName = "Time's Up Color",
            maleHashName = "MP_Xmas2_M_Tat_021",
            femaleHashName = "MP_Xmas2_F_Tat_021",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_022",
            displayName = "You're Next Outline",
            maleHashName = "MP_Xmas2_M_Tat_022",
            femaleHashName = "MP_Xmas2_F_Tat_022",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_023",
            displayName = "You're Next Color",
            maleHashName = "MP_Xmas2_M_Tat_023",
            femaleHashName = "MP_Xmas2_F_Tat_023",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_024",
            displayName = "Snake Head Outline",
            maleHashName = "MP_Xmas2_M_Tat_024",
            femaleHashName = "MP_Xmas2_F_Tat_024",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_025",
            displayName = "Snake Head Color",
            maleHashName = "MP_Xmas2_M_Tat_025",
            femaleHashName = "MP_Xmas2_F_Tat_025",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_026",
            displayName = "Fuck Luck Outline",
            maleHashName = "MP_Xmas2_M_Tat_026",
            femaleHashName = "MP_Xmas2_F_Tat_026",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_027",
            displayName = "Fuck Luck Color",
            maleHashName = "MP_Xmas2_M_Tat_027",
            femaleHashName = "MP_Xmas2_F_Tat_027",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_028",
            displayName = "Executioner",
            maleHashName = "MP_Xmas2_M_Tat_028",
            femaleHashName = "MP_Xmas2_F_Tat_028",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpchristmas2_overlays",
            tattooName = "TAT_X2_029",
            displayName = "Beautiful Death",
            maleHashName = "MP_Xmas2_M_Tat_029",
            femaleHashName = "MP_Xmas2_F_Tat_029",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_000",
            displayName = "Bullet Proof",
            maleHashName = "MP_Gunrunning_Tattoo_000_M",
            femaleHashName = "MP_Gunrunning_Tattoo_000_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_001",
            displayName = "Crossed Weapons",
            maleHashName = "MP_Gunrunning_Tattoo_001_M",
            femaleHashName = "MP_Gunrunning_Tattoo_001_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_002",
            displayName = "Grenade",
            maleHashName = "MP_Gunrunning_Tattoo_002_M",
            femaleHashName = "MP_Gunrunning_Tattoo_002_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_003",
            displayName = "Lock & Load",
            maleHashName = "MP_Gunrunning_Tattoo_003_M",
            femaleHashName = "MP_Gunrunning_Tattoo_003_F",
            zone = "ZONE_HEAD"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_004",
            displayName = "Sidearm",
            maleHashName = "MP_Gunrunning_Tattoo_004_M",
            femaleHashName = "MP_Gunrunning_Tattoo_004_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_005",
            displayName = "Patriot Skull",
            maleHashName = "MP_Gunrunning_Tattoo_005_M",
            femaleHashName = "MP_Gunrunning_Tattoo_005_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_006",
            displayName = "Combat Skull",
            maleHashName = "MP_Gunrunning_Tattoo_006_M",
            femaleHashName = "MP_Gunrunning_Tattoo_006_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_007",
            displayName = "Stylized Tiger",
            maleHashName = "MP_Gunrunning_Tattoo_007_M",
            femaleHashName = "MP_Gunrunning_Tattoo_007_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_008",
            displayName = "Bandolier",
            maleHashName = "MP_Gunrunning_Tattoo_008_M",
            femaleHashName = "MP_Gunrunning_Tattoo_008_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_009",
            displayName = "Butterfly Knife",
            maleHashName = "MP_Gunrunning_Tattoo_009_M",
            femaleHashName = "MP_Gunrunning_Tattoo_009_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_010",
            displayName = "Cash Money",
            maleHashName = "MP_Gunrunning_Tattoo_010_M",
            femaleHashName = "MP_Gunrunning_Tattoo_010_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_011",
            displayName = "Death Skull",
            maleHashName = "MP_Gunrunning_Tattoo_011_M",
            femaleHashName = "MP_Gunrunning_Tattoo_011_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_012",
            displayName = "Dollar Daggers",
            maleHashName = "MP_Gunrunning_Tattoo_012_M",
            femaleHashName = "MP_Gunrunning_Tattoo_012_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_013",
            displayName = "Wolf Insignia",
            maleHashName = "MP_Gunrunning_Tattoo_013_M",
            femaleHashName = "MP_Gunrunning_Tattoo_013_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_014",
            displayName = "Backstabber",
            maleHashName = "MP_Gunrunning_Tattoo_014_M",
            femaleHashName = "MP_Gunrunning_Tattoo_014_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_015",
            displayName = "Spiked Skull",
            maleHashName = "MP_Gunrunning_Tattoo_015_M",
            femaleHashName = "MP_Gunrunning_Tattoo_015_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_016",
            displayName = "Blood Money",
            maleHashName = "MP_Gunrunning_Tattoo_016_M",
            femaleHashName = "MP_Gunrunning_Tattoo_016_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_017",
            displayName = "Dog Tags",
            maleHashName = "MP_Gunrunning_Tattoo_017_M",
            femaleHashName = "MP_Gunrunning_Tattoo_017_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_018",
            displayName = "Dual Wield Skull",
            maleHashName = "MP_Gunrunning_Tattoo_018_M",
            femaleHashName = "MP_Gunrunning_Tattoo_018_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_019",
            displayName = "Pistol Wings",
            maleHashName = "MP_Gunrunning_Tattoo_019_M",
            femaleHashName = "MP_Gunrunning_Tattoo_019_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_020",
            displayName = "Crowned Weapons",
            maleHashName = "MP_Gunrunning_Tattoo_020_M",
            femaleHashName = "MP_Gunrunning_Tattoo_020_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_021",
            displayName = "Have a Nice Day",
            maleHashName = "MP_Gunrunning_Tattoo_021_M",
            femaleHashName = "MP_Gunrunning_Tattoo_021_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_022",
            displayName = "Explosive Heart",
            maleHashName = "MP_Gunrunning_Tattoo_022_M",
            femaleHashName = "MP_Gunrunning_Tattoo_022_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_023",
            displayName = "Rose Revolver",
            maleHashName = "MP_Gunrunning_Tattoo_023_M",
            femaleHashName = "MP_Gunrunning_Tattoo_023_F",
            zone = "ZONE_LEFT_LEG"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_024",
            displayName = "Combat Reaper",
            maleHashName = "MP_Gunrunning_Tattoo_024_M",
            femaleHashName = "MP_Gunrunning_Tattoo_024_F",
            zone = "ZONE_RIGHT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_025",
            displayName = "Praying Skull",
            maleHashName = "MP_Gunrunning_Tattoo_025_M",
            femaleHashName = "MP_Gunrunning_Tattoo_025_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_026",
            displayName = "Restless Skull",
            maleHashName = "MP_Gunrunning_Tattoo_026_M",
            femaleHashName = "MP_Gunrunning_Tattoo_026_F",
            zone = "ZONE_RIGHT_LEG"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_027",
            displayName = "Serpent Revolver",
            maleHashName = "MP_Gunrunning_Tattoo_027_M",
            femaleHashName = "MP_Gunrunning_Tattoo_027_F",
            zone = "ZONE_LEFT_ARM"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_028",
            displayName = "Micro SMG Chain",
            maleHashName = "MP_Gunrunning_Tattoo_028_M",
            femaleHashName = "MP_Gunrunning_Tattoo_028_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_029",
            displayName = "Win Some Lose Some",
            maleHashName = "MP_Gunrunning_Tattoo_029_M",
            femaleHashName = "MP_Gunrunning_Tattoo_029_F",
            zone = "ZONE_TORSO"
            },
            new TattooModel {
            collection = "mpgunrunning_overlays",
            tattooName = "TAT_GR_030",
            displayName = "Pistol Ace",
            maleHashName = "MP_Gunrunning_Tattoo_030_M",
            femaleHashName = "MP_Gunrunning_Tattoo_030_F",
            zone = "ZONE_RIGHT_LEG"
            },
        };
        
        private static Dictionary<string, string> equippedTattoos = new Dictionary<string, string>();
        private Dictionary<string, string> previousTattoos = new Dictionary<string, string>();
        private MenuModel tattooMenu;
        private List<MenuModel> tattooSubMenus;
        private bool inTattooMenu = false;
        private const int tattooPrice = 25;

        public Tattoos(Client client) : base(client)
        {
            Task.Factory.StartNew(async () => {
                var _menuItems = new List<MenuItem>();
                var menuCategories = new Dictionary<string, List<MenuItem>>();
                await tattooModels.ForEachAsync(async o =>
                {
                    var zoneSplit = o.zone.Split('_').ToList();
                    zoneSplit.RemoveAt(0);
                    zoneSplit = zoneSplit.Select(b => b.ToLower().FirstLetterToUpper()).ToList();
                    var zoneName = string.Join(" ", zoneSplit);
                    if(!menuCategories.ContainsKey(zoneName)) menuCategories.Add(zoneName, new List<MenuItem>());
                    menuCategories[zoneName].Add(new MenuItemStandard
                    {
                        Title = o.displayName,
                        OnSelect = item =>
                        {
                            var tattooHash = Game.PlayerPed.Gender == Gender.Male ? o.maleHashName : o.femaleHashName;
                            
                            ((MenuItemStandard)item).Detail = equippedTattoos.ContainsKey(tattooHash) ? "(equipped)" : "";
                        },
                        OnActivate = item =>
                        {
                            var tattooHash = Game.PlayerPed.Gender == Gender.Male ? o.maleHashName : o.femaleHashName;
                            if (!equippedTattoos.ContainsKey(tattooHash))
                            {
                                API.ApplyPedOverlay(Game.PlayerPed.Handle, (uint) Game.GenerateHash(o.collection), (uint) Game.GenerateHash(tattooHash));
                                item.Detail = "(equipped)";
                                equippedTattoos.Add(tattooHash, o.collection);
                            }
                            else
                            {
                                equippedTattoos.Remove(tattooHash);
                                API.ClearPedDecorations(Game.PlayerPed.Handle);
                                equippedTattoos.ToList().ForEach(b =>
                                {
                                    API.ApplyPedOverlay(Game.PlayerPed.Handle, (uint)Game.GenerateHash(b.Value), (uint)Game.GenerateHash(b.Key));
                                });
                                item.Detail = "";
                            }
                        }
                    });
                    await BaseScript.Delay(0);
                });

                tattooSubMenus = new List<MenuModel>();
                menuCategories.ToList().ForEach(o =>
                {
                    tattooSubMenus.Add(new MenuModel
                    {
                        headerTitle = o.Key,
                        menuItems = o.Value
                    });
                    _menuItems.Add(new MenuItemSubMenu
                    {
                        Title = o.Key,
                        SubMenu = tattooSubMenus.Last()
                    });
                });

                tattooMenu = new MenuModel
                {
                    headerTitle = "Tattoos",
                    menuItems = _menuItems
                };

                client.Get<InteractionUI>().RegisterInteractionMenuItem(new MenuItemSubMenu
                {
                    Title = "Tattoos",
                    SubMenu = tattooMenu,
                    OnActivate = item =>
                    {
                        previousTattoos = new Dictionary<string, string>(equippedTattoos);
                        inTattooMenu = true;
                    }
                }, Locations.TattooParlours.Positions.Where(o => o.DistanceToSquared(Game.PlayerPed.Position) < 5.0f).Any, 510);    

                Client.RegisterEventHandler("Player.OnLoginComplete", new Action(OnLogin));
                Client.RegisterEventHandler("Player.CheckForInteraciton", new Action(OnInteraction));
                Client.RegisterTickHandler(OnTick);
            });
        }

        public static Dictionary<string, string> CurrentTattoos
        {
            get => equippedTattoos; 
            set => equippedTattoos = value;
        }

        private async Task OnTick()
        {
            if (inTattooMenu && InteractionUI.Observer.CurrentMenu != tattooMenu && !tattooSubMenus.Contains(InteractionUI.Observer.CurrentMenu))
            {
                inTattooMenu = false;
                var tattooChanges = equippedTattoos.Count - previousTattoos.Count;
                //Log.ToChat($"tattooChanges {tattooChanges}");
                var tattooCost = tattooChanges * tattooPrice;
                //Log.ToChat(tattooCost.ToString());
                if (tattooCost > 0)
                {
                    var playerSession = Client.Get<SessionManager>().GetPlayer(Game.Player);
                    if (playerSession == null) return;

                    if (await playerSession.CanPayForItem(tattooPrice))
                    {
                        Log.ToChat("[Bank]", $"You paid ${tattooCost} for the tattoo(s)", ConstantColours.Bank);
                        Magicallity.Client.Client.Instance.TriggerServerEvent("Payment.PayForItem", tattooCost, "tattoo shop");
                    }
                    else
                    {
                        Log.ToChat("[Bank]", $"You are not able to pay for these tattoos", ConstantColours.Bank);
                        equippedTattoos = previousTattoos;
                        API.ClearPedDecorations(Game.PlayerPed.Handle);
                        equippedTattoos.ToList().ForEach(o =>
                        {
                            API.ApplyPedOverlay(Game.PlayerPed.Handle, (uint) Game.GenerateHash(o.Value),
                                (uint) Game.GenerateHash(o.Key));
                        });
                    }
                }
                else
                {
                    if(tattooChanges != 0)
                        Log.ToChat("[Tattoo]", $"You removed {tattooChanges * -1} tattoos", ConstantColours.Tattoo);
                }
                CharacterEditorMenu.skinData = new PedData().getSaveableData(true)/*.ToExpando()*/;
                Magicallity.Client.Client.Instance.TriggerServerEvent("Skin.UpdatePlayerSkin", JsonConvert.SerializeObject(CharacterEditorMenu.skinData));
            }
        }

        private async void OnLogin()
        {
            await BlipHandler.AddBlipAsync("Tattoo Parlour", Locations.TattooParlours.Positions, new BlipOptions
            {
                Sprite = BlipSprite.TattooParlor
            });

            await MarkerHandler.AddMarkerAsync(Locations.TattooParlours.Positions, new MarkerOptions
            {
                ScaleFloat = 3.0f
            });
        }

        private void OnInteraction()
        {
            Locations.TattooParlours.Positions.ForEach(o =>
            {
                if (o.DistanceToSquared(Game.PlayerPed.Position) < 5.0f)
                {
                    InteractionUI.Observer.OpenMenu(tattooMenu);
                    equippedTattoos = new Dictionary<string, string>();
                    if (CharacterEditorMenu.skinData.Tattoos.GetType() != typeof(List<object>))
                    {
                        var tatData = (IDictionary<string, object>)CharacterEditorMenu.skinData.Tattoos;
                        tatData.ToList().ForEach(tattooData =>
                        {
                            equippedTattoos.Add(tattooData.Key.ToString(), tattooData.Value.ToString());
                        });
                    }
                    else
                    {
                        ((List<object>)CharacterEditorMenu.skinData.Tattoos).ForEach(m =>
                        {
                            var tatData = (IDictionary<string, object>)m;
                            dynamic tattooData = tatData.ToList().ToDictionary(b => b.Key, b => b.Value).ToExpando();
                            equippedTattoos.Add(tattooData.Key.ToString(), tattooData.Value.ToString());
                        });
                    }
                    previousTattoos = new Dictionary<string, string>(equippedTattoos);
                    inTattooMenu = true;
                }
            });
        }
    }
}
