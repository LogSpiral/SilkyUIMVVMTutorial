using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SilkyUIMVVMTutorial.Common;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader.UI;
using Terraria.UI;
namespace SilkyUIMVVMTutorial.LegacyUIExample;

public class LegacyZhuUI : UIState
{
    private static string[] ZhuStrings { get; } = ["住", "煮", "驻", "祝", "铸", "竹"];
    public UIPanel MainPanel { get; set; }
    public UITextPanel<string> ZhuityViewer { get; set; }
    public UIList ZhuList { get; set; }
    public override void OnInitialize()
    {
        base.OnInitialize();
        Left = new(400, 0);
        Top = new(400, 0);

        UIPanel mainPanel = new()
        {
            BackgroundColor = UICommon.DefaultUIBlue * .5f,
            BorderColor = UICommon.DefaultUIBorder,
            Width = new(400, 0),
            Height = new(300, 0),
            PaddingBottom = 0,
            PaddingLeft = 0,
            PaddingRight = 0,
            PaddingTop = 0,
            MarginBottom = 0,
            MarginLeft = 0,
            MarginRight = 0,
            MarginTop = 0,
        };
        MainPanel = mainPanel;
        Append(mainPanel);

        DraggablePanel headerPanel = new()
        {
            Width = new(0, 1),
            Height = new(40, 0),
            BackgroundColor = Color.Black * .25f,
            BorderColor = Color.Transparent,
            ControlTarget = mainPanel,
            PaddingBottom = 0,
            PaddingLeft = 8,
            PaddingRight = 8,
            PaddingTop = 0,
            MarginBottom = 0,
            MarginLeft = 0,
            MarginRight = 0,
            MarginTop = 0,
        };
        mainPanel.Append(headerPanel);

        UIText title = new("铸板")
        {
            Width = new(-32, 1),
            Height = new(0, 1),
            TextOriginX = 0,
            TextOriginY = 0.5f,
            IgnoresMouseInteraction = true
        };
        headerPanel.Append(title);

        UIImageButton close = new(Main.Assets.Request<Texture2D>("Images/CoolDown"))
        {
            Width = new(32, 0),
            Height = new(32, 0),
            HAlign = 1,
            VAlign = 0.5f
        };
        close.OnLeftClick += delegate
        {
            LegacyZhuSystem.Instance.ToggleShowZhuUI();
        };
        headerPanel.Append(close);

        ZhuButton zhuityButton = new()
        {
            Width = new(23, 0),
            Height = new(23, 0),
            Left = new(12, 0),
            Top = new(48, 0)
        };
        mainPanel.Append(zhuityButton);
        zhuityButton.OnLeftClick += UpdateZhuity;

        ZhuButton zhuListButton = new()
        {
            Width = new(23, 0),
            Height = new(23, 0),
            Left = new(12, 0),
            Top = new(72, 0)
        };
        mainPanel.Append(zhuListButton);
        zhuListButton.OnLeftClick += UpdateZhuList;



        UITextPanel<string> zhuityViewer = new("")
        {
            Width = new(-48, 1),
            Height = new(40, 0),
            Left = new(40, 0),
            Top = new(52, 0),
            BackgroundColor = Color.Black * .25f,
            BorderColor = Color.Transparent
        };
        ZhuityViewer = zhuityViewer;
        mainPanel.Append(zhuityViewer);
        UIPanel listPanel = new()
        {
            Width = new(-16, 1),
            Height = new(0, 0.6f),
            MarginLeft = 8,
            MarginRight = 8,
            MarginBottom = 16,
            VAlign = 1,
            BackgroundColor = Color.Black * .25f,
            BorderColor = Color.Transparent
        };
        mainPanel.Append(listPanel);

        UIList zhuList = new()
        {
            VAlign = 1,
            Height = new(0, 1f),
            Width = new(-30, 1),
            ListPadding = 4
        };
        ZhuList = zhuList;
        UIScrollbar zhubar = new()
        {
            HAlign = 1,
            VAlign = 1,
            Height = new(0, 1f)
        };
        listPanel.Append(zhuList);
        listPanel.Append(zhubar);
        zhuList.SetScrollbar(zhubar);
    }




    public override void Update(GameTime gameTime)
    {
        if (MainPanel.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()))
            Main.LocalPlayer.mouseInterface = true;

        base.Update(gameTime);
    }

    /// <summary>
    /// 更新Zhuity查看区的文本
    /// </summary>
    public void SetupText()
    {
        ZhuityViewer.SetText(Main.LocalPlayer.GetModPlayer<ZhuZhuPlayer>().zhuity.ToString());
    }

    /// <summary>
    /// 更新ZhuList的显示列表内容
    /// </summary>
    public void SetupList()
    {
        ZhuList.Clear();
        var zhuPlayer = Main.LocalPlayer.GetModPlayer<ZhuZhuPlayer>();

        foreach (var zhu in zhuPlayer.zhuList)
            ZhuList.Add(new UITextPanel<string>(zhu)
            {
                Height = new(40, 0),
                BackgroundColor = Color.Black * .25f,
                BorderColor = Color.Transparent,
                Width = new(0, 1)
            });
    }

    /// <summary>
    /// 按下按钮之后的处理
    /// </summary>
    /// <param name="evt"></param>
    /// <param name="listeningElement"></param>
    private void UpdateZhuity(UIMouseEvent evt, UIElement listeningElement)
    {
        var zhuPlayer = Main.LocalPlayer.GetModPlayer<ZhuZhuPlayer>();
        zhuPlayer.zhuity++;
        SoundEngine.PlaySound(SoundID.Item59);
        SetupText();
    }

    /// <summary>
    /// 按下按钮的另一个处理
    /// </summary>
    /// <param name="evt"></param>
    /// <param name="listeningElement"></param>
    private void UpdateZhuList(UIMouseEvent evt, UIElement listeningElement)
    {
        var zhuPlayer = Main.LocalPlayer.GetModPlayer<ZhuZhuPlayer>();
        zhuPlayer.zhuList.Add(Main.rand.Next(ZhuStrings));
        SoundEngine.PlaySound(SoundID.Item59);
        SetupList();
    }
    public void OnShowUI()
    {
        SetupList();
        SetupText();
    }
}
