using Project.Business.Interfaces;
using Project.Business.Models;
using Project.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository, ICommunicator communicator) : base(communicator)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> Add(Supplier supplier)
        {
            if (!ExecuteValidations(new SupplierValidation(), supplier)
                    || !ExecuteValidations(new AddressValidation(), supplier.Address)) return false;

            if(_supplierRepository.Search(s => s.Document == supplier.Document).Result.Any())
            {
                Notify("There is already a supplier with this document");
                return false;
            }

            await _supplierRepository.Add(supplier);
            return true;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            if (!ExecuteValidations(new SupplierValidation(), supplier)) return false;

            if (_supplierRepository.Search(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any())
            {
                Notify("There is already a supplier with this document");
                return false;
            }

            await _supplierRepository.Update(supplier);
            return true;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidations(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }

        public async Task<bool> Remove(Guid supplierId)
        {
            if (_supplierRepository.GetSupplierProductsAddress(supplierId).Result.Books.Any())
            {
                Notify("The supplier has registered products");
                return false;
            }

            var address = await _addressRepository.GetAddressBySupplier(supplierId);

            if(address != null)
            {
                await _addressRepository.Remove(address.Id);
            }

            await _supplierRepository.Remove(supplierId);
            return true;
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
