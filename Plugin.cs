using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.Core.Plugins;
using Steamworks;
using UnityEngine;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Rocket.API.Collections;
using System.Timers;
using Rocket.Unturned.Events;
using System.Collections;
using Rocket.API;

namespace KickOnDeath
{
    public class Plugin : RocketPlugin<PluginConfiguration>
    {
        private const string Discord = "discord.gg/4Fq2Spe";
        public static Plugin Instance;
        protected override void Load()
        {
            Instance = this;
            Rocket.Core.Logging.Logger.LogWarning("KickOnDeath by Anomoly");
            Rocket.Core.Logging.Logger.LogWarning("Have an issue? Join my Discord: " + Discord);
            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.LogWarning("KickOnDeath has unloaded!");
            UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
        }
        private void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, CSteamID murderer)
        {
            if (Configuration.Instance.KickForAnyDeath)
            {
                StartCoroutine(DeathKick(player));
            }
            if (Configuration.Instance.KickOnSucide)
            {
                if(cause.ToString() == "SUCIDE")
                {
                    StartCoroutine(DeathKick(player));
                }
            }
            
        }
        public IEnumerator DeathKick(UnturnedPlayer player)
        {
            if (player.HasPermission("kick.ignore"))
            {
                yield break;
            }
            else if(Configuration.Instance.WaitTime <= 0f)
            {
                yield return new WaitForSeconds(1f);
                player.Kick(Configuration.Instance.KickReason);
            }
            else
            {
                yield return new WaitForSeconds(Configuration.Instance.WaitTime);
                player.Kick(Configuration.Instance.KickReason);
            }
        }
    }
}
