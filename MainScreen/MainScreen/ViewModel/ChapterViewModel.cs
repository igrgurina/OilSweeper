using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MainScreen.Annotations;

namespace MainScreen.ViewModel
{
    public class ChapterViewModel : INotifyPropertyChanged
    {
        private int _chapterNumber;
        private string _title;
        private string _thumbnail;
        private string _image;
        private List<SlideViewModel> _slides;
        private List<QuestionViewModel> _questions;
        public event PropertyChangedEventHandler PropertyChanged;

        public int ChapterNumber
        {
            get { return _chapterNumber; }
            set
            {
                if (value == _chapterNumber) return;
                _chapterNumber = value;
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

        public List<SlideViewModel> Slides
        {
            get { return _slides; }
            set
            {
                if (Equals(value, _slides)) return;
                _slides = value;
                OnPropertyChanged();
            }
        }

        public List<QuestionViewModel> Questions
        {
            get { return _questions; }
            set
            {
                if (Equals(value, _questions)) return;
                _questions = value;
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
