using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Events;

namespace AnnotationTool.ViewModels.Video
{
    public class VideoControlEvent : PubSubEvent<string> { }
    public class VideoControlsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        public ICommand VideoControlClickCommand { get; set; }
        public VideoControlsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            VideoControlClickCommand = new DelegateCommand<string>(VideoControlClickCommandImplementation);
        }

        private void VideoControlClickCommandImplementation(string command)
        {
            _eventAggregator.GetEvent<VideoControlEvent>().Publish(command);
        }
    }
}
