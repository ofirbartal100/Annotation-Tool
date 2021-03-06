﻿using AnnotationTool.Utils;
using AnnotationTool.ViewModels.Video;
using Emgu.CV;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

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