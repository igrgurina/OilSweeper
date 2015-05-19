using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MainScreen.Annotations;

namespace MainScreen.ViewModel
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private int _questionId;
        private string _text;
        private string _image;
        private List<string> _options;
        private string _correct;
        public event PropertyChangedEventHandler PropertyChanged;

        public int QuestionId
        {
            get { return _questionId; }
            set
            {
                if (value == _questionId) return;
                _questionId = value;
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

        public List<string> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged();   
            }
        }

        public string Correct
        {
            get { return _correct; }
            set
            {
                _correct = value;
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
