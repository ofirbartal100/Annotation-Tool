using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnnotationTool.Utils;
using Prism.Events;
using Prism.Regions;

namespace AnnotationTool.ViewModels.Video
{
    public class LoadFrameEvent : PubSubEvent<AnnotatedFrame>
    {
    }

    public class VideoAnnotationCanvasViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private AnnotatedVideo _video;
        public VideoAnnotationCanvasViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Dictionary<string, string> navigation = new Dictionary<string, string>();
            navigation["CanvasRegion"] = "AnnotationCanvas";
            navigation["VideoControlsRegion"] = "VideoControls";
            regionManager.InitializeNavigations(navigation);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LoadVideoEvent>().Subscribe(LoadVideoEventHandler);
            _eventAggregator.GetEvent<VideoControlEvent>().Subscribe(VideoControlEventHandler);

        }

        #region Private Functions

        private void PlayVideo()
        {
            //open capture if not open
            if (_video.State == VideoStates.Stopped)
            {
                _video.Open();
            }
            _video.State = VideoStates.Running;

            Task.Factory.StartNew(() =>
            {
                //setup
                AnnotatedFrame frame = new AnnotatedFrame();
                int interval = 1000 / _video.GetFPS();

                //retrieve frames, and publish to the canvas
                while (frame != null && interval > 0 && _video.State == VideoStates.Running)
                {
                    frame = _video.GetAnnotatedFrame(0);
                    if (frame != null)
                        _eventAggregator.GetEvent<LoadFrameEvent>().Publish(frame);
                    Thread.Sleep(interval);
                }

                //stop _video
                if (_video.State == VideoStates.Running)
                {
                    StopVideo();
                }
            });

        }

        private void PauseVideo()
        {
            if (_video.State == VideoStates.Running)
            {
                _video.State = VideoStates.Paused;
            }
        }

        private void StopVideo()
        {
            _video.State = VideoStates.Stopped;
            _video.Close();
            _eventAggregator.GetEvent<LoadFrameEvent>().Publish(new AnnotatedFrame());

        }


        #endregion
        #region Event Handlers

        private void LoadVideoEventHandler(string path)
        {
            _video = new AnnotatedVideo(path);
            _video.Open();
            var frame = _video.GetAnnotatedFrame(0);
            _video.Close();

            if (frame != null)
                _eventAggregator.GetEvent<LoadFrameEvent>().Publish(frame);
        }

        private void VideoControlEventHandler(string command)
        {
            switch (command)
            {
                case "Play":
                    PlayVideo();
                    break;

                case "Pause":
                    PauseVideo();
                    break;

                case "Stop":
                    StopVideo();
                    break;
            }
        }
        #endregion
    }
}
