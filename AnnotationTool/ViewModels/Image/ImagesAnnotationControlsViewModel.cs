using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AnnotationTool.Utils;
using AnnotationTool.ViewModels.Video;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace AnnotationTool.ViewModels.Image
{
    public class LoadImagesEvent : PubSubEvent<string> { }
    public class ImagesAnnotationControlsViewModel : BindableBase
    {
        #region Properties
        private ObservableCollection<IAnnotationTool> _annotationTools;

        public ObservableCollection<IAnnotationTool> AnnotationTools
        {
            get { return _annotationTools; }
            set { SetProperty(ref _annotationTools, value); }
        }

        private string _selectedVideoTextBoxText;

        public string SelectedImagesTextBoxText
        {
            get { return _selectedVideoTextBoxText; }
            set { SetProperty(ref _selectedVideoTextBoxText, value); }
        } 
        #endregion

        private IEventAggregator _eventAggregator;

        public ICommand SelectImagesButtonClickCommand { get; set; }
        public ICommand LoadImagesToCanvasButtonClickCommand { get; set; }
        public ImagesAnnotationControlsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
            AnnotationTools = new ObservableCollection<IAnnotationTool>();
            AnnotationTools.Add(new BoundingBoxTool());
            AnnotationTools.Add(new LandmarksTool());

            SelectImagesButtonClickCommand = new DelegateCommand(SelectImagesButtonClickCommandImplementation);
            LoadImagesToCanvasButtonClickCommand = new DelegateCommand(LoadImagesToCanvasButtonClickCommandImplementation);
        }

        #region Commands
        private void SelectImagesButtonClickCommandImplementation()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    SelectedImagesTextBoxText = openFileDialog.FileName;
            //}

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users\\ofir";
            //dialog.IsFolderPicker = true;
            dialog.Filters.Add(new CommonFileDialogFilter("Image files", "*.png;*.jpg;*jpeg;*.bmp"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedImagesTextBoxText =  dialog.FileName;
            }
        }

        private void LoadImagesToCanvasButtonClickCommandImplementation()
        {
            _eventAggregator.GetEvent<LoadImagesEvent>().Publish(SelectedImagesTextBoxText);
        }

        public void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            var annotationTool = radioButton.DataContext as IAnnotationTool;
            _eventAggregator.GetEvent<AnnotationToolSelectedEvent>().Publish(annotationTool.ToolName);
        } 
        #endregion
    }

}
