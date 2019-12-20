using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;

namespace AnnotationTool.ViewModels.Video
{
    public class LoadVideoEvent : PubSubEvent<string> { }

    public class VideoAnnotationControlsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        #region Properties

        private string _selectedVideoTextBoxText;

        public string SelectedVideoTextBoxText
        {
            get { return _selectedVideoTextBoxText; }
            set { SetProperty(ref _selectedVideoTextBoxText, value); }
        }

        #endregion Properties

        public ICommand SelectVideoButtonClickCommand { get; set; }
        public ICommand LoadVideoToCanvasButtonClickCommand { get; set; }

        public VideoAnnotationControlsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SelectVideoButtonClickCommand = new DelegateCommand(SelectVideoButtonClickCommandImplementation);
            LoadVideoToCanvasButtonClickCommand = new DelegateCommand(LoadVideoToCanvasButtonClickCommandImplementation);
        }

        #region Commands

        private void SelectVideoButtonClickCommandImplementation()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedVideoTextBoxText = openFileDialog.FileName;
            }
        }

        private void LoadVideoToCanvasButtonClickCommandImplementation()
        {
            _eventAggregator.GetEvent<LoadVideoEvent>().Publish(SelectedVideoTextBoxText);
        }

        #endregion Commands
    }
}