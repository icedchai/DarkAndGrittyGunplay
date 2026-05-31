using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DarkAndGrittyGunplay.Configs
{
    public class BoneGoreSpecs
    {
        public BoneGoreSpecs() {}

        public BoneGoreSpecs(int goreBits, List<SerializedSchematic> gibs)
        {
            GoreBits = goreBits;
            Gibs = gibs;
        }

        public int GoreBits { get; set; } = 5;

        public List<SerializedSchematic> Gibs { get; set; } = new List<SerializedSchematic>() { new SerializedSchematic("Bone", Vector3.zero, Vector3.zero)};
    }
}
