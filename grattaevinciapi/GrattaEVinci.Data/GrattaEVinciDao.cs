using System.Data;
using Dapper;
using Gamenet.Infrastructure;
using Gamenet.Logger;
using GrattaEVinci.Common.Entities;

namespace GrattaEVinci.Data
{
    public class GrattaEVinciDao : DapperBaseDao
    {
        public GrattaEVinciDao(IDaoConnectionStringsProvider connectionStringsProvider, ILog logger = null) : base(connectionStringsProvider, logger)
        {
        }

        #region Stored
        private static string RESERVE_TICKET = "Dati.Biglietto_Reserve";
        private static string UPDATE_RESERVE = "Dati.Biglietto_Prenotazione_Update";
        private static string VERIFICA_TOKEN_TRANSAZIONE = "Dati.Verifica_Token_Transazione";
        private static string COMMIT_TICKET = "Dati.Biglietto_Commit";
        private static string GET_DATI_RESERVE = "Dati.Biglietto_Prenotazione_Get";
        private static string GET_DATI_BIGLIETTO = "Dati.Biglietto_Get";
        private static string CANCEL_RESERVE = "Dati.Reserve_Cancel";
        private static string SESSIONE_START = "Dati.Sessione_Start";
        private static string SESSIONE_END = "Dati.Sessione_End";
        private static string SESSIONE_VERIFY = "Dati.Sessione_Verifica";
        private static string ANAGRAFICA_SAVE_KEY = "Dati.Chiave_Encrypt_Anagrafica_Insert";
        private static string VERIFICA_VINCITA_FASCIA_ALTA = "Dati.Verifica_Esistenza_Vincite_Fascia_Alta";
        private static string GET_CHIAVE_CONTO_GIOCO = "Dati.Chiave_Conto_Gioco_GET";
        private static string GET_IDUTENTE_DA_CODICE_CONTRATTO = "Dati.Id_Utente_Da_Codice_Contratto_Get";
        private static string ANAGRAFICA_CRIPTATA_GET = "Dati.Anagrafica_Encript_Get";
        private static string IDPRODOTTO_BY_CODICEGIOCO = "Dati.IdProdotto_ByCodiceGioco_GET";
        #endregion


        #region Cashier
        public async Task<PrenotazioneBiglietto> Reserve(int idUtente, DateTime dataPrenotazione, string idTransazione, string token, int costoGiocata, string idConcorso, string codiceGiocoPartner)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@IdUtente", idUtente);
            parameters.Add("@DataPrenotazione", dataPrenotazione);
            parameters.Add("@IdTransazione", idTransazione);
            parameters.Add("@Token", token);
            parameters.Add("@CostoGiocata", costoGiocata);
            parameters.Add("@IdConcorso", idConcorso);
            parameters.Add("@CodiceGiocoPartner", codiceGiocoPartner);

            CommandDefinition cmd = new CommandDefinition(RESERVE_TICKET, parameters: parameters, commandType: CommandType.StoredProcedure);
            return await QuerySingleAsync<PrenotazioneBiglietto>(cmd);

        }

        public async Task UpdateReserve(int idPrenotazioneBiglietto, long idMovimento, int importoBonus, int idCanale, int idProdotto)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@IdPrenotazioneBiglietto", idPrenotazioneBiglietto);
            parameters.Add("@IdMovimento", idMovimento);
            parameters.Add("@ImportoBonus", importoBonus);
            parameters.Add("@IdCanale", idCanale);
            parameters.Add("@IdProdotto", idProdotto);

