using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;

namespace AnnotationTool.ViewModels.Video
{
    public class VideoControlEvent : PubSubEvent<string> { }

    public class SliderMouseUpEvent : PubSubEvent<int> { }

    public class SliderMouseDownEvent : PubSubEvent { }

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
        public ICommand SliderMouseDownCommand { get; set; }
        public ICommand SliderMouseUpCommand { get; set; }

        public VideoControlsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CurrentFrameNumberEvent>().Subscribe(CurrentFrameNumberEventHandler);
            _eventAggregator.GetEvent<TotalFrameNumberEvent>().Subscribe(TotalFrameNumberEventHandler);
            VideoControlClickCommand = new DelegateCommand<string>(VideoControlClickCommandImplementation);
            SliderMouseDownCommand = new DelegateCommand(SliderMouseDownCommandImplementation);
            SliderMouseUpCommand = new DelegateCommand(SliderMouseUpCommandImplementation);
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

        private void SliderMouseDownCommandImplementation()
        {
        }

        private void SliderMouseUpCommandImplementation()
        {
            _eventAggregator.GetEvent<SliderMouseUpEvent>().Publish(FramesSliderValue);
        }
    }
}