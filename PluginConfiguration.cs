using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using System.Xml.Serialization;
using UnityEngine;
using Steamworks;

namespace KickOnDeath
{
    public class PluginConfiguration : IRocketPluginConfiguration
    {
        public string KickReason;
        public float WaitTime;
        public void LoadDefaults()
        {
            KickReason = "You died!";
            WaitTime = 2;
        }
    }
}
