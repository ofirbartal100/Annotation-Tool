using AnnotationTool.Utils;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace AnnotationTool.ViewModels.Video
{
    public class VideoAnnotationStudioViewModel : BindableBase
    {
        public VideoAnnotationStudioViewModel(IRegionManager regionManager)
        {
            Dictionary<string, string> navigation = new Dictionary<string, string>();
            navigation["ControlsRegion"] = "Video" + "AnnotationControls";
            navigation["WorkspaceRegion"] = "Video" + "AnnotationCanvas";
            regionManager.Navigate(navigation);
        }
    }
}