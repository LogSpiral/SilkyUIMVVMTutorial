using Microsoft.Xna.Framework;
using SilkyUIMVVMTutorial.MVVMExample.View;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SilkyUIMVVMTutorial.Content.Items;

public class ZhuUIShower : ModItem
{
    public override string Texture => $"Terraria/Images/Item_{ItemID.MoneyTrough}";
    public override void SetDefaults()
    {
        Item.useTime = Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
    }
    public override bool? UseItem(Player player)
    {
        if (Main.dedServ || player.itemAnimation != 15) return null;
        ZhuUI.Instance?.Toggle();
        return base.UseItem(player);
    }
    public override void AddRecipes()
    {
        CreateRecipe().Register();
    }
    public override Color? GetAlpha(Color lightColor) => Main.DiscoColor;
}
