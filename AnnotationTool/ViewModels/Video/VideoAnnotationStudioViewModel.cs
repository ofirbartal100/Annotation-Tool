using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using AnnotationTool.Utils;
using AnnotationTool.Views;
using Prism.Regions;

namespace AnnotationTool.ViewModels.Video
{
    public class VideoAnnotationStudioViewModel : BindableBase
    {
        public VideoAnnotationStudioViewModel(IRegionManager regionManager)
        {
            Dictionary<string, string> navigation = new Dictionary<string, string>();
            navigation["ControlsRegion"] = "VideoAnnotationControls";
            navigation["WorkspaceRegion"] = "VideoAnnotationCanvas";
            regionManager.InitializeNavigations(navigation);
        }

    }
}
