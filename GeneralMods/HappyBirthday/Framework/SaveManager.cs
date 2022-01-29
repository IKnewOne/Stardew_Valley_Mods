using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.HappyBirthday.Framework.Utilities;
using StardewValley;

namespace Omegasis.HappyBirthday.Framework
{
    /// <summary>
    /// Manages save data for happy birthday.
    /// </summary>
    public static class SaveManager
    {

        /// <summary>
        /// Forces a read when the day starts since for farmhands they don't seem to hook into the OnSave/OnLoad methods with SMAPI.
        /// </summary>
        public static void OnDayStarted(object Sender, StardewModdingAPI.Events.DayStartedEventArgs args)
        {
            if (HappyBirthday.Instance.birthdayManager.hasChosenBirthday() == false)
            {
                HappyBirthday.Instance.Monitor.Log("Loading player's birthday on new day started.");
                Load(Game1.player.uniqueMultiplayerID);
            }
        }

        /// <summary>
        /// Forces player's birthday to be saved at the end of the day.
        /// </summary>
        public static void OnDayEnded(object Sender, StardewModdingAPI.Events.DayEndingEventArgs args)
        {
            Save(Game1.player.uniqueMultiplayerID);
        }

        /// <summary>
        /// Called when the day has ended. Updates the mod's birthday info and villager queue if they have changed.
        /// </summary>
        /// <param name="UniqueMultiplayerId"></param>
        public static void Save(long UniqueMultiplayerId)
        {
            Farmer player = Game1.getFarmer(UniqueMultiplayerId);
            string uniqueSaveName = $"{player.Name}_{player.UniqueMultiplayerID}";
            string dataDirectory = Path.Combine("data", uniqueSaveName);
            string dataFilePath = Path.Combine(dataDirectory,uniqueSaveName+".json");
            string villagerQueuePath = Path.Combine(dataDirectory , uniqueSaveName + "_VillagerBirthdayGiftsQueue.json");

            if (HappyBirthday.Instance.birthdayManager.hasChosenBirthday())
            {
                Directory.CreateDirectory(dataDirectory);

                //Write birthday file to disk.
                HappyBirthday.Instance.Helper.Data.WriteJsonFile(dataFilePath, HappyBirthday.Instance.birthdayManager.playerBirthdayData);
                HappyBirthday.Instance.Helper.Data.WriteJsonFile(villagerQueuePath, HappyBirthday.Instance.birthdayManager.villagerQueue);
            }
        }

        /// <summary>
        /// Loads the player's birthday information from disk.
        /// </summary>
        /// <param name="UniqueMultiplayerId"></param>
        public static void Load(long UniqueMultiplayerId)
        {
            Farmer player=Game1.getFarmer(UniqueMultiplayerId);
            string uniqueSaveName = $"{player.Name}_{player.UniqueMultiplayerID}";
            string dataDirectory = Path.Combine("data", uniqueSaveName);
            string dataFilePath = Path.Combine(dataDirectory, uniqueSaveName + ".json");
            string villagerQueuePath = Path.Combine(dataDirectory, uniqueSaveName + "_VillagerBirthdayGiftsQueue.json");
            // reset state
            HappyBirthday.Instance.birthdayManager.setCheckedForBirthday(false);

            //Loads the player's birthday from disk.
            HappyBirthday.Instance.birthdayManager.playerBirthdayData = HappyBirthday.Instance.Helper.Data.ReadJsonFile<PlayerData>(dataFilePath) ?? new PlayerData();
            HappyBirthday.Instance.birthdayManager.villagerQueue = HappyBirthday.Instance.Helper.Data.ReadJsonFile<Dictionary<string, VillagerInfo>>(villagerQueuePath) ?? new Dictionary<string, VillagerInfo>();

            if (HappyBirthday.Instance.birthdayManager.playerBirthdayData != null)
            {
                //This is still necessary to so that other players know when this player's birthday is.
                MultiplayerUtilities.SendBirthdayInfoToOtherPlayers();
            }
        }

    }
}