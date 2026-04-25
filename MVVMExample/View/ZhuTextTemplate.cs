using Microsoft.Xna.Framework;
using SilkyUIFramework.Elements;

namespace SilkyUIMVVMTutorial.MVVMExample.View;

public class ZhuTextTemplate : ISourcedUIViewTemplate
{
    public static ZhuTextTemplate Instance { get; } = new();
    UIView ISourcedUIViewTemplate.ConstructFromSource(object sourceData)
    {
        return new UITextView()
        {
            FitWidth = false,
            FitHeight = false,
            Width = new(0, 1),
            Height = new(40, 0),
            BackgroundColor = Color.Black * .25f,
            Margin = new(2),
            BorderRadius = new(8),
            TextAlign = new(0.5f),
            Text = sourceData.ToString()
        };
    }
}
