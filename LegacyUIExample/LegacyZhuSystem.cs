using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
namespace SilkyUIMVVMTutorial.LegacyUIExample;

public class LegacyZhuSystem : ModSystem
{
    public static LegacyZhuSystem Instance { get; set; }
    public LegacyZhuUI ZhuUIInstance { get; set; }
    private Terraria.UI.UserInterface ZhuUIInterface { get; set; }
    public void ToggleShowZhuUI()
    {
        if (ZhuUIInterface is not { } zinterface) return;
        if (zinterface.CurrentState == null)
        {
            zinterface.SetState(ZhuUIInstance);
            ZhuUIInstance.OnShowUI();
        }
        else
            zinterface.SetState(null);
    }
    public override void Load()
    {
        Instance = this;
    }
    public override void Unload()
    {
        Instance = null;
    }
    public override void PostSetupContent()
    {
        ZhuUIInstance = new();
        ZhuUIInterface = new();
        ZhuUIInstance.Activate();
    }
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer($"{nameof(SilkyUIMVVMTutorial)}: LegacyZhuUI", () =>
            {
                if (ZhuUIInterface is { CurrentState: not null })
                    ZhuUIInterface.Draw(Main.spriteBatch, new GameTime());
                return true;
            }, InterfaceScaleType.UI)
            );
        }
        base.ModifyInterfaceLayers(layers);
    }
    public override void UpdateUI(GameTime gameTime)
    {
        if (ZhuUIInterface is { CurrentState: not null })
            ZhuUIInterface.Update(gameTime);
        base.UpdateUI(gameTime);
    }
}
