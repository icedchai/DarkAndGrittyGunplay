using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DarkAndGrittyGunplay.Configs
{
    public class SerializedSchematic
    {
        public string SchematicName { get; set; } = "";

        public Vector3 PositionOffset { get; set; }

        public Vector3 RotationOffset { get; set; }

        public SerializedSchematic() { }

        public SerializedSchematic(string schematicName, Vector3 positionOffset, Vector3 rotationOffset)
        {
            SchematicName = schematicName;
            PositionOffset = positionOffset;
            RotationOffset = rotationOffset;
        }
        public SerializedSchematic(string schematicName, Vector3 positionOffset)
        {
            SchematicName = schematicName;
            PositionOffset = positionOffset;
        }
    }
}
