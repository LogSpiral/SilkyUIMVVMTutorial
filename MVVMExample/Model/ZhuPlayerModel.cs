using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ModLoader;

namespace MVVMTutorial.MVVMExample.Model;

[INotifyPropertyChanged]
public partial class ZhuPlayerModel : ModPlayer
{
    [ObservableProperty]
    public partial int Zhuity { get; set; } = 1;
    public ObservableCollection<string> ZhuListObservable { get; } = ["铸", "祝", "煮", "竹"];
    public IReadOnlyList<string> ZhuList => ZhuListObservable;
}
