using DarkAndGrittyGunplay.Configs;
using DarkAndGrittyGunplay.Features;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System.Dynamic;
using System.Numerics;
using UnityEngine;
using Utf8Json.Internal;
using Logger = LabApi.Features.Console.Logger;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace DarkAndGrittyGunplay.Events.Handlers
{
    public class EventHandlers
    {
        public void SubscribeEvents()
        {
            PlayerEvents.ChangedRole += OnPlayerChangedRole;
            PlayerEvents.Death += OnPlayerDeath;
            PlayerEvents.SpawningRagdoll += OnPlayerSpawningRagdoll;
        }

        public void UnsubscribeEvents()
        {
            PlayerEvents.ChangedRole -= OnPlayerChangedRole;
            PlayerEvents.Death -= OnPlayerDeath;
            PlayerEvents.SpawningRagdoll -= OnPlayerSpawningRagdoll;
        }

        Dictionary<string, Vector3> dict = new Dictionary<string, Vector3>()
        {
            { "chest", new Vector3(-0.01f, 0.27f, -0.01f) },
            { "head", new Vector3(-0.01f, 0.56f, 0.03f) },
            { "arm.l", new Vector3(-0.19f, 0.44f, 0.03f) },
            { "arm.r", new Vector3(0.15f, 0.45f, -0.04f) },
            { "forearm.l", new Vector3(-0.26f, 0.14f, -0.03f) },
            { "forearm.r", new Vector3(0.23f, 0.15f, -0.09f) },
            { "thigh.l", new Vector3(-0.1f, -0.08f, -0.04f) },
            { "thigh.r", new Vector3(0.08f, -0.07f, -0.06f) },
            { "leg.l", new Vector3(-0.13f, -0.48f, 0.09f) },
            { "leg.r", new Vector3(0.14f, -0.49f, -0.02f) },
        };

        Dictionary<Player, List<Gib>> gibs = new Dictionary<Player, List<Gib>>();

        private void OnRoundStarted()
        {
            gibs.Clear();
        }

        private void OnPlayerChangedRole(PlayerChangedRoleEventArgs e)
        {


            /* // generate dictionary for each bone & its position
            if (e.NewRole is IFpcRole fpcRole)
            {
                Timing.CallDelayed(3, () =>
                {
                    bool empty = false;
                    empty = dict.Count == 0;
                    foreach (HitboxIdentity hitbox in fpcRole.FpcModule.CharacterModelInstance.Hitboxes)
                    {
                        if (empty)
                        {
                            dict.Add(hitbox.name, hitbox.transform.position - e.Player.Position);
                        }
                    }
                });
            }*/
            if (!e.OldRole.IsHuman())
            {
                return;
            }

            if (gibs.TryGetValue(e.Player, out List<Gib> gibList))
            {
                foreach(Gib gib in gibList)
                {
                    gib.Remove();
                }
            }
            gibs.Remove(e.Player);


            List<Gib> spawnedGibs = new List<Gib>();

            Config config = Plugin.Singleton.Config;
            foreach (var pair in dict)
            {
                if (config.GoreSettings.TryGetValue(pair.Key.ToLower(), out var goreSpecs))
                {
                    for (int i = 0; i < goreSpecs.GoreBits; i++)
                    {
                        PrimitiveObjectToy gib = PrimitiveObjectToy.Create(Vector3.zero, null, false);
                        gib.Scale = new Vector3(0.1f, 0.1f, 0.1f);
                        gib.Type = PrimitiveType.Cube;
                        gib.Color = Color.red;
                        gib.Flags = AdminToys.PrimitiveFlags.Visible;
                        gib.MovementSmoothing = 60;
                        gib.Spawn();
                        /*
                        TextToy text = TextToy.Create(gib.Transform, false);
                        text.TextFormat = pair.Key;
                        text.Transform.localPosition = new (1, 1, 1);
                        text.Spawn();*/
                        Gib goreBit = gib.GameObject.AddComponent<Gib>();
                        goreBit.pair = pair;
                        spawnedGibs.Add(goreBit);
                    }
                    foreach (SerializedSchematic gib in goreSpecs.Gibs)
                    {
                        SchematicObject bit = ObjectSpawner.SpawnSchematic(gib.SchematicName, Vector3.zero);
                        Gib goreBit = bit.gameObject.AddComponent<Gib>();
                        goreBit.isSplatter = false;
                        goreBit.pair = pair;
                        goreBit.schematicInfo = gib;
                        spawnedGibs.Add(goreBit);
                    }
                }
            }
            gibs.Add(e.Player, spawnedGibs);
        }

        private void OnPlayerSpawningRagdoll(PlayerSpawningRagdollEventArgs e)
        {
            if (!e.RagdollPrefab.Role.IsHuman() && e.DamageHandler is ExplosionDamageHandler edh)
            {
                e.IsAllowed = false;
            }
        }

        private void OnPlayerDeath(PlayerDeathEventArgs e)
        {
            if (!e.OldRole.IsHuman() || e.DamageHandler is not ExplosionDamageHandler edh)
            {
                return;
            }
            Config config = Plugin.Singleton.Config;

            if (gibs.TryGetValue(e.Player, out List<Gib> gibList))
            {
                foreach(Gib gib in gibList)
                {
                    gib.Activate(e);
                }
            }
            gibs.Remove(e.Player);

            return;

            foreach (var pair in dict)
            {
                if (config.GoreSettings.TryGetValue(pair.Key.ToLower(), out var goreSpecs))
                {
                    for (int i = 0; i < goreSpecs.GoreBits; i++)
                    {
                        PrimitiveObjectToy gib = PrimitiveObjectToy.Create(e.OldPosition + pair.Value + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), null, false);
                        gib.Scale = new Vector3(0.1f, 0.1f, 0.1f);
                        gib.Type = PrimitiveType.Cube;
                        gib.Color = Color.red;
                        gib.Flags = AdminToys.PrimitiveFlags.Visible;
                        gib.MovementSmoothing = 60;
                        gib.Spawn();
                        /*
                        TextToy text = TextToy.Create(gib.Transform, false);
                        text.TextFormat = pair.Key;
                        text.Transform.localPosition = new (1, 1, 1);
                        text.Spawn();*/
                        var rb = gib.GameObject.AddComponent<Rigidbody>();
                        rb.AddForce(pair.Value * 1000 + new Vector3(Random.Range(-700f, 700f), Random.Range(-700f, 700f), Random.Range(-700f, 700f)));
                        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                        gib.GameObject.AddComponent<Gib>();


                        var sphereCollider = gib.GameObject.AddComponent<SphereCollider>();
                        sphereCollider.gameObject.layer =  1 << 25;
                    }
                    foreach (SerializedSchematic gib in goreSpecs.Gibs)
                    {
                        SchematicObject bit = ObjectSpawner.SpawnSchematic(gib.SchematicName, e.OldPosition + pair.Value + gib.PositionOffset);
                        bit.Rotation = Quaternion.Euler(gib.RotationOffset);
                        Gib goreBit = bit.gameObject.AddComponent<Gib>();
                        goreBit.isSplatter = false;
                        var rb = bit.gameObject.AddComponent<Rigidbody>();
                        rb.AddForce(pair.Value * 1000 + new Vector3(Random.Range(-700f, 700f), Random.Range(-700f, 700f), Random.Range(-700f, 700f)));
                        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                        bit.gameObject.layer = 1 << 25;
                    }
                }
            }
        }
    }
}
