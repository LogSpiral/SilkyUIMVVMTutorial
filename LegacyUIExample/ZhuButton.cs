using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
namespace SilkyUIMVVMTutorial.LegacyUIExample;

public class ZhuButton : UIElement
{
    public bool IsPressing { get; set; }
    public override void LeftMouseDown(UIMouseEvent evt)
    {
        IsPressing = true;
        base.LeftMouseDown(evt);
    }
    public override void LeftMouseUp(UIMouseEvent evt)
    {
        IsPressing = false;
        base.LeftMouseUp(evt);
    }
    public override void DrawSelf(SpriteBatch spriteBatch)
    {
        var position = GetDimensions().Position();
        if (IsPressing)
            spriteBatch.Draw(ModAsset.ZhuPressed_Premultiplied.Value, position, Color.Gray);
        else
            spriteBatch.Draw(ModAsset.ZhuReleased_Premultiplied.Value, position, Color.White);
    }
}
