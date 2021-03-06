﻿using System;
using Tera.Communication;
using Tera.Data.Enums;
using Tera.Data.Interfaces;
using Tera.Network.old_Server;
using Utils;
using Utils.Logger;

namespace Tera.AdminEngine.AdminCommands
{
    class NpcCommand : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                string[] args = msg.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                Data.Structures.Npc.Npc npc = null;
                double minDistance = double.MaxValue;

                foreach (var visibleNpc in connection.Player.VisibleNpcs)
                {
                    double d = visibleNpc.Position.DistanceTo(connection.Player.Position);
                    if (d < minDistance)
                    {
                        npc = visibleNpc;
                        minDistance = d;
                    }
                }

                if (npc == null)
                    return;

                switch (args[0])
                {
                    case "name":
                        new SpChatMessage(string.Format("Name: {0},{1}",
                                                        npc.NpcTemplate.HuntingZoneId,
                                                        npc.NpcTemplate.Id), ChatType.System).Send(connection);
                        return;

                    case "skill":
                        if (args.Length > 1)
                        {
                            int skillId = int.Parse(args[1]);
                            npc.Target = connection.Player;
                            Global.SkillEngine.UseSkill(npc, Data.Data.Skills[npc.NpcTemplate.HuntingZoneId][npc.NpcTemplate.Id][skillId]);
                            
                            return;
                        }

                        foreach (var skill in Data.Data.Skills[npc.NpcTemplate.HuntingZoneId][npc.NpcTemplate.Id].Values)
                            new SpChatMessage(string.Format("Skill: {0}", skill.Id), ChatType.System).Send(connection);

                        return;
                }
            }
            catch(Exception ex)
            {
                new SpChatMessage("Error: " + ex.Message, ChatType.System).Send(connection);
                Logger.WriteLine(LogState.Exception,"AdminEngine: Npc:" + ex);
            }
        }
    }
}
