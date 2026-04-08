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
            transaction.TransactionID = dto.TransactionID;
            var updated = await _transactionRepo.UpdateAsync(transaction);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int transactionId) =>
            await _transactionRepo.DeleteAsync(transactionId);

        // Fixed Mapping Logic: Updated to match your new Model and DTO structure
        private Transaction MapToEntity(TransactionDto dto) =>
      new Transaction
      {
          TransactionID = dto.TransactionID,
          AccountId = dto.AccountId,
          CustomerId = dto.CustomerId,
          CustomerType = dto.CustomerType,
          CounterpartyAccount = dto.CounterpartyAccount,
          Amount = dto.Amount,
          Currency = dto.Currency,
          TransactionType = dto.TransactionType,
          Channel = dto.Channel,
          Timestamp = dto.Timestamp,
          GeoLocation = dto.GeoLocation,
          Status = dto.Status,
          SourceType = dto.SourceType
      };

        private TransactionDto MapToDto(Transaction t) =>
            new TransactionDto
            {
                TransactionID = t.TransactionID,
                AccountId = t.AccountId,
                CustomerId = t.CustomerId,
                CustomerType = t.CustomerType,
                CounterpartyAccount = t.CounterpartyAccount,
                Amount = t.Amount,
                Currency = t.Currency,
                TransactionType = t.TransactionType,
                Channel = t.Channel,
                Timestamp = t.Timestamp,
                GeoLocation = t.GeoLocation,
                Status = t.Status,
                SourceType = t.SourceType
            };
    }
}