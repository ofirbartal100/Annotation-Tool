using Emgu.CV;
using Emgu.CV.CvEnum;
using System;

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

        private bool IsOpenAndValid()
        {
            return videoCapture.Ptr != IntPtr.Zero && videoCapture.IsOpened;
        }

        public void Open()
        {
            videoCapture = new VideoCapture(Path);
        }

        public int GetFPS()
        {
            if (IsOpenAndValid())
            {
                return (int)videoCapture.GetCaptureProperty(CapProp.Fps);
            }
            return -1;
        }

        public int GetCurrentFrameNumber()
        {
            if (IsOpenAndValid())
            {
                return (int)videoCapture.GetCaptureProperty(CapProp.PosFrames);
            }
            return -1;
        }

        public int GetTotalFrameNumber()
        {
            if (IsOpenAndValid())
            {
                return (int)videoCapture.GetCaptureProperty(CapProp.FrameCount);
            }
            return -1;
        }

        public AnnotatedFrame GetAnnotatedFrame()
        {
            if (IsOpenAndValid())
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

        public bool SetPosFrames(int index)
        {
            if (IsOpenAndValid() && videoCapture.GetCaptureProperty(CapProp.FrameCount) > index)
            {
                //move to relevant pos
                videoCapture.SetCaptureProperty(CapProp.PosFrames, index);

                return true;
            }

            return false;
        }

        public void Close()
        {
            videoCapture?.Dispose();
        }
    }
}