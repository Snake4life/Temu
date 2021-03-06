﻿using Tera.Communication;
using Tera.Communication.Logic;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;
using Utils;

namespace Tera.Controllers
{
    class DeathController : IController
    {
        public Player Player;

        protected long AutoRebirthUts = 0;

        public void Start(Player player)
        {
            Player = player;

            AutoRebirthUts = RandomUtilities.GetCurrentMilliseconds() + 1200000; //30 minutes

            Player.LifeStats.Kill();
            PlayerLogic.PleyerDied(player);
        }

        public void Release()
        {
            Player.LifeStats.Rebirth();
            Global.VisibleService.Send(Player, new SpCharacterDeath(Player, false));
            Player = null;
        }

        public void Action()
        {
            if (RandomUtilities.GetCurrentMilliseconds() >= AutoRebirthUts)
                PlayerLogic.Ressurect(Player, 0, -1);
        }
    }
}
