using System.ComponentModel;
using System.Runtime.CompilerServices;
using MainScreen.Annotations;

namespace MainScreen.ViewModel
{
    public class SlideViewModel : INotifyPropertyChanged
    {
        private int _slideNumber;
        private string _title;
        private string _thumbnail;
        private string _image;
        private string _text;
        public event PropertyChangedEventHandler PropertyChanged;

        public int SlideNumber
        {
            get { return _slideNumber; }
            set
            {
                if (value == _slideNumber) return;
                _slideNumber = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Thumbnail
        {
            get { return _thumbnail; }
            set
            {
                if (value == _thumbnail) return;
                _thumbnail = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                if (value == _image) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
