using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DarkAndGrittyGunplay.Features
{
    public class GoreSpawner : MonoBehaviour
    {
        public static GoreSpawner Singleton { get; internal set; }

        public Queue<Gib> gibQueue { get; private set; }


        private void Start()
        {
            Singleton = this;
            gibQueue = new Queue<Gib>();
        }

        private void Update()
        {
            if (!gibQueue.IsEmpty())
            {
                for (int i = 0; i < Plugin.Singleton.Config.MaxGibsActivatedPerTick; i++)
                {
                    Gib gib = gibQueue.Dequeue();
                    if (gib == null)
                    {
                        return;
                    }

                    gib.Activate();
                }
            }
        }
    }
}
