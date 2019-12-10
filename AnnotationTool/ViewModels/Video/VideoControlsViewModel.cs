using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AnnotationTool.Utils;
using Prism.Events;

namespace AnnotationTool.ViewModels.Video
{
    public class VideoControlEvent : PubSubEvent<string> { }
    public class VideoControlsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private int _framesSliderValue;

        public int FramesSliderValue
        {
            get { return _framesSliderValue; }
            set { SetProperty(ref _framesSliderValue, value); }
        }

        private int _framesSliderMaximum;

        public int FramesSliderMaximum
        {
            get { return _framesSliderMaximum; }
            set { SetProperty(ref _framesSliderMaximum, value); }
        }


        public ICommand VideoControlClickCommand { get; set; }
        public VideoControlsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CurrentFrameNumberEvent>().Subscribe(CurrentFrameNumberEventHandler);
            _eventAggregator.GetEvent<TotalFrameNumberEvent>().Subscribe(TotalFrameNumberEventHandler);
            VideoControlClickCommand = new DelegateCommand<string>(VideoControlClickCommandImplementation);
        }

        private void CurrentFrameNumberEventHandler(int frameNum)
        {
            FramesSliderValue = frameNum;
        }

        private void TotalFrameNumberEventHandler(int total)
        {
            FramesSliderMaximum = total;
        }

        private void VideoControlClickCommandImplementation(string command)
        {
            _eventAggregator.GetEvent<VideoControlEvent>().Publish(command);
        }
    }
}
