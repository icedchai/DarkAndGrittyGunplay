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
    internal class Gib : MonoBehaviour
    {
        internal bool isSplatter = true;
        internal KeyValuePair<string, Vector3> pair;
        internal SerializedSchematic schematicInfo;

        private void OnCollisionStay(Collision collision)
        {
            if (!isSplatter || collision.gameObject.TryGetComponent<Gib>(out _) || collision.gameObject.TryGetComponent<ItemPickupBase>(out _) || collision.gameObject.TryGetComponent<DoorVariant>(out _) || collision.gameObject.TryGetComponent<ReferenceHub>(out _))
            {
                return;
            }

            DecalRpcCache.SpawnDecal(collision.contacts[0].point, transform.position, DecalPoolType.Blood);
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

        internal void Activate(PlayerDeathEventArgs e)
        {
            if (isSplatter)
            {
                PrimitiveObjectToy gib = PrimitiveObjectToy.Get(GetComponent<AdminToys.PrimitiveObjectToy>());
                gib.Position = e.OldPosition + pair.Value + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
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


                var sphereCollider = gib.GameObject.AddComponent<SphereCollider>();
                sphereCollider.gameObject.layer = 1 << 25;
            }
            else
            {
                transform.position = e.OldPosition + pair.Value + schematicInfo.PositionOffset;
                transform.rotation = Quaternion.Euler(schematicInfo.RotationOffset);

                var rb = gameObject.AddComponent<Rigidbody>();
                rb.AddForce(pair.Value * 1000 + new Vector3(Random.Range(-700f, 700f), Random.Range(-700f, 700f), Random.Range(-700f, 700f)));
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                gameObject.layer = 1 << 25;
            }
        }
    }
}
