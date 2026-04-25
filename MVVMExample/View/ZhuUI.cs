using Microsoft.Xna.Framework;
using MVVMTutorial.MVVMExample.ViewModel;
using SilkyUIFramework;
using SilkyUIFramework.Attributes;
using SilkyUIFramework.Elements;
using SilkyUIMVVMTutorial.MVVMExample.View;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;

namespace MVVMTutorial.MVVMExample.View;

[RegisterUI]
public partial class ZhuUI : BaseBody
{
    public static ZhuUI Instance { get; private set; }
    public void Toggle() => Enabled = !Enabled;
    public override IEnumerable<UIView> BlurElements => [MainContainer];

    protected override void OnEnterTree() => LocalDataContext = new ZhuUIViewModel();
    protected override void OnExitTree()
    {
        (LocalDataContext as IDisposable)?.Dispose();
        LocalDataContext = null;
    }
    protected override void OnInitialize()
    {
        Instance = this;

        // 设置上下文
        LocalDataContext = new ZhuUIViewModel();

        // 初始化UI
        InitializeComponent();

        // 标题行拖动控制的对象
        Header.ControlTarget = this;

        // 设置标题文本使用的字体
        Title.UseDeathText();

        // 主容器的配色
        MainContainer.BackgroundColor = SUIColor.Background * .5f;
        MainContainer.BorderColor = SUIColor.Border;

        // 指定列表的模板
        ZhuTextList.ViewTemplate = ZhuTextTemplate.Instance;

        // 关闭按钮的关闭实现以及鼠标悬停动画
        Close.LeftMouseDown += delegate { Enabled = false; };
        Close.OnUpdateStatus += delegate
        {
            Close.ImageColor = Color.White * Close.HoverTimer.Lerp(0.5f, 1f);
        };

        // 按下按钮播放小猪音效
        ZhuityButton.LeftMouseClick += MakeSomePiggySound;
        ZhuListButton.LeftMouseClick += MakeSomePiggySound;
    }

    // **高级小猪音效**
    private static void MakeSomePiggySound(UIView sender, UIMouseEvent evt)
    {
        SoundEngine.PlaySound(SoundID.Item59);
    }
}
