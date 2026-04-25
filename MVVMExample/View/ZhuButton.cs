using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SilkyUIFramework;
using SilkyUIFramework.Attributes;
using SilkyUIFramework.Elements;

namespace SilkyUIMVVMTutorial.MVVMExample.View;

[XmlElementMapping("ZhuButton")]
public class ZhuButton : UIView
{
    public bool IsPressing { get; set; }
    public override void OnLeftMouseDown(UIMouseEvent evt)
    {
        IsPressing = true;
        base.OnLeftMouseDown(evt);
    }
    public override void OnLeftMouseUp(UIMouseEvent evt)
    {
        IsPressing = false;
        base.OnLeftMouseUp(evt);
    }
    protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var position = Bounds.Position;
        if (IsPressing)
            spriteBatch.Draw(ModAsset.ZhuPressed_Premultiplied.Value, position, Color.Gray);
        else
            spriteBatch.Draw(ModAsset.ZhuReleased_Premultiplied.Value, position, Color.White);
    }
}
