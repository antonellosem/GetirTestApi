using Gamenet.Common.Enum;
using Gamenet.Common.Exceptions;
using Gamenet.Data;
using Gamenet.Logger;
using GrattaEVinci.Common;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Model;
using GrattaEVinciReference;
using GestioneAnagraficaReference;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Enum = GrattaEVinci.Common.Enum;

namespace GrattaEVinci.Data
{
    public class TransactionManagerMongoDb : MongoDbDao<Transaction>
    {
        public TransactionManagerMongoDb(IOptions<TransactionMongoOptions> options,ILog log) : base(options, log)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(modificaContoGiocoRequest)))
                BsonClassMap.RegisterClassMap<modificaContoGiocoRequest>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(modificaContoGiocoResponse)))
                BsonClassMap.RegisterClassMap<modificaContoGiocoResponse>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(AddebitoResponse)))
                BsonClassMap.RegisterClassMap<AddebitoResponse>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(registraContoGiocoRequest)))
                BsonClassMap.RegisterClassMap<registraContoGiocoRequest>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(registraContoGiocoResponse1)))
                BsonClassMap.RegisterClassMap<registraContoGiocoResponse1>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            if (!BsonClassMap.IsClassMapRegistered(typeof(ReserveAddebitoRequestModel)))
                BsonClassMap.RegisterClassMap<ReserveAddebitoRequestModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            if (!BsonClassMap.IsClassMapRegistered(typeof(CommitAddebitoRequestModel)))
                BsonClassMap.RegisterClassMap<CommitAddebitoRequestModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            if (!BsonClassMap.IsClassMapRegistered(typeof(RollbackAddebitoRequestModel)))
                BsonClassMap.RegisterClassMap<RollbackAddebitoRequestModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });

            var indexTtl = new IndexKeysDefinitionBuilder<Transaction>().Ascending(c => c.Timestamp);
            //Scadenza Transazioni 7 gg -- 168 ore
            this.Collection.Indexes.CreateOneAsync(new CreateIndexModel<Transaction>(indexTtl, new CreateIndexOptions { ExpireAfter = TimeSpan.FromHours(168) }));

            var indiceRicerca = new IndexKeysDefinitionBuilder<Transaction>().Descending(c => c.SourceTransactionCode)
                .Descending(c => c.RelatedTransactionCode).Descending(c => c.OperationType);
            this.Collection.Indexes.CreateOneAsync(new CreateIndexModel<Transaction>(indiceRicerca));
        }

        public override void Delete(Transaction item)
        {
            var filter = Builders<Transaction>.Filter.Where(data => data.Id == item.Id);
            this.Collection.FindOneAndDelete<Transaction>(filter);
        }

        public override Transaction GetByKey(Guid key)
        {
            return base.Get().SingleOrDefault(data => data.Id == key);
        }

        public override void Update(Transaction item)
        {
            var filter = Builders<Transaction>.Filter.Where(data => data.Id == item.Id);
            this.Collection.FindOneAndReplace<Transaction>(filter, item);
        }

        public override async Task UpdateAsync(Transaction item)
        {
            var filter = Builders<Transaction>.Filter.Where(data => data.Id == item.Id);
            await this.Collection.FindOneAndReplaceAsync<Transaction>(filter, item);
        }

        public override async Task DeleteAsync(Transaction item)
        {
            var filter = Builders<Transaction>.Filter.Where(data => data.Id == item.Id);
            await this.Collection.FindOneAndDeleteAsync<Transaction>(filter);
        }

        public override async Task<Transaction> GetByKeyAsync(Guid key)
        {
            return base.GetAsync().Result.SingleOrDefault(data => data.Id == key);
        }

        public async Task<Transaction> GetByTransactionCodeAndOperationType(string sourceTransactionCode, string relatedTransactionCode, CasinoOperationType operationTypeWallet)
        {


            var test = await base.GetAsync();
                var transaction = test.SingleOrDefault(data => data.SourceTransactionCode == sourceTransactionCode && data.RelatedTransactionCode == relatedTransactionCode 
                                                          && data.OperationType == operationTypeWallet
                                                          && data.Result != (int)ResultCodeTransaction.GenericFault
                                                          && data.Result != (int)Enum.Result.Errore_Di_Sistema
                                                          && data.Result != (int)Enum.Result.Sistema_Non_Disponibile
                                                          );

            return transaction;

        }

        public async Task IncreaseRetry(string sourceTransactionCode, string relatedTransactionCode , CasinoOperationType operationTypeWallet)

        {
            var collection = base.GetAsync();
            var oldTransaction = collection.Result.SingleOrDefault(data => data.SourceTransactionCode == sourceTransactionCode && data.RelatedTransactionCode == relatedTransactionCode 
                                                                                                                                && data.OperationType == operationTypeWallet
                                                                                                                                && data.Result != (int)ResultCodeTransaction.GenericFault
                                                                                                                                && data.Result != (int)Enum.Result.Errore_Di_Sistema
                                                                                                                                && data.Result != (int)Enum.Result.Sistema_Non_Disponibile
                                                                    );
            if (oldTransaction != null)
            {
                oldTransaction.NumeroRetry++;
                await UpdateAsync(oldTransaction);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }
    }
}
