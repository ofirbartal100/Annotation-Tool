using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AnnotationTool.ViewModels.Video;

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
