using Gamenet.Infrastructure;
using Gamenet.Logger;
using System.Data;
using System.Linq;
using Dapper;
using PgadData.Const;
using PgadData.Entity;

namespace PgadData
{
    public class PacgProtocolloDao : DapperBaseDao
    {
        private ITransazioniConnectionStringsProvider _transazioniConnectionStringsProvider;

        public PacgProtocolloDao(ITransazioniConnectionStringsProvider connectionStringsProvider, ILog logger = null) : base(connectionStringsProvider, logger)
        {
            _transazioniConnectionStringsProvider = connectionStringsProvider;
        }

        public long GetIdTransazionePacg()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Direzione", 1);

            CommandDefinition cmd = new CommandDefinition(StoredProcedure.GestisciTransazionePgad,
                parameters: parameters, commandType: CommandType.StoredProcedure);

            int output = Query<int>(cmd).FirstOrDefault();
            ;

            return output;
        }

        public ConcessionarioInfo GetConcessionarioInfo(int titolareSistema)
        {
            ConcessionarioInfo result;

            var parameters = new DynamicParameters();
            parameters.Add("@CodiceTitolareSistema", titolareSistema);

            var cmd = new CommandDefinition(StoredProcedure.GetConcessionarioInfo, parameters: parameters,
                commandType: CommandType.StoredProcedure);
            result = QuerySingle<ConcessionarioInfo>(cmd);

            return result;
        }
    }
}
