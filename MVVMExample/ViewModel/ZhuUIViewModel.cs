using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Xna.Framework.Graphics;
using MVVMTutorial.MVVMExample.Model;
using ReLogic.Content;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace MVVMTutorial.MVVMExample.ViewModel;

public partial class ZhuUIViewModel : ObservableObject
{
    private static string[] ZhuStrings { get; } = ["住", "煮", "驻", "祝", "铸", "竹"];

    [ObservableProperty]
    public partial Asset<Texture2D> CloseIcon { get; set; } = Main.Assets.Request<Texture2D>("Images/CoolDown");

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ZhuityText))]
    public partial int Zhuity { get; set; }

    public string ZhuityText => Zhuity.ToString();

    public ObservableCollection<string> ZhuList { get; set; } = [];

    private ZhuPlayerModel _model;
    public ZhuUIViewModel()
    {
        SetModel(Main.LocalPlayer.GetModPlayer<ZhuPlayerModel>());
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        switch (e.PropertyName)
        {
            case nameof(Zhuity):
                _model?.Zhuity = Zhuity;
                break;
        }
    }

    public void SetModel(ZhuPlayerModel model)
    {
        _model = model;
        Zhuity = model.Zhuity;
        ZhuList.Clear();
        foreach (var zhu in model.ZhuList)
            ZhuList.Add(zhu);
        model.ZhuListObservable.CollectionChanged += Model_ZhuList_CollectionChanged;
        model.PropertyChanged += Model_PropertyChanged;
    }

    private void Model_ZhuList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                // 其实用ObservableCollection的话注定是只有一个的。。
                if (e.NewItems.Count == 1)
                    ZhuList.Add(e.NewItems[0] as string);
                else
                    foreach (var obj in e.NewItems)
                        ZhuList.Add(obj as string);
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                ZhuList.Clear();
                break;
        }
    }

    private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ZhuPlayerModel.Zhuity):
                Zhuity = (sender as ZhuPlayerModel).Zhuity;
                break;
        }
    }

    [RelayCommand]
    public void IncreaseZhuity()
    {
        Zhuity++;
    }

    [RelayCommand]
    public void ExtendZhuList()
    {
        _model.ZhuListObservable.Add(Main.rand.Next(ZhuStrings));
        SoundEngine.PlaySound(SoundID.Item59);
    }
}
