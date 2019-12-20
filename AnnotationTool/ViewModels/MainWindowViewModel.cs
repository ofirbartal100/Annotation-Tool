using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AnnotationTool.Utils;
using AnnotationTool.Views;
using Prism.Mvvm;
using Prism.Regions;
using Unity;

namespace AnnotationTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Annotation Tool";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            Dictionary<string, string> navigation= new Dictionary<string, string>();
            navigation["ContentRegion"] = "SourceSelection";
            regionManager.Navigate(navigation);
        }

    }
}
