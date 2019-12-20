using AnnotationTool.ViewModels.Video;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnnotationTool.Views.Video
{
    /// <summary>
    /// Interaction logic for VideoControls
    /// </summary>
    public partial class VideoControls : UserControl
    {
        public VideoControls()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((VideoControlsViewModel)DataContext).SliderMouseDownCommand.Execute(null);
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ((VideoControlsViewModel)DataContext).SliderMouseUpCommand.Execute(null);
        }
    }
}