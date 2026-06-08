using DarkAndGrittyGunplay.Configs;
using DarkAndGrittyGunplay.Features.Enums;
using System.ComponentModel;
using UnityEngine;

namespace DarkAndGrittyGunplay;
#pragma warning disable SA1600 // Elements should be documented
public class Config
{
    [Description("How many people die simultaneously before blood bits are disallowed from spawning")]
    public int StopSpawningBloodBitsThreshold { get; set; } = 4;

    [Description("How many gibs can be activated per tick. The lower this value is, the easier on the server, but gibs will be noticeably delayed if there are a lot of exploding people.")]
    public int MaxGibsActivatedPerTick { get; set; } = 75;

    [Description("How long in seconds before a gib has its physics disabled")]
    public float GibPhysicsLifetime { get; set; } = 30;

    [Description("A number, such that the gib physics lifetime will elapse between zero and this number seconds more.")]
    public float GibPhysicsLifetimeVariance { get; set; } = 15f;

    [Description("How long in seconds before a gib is deleted automatically")]
    public float GibLifetime { get; set; } = 300;

    [Description("A number, such that the gib lifetime will elapse between zero and this number seconds more.")]
    public float GibLifetimeVariance { get; set; } = 15f;

    [Description("Determines which DamageTypes enable explosion deaths and more config on each one.")]
    public Dictionary<DamageType, GibEffectOption> AllowedExplosionDeaths { get; set; } = new Dictionary<DamageType, GibEffectOption>()
    {
        { DamageType.Scp939LungeTarget, new GibEffectOption() },
        { DamageType.Scp096, new GibEffectOption() },
        { DamageType.Jailbird, new GibEffectOption() },
        { DamageType.Explosion, new GibEffectOption() },
    };

    public Dictionary<string, BoneGoreSpecs> GoreSettings { get; set; } = new Dictionary<string, BoneGoreSpecs>()
    {
        {
            "head",
            new BoneGoreSpecs(5, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Eyeball", new Vector3(-0.0175f, 0.092f, 0.1079f)),
                new SerializedSchematic ("Brain", new Vector3(0, 0.092f, 0)),
                new SerializedSchematic ("Eyeball", new Vector3(0.0421f, 0.092f, 0.1079f)),
            })
        },
        {
            "chest",
            new BoneGoreSpecs(30, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Heart", Vector3.zero),
                new SerializedSchematic ("Kidney", Vector3.zero),
                new SerializedSchematic ("Liver", Vector3.zero),
                new SerializedSchematic ("Bone", new Vector3(0.03f, 0.5f)),
                new SerializedSchematic ("Bone", new Vector3(0.03f, 0f)),
            })
        },
        {
            "arm.l",
            new BoneGoreSpecs(2, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", new Vector3(0.03f, 0f), new Vector3(11.731f, 0f, -17.445f)),
            })
        },
        {
            "arm.r",
            new BoneGoreSpecs(2, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", Vector3.zero, new Vector3(11.731f, 0f, 17.445f)),
            })
        },
        {
            "forearm.l",
            new BoneGoreSpecs(2, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", Vector3.zero, new Vector3(-20f, 0f, -13f)),
            })
        },
        {
            "forearm.r",
            new BoneGoreSpecs(2, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", new Vector3(), new Vector3(-20f, 0f, 13f)),
            })
        },
        {
            "thigh.l",
            new BoneGoreSpecs(4, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", new Vector3(0.02f, -0.06f, 0.06f), new Vector3(-13, 3, -10)),
            })
        },
        {
            "leg.l",
            new BoneGoreSpecs(2, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", new Vector3(0, -3f), new Vector3(8, 4, 0)),
            })
        },
        {
            "thigh.r",
            new BoneGoreSpecs(4, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", new Vector3(0.02f, -0.5f, 0.03f), new Vector3(-3, 0, 8)),
            })
        },
        {
            "leg.r",
            new BoneGoreSpecs(2, new List<SerializedSchematic>()
            {
                new SerializedSchematic ("Bone", Vector3.zero, new Vector3(11, 1, 7)),
            })
        },
    };
}
