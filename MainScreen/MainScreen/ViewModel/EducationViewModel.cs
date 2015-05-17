using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MainScreen.Annotations;

namespace MainScreen.ViewModel {
    public class EducationViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ChapterViewModel> Chapters { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
