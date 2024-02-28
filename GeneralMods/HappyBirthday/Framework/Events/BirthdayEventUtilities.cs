using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Omegasis.HappyBirthday.Framework.Constants;
using Omegasis.HappyBirthday.Framework.ContentPack;
using Omegasis.HappyBirthday.Framework.Events.Compatibility;
using Omegasis.HappyBirthday.Framework.Events.EventPreconditions;
using Omegasis.HappyBirthday.Framework.Menus;
using Omegasis.StardustCore.Events;
using StardewModdingAPI.Events;
using StardewValley;

namespace Omegasis.HappyBirthday.Framework.Events
{
    public static class BirthdayEventUtilities
    {
        /// <summary>
        /// A flag used to gate specific dialogue parsing to ensure that the <see cref="HappyBirthdayEventHelper"/> can write the generic spouse data to a json string and parse it properly for when serving it to the game for events.
        /// </summary>
        public static bool NEED_TO_WRITE_DEFAULT_BIRTHDAY_EVENTS_TO_JSON;

        public static EventManager BirthdayEventManager
        {
            get
            {
                return EventManager.Instance;
            }
        }
        public static bool ShouldAskPlayerForBirthday;
        public static bool ShouldAskPlayerForFavoriteGift;

        public static void Player_Warped(object sender, WarpedEventArgs e)
        {
            StartEventAtLocationIfPossible(e.NewLocation);
        }

        public static void OnDayStarted()
        {
            StartEventAtLocationIfPossible(Game1.player.currentLocation);
        }

        public static void StartEventAtLocationIfPossible(GameLocation location)
        {
            BirthdayEventManager.startEventsAtLocationIfPossible();
        }

        public static void ClearEventsFromFarmer()
        {
            foreach (KeyValuePair<string, EventHelper> v in BirthdayEventManager.events)
                BirthdayEventManager.clearEventFromFarmer(v.Key);
        }

        public static void InitializeBirthdayEventCommands()
        {
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.ShowTranslatedMessage", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.showTranslatedMessage));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.SpeakWithBirthdayMessageIncluded", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.speakWithBirthdayIncluded));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.SpeakWithTranslatedMessage", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.speakWithTranslatedMessage));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.SpeakWithTranslatedMessageFromFile", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.speakWithTranslatedMessageFromFile));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.SpeakIfTodayIsPlayersBirthday", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.speakIfTodayIsPlayersBirthday));


            //Menu commands.
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.ShowBirthdaySelectionMenu", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.setShouldShowChooseBirthdayMenu));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.ShowFavoriteGiftSelectionMenu", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.setShouldShowChooseFavoriteGiftMenu));

            //Utility Commands
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.SkipNextCommand", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.skipNextCommand));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.GivePlayerFavoriteGift", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.givePlayerFavoriteGift));
            Event.RegisterCommand("Omegasis.HappyBirthday.Events.MakeObjectsTemporarilyInvisible", new StardewValley.Delegates.EventCommandDelegate(BirthdayEventCommands.makeObjectsTemporarilyInvisible));

            //TODO: Maybe make preconditions registered properly using  Event.RegisterPrecondition
            //Additional Preconditions
            BirthdayEventManager.eventPreconditionParsingMethods.Add(FarmerBirthdayPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseFarmerBirthdayPrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(SpouseBirthdayPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseSpouseBirthdayPrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(HasChosenBirthdayPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseHasChosenBirthdayPrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(HasChosenFavoriteGiftPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseHasChosenFavoriteGiftPrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(IsMarriedToPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseIsMarriedToPrecondition);

            BirthdayEventManager.eventPreconditionParsingMethods.Add(IsMarriedPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseIsMarriedPrecondition);

            BirthdayEventManager.eventPreconditionParsingMethods.Add(GameLocationIsHomePrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseGameLocationIsHomePrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(FarmHouseLevelPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseFarmHouseLevelPrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(YearPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseYearGreaterThanOrEqualToPrecondition);
            BirthdayEventManager.eventPreconditionParsingMethods.Add(VillagersHaveEnoughFriendshipBirthdayPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseVillagersHaveEnoughFriendshipBirthdayPrecondition);

            //Compatibility event preconditions
            BirthdayEventManager.eventPreconditionParsingMethods.Add(IsStardewValleyExpandedInstalledPrecondition.EventPreconditionId, HappyBirthdayPreconditionParsingMethods.ParseIsStardewValleyExpandedInstalledPrecondition);
        }

        public static void InitializeBirthdayEvents()
        {
            foreach(HappyBirthdayContentPack contentPack in HappyBirthdayModCore.Instance.happyBirthdayContentPackManager.getHappyBirthdayContentPacksForCurrentLanguageCode())
            {
                contentPack.loadBirthdayEvents();
            }
        }

        public static void UpdateEventManager()
        {
            if (BirthdayEventManager != null)
                BirthdayEventManager.update();
        }




        /// <summary>
        /// Gets a string to be displayed during the event.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetEventString(string Key)
        {
            string eventString = HappyBirthdayModCore.Instance.translationInfo.getEventString(Key);
            return ReplaceSpecialDialogueTokens(eventString);
        }

        public static string ReplaceSpecialDialogueTokens(string originalString)
        {
            string modifiedString = originalString;
            modifiedString = modifiedString.Replace("{AffectionateSpouseWord}", HappyBirthdayModCore.Instance.birthdayMessages.getAffectionateSpouseWord());
            modifiedString = modifiedString.Replace("{TimeOfDay}", HappyBirthdayModCore.Instance.birthdayMessages.getTimeOfDayString());
            modifiedString = modifiedString.Replace("@", Game1.player.Name);
            return modifiedString;
        }

    }
}
