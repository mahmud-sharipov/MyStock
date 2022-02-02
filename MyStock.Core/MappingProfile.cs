using AutoMapper;

namespace MyStock.Core;

public class MappingProfile<TEntity, TViewModel> : Profile
   where TEntity : EntityBase
   where TViewModel : IViewModel
{
    public MappingProfile() : base()
    {
        CreateMap<TEntity, TViewModel>().IncludeAllDerived();
        CreateMap<TViewModel, TEntity>().IncludeAllDerived();
    }
}
