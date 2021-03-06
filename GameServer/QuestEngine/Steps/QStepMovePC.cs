﻿using System.Collections.Generic;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.Quest.Tasks;
using Tera.Data.Structures.World.Continent;
using Utils;
using Utils.Logger;

namespace Tera.QuestEngine.Steps
{
    class QStepMovePc : QStepDefault
    {
        protected static Dictionary<int, int> TaskAreaIdSectionNameIdMap;

        static QStepMovePc()
        {
            TaskAreaIdSectionNameIdMap = new Dictionary<int, int>
                {
                    //Shrine of Yurian
                    {21300020, 13024},

                    //Tainted Gorge Bridge
                    {21300011, 13009},
                };
        }

        protected QTaskMovePC Task;

        public QStepMovePc(QTaskMovePC task)
        {
            Task = task;
        }

        public override void OnEnterZone(Player player, Section section)
        {
            if(section == null)
            {
                Logger.WriteLine(LogState.Warn,"QStepMovePc: Warning, current player section is NULL!");
                return;
            }

            if (Task.TargetArea == null
                || Task.TargetArea.Length < 2
                || !TaskAreaIdSectionNameIdMap.ContainsKey(Task.TargetArea[1]))
                return;

            if (TaskAreaIdSectionNameIdMap[Task.TargetArea[1]] == section.NameId)
                Processor.FinishStep(player);
        }
    }
}
