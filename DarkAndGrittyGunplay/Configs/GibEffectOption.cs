using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkAndGrittyGunplay.Configs;

/// <summary>
/// Defines a death effect option.
/// </summary>
public class GibEffectOption
{
    /// <summary>
    /// Gets or sets a value indicating whether to gib a player on death.
    /// </summary>
    public bool ExplodeOnDeath { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to disallow the ragdoll from spawning.
    /// </summary>
    public bool DisallowRagdoll { get; set; } = true;
}
