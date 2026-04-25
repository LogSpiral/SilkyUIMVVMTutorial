using SilkyUIFramework.Elements;

namespace SilkyUIMVVMTutorial.MVVMExample.View;

public interface ISourcedUIViewTemplate
{
    UIView ConstructFromSource(object sourceData);
}