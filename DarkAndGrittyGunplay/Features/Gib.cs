using AdminToys;
using DarkAndGrittyGunplay.Configs;
using Decals;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Autosync;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using RelativePositioning;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utf8Json.Internal;
using Logger = LabApi.Features.Console.Logger;
using PrimitiveObjectToy = LabApi.Features.Wrappers.PrimitiveObjectToy;
using Random = UnityEngine.Random;

namespace DarkAndGrittyGunplay.Features
{
    public class Gib : MonoBehaviour
    {
        internal bool isSplatter = true;
        internal KeyValuePair<string, Vector3> pair;
        internal PlayerDeathEventArgs e;
        internal SerializedSchematic schematicInfo;

        private void OnCollisionStay(Collision collision)
        {
            if (!isSplatter || collision.gameObject.TryGetComponent<Gib>(out _) || collision.gameObject.TryGetComponent<ItemPickupBase>(out _) || collision.gameObject.TryGetComponent<DoorVariant>(out _) || collision.gameObject.TryGetComponent<ReferenceHub>(out _))
            {
                return;
            }

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, collision.contacts[0].normal);

            for (int i = -1; i < 1; i++)
            {
                DecalRpcCache.SpawnDecal(collision.contacts[0].point + (i) * (targetRotation * Vector3.right), transform.position);
            }

            //DecalRpcCache.PlaceBlood(collision.contacts[0].point, targetRotation.eulerAngles);

            /*PrimitiveObjectToy splat = PrimitiveObjectToy.Create(null, false);
            splat.Type = PrimitiveType.Cylinder;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, collision.contacts[0].normal) * transform.rotation;
            splat.Rotation = targetRotation;

            float diagonality = 1f - (float)Math.Sqrt(Math.Pow(targetRotation.eulerAngles.x % 90, 2) + Math.Pow(targetRotation.eulerAngles.y % 90, 2) + Math.Pow(targetRotation.eulerAngles.z % 90, 2)) / 45f;

            splat.Scale = new Vector3(GetComponent<Rigidbody>().linearVelocity.magnitude * diagonality / 5, 0.005f, GetComponent<Rigidbody>().linearVelocity.magnitude * diagonality / 5);
            if (collision.collider.TryGetComponent<AdminToyBase>(out _))
            {
                splat.Transform.parent = collision.gameObject.transform;
            }

            splat.Color = Color.red;
            splat.Flags = AdminToys.PrimitiveFlags.Visible;
            splat.Position = collision.contacts[0].point;
            splat.Spawn();*/

            Destroy(gameObject);
        }

        internal void Remove()
        {
            Destroy(gameObject);
        }

        public void Activate()
        {
            if (isSplatter)
            {
                PrimitiveObjectToy gib = PrimitiveObjectToy.Get(GetComponent<AdminToys.PrimitiveObjectToy>());
                gib.Position = e.OldPosition + pair.Value + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                
                /*
                TextToy text = TextToy.Create(gib.Transform, false);
                text.TextFormat = pair.Key;
                text.Transform.localPosition = new (1, 1, 1);
                text.Spawn();*/

            }
            else
            {
                transform.position = e.OldPosition + pair.Value + schematicInfo.PositionOffset;
                transform.rotation = Quaternion.Euler(schematicInfo.RotationOffset);
            }

            var rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce((pair.Value + new Vector3(0, 0.25f, 0)) * 1000 + (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1.5f), Random.Range(-1f, 1f)) * 700));
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            Timing.CallDelayed(Plugin.Singleton.Config.GibPhysicsLifetime, () =>
            {
                if (gameObject != null)
                {
                    Destroy(rb);

                    foreach(Collider collider in GetComponentsInChildren<Collider>())
                    {
                        Destroy(collider);
                    }
                }
            });

            Timing.CallDelayed(Plugin.Singleton.Config.GibLifetime, () =>
            {
                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
            });

            gameObject.layer = 1 << 25;
        }
    }
}
