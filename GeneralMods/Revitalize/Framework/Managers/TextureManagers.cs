using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.StardustCore.UIUtilities;

namespace Omegasis.Revitalize.Framework.Managers
{
    public class TextureManagers
    {
        private static bool HasLoadedTextureManagers;

        public static TextureManager HUD;

        public static TextureManager Items_Resources_Ore;
        public static TextureManager Items_Crafting;

        public static TextureManager Objects_Crafting;
        public static TextureManager Objects_Furniture;
        public static TextureManager Objects_Machines;

        public static TextureManager Menus_Misc;
        public static TextureManager Menus_CraftingMenu;
        public static TextureManager Menus_EnergyMenu;
        public static TextureManager Menus_InventoryMenu;

        public static TextureManager Resources_Ore;

        public static TextureManager Resources_Misc;

        public static TextureManager Tools;

        /// <summary>
        /// Loads in textures to be used by the mod.
        /// </summary>
        public static void loadInTextures()
        {

            if (HasLoadedTextureManagers) return;

            HUD = InitializeTextureManager("Revitalize.HUD", Path.Combine("Content", "Graphics", "HUD"));

            Items_Resources_Ore = InitializeTextureManager("Revitalize.Items.Resources.Ore", Path.Combine("Content", "Graphics", "Items", "Resources", "Ore"));
            Items_Crafting = InitializeTextureManager("Revitalize.Items.Crafting", Path.Combine("Content", "Graphics", "Items", "Crafting"));

            Objects_Crafting = InitializeTextureManager("Revitalize.Objects.Crafting", Path.Combine("Content", "Graphics", "Objects", "Crafting"));
            Objects_Furniture = InitializeTextureManager("Revitalize.Furniture", Path.Combine("Content", "Graphics", "Objects", "Furniture"));
            Objects_Machines = InitializeTextureManager("Revitalize.Machines", Path.Combine("Content", "Graphics", "Objects", "Machines"));

            Menus_Misc = InitializeTextureManager("Revitalize.Menus", Path.Combine("Content", "Graphics", "Menus", "Misc"));
            Menus_CraftingMenu = InitializeTextureManager("Revitalize.CraftingMenu", Path.Combine("Content", "Graphics", "Menus", "CraftingMenu"));
            Menus_EnergyMenu = InitializeTextureManager("Revitalize.Menus.EnergyMenu", Path.Combine("Content", "Graphics", "Menus", "EnergyMenu"));
            Menus_InventoryMenu = InitializeTextureManager("Revitalize.InventoryMenu", Path.Combine("Content", "Graphics", "Menus", "InventoryMenu"));

            Resources_Ore = InitializeTextureManager("Revitalize.Resources.Ore", Path.Combine("Content", "Graphics", "Objects", "Resources", "Ore"));
            Resources_Misc = InitializeTextureManager("Revitalize.Items.Resources.Misc", Path.Combine("Content", "Graphics", "Items", "Resources", "Misc"));

            Tools = InitializeTextureManager("Revitalize.Tools", Path.Combine("Content", "Graphics", "Items", "Tools"));

            HasLoadedTextureManagers = true;
        }

        private static TextureManager InitializeTextureManager(string TextureManagerId, string TextureManagerPathToSearch)
        {
            TextureManager.AddTextureManager(RevitalizeModCore.Instance.Helper.DirectoryPath, RevitalizeModCore.Instance.ModManifest, TextureManagerId);
            TextureManager textureManager = TextureManager.GetTextureManager(RevitalizeModCore.Instance.ModManifest, TextureManagerId);
            textureManager.searchForTextures(RevitalizeModCore.ModHelper, RevitalizeModCore.Manifest, TextureManagerPathToSearch);
            return textureManager;
        }

    }
}
