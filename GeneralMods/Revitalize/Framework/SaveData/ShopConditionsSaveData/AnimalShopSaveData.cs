using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Revitalize.Framework.SaveData.ShopConditionsSaveData
{
    public class AnimalShopSaveData:SaveDataBase
    {
        [JsonProperty]
        protected bool hasBuiltTier2BarnOrCoop;

        public AnimalShopSaveData()
        {

        }

        public override void save()
        {
            ModCore.ModHelper.Data.WriteJsonFile(Path.Combine(ModCore.SaveDataManager.getFullSaveDataPath(), "ShopConditionsSaveData", "AnimalShopSaveData.json"), this);
            this.cleanUpPostSave();
        }

        public virtual void setHasBuiltTier2OrHigherBarnOrCoop()
        {
            this.hasBuiltTier2BarnOrCoop = true;
            this.shouldSaveData = true;
        }

        public virtual bool getHasBuiltTier2OrHigherBarnOrCoop()
        {
            return this.hasBuiltTier2BarnOrCoop;
        }

        public static AnimalShopSaveData LoadOrCreate()
        {
            if (File.Exists(Path.Combine(ModCore.SaveDataManager.getFullSaveDataPath(), "ShopConditionsSaveData", "AnimalShopSaveData.json")))
                return ModCore.ModHelper.Data.ReadJsonFile<AnimalShopSaveData>(Path.Combine(ModCore.SaveDataManager.getRelativeSaveDataPath(), "ShopConditionsSaveData", "AnimalShopSaveData.json"));
            else
            {
                AnimalShopSaveData Config = new AnimalShopSaveData();
                ModCore.ModHelper.Data.WriteJsonFile(Path.Combine(ModCore.SaveDataManager.getRelativeSaveDataPath(), "ShopConditionsSaveData", "AnimalShopSaveData.json"), Config);
                return Config;
            }
        }

    }
}