using MyStock.Application.Vendors.Pages;
using MyStock.Application.Vendors.Validators;

namespace MyStock.Application.Vendors
{
    public class VendorViewModel : EntityPageViewModel<Vendor, VendorValidator, IVendorEntityPage>
    {
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _address;
        private string _phone;

        public VendorViewModel(Vendor entity, IContext context) : base(entity, context)
        {
        }

        public string FirstName { get => _firstName; set => this.RaiseAndSetAndValidateIfChanged(ref _firstName, value); }
        public string LastName { get => _lastName; set => RaiseAndSetAndValidateIfChanged(ref _lastName, value); }
        public string MiddleName { get => _middleName; set => RaiseAndSetIfChanged(ref _middleName, value); }
        public string Address { get => _address; set => RaiseAndSetIfChanged(ref _address, value); }
        public string Phone { get => _phone; set => RaiseAndSetAndValidateIfChanged(ref _phone, value); }

        protected override void InitializeAssociatedProperties()
        {
            base.InitializeAssociatedProperties();
        }

        public override bool ValidateBeforeSaveChange()
        {
            return base.ValidateBeforeSaveChange();
        }

        public override void ActionBeforeSave()
        {
            base.ActionBeforeSave();
        }
    }
}