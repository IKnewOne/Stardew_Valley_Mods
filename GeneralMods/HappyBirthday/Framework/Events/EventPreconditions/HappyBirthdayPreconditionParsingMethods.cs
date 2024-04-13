using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.HappyBirthday.Framework.Events.Compatibility;
using Omegasis.HappyBirthday.Framework.Utilities;

namespace Omegasis.HappyBirthday.Framework.Events.EventPreconditions
{
    public static class HappyBirthdayPreconditionParsingMethods
    {
        public static FarmerBirthdayPrecondition ParseFarmerBirthdayPrecondition(string[] preconditionData)
        {
            return new FarmerBirthdayPrecondition();
        }

        public static SpouseBirthdayPrecondition ParseSpouseBirthdayPrecondition(string[] preconditionData)
        {
            return new SpouseBirthdayPrecondition();
        }

        public static HasChosenBirthdayPrecondition ParseHasChosenBirthdayPrecondition(string[] preconditionData)
        {
            return new HasChosenBirthdayPrecondition(Convert.ToBoolean(preconditionData[1]));
        }

        public static HasChosenFavoriteGiftPrecondition ParseHasChosenFavoriteGiftPrecondition(string[] preconditionData)
        {
            return new HasChosenFavoriteGiftPrecondition(Convert.ToBoolean(preconditionData[1]));
        }

        public static IsMarriedToPrecondition ParseIsMarriedToPrecondition(string[] preconditionData)
        {
            return new IsMarriedToPrecondition(preconditionData[1]);
        }

        public static IsMarriedPrecondition ParseIsMarriedPrecondition(string[] preconditionData)
        {
            return new IsMarriedPrecondition();
        }

        public static GameLocationIsHomePrecondition ParseGameLocationIsHomePrecondition(string[] preconditionData)
        {
            return new GameLocationIsHomePrecondition();
        }

        public static FarmHouseLevelPrecondition ParseFarmHouseLevelPrecondition(string[] preconditionData)
        {
            //Since some legacy data does not use the full length, I need to check for the condition here that there is a second variable or not for the comparison of the farmhouse levels.
            if (preconditionData.Length == 3)
            {
                return new FarmHouseLevelPrecondition(Convert.ToInt32(preconditionData[1]), Enum.Parse<Enums.ComparisonType>(preconditionData[2]));
            }
            return new FarmHouseLevelPrecondition(Convert.ToInt32(preconditionData[1]));
        }

        public static YearPrecondition ParseYearGreaterThanOrEqualToPrecondition(string[] preconditionData)
        {
            return new YearPrecondition(Convert.ToInt32(preconditionData[1]), Enum.Parse<Enums.ComparisonType>(preconditionData[2]));
        }

        public static VillagersHaveEnoughFriendshipBirthdayPrecondition ParseVillagersHaveEnoughFriendshipBirthdayPrecondition(string[] preconditionData)
        {
            return new VillagersHaveEnoughFriendshipBirthdayPrecondition();
        }

        public static IsStardewValleyExpandedInstalledPrecondition ParseIsStardewValleyExpandedInstalledPrecondition(string[] preconditionData)
        {
            return new IsStardewValleyExpandedInstalledPrecondition(Convert.ToBoolean(preconditionData[1]));
        }
    }
}
