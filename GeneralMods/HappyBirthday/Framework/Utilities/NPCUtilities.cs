using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;

namespace Omegasis.HappyBirthday.Framework.Utilities
{
    public static class NPCUtilities
    {
        public static NPC LastSpeaker;

        public static List<NPC> GetAllNpcs()
        {
            List<NPC> npcs = new List<NPC>();
            foreach (GameLocation location in Game1.locations)
            {
                foreach (NPC npc in location.characters)
                {
                    npcs.Add(npc);
                }
            }
            return npcs;
        }

        /// <summary>
        /// Gets all human npcs.
        /// </summary>
        /// <returns></returns>
        public static List<NPC> GetAllHumanNpcs()
        {
            List<NPC> npcs = new List<NPC>();
            foreach (NPC npc in GetAllNpcs())
            {
                if (npc is Child || npc is Horse || npc is Junimo || npc is Monster || npc is Pet)
                    continue;
                npcs.Add(npc);
            }
            return npcs;
        }

        public static List<NPC> GetAllNonSpecialHumanNpcs()
        {
            List<NPC> npcs = new List<NPC>();
            foreach (NPC npc in GetAllHumanNpcs())
            {
                if (npc.Name.Equals("Mr. Qi") || npc.Name.Equals("Mister Qi") || npc.Name.Equals("Birdie") || npc.Name.Equals("Henchman") || npc.Name.Equals("Gunther") || npc.Name.Equals("Bouncer") || npc.Name.Equals("Marlon"))
                    continue;
                npcs.Add(npc);
            }
            return npcs;
        }

        /// <summary>
        /// Checks to see if an npc should wish the player happy birthday or not.
        /// </summary>
        /// <param name="NpcName"></param>
        /// <returns></returns>
        public static bool ShouldWishPlayerHappyBirthday(string NpcName)
        {
            if (HappyBirthdayModCore.Instance.birthdayManager.isVillagerInQueue(NpcName) == false) return false;
            if (HappyBirthdayModCore.Instance.birthdayManager.hasGivenBirthdayWish(NpcName) == true) return false;
            if (Game1.player.getFriendshipHeartLevelForNPC(NpcName) < HappyBirthdayModCore.Configs.modConfig.minimumFriendshipLevelForBirthdayWish)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to see if an npc should give the player a gift or not.
        /// </summary>
        /// <param name="NpcName"></param>
        /// <returns></returns>
        public static bool ShouldGivePlayerBirthdayGift(string NpcName)
        {
            if (HappyBirthdayModCore.Instance.birthdayManager.isVillagerInQueue(NpcName) == false) return false;
            if (HappyBirthdayModCore.Instance.birthdayManager.hasGivenBirthdayGift(NpcName) == true) return false;
            if (Game1.player.getFriendshipHeartLevelForNPC(NpcName) < HappyBirthdayModCore.Configs.modConfig.minimumFriendshipLevelForBirthdayWish)
            {
                return false;
            }
            return true;
        }

    }
}
