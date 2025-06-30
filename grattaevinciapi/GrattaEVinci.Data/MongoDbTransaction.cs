using Gamenet.Common.Enum;
using Gamenet.Common.Interface;
using GrattaEVinci.Common;

namespace GrattaEVinci.Data
{
    public class MongoDbTransaction : ITransaction<Transaction>
    {
        private TransactionManagerMongoDb transactionManagerMongoDb = null;
        public MongoDbTransaction(TransactionManagerMongoDb transactionManagerMongoDb)
        {
            this.transactionManagerMongoDb = transactionManagerMongoDb;
        }

        public Transaction Get(string sourceTransactionCode, string relatedTransactionCode, CasinoOperationType operationTypeWallet)
        {
            try
            {
                return transactionManagerMongoDb.GetByTransactionCodeAndOperationType(sourceTransactionCode, relatedTransactionCode, operationTypeWallet).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public List<Transaction> GetList(string sourceTransactionCode, CasinoOperationType operationTypeWallet)
        {
            throw new NotImplementedException();
        }

        public void IncreaseRetry(string sourceTransactionCode, string relatedTransactionCode, CasinoOperationType operationTypeWallet)
        {
            try
            {
                transactionManagerMongoDb.IncreaseRetry(sourceTransactionCode, relatedTransactionCode, operationTypeWallet);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Save(Transaction transaction)
        {
            try
            {
                transactionManagerMongoDb.Insert(transaction);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Update(Transaction transaction)
        {
            try
            {
                transactionManagerMongoDb.Update(transaction);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Delete(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> GetAsync(string sourceTransactionCode, string relatedTransactionCode, CasinoOperationType operationTypeWallet)
        {
            return await transactionManagerMongoDb.GetByTransactionCodeAndOperationType(sourceTransactionCode, relatedTransactionCode, operationTypeWallet);
        }

        public Task<List<Transaction>> GetListAsync(string sourceTransactionCode, CasinoOperationType operationTypeWallet)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(Transaction transaction)
        {
            await transactionManagerMongoDb.InsertAsync(transaction);
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            await transactionManagerMongoDb.DeleteAsync(transaction);
        }

        public async Task IncreaseRetryAsync(string sourceTransactionCode, string relatedTransactionCode, CasinoOperationType operationTypeWallet)
        {
            await transactionManagerMongoDb.IncreaseRetry(sourceTransactionCode, relatedTransactionCode, operationTypeWallet);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await transactionManagerMongoDb.UpdateAsync(transaction);
        }

        public Task SetDataResponseAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
