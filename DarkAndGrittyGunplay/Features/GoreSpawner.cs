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
        public static GoreSpawner Singleton { get; private set; }

        private void Start()
        {
            Singleton = this;
        }

        private void Update()
        {

        }
    }
}
