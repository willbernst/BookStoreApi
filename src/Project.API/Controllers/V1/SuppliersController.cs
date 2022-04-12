using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.API.Dto;
using Project.API.Extensions;
using Project.Business.Interfaces;
using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/suppliers")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository, ISupplierService supplierService,
                                   IMapper mapper, IUser user, ICommunicator communicator, IAddressRepository addressRepository) : base(communicator, user)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _supplierService = supplierService;
            _addressRepository = addressRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<SupplierDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<SupplierDto>>(await _supplierRepository.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SupplierDto>> GetById(Guid id)
        {
            var supplier = await GetSupplierProductsAddress(id);

            if(supplier == null) return NotFound();

            return supplier;
        }

        [ClaimAuthorize("Supplier", "Add")]
        [HttpPost]
        public async Task<ActionResult<SupplierDto>> Add(SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Add(_mapper.Map<Supplier>(supplierDto));

            return CustomResponse(supplierDto);
        }

        [ClaimAuthorize("Supplier", "Update")]
        [HttpPut]
        public async Task<ActionResult<SupplierDto>> Update(Guid id, SupplierDto supplierDto)
        {
            if(id != supplierDto.Id)
            {
                NotifyError("The id provided is not the same as the one passed in the query");
                return CustomResponse(supplierDto);
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Update(_mapper.Map<Supplier>(supplierDto));
            return CustomResponse(supplierDto);
        }

        [ClaimAuthorize("Supplier", "Remove")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierDto>> Remove(Guid id)
        {
            var supplierDto = await GetSupplierAddress(id);

            if(supplierDto == null) return NotFound();

            await _supplierService.Remove(id);

            return CustomResponse(supplierDto);
        }

        [HttpGet("address/{id:guid}")]
        public async Task<IActionResult> GetAddressById(Guid id, AddressDto addressDto)
        {
            if(id != addressDto.Id)
            {
                NotifyError("The id provided is not the same as the one passed in the query");
                return CustomResponse(addressDto);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressDto));

            return CustomResponse(addressDto);
        }

        private async Task<SupplierDto> GetSupplierProductsAddress(Guid id)
        {
            return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierProductsAddress(id));
        }

        private async Task<SupplierDto> GetSupplierAddress(Guid id)
        {
            return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAddress(id));
        }
    }
}
