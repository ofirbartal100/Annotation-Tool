using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using AnnotationTool.Utils;
using AnnotationTool.ViewModels.Video;
using Emgu.CV;
using Prism.Events;

namespace AnnotationTool.ViewModels.Canvas
{
    public class AnnotationCanvasViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private BitmapSource _frameBitmapSource;

        public BitmapSource FrameBitmapSource
        {
            get { return _frameBitmapSource; }
            set { SetProperty(ref _frameBitmapSource, value); }
        }


        public AnnotationCanvasViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LoadFrameEvent>().Subscribe(LoadFrameEventHandler);
        }

        private void LoadFrameEventHandler(AnnotatedFrame annotatedFrame)
        {
            Mat frame = annotatedFrame.GetFrame();
            if (frame != null && !frame.IsEmpty)
            {
                FrameBitmapSource = frame.Bitmap.ToBitmapSource();
                FrameBitmapSource.Freeze();
            }
        }

    }
}
