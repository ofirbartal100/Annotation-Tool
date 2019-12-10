using AnnotationTool.ViewModels.Video;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace AnnotationTool.Utils
{
    public enum VideoStates
    {
        Running,
        Paused,
        Stopped
    }
    internal class AnnotatedVideo
    {
        public string Path { get; set; }
        public VideoStates State { get; set; } = VideoStates.Stopped;
        private VideoCapture videoCapture;
        public AnnotatedVideo(string path)
        {
            Path = path;
        }

        public void Open()
        {
            videoCapture = new VideoCapture(Path);
        }

        public int GetFPS()
        {
            if (videoCapture.IsOpened)
            {
                return (int)videoCapture.GetCaptureProperty(CapProp.Fps);
            }
            return -1;
        }

        public int GetCurrentFrameNumber()
        {
            if (videoCapture.IsOpened)
            {
                return (int)videoCapture.GetCaptureProperty(CapProp.PosFrames);
            }
            return -1;
        }
        public int GetTotalFrameNumber()
        {
            if (videoCapture.IsOpened)
            {
                return (int)videoCapture.GetCaptureProperty(CapProp.FrameCount);
            }
            return -1;
        }
        public AnnotatedFrame GetAnnotatedFrame(int index)
        {
            if (videoCapture.IsOpened)
            {
                Mat frame = videoCapture.QueryFrame();
                if (frame != null && !frame.IsEmpty)
                {
                    return new AnnotatedFrame(frame);
                }
                return null;
            }

            return null;
        }

        public void Close()
        {
            videoCapture?.Dispose();
        }
    }
}