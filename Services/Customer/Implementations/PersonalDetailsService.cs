using FraudMonitoringSystem.DTOs.Customer;
using FraudMonitoringSystem.Exceptions;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces;

namespace FraudMonitoringSystem.Services.Customer.Implementations
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IPersonalDetailsRepository _repo;

        public PersonalDetailsService(IPersonalDetailsRepository repo)
        {
            _repo = repo;
        }

        public async Task<CustomerDto> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw PersonalDetailsException.NotFound($"Customer {id} not found");

            return MapToDto(entity);
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(MapToDto).ToList();
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            var entity = MapToEntity(dto);
            var result = await _repo.AddAsync(entity);

            if (result == 0)
                throw PersonalDetailsException.Validation("Failed to create customer");

            return MapToDto(entity);
        }

        public async Task<List<CustomerDto>> SearchByNameAsync(string name)
        {
            var list = await _repo.SearchByNameAsync(name);
            return list.Select(MapToDto).ToList();
        }

        public async Task<CustomerDto?> GetByEmailAsync(string email)
        {
            var entity = await _repo.GetByEmailAsync(email);
            return entity == null ? null : MapToDto(entity);
        }


        public async Task<CustomerDto> UpdateAsync(CustomerDto dto)
        {
            var entity = MapToEntity(dto);
            var result = await _repo.UpdateAsync(entity);

            if (result == 0)
                throw PersonalDetailsException.NotFound($"Customer {dto.CustomerId} not found");

            return MapToDto(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var result = await _repo.DeleteAsync(id);

            if (result == 0)
                throw PersonalDetailsException.NotFound($"Customer {id} not found");
        }

        // ✅ Corrected mapping: includes ALL fields
        private CustomerDto MapToDto(PersonalDetails e) => new CustomerDto
        {
            CustomerId = e.CustomerId,
            FirstName = e.FirstName,
            MiddleName = e.MiddleName,
            LastName = e.LastName,
            FatherName = e.FatherName,
            MotherName = e.MotherName,
            CustomerType = e.CustomerType,
            Email = e.Email,
            Phone = e.Phone,
            DOB = e.DOB,
            PermanentAddress = e.PermanentAddress,
            CurrentAddress = e.CurrentAddress,
            ProfileImagePath = e.ProfileImagePath
        };

        private PersonalDetails MapToEntity(CustomerDto d) => new PersonalDetails
        {
            CustomerId = d.CustomerId,
            FirstName = d.FirstName,
            MiddleName = d.MiddleName,
            LastName = d.LastName,
            FatherName = d.FatherName,
            MotherName = d.MotherName,
            CustomerType = d.CustomerType,
            Email = d.Email,
            Phone = d.Phone,
            DOB = d.DOB,
            PermanentAddress = d.PermanentAddress,
            CurrentAddress = d.CurrentAddress,
            ProfileImagePath = d.ProfileImagePath
        };
    }
}
