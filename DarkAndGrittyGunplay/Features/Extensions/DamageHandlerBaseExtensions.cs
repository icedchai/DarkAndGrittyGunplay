using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkAndGrittyGunplay.Features.Enums;
using PlayerRoles.PlayableScps.Scp939;
using PlayerStatsSystem;

namespace DarkAndGrittyGunplay.Features.Extensions;

/// <summary>
/// <see cref="DamageHandlerBase"/> extensions.
/// </summary>
public static class DamageHandlerBaseExtensions
{
    public static DamageType GetDamageType(this DamageHandlerBase damageHandler)
    {
        switch (damageHandler)
        {
            case ExplosionDamageHandler:
                return DamageType.Explosion;
            case JailbirdDamageHandler:
                return DamageType.Jailbird;
            case Scp939DamageHandler dh939:
                switch (dh939.Scp939DamageType)
                {
                    case Scp939DamageType.LungeTarget:
                        return DamageType.Scp939LungeTarget;
                    case Scp939DamageType.LungeSecondary:
                        return DamageType.Scp939LungeCollateral;
                    case Scp939DamageType.Claw:
                        return DamageType.Scp939Claw;
                    default:
                        return DamageType.None;
                }

            case Scp096DamageHandler:
                return DamageType.Scp096;
            default:
                return DamageType.Unknown;
        }
    }
}
