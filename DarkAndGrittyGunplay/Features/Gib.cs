using AdminToys;
using Decals;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Autosync;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Pickups;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using RelativePositioning;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;
using PrimitiveObjectToy = LabApi.Features.Wrappers.PrimitiveObjectToy;

namespace DarkAndGrittyGunplay.Features
{
    internal class Gib : MonoBehaviour
    {
        internal bool isSplatter = true;
        private void OnCollisionStay(Collision collision)
        {
            if (!isSplatter || collision.gameObject.TryGetComponent<Gib>(out _) || collision.gameObject.TryGetComponent<ItemPickupBase>(out _) || collision.gameObject.TryGetComponent<DoorVariant>(out _) || collision.gameObject.TryGetComponent<ReferenceHub>(out _))
            {
                return;
            }

            DecalRpcCache.SpawnDecal(collision.contacts[0].point, transform.position, DecalPoolType.Blood);
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
    }
}
