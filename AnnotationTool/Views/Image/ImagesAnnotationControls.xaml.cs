using System.Windows;
using System.Windows.Controls;
using AnnotationTool.ViewModels.Image;

namespace AnnotationTool.Views.Image
{
    /// <summary>
    /// Interaction logic for ImagesAnnotationControls
    /// </summary>
    public partial class ImagesAnnotationControls : UserControl
    {
        public ImagesAnnotationControls()
        {
            InitializeComponent();
        }


        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            ((ImagesAnnotationControlsViewModel)DataContext).ToggleButton_OnChecked(sender,e);
        }
    }
}
