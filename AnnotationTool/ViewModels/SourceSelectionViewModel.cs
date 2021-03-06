﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace AnnotationTool.ViewModels
{
    public class SourceSelectionViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        public ICommand SourceButtonClickCommand { get; set; }

        public SourceSelectionViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SourceButtonClickCommand = new DelegateCommand<string>(SourceButtonClickCommandImplementation);
        }

        private void SourceButtonClickCommandImplementation(string source)
        {
            if (source.Contains("Video"))
                _regionManager.RequestNavigate("ContentRegion", new Uri("VideoAnnotationStudio", UriKind.Relative));
            else if (source.Contains("Images"))
                _regionManager.RequestNavigate("ContentRegion", new Uri("ImagesAnnotationStudio", UriKind.Relative));
        }
    }
}