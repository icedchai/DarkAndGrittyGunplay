using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkAndGrittyGunplay.Features.Enums;

/// <summary>
/// The various damage types.
/// </summary>
public enum DamageType
{
    /// <summary>
    /// None.
    /// </summary>
    None,

    /// <summary>
    /// Unknown damage type.
    /// </summary>
    Unknown,

    /// <summary>
    /// Damage from an explosion.
    /// </summary>
    Explosion,

    /// <summary>
    /// Any damage from SCP-096
    /// </summary>
    Scp096,

    /// <summary>
    /// Any type of damage from a Jailbird.
    /// </summary>
    Jailbird,

    /// <summary>
    /// Direct damage from a SCP-939 lunge.
    /// </summary>
    Scp939LungeTarget,

    /// <summary>
    /// Indirect damage from a SCP-939 lunge.
    /// </summary>
    Scp939LungeCollateral,

    /// <summary>
    /// Damage from an SCP-939 claw.
    /// </summary>
    Scp939Claw,
}
