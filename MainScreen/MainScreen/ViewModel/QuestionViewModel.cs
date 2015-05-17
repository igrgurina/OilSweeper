using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using MainScreen.Annotations;

namespace MainScreen.ViewModel
{
    class QuestionViewModel : INotifyPropertyChanged
    {
        private int _questionId;
        private string _text;
        private string _image;
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

        //public 

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
