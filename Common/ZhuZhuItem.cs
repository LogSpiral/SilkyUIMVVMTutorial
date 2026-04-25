using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SilkyUIMVVMTutorial.Common;

public class ZhuZhuItem : GlobalItem
{
    public override bool CanUseItem(Item item, Player player)
    {
        if (item.type == ItemID.PiggyBank)
            return player.GetModPlayer<ZhuZhuPlayer>().zhuity >= 10;

        return base.CanUseItem(item, player);
    }
    public override bool? UseItem(Item item, Player player)
    {
        if (item.type == ItemID.PiggyBank && player.itemAnimation == player.itemAnimationMax)
        {
            var str = string.Concat(player.GetModPlayer<ZhuZhuPlayer>().zhuList);
            CombatText.NewText(player.Hitbox, Color.Pink, str, true);
        }
        return base.UseItem(item, player);
    }
}