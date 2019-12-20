using System.IO;
using Emgu.CV;

namespace AnnotationTool.Utils
{
    public class AnnotatedFrame
    {
        private Mat Frame;

        public AnnotatedFrame()
        {
            var s = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "../../Resources/nothing.jpeg");
            Frame = CvInvoke.Imread(s);
        }
        public AnnotatedFrame(string path)
        {
            if (File.Exists(path))
            {
                var s = Path.Combine(path);
                Frame = CvInvoke.Imread(s);
            }
            else
            {
                var s = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "../../Resources/nothing.jpeg");
                Frame = CvInvoke.Imread(s);
            }

        }
        public AnnotatedFrame(Mat frame)
        {

            Frame = frame;
        }

        public Mat GetFrame()
        {
            return Frame;
        }
    }
}