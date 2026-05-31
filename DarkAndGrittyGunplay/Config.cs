using DarkAndGrittyGunplay.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DarkAndGrittyGunplay
{
    public class Config
    {
        public Dictionary<string, BoneGoreSpecs> GoreSettings { get; set; } = new Dictionary<string, BoneGoreSpecs>()
        {
            {
                "head",
                new BoneGoreSpecs(20, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Eyeball", new Vector3(-0.0175f, 0.092f, 0.1079f)),
                    new SerializedSchematic ("Brain", new Vector3(0, 0.092f, 0)),
                    new SerializedSchematic ("Eyeball", new Vector3(0.0421f, 0.092f, 0.1079f))
                })
            },
            {
                "chest",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Heart", Vector3.zero, new Vector3(11.731f, 0f, 17.445f))
                })
            },
            {
                "arm.l",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", new Vector3(0.03f, 0f), new Vector3(11.731f, 0f, -17.445f))
                })
            },
            {
                "arm.r",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", Vector3.zero, new Vector3(11.731f, 0f, 17.445f))
                })
            },
            {
                "forearm.l",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", Vector3.zero, new Vector3(-20f, 0f, -13f))
                })
            },
            {
                "forearm.r",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", new Vector3(), new Vector3(-20f, 0f, 13f))
                })
            },
            {
                "thigh.l",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", new Vector3(0.02f, -0.06f, 0.06f), new Vector3(-13, 3, -10))
                })
            },
            {
                "leg.l",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", new Vector3(0, -3f), new Vector3(8, 4, 0))
                })
            },
            {
                "thigh.r",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", new Vector3(0.02f, -0.5f, 0.03f), new Vector3(-3, 0, 8))
                })
            },
            {
                "leg.r",
                new BoneGoreSpecs(5, new List<SerializedSchematic>()
                {
                    new SerializedSchematic ("Bone", Vector3.zero, new Vector3(11, 1, 7))
                })
            },
        };
    }
}
