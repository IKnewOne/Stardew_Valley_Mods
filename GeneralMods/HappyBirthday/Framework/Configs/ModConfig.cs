using System.IO;
using StardewModdingAPI;

namespace Omegasis.HappyBirthday.Framework.Configs
{
    /// <summary>The mod configuration.</summary>
    public class ModConfig
    {
        /// <summary>The key which shows the menu.</summary>
        public SButton KeyBinding { get; set; } = SButton.O;

        /// <summary>The minimum amount of friendship needed to get a happy birthday greeting from an npc.</summary>
        public int minimumFriendshipLevelForBirthdayWish = 2;

        /// <summary>
        /// The minimum amount of friendship needed with all villagers that would be present to get the saloon birthday party;
        /// </summary>
        public int minimumFriendshipLevelForCommunityBirthdayParty = 5;

        /// <summary>
        /// Attempts to use the English content pack when a properly localized one does not exist.
        /// </summary>
        public bool fallbackToEnglishTranslationWhenPossible=false;

        /// <summary>
        /// Makes it so that the default gift selection list provided by the mod doesn't populate the list of potential gifts you can receive from your birthday from your spouse.
        /// </summary>
        public bool useOnlyFavoriteGiftsListToSelectFrom = false;
        

        /// <summary>Construct an instance.</summary>
        public ModConfig()
        {
        }


        /// <summary>
        /// Initializes the config for the blacksmith shop prices.
        /// </summary>
        /// <returns></returns>
        public static ModConfig InitializeConfig()
        {
            if (HappyBirthdayModCore.Configs.doesConfigExist("ModConfig.json"))
            {
                return HappyBirthdayModCore.Configs.ReadConfig<ModConfig>("ModConfig.json");
            }
            else
            {
                ModConfig Config = new ModConfig();
                HappyBirthdayModCore.Configs.WriteConfig("ModConfig.json", Config);
                return Config;
            }
        }
    }
}
