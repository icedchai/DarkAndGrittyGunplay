using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LabApi.Features.Console;
using Logger = LabApi.Features.Console.Logger;

namespace DarkAndGrittyGunplay.Features
{
    public class GoreSpawner : MonoBehaviour
    {
        public static GoreSpawner Singleton { get; internal set; }

        public Queue<Gib> gibQueue { get; private set; }

        /// <summary>
        /// Gets the number of players who have died last frame in a way that will lead to them gibbing.
        /// </summary>
        public int CurrentExplosionDeaths { get; internal set; } = 0;

        private void Start()
        {
            Singleton = this;
            gibQueue = new Queue<Gib>();
        }

        private void Update()
        {
            if (CurrentExplosionDeaths != 0)
            {
                CurrentExplosionDeaths = 0;
            }

            if (!gibQueue.IsEmpty())
            {
                for (int i = 0; i < Plugin.Singleton.Config.MaxGibsActivatedPerTick; i++)
                {
                    if (!gibQueue.TryDequeue(out Gib? gib))
                    {
                        return;
                    }

                    if (gib == null)
                    {
                        continue;
                    }

                    gib.Activate();
                }
            }
        }
    }
}
