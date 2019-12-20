using AnnotationTool.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace AnnotationTool.ViewModels.Image
{
    public class ImagesAnnotationStudioViewModel : BindableBase
    {
        public ImagesAnnotationStudioViewModel(IRegionManager regionManager)
        {
            Dictionary<string, string> navigation = new Dictionary<string, string>();
            navigation["ControlsRegion"] = "Images" + "AnnotationControls";
            navigation["WorkspaceRegion"] = "Images" + "AnnotationCanvas";
            regionManager.Navigate(navigation);
        }
    }
}