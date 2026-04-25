using Microsoft.Xna.Framework;
using MVVMTutorial.MVVMExample.Model;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SilkyUIMVVMTutorial.MVVMExample.Model;

public class ZhuItem : GlobalItem
{
    public override bool CanUseItem(Item item, Player player)
    {
        if (item.type == ItemID.MoneyTrough)
            return player.GetModPlayer<ZhuPlayerModel>().Zhuity >= 10;

        return base.CanUseItem(item, player);
    }
    public override bool? UseItem(Item item, Player player)
    {
        if (item.type == ItemID.MoneyTrough && player.itemAnimation == player.itemAnimationMax)
        {
            var str = string.Concat(player.GetModPlayer<ZhuPlayerModel>().ZhuList);
            CombatText.NewText(player.Hitbox, Color.Pink, str, true);
        }
        return base.UseItem(item, player);
    }
}
