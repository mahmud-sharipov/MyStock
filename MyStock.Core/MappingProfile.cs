using AutoMapper;

namespace MyStock.Core;

public class MappingProfile<TEntity, TViewModel> : Profile
   where TEntity : EntityBase
   where TViewModel : IViewModel
{
    public IMappingExpression<TEntity, TViewModel> EntityToViewModelMapper { get; }
    public IMappingExpression<TViewModel, TEntity> ViewModelToEntityMapper { get; }

    public MappingProfile() : base()
    {
        EntityToViewModelMapper = CreateMap<TEntity, TViewModel>();
        ViewModelToEntityMapper = CreateMap<TViewModel, TEntity>();
    }
}
