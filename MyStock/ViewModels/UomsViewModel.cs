using MyStock.Assets.Lang;

namespace MyStock.ViewModels;

public class UomsViewModel : BaseViewModel
{
    public UomsViewModel() : base()
    {
    }

    ObservableCollection<Uom> _uoms;
    public ObservableCollection<Uom> Uoms
    {
        get
        {
            if (_uoms == null)
                _uoms = new ObservableCollection<Uom>(Context.Set<Uom>());
            return _uoms;
        }
    }

    #region Commands
    public RelayCommand Delete => new RelayCommand(param =>
    {
        if (param is Uom uom)
        {
            if (uom.DocumentDetails.Any() || uom.Products.Any())
                NotificationManager.Error(Lang.UomUsedInDocumentsAndProductsMessage.Format(uom.Name));
            else
            {
                Context.Delete(uom);
                Uoms.Remove(uom);
                Context.SaveChanges();
            }
        }
    });

    public RelayCommand Add => new RelayCommand(async param =>
    {
        var uom = new Uom();
        if (await DialogManger.Show<UomDialog, Uom>(uom))
        {
            Context.Add(uom);
            Context.SaveChanges();
            Uoms.Add(uom);
        }
    });

    public RelayCommand Update => new RelayCommand(async param =>
    {
        if (param is Uom uom)
        {
            var dto = new Uom()
            {
                Code = uom.Code,
                Description = uom.Description,
                Name = uom.Name,
            };

            if (await DialogManger.Show<UomDialog, Uom>(dto))
            {
                uom.Name = dto.Name;
                uom.Description = dto.Description;
                uom.Code = dto.Code;
                Context.Update(uom);
                Context.SaveChanges();
            }
        }
    });
    #endregion

    protected override void OnDispose()
    {
        _uoms = null;
        base.OnDispose();
    }
}