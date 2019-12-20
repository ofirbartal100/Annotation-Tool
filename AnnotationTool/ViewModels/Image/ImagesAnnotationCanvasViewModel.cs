using AnnotationTool.Utils;
using AnnotationTool.ViewModels.Video;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace AnnotationTool.ViewModels.Image
{
    public class ImagesAnnotationCanvasViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public ImagesAnnotationCanvasViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Dictionary<string, string> navigation = new Dictionary<string, string>();
            navigation["CanvasRegion"] = "AnnotationCanvas";
            navigation["ImagesControlsRegion"] = "ImagesControls";
            regionManager.Navigate(navigation);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LoadImagesEvent>().Subscribe(LoadImagesEventHandler);
        }

        private void LoadImagesEventHandler(string path)
        {
            var frame = new AnnotatedFrame(path);
            if (frame != null)
                _eventAggregator.GetEvent<LoadFrameEvent>().Publish(frame);
        }
    }
}