using Microsoft.Xna.Framework;
using SilkyUIMVVMTutorial.Common;
using SilkyUIMVVMTutorial.LegacyUIExample;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SilkyUIMVVMTutorial.Content.Items;

public class ZhuLegacyUIInteractor : ModItem
{
    public override string Texture => $"Terraria/Images/Item_{ItemID.PiggyBank}";
    public override void SetDefaults()
    {
        Item.useTime = Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
    }
    public override bool? UseItem(Player player)
    {
        if (Main.dedServ || player.itemAnimation != 15) return null;
        var zhuPlayer = player.GetModPlayer<ZhuZhuPlayer>();
        if (player.altFunctionUse == 2)
        {
            zhuPlayer.zhuList.Clear();
            LegacyZhuSystem.Instance?.ZhuUIInstance?.SetupList();
        }
        else
        {
            zhuPlayer.zhuity = 0;
            LegacyZhuSystem.Instance?.ZhuUIInstance?.SetupText();
        }
        return base.UseItem(player);
    }
    public override void AddRecipes()
    {
        CreateRecipe().Register();
    }
    public override bool AltFunctionUse(Player player) => true;

    public override Color? GetAlpha(Color lightColor) => Color.Pink;
}
