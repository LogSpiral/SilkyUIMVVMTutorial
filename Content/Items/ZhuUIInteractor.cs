using Microsoft.Xna.Framework;
using MVVMTutorial.MVVMExample.Model;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SilkyUIMVVMTutorial.Content.Items;

public class ZhuUIInteractor : ModItem
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
        var zhuPlayer = player.GetModPlayer<ZhuPlayerModel>();
        if (player.altFunctionUse == 2)
            zhuPlayer.ZhuListObservable.Clear();
        else
            zhuPlayer.Zhuity = 0;

        return base.UseItem(player);
    }
    public override void AddRecipes()
    {
        CreateRecipe().Register();
    }
    public override bool AltFunctionUse(Player player) => true;

    public override Color? GetAlpha(Color lightColor) => Color.Pink;
}
