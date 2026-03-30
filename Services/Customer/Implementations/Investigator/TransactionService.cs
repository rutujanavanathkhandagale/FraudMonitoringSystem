using FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.DTOs.Investigator;

namespace FraudMonitoringSystem.Services.Customer.Implementations.Investigator
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<TransactionDto> AddTransactionAsync(TransactionDto dto)
        {
            var transaction = MapToEntity(dto);
            var saved = await _transactionRepo.AddTransactionAsync(transaction);
            return MapToDto(saved);
        }

        public async Task<TransactionDto?> GetByIdAsync(int transactionId)
        {
            var transaction = await _transactionRepo.GetByIdAsync(transactionId);
            return transaction == null ? null : MapToDto(transaction);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepo.GetAllAsync();
            return transactions.Select(MapToDto);
        }

        public async Task<TransactionDto> UpdateAsync(TransactionDto dto)
        {
            var transaction = MapToEntity(dto);
            transaction.TransactionID = dto.TransactionID; // ensure ID is mapped
            var updated = await _transactionRepo.UpdateAsync(transaction);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int transactionId) =>
            await _transactionRepo.DeleteAsync(transactionId);

        // Mapping Logic: aligned with Transaction entity
        private Transaction MapToEntity(TransactionDto dto) =>
            new Transaction
            {
                CustomerId = dto.CustId,                     // DTO → Entity
                CounterpartyAccount = dto.CounterAccount,    // DTO → Entity
                Amount = dto.Amount,
                GeoLocation = dto.Location,                  // DTO.Location → Entity.GeoLocation
                Timestamp = dto.TransactionDateTime,         // DTO.TransactionDateTime → Entity.Timestamp
                Channel = dto.Channel,
                TransactionType = dto.TransactionType,
                SourceType = dto.SourceType
            };

        private TransactionDto MapToDto(Transaction t) =>
            new TransactionDto
            {
                TransactionID = t.TransactionID,
                CustId = (int)t.CustomerId,                  // Entity → DTO
                CounterAccount = t.CounterpartyAccount,      // Entity.CounterpartyAccount → DTO.CounterAccount
                Amount = t.Amount,
                Location = t.GeoLocation,                    // Entity.GeoLocation → DTO.Location
                TransactionDateTime = t.Timestamp,           // Entity.Timestamp → DTO.TransactionDateTime
                Channel = t.Channel,
                TransactionType = t.TransactionType,
                SourceType = t.SourceType
            };
    }
}
