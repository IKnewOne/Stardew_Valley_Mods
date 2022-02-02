using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace StardustCore.Events.Preconditions.PlayerSpecific
{
    public class CommunityCenterCompleted: EventPrecondition
    {
        /// <summary>
        /// False means the community center doesn't need to be completed.
        /// </summary>
        public bool communityCenterNeedsToBeCompleted;

        public CommunityCenterCompleted()
        {

        }

        public CommunityCenterCompleted(bool NeedsToBeCompleted)
        {
            this.communityCenterNeedsToBeCompleted = NeedsToBeCompleted;
        }

        public override string ToString()
        {
            return "StardustCore.Events.Preconditions.Player.CommunityCenterCompleted "+this.communityCenterNeedsToBeCompleted;
        }

        public override bool meetsCondition()
        {
            return this.communityCenterNeedsToBeCompleted == Game1.player.hasCompletedCommunityCenter();
        }

    }
}
