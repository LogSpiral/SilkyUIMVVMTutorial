using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SilkyUIMVVMTutorial.LegacyUIExample;

public class DraggablePanel : UIPanel
{
    private Vector2 offset;
    private bool dragging;
    public UIElement ControlTarget { get; set; }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);
        if (evt.Target == this)
            DragStart(evt);
    }

    public override void LeftMouseUp(UIMouseEvent evt)
    {
        base.LeftMouseUp(evt);
        if (evt.Target == this)
            DragEnd(evt);
    }

    private void DragStart(UIMouseEvent evt)
    {
        var target = ControlTarget ??= this;
        offset = new Vector2(evt.MousePosition.X - target.Left.Pixels, evt.MousePosition.Y - target.Top.Pixels);
        dragging = true;
    }

    private void DragEnd(UIMouseEvent evt)
    {
        var target = ControlTarget ??= this;
        Vector2 endMousePosition = evt.MousePosition;
        dragging = false;

        target.Left.Set(endMousePosition.X - offset.X, 0f);
        target.Top.Set(endMousePosition.Y - offset.Y, 0f);

        Recalculate();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (ContainsPoint(Main.MouseScreen))
            Main.LocalPlayer.mouseInterface = true;

        if (dragging)
        {
            var target = ControlTarget ??= this;
            target.Left.Set(Main.mouseX - offset.X, 0f);
            target.Top.Set(Main.mouseY - offset.Y, 0f);
            Recalculate();
        }
    }
}
