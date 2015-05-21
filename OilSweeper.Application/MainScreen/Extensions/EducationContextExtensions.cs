using AutoMapper;
using Engine.Models;
using MainScreen.ViewModel;

namespace MainScreen.Extensions {
    public static class EducationContextExtensions {

        public static EducationViewModel ToViewModel(this EducationContext context)
        {
            Mapper.CreateMap<Engine.Models.Slide, SlideViewModel>();
            Mapper.CreateMap<Engine.Models.Question, QuestionViewModel>();
            Mapper.CreateMap<Chapter, ChapterViewModel>();
            Mapper.CreateMap<EducationContext, EducationViewModel>();
            return Mapper.Map<EducationViewModel>(context);
        }

    }
}