            CommandDefinition cmd = new CommandDefinition(UPDATE_RESERVE, parameters: parameters, commandType: CommandType.StoredProcedure);
            await ExecuteAsync(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));
        }
        #endregion

        public async Task<bool> VerificaTokenTransazione(int idUtente, string token, string idTransazione)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@IdUtente", idUtente);
            parameters.Add("@Token", token);
            parameters.Add("@IdTransazione", idTransazione);

            CommandDefinition cmd = new CommandDefinition(VERIFICA_TOKEN_TRANSAZIONE, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<bool>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task<CommitBiglietto> CommitReserve(string codiceTicket, string idTransazione, int importoVincita, int importoAccredito, string fasciaVincita, DateTime dataOperazione, int idUtente, string idConcorso)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@CodiceTicket", codiceTicket);
            parameters.Add("@IdTransazione", idTransazione);
            parameters.Add("@ImportoVincita", importoVincita);
            parameters.Add("@ImportoAccredito", importoAccredito);
            parameters.Add("@FasciaVincita", fasciaVincita);
            parameters.Add("@DataOperazione", dataOperazione);
            parameters.Add("@IdUtente", idUtente);
            parameters.Add("@IdConcorso", idConcorso);

            CommandDefinition cmd = new CommandDefinition(COMMIT_TICKET, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<CommitBiglietto>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task<InfoPrenotazione?> GetDatiReserve(string idTransazione)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@IdTransazione", idTransazione);

            CommandDefinition cmd = new CommandDefinition(GET_DATI_RESERVE, parameters: parameters, commandType: CommandType.StoredProcedure);
            List<InfoPrenotazione> result = await QueryAsync<InfoPrenotazione>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result.FirstOrDefault();
        }
        public async Task<Biglietto> GetDatiBiglietto(string idTransazione)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@IdTransazione", idTransazione);

            CommandDefinition cmd = new CommandDefinition(GET_DATI_BIGLIETTO, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<Biglietto>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task CancelReserve(string idTransazione)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@IdTransazione", idTransazione);

            CommandDefinition cmd = new CommandDefinition(CANCEL_RESERVE, parameters: parameters, commandType: CommandType.StoredProcedure);
            await ExecuteAsync(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));
        }

        public async Task SessionStart(int idUtente, string token)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@idUtente", idUtente);
            parameters.Add("@token", token);

            CommandDefinition cmd = new CommandDefinition(SESSIONE_START, parameters: parameters, commandType: CommandType.StoredProcedure);
            await ExecuteAsync(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));
        }

        public async Task SessionEnd(int idUtente, string token)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@idUtente", idUtente);
            parameters.Add("@token", token);

            CommandDefinition cmd = new CommandDefinition(SESSIONE_END, parameters: parameters, commandType: CommandType.StoredProcedure);
            await ExecuteAsync(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));
        }

        public async Task<bool> Session_Verify(int idUtente, string token)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@idUtente", idUtente);
            parameters.Add("@token", token);

            CommandDefinition cmd = new CommandDefinition(SESSIONE_VERIFY, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<bool>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task SalvaChiaveAnagrafrica(int idUtente, string chiaveEncrypt, string anagraficaCriptata)
        {
            var parameters = InstanceDynamicParameterWithResultValue();

            parameters.Add("@IdUtente", idUtente);
            parameters.Add("@ChiaveCriptazione", chiaveEncrypt);
            parameters.Add("@AnagraficaCriptata", anagraficaCriptata);

            CommandDefinition cmd = new CommandDefinition(ANAGRAFICA_SAVE_KEY, parameters: parameters,
                commandType: CommandType.StoredProcedure);
            await ExecuteAsync(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));
        }

        public async Task<bool> VerificaVincitaFasciaAlta(int idUtente)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@IdUtente", idUtente);

            CommandDefinition cmd = new CommandDefinition(VERIFICA_VINCITA_FASCIA_ALTA, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<bool>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task<string> GetChiaveContoGioco(int idUtente)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@IdUtente", idUtente);

            CommandDefinition cmd = new CommandDefinition(GET_CHIAVE_CONTO_GIOCO, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<string>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task<UtenteBrand> GetIdUtenteDaCodiceContratto(string codiceContratto)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@CodiceContratto", codiceContratto);

            CommandDefinition cmd = new CommandDefinition(GET_IDUTENTE_DA_CODICE_CONTRATTO, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleOrDefaultAsync<UtenteBrand>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task<AnagraficaKeys> AnagraficaEncryptGet(int idUtente)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@IdUtente", idUtente);

            CommandDefinition cmd = new CommandDefinition(ANAGRAFICA_CRIPTATA_GET, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleAsync<AnagraficaKeys>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }

        public async Task<int?> GetIdProdottoByCodiceGiocoPartner(string codiceGiocoPartner,string idTransazione)
        {
            var parameters = InstanceDynamicParameterWithResultValue();
            parameters.Add("@CodiceGiocoPartner", codiceGiocoPartner);
            parameters.Add("@IdTransazione", idTransazione);

            CommandDefinition cmd = new CommandDefinition(IDPRODOTTO_BY_CODICEGIOCO, parameters: parameters, commandType: CommandType.StoredProcedure);
            var result = await QuerySingleOrDefaultAsync<int?>(cmd);
            CheckReturnValue(parameters.Get<int>("ErrorNumber"), parameters.Get<string>("ErrorMessage"));

            return result;
        }
    }
}