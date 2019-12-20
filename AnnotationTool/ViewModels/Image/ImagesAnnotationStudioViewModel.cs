using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AnnotationTool.Utils;
using Prism.Regions;

namespace AnnotationTool.ViewModels.Image
{
    public class ImagesAnnotationStudioViewModel : BindableBase
    {
        public ImagesAnnotationStudioViewModel(IRegionManager regionManager)
        {
            Dictionary<string, string> navigation = new Dictionary<string, string>();
            navigation["ControlsRegion"] = "Images"+"AnnotationControls";
            navigation["WorkspaceRegion"] = "Images"+"AnnotationCanvas";
            regionManager.Navigate(navigation);
        }
    }
}
