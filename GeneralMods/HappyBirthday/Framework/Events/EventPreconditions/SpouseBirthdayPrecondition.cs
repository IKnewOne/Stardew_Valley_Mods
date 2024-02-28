using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.StardustCore.Events.Preconditions;
using StardewValley;

namespace Omegasis.HappyBirthday.Framework.Events.EventPreconditions
{
    public class SpouseBirthdayPrecondition:EventPrecondition
    {
        public const string EventPreconditionId = "Omegasis.HappyBirthday.Framework.EventPreconditions.SpouseBirthdayPrecondition";

        public SpouseBirthdayPrecondition()
        {

        }

        public override string ToString()
        {
            return EventPreconditionId;
        }

        public override bool meetsCondition()
        {
            if (Game1.player.getSpouse() == null) return false;
            else
            {
                NPC spouse = Game1.player.getSpouse();
                if (spouse.isBirthday()){
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
