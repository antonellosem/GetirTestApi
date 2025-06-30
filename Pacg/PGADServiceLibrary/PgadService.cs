using System;
using System.Collections.Generic;
using System.ServiceModel;
using PgadCommunication.Utility;
using PgadCommunication.Business;
using PgadCommunication;
using System.Data;
using PgadCommon.Enum;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;
using System.Diagnostics;
using System.Reflection;
using NLog;
using PgadCommunication.Pgad;

namespace PGADServiceLibrary
{


    [ServiceBehavior(Namespace = "http://www.intralot.it/pgad")]
    public class PGADService : IPgad
    {
        public PgadGatewayResponse ApriContoPersonaGiuridica(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string PartitaIvaPG, string RagioneSociale,
            string Indirizzo, string Comune, string Provincia, string Cap,
            string Email, string UserName)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {

                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioPersonaGiuridica società = new MessaggioPersonaGiuridica(CodiceAgenzia, CodiceConto, IdTransazione);
                    società.SetAnagrafica(PartitaIvaPG, RagioneSociale, Email, UserName);
                    società.SetSede(Indirizzo, Comune, Provincia, Cap);
                    PgadGatewayResponse resp = pgad.ApriContoPersonaGiuridica(società);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse MovimentazioneCredito(long IdTransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return MovimentazioneCredito(IdTransazione, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, DatasetToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        public PgadGatewayResponse MovimentazioneCredito_V2(long IdTransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return MovimentazioneCredito(IdTransazione, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        private PgadGatewayResponse MovimentazioneCredito(long IdTransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<MessaggioDettaglioBonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioMovimentoConto movimento = new MessaggioMovimentoConto(TitolareSistema, ContoDiGioco, IdTransazione);
                    movimento.SetMovimento(CausaleDiMovimento, MezzoDiPagamento, ImportoMovimento);
                    movimento.SetSaldo(SaldoUtente, DataSaldo);
                    movimento.SetBonus(ListBonusPresenti);
                    movimento.SetSaldo(SaldoUtente, DataSaldo);
                    PgadGatewayResponse response = pgad.Movimento(movimento);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse MovimentazioneBonus(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, System.Data.DataSet ListBonusDaAssegnare, System.Data.DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return MovimentazioneBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, DatasetToMessaggioDettaglioBonus(ListBonusDaAssegnare), DatasetToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        public PgadGatewayResponse MovimentazioneBonus_V2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return MovimentazioneBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListOfBonusToMessaggioDettaglioBonus(ListBonusDaAssegnare), ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        private PgadGatewayResponse MovimentazioneBonus(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<MessaggioDettaglioBonus> bonusMovimentati, List<MessaggioDettaglioBonus> bonusAttivi, int SaldoUtente, DateTime DataSaldo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {

                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioMovimentoBonus movimentoBonus = new MessaggioMovimentoBonus(TitolareSistema, ContoDiGioco, IdTransazione);

                    //Bonus Movimentato
                    movimentoBonus.SetMovimentoBonus(CausaleMovimentoBonus.BONUS, bonusMovimentati);

                    //Bonus Attivi
                    movimentoBonus.SetSaldoBonus(bonusAttivi);
                    movimentoBonus.SetSaldo(SaldoUtente, DataSaldo);

                    PgadGatewayResponse response = pgad.MovimentazioneBonus(movimentoBonus);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse StornoBonus(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, System.Data.DataSet ListBonusDaAssegnare, System.Data.DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, DatasetToMessaggioDettaglioBonus(ListBonusDaAssegnare), DatasetToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        public PgadGatewayResponse StornoBonus_V2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListOfBonusToMessaggioDettaglioBonus(ListBonusDaAssegnare), ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        private PgadGatewayResponse StornoBonus(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<MessaggioDettaglioBonus> ListBonusDaAssegnare, List<MessaggioDettaglioBonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {

                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioMovimentoBonus movimentoBonus = new MessaggioMovimentoBonus(TitolareSistema, ContoDiGioco, IdTransazione);

                    //Bonus movimentati ( da stornare)
                    movimentoBonus.SetMovimentoBonus(CausaleMovimentoBonus.STORNO_BONUS, ListBonusDaAssegnare);

                    //Bonus attivi (già erogati)
                    movimentoBonus.SetSaldoBonus(ListBonusPresenti);
                    movimentoBonus.SetSaldo(SaldoUtente, DataSaldo);

                    PgadGatewayResponse response = pgad.MovimentazioneBonus(movimentoBonus);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse CambioStato(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, CausaleCambioStatoConto CausaleCambioStato, StatoConto StatoConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {

                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioCambioStatoConto cambioStato = new MessaggioCambioStatoConto(CodiceAgenzia, CodiceContratto, IdTransazione);
                    cambioStato.SetStato(CausaleCambioStato, StatoConto);
                    PgadGatewayResponse response = pgad.CambioStato(cambioStato);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {CodiceContratto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContratto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse SaldoContoUtente(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, System.Data.DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return SaldoContoUtente(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, DatasetToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        public PgadGatewayResponse SaldoContoUtente_V2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return SaldoContoUtente(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        private PgadGatewayResponse SaldoContoUtente(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<MessaggioDettaglioBonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioSaldoConto _saldo = new MessaggioSaldoConto(TitolareSistema, ContoDiGioco, IdTransazione);
                    _saldo.SetSaldo(SaldoUtente, DataSaldo);
                    _saldo.SetSaldoBonus(ListBonusPresenti);
                    PgadGatewayResponse response = pgad.Saldo(_saldo);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse ModificaProvinciaResidenza(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, string ProvinciaResidenza)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioModificaProvinciaResidenza modificaProvincia = new MessaggioModificaProvinciaResidenza(CodiceAgenzia, CodiceContratto, IdTransazione);
                    modificaProvincia.SetProvincia(ProvinciaResidenza);
                    PgadGatewayResponse resp = pgad.ModificaResidenza(modificaProvincia);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceContratto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContratto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayStatoResponse StatoConto(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioStatoConto stato = new MessaggioStatoConto(CodiceAgenzia, CodiceContratto, IdTransazione);
                    PgadGatewayStatoResponse resp = pgad.Stato(stato);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceContratto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContratto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse MigrazioneContoGioco(long IdTransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, DataSet ListBonusPresenti)
        {
            return MigrazioneContoGioco(IdTransazione, PartitaIva, IdReteContoOriginario, TitolareSistemaOriginario, CodiceContoOriginario, IdReteContoDestinazione, TitolareSistemaDestinazione, CodiceContoDestinazione, CodiceFiscale, ImportoSaldo, DataSaldo, DatasetToMessaggioDettaglioBonus(ListBonusPresenti));
        }

        public PgadGatewayResponse MigrazioneContoGioco_V2(long IdTransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, List<Bonus> ListBonusPresenti)
        {
            return MigrazioneContoGioco(IdTransazione, PartitaIva, IdReteContoOriginario, TitolareSistemaOriginario, CodiceContoOriginario, IdReteContoDestinazione, TitolareSistemaDestinazione, CodiceContoDestinazione, CodiceFiscale, ImportoSaldo, DataSaldo, ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti));
        }

        private PgadGatewayResponse MigrazioneContoGioco(long IdTransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, List<MessaggioDettaglioBonus> ListBonusPresenti)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistemaOriginario);
                    MessaggioMigrazioneConto migrazione = new MessaggioMigrazioneConto(TitolareSistemaOriginario, IdTransazione);
                    migrazione.SetContoOriginario(IdReteContoOriginario, TitolareSistemaOriginario, CodiceContoOriginario);
                    migrazione.SetContoDestinazione(IdReteContoDestinazione, TitolareSistemaDestinazione, CodiceContoDestinazione);
                    migrazione.SetCodiceFiscale(CodiceFiscale);
                    migrazione.SetSaldo(ImportoSaldo, DataSaldo);
                    migrazione.SetSaldoBonus(ListBonusPresenti);
                    PgadGatewayResponse resp = pgad.MigrazioneContoGioco(migrazione);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceContoOriginario} - {CodiceContoDestinazione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContoOriginario} - {CodiceContoDestinazione} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse AggiornaDocumento(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioAggiornamentoDocumento aggiornamentoDocumento = new MessaggioAggiornamentoDocumento(CodiceAgenzia, CodiceConto, IdTransazione);
                    aggiornamentoDocumento.SetDocumento(DataRilascioDocumento, AutoritaRilascioDocumento, LocalicaRilascioDocumento, NumeroDocumento, IdTipoDocumento);
                    PgadGatewayResponse response = pgad.AggiornaDocumento(aggiornamentoDocumento);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(long IdTransazione, string PartitaIva,
            int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento,
            string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,sbyte tipoFornituraDatiPersonali)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioAggiornamentoDocumentoTitolareContoGioco2 aggiornamentoDocumento = new MessaggioAggiornamentoDocumentoTitolareContoGioco2(CodiceAgenzia, CodiceConto, IdTransazione, tipoFornituraDatiPersonali);
                    aggiornamentoDocumento.SetDocumento(DataRilascioDocumento, AutoritaRilascioDocumento, LocalicaRilascioDocumento, NumeroDocumento, IdTipoDocumento);
                    PgadGatewayResponse response = pgad.AggiornaDocumentoTitolareContoGioco2(aggiornamentoDocumento);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse RiepilogoOperazioniMovimentazione(long IdTransazione, string PartitaIva, int TitolareSistema, DateTime Data)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioRiepilogoOperazioniMovimentazione _movimentazioni = new MessaggioRiepilogoOperazioniMovimentazione(TitolareSistema, IdTransazione);
                    _movimentazioni.setData(Data);
                    PgdaGatewayRiepilogoOperazioniMovimentazioneResponse response = pgad.RiepilogoOperazioniMovimentazione(_movimentazioni);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }

        public PgdaGatewayRiepilogoOperazioniServizioResponse RiepilogoOperazioniServizio(long IdTransazione, string PartitaIva, int TitolareSistema, DateTime Data)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioRiepilogoOperazioniServizio _operazioni = new MessaggioRiepilogoOperazioniServizio(TitolareSistema, IdTransazione);
                    _operazioni.SetData(Data);
                    PgdaGatewayRiepilogoOperazioniServizioResponse response = pgad.RiepilogoOperazioniServizio(_operazioni);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse StornoMovimentazioneCredito(long IdTransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoMovimentazioneCredito(IdTransazione, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, TipoStorno, idRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, DatasetToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        public PgadGatewayResponse StornoMovimentazioneCredito_V2(long IdTransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoMovimentazioneCredito(IdTransazione, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, TipoStorno, idRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);
        }

        private PgadGatewayResponse StornoMovimentazioneCredito(long IdTransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<MessaggioDettaglioBonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioStornoMovimentoConto stornoMovimento = new MessaggioStornoMovimentoConto(TitolareSistema, ContoDiGioco, IdTransazione);
                    stornoMovimento.SetMovimento(CausaleDiMovimento, MezzoDiPagamento, TipoStorno, ImportoMovimento, idRicevutaDaStornare);
                    stornoMovimento.SetSaldo(SaldoUtente, DataSaldo);
                    //Bonus attivi
                    stornoMovimento.SetBonus(ListBonusPresenti);
                    stornoMovimento.SetSaldo(SaldoUtente, DataSaldo);
                    PgadGatewayResponse response = pgad.StornoMovimento(stornoMovimento);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse SubRegistrazione2(int Saldo, int SaldoBonus, long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioSubRegistrazione2 subregistrazione2 = new MessaggioSubRegistrazione2(Saldo, SaldoBonus, CodiceAgenzia, CodiceContratto, IdTransazione);
                    PgadGatewayResponse resp = pgad.SubRegistrazione2(subregistrazione2);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceContratto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContratto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse ElencoContiGiocoSenzaSubregistrazione(long IdTransazione, DateTime DataRichiesta, string PartitaIva, int CodiceAgenzia, sbyte stato, short inizio, short fine)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioElencoContiGiocoSenzaSubregistrazione elencoContiSenzaSubregistrazione = new MessaggioElencoContiGiocoSenzaSubregistrazione(stato, inizio, fine, CodiceAgenzia, IdTransazione, DataRichiesta);
                    //personaSemplificatoIntegrazione.setStato(stato);
                    PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse resp = pgad.ElencoContiGiocoSenzaSubregistrazione(elencoContiSenzaSubregistrazione);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse ContoGiocoDormiente(int Saldo, long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, DateTime DataDormiente)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioContoDormiente contodormiente = new MessaggioContoDormiente(Saldo, CodiceAgenzia, CodiceContratto, IdTransazione, DataDormiente);
                    PgadGatewayResponse resp = pgad.ContoGiocoDormiente(contodormiente);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceContratto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContratto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayElencoContiGiocoDormientiResponse ElencoContiGiocoDormienti(long IdTransazione, string PartitaIva, int CodiceAgenzia, int anno, int mese, int giorno, short inizio, short fine)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioElencoContiGiocoDormienti elencoContiDormienti = new MessaggioElencoContiGiocoDormienti(anno, mese, giorno, inizio, fine, CodiceAgenzia, IdTransazione);
                    PgadGatewayElencoContiGiocoDormientiResponse resp = pgad.ElencoContiGiocoDormienti(elencoContiDormienti);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayInterrogazioneEstremiDocumentoResponse InterrogazioneEstremiDocumento(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazioneEstremiDocumento interrogazioneEstremiDocumentoTitolare = new MessaggioInterrogazioneEstremiDocumento(CodiceAgenzia, IdTransazione, CodiceConto);
                    PgadGatewayInterrogazioneEstremiDocumentoResponse resp = pgad.InterrogazioneEstremiDocumento(interrogazioneEstremiDocumentoTitolare);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }
            }
        }

        public PgadGatewayAggiornaLimiteResponse AggiornaLimite(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, sbyte GestioneLimite, sbyte TipoLimite, int Importo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioAggiornaLimite aggiornaLimite = new MessaggioAggiornaLimite(CodiceAgenzia, IdTransazione, CodiceContratto, GestioneLimite, TipoLimite, Importo);
                    PgadGatewayAggiornaLimiteResponse resp = pgad.AggiornaLimite(aggiornaLimite);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceContratto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceContratto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayInterrogazioneLimitiResponse InterrogazioneLimiti(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazioneLimiti interrogazioneLimiti = new MessaggioInterrogazioneLimiti(CodiceAgenzia, IdTransazione, CodiceConto);
                    PgadGatewayInterrogazioneLimitiResponse resp = pgad.InterrogazioneLimiti(interrogazioneLimiti);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayGestioneAutoesclusioneTrasversaleResponse GestioneAutoesclusioneTrasversale(long IdTransazione, string PartitaIva, int CodiceAgenzia, sbyte GestioneAutoesclusione, sbyte TipoAutoesclusione, string CodiceFiscale)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioGestioneAutoesclusioneTrasversale gestioneAutoesclusioneTrasversale = new MessaggioGestioneAutoesclusioneTrasversale(CodiceAgenzia, IdTransazione, GestioneAutoesclusione, TipoAutoesclusione, CodiceFiscale);
                    PgadGatewayGestioneAutoesclusioneTrasversaleResponse resp = pgad.GestioneAutoesclusioneTrasversale(gestioneAutoesclusioneTrasversale);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceFiscale}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceFiscale} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayElencoContiAutoesclusiResponse ElencoContiAutoesclusi(long IdTransazione, string PartitaIva, int CodiceAgenzia, short Inizio, short Fine)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioElencoContiAutoesclusi elencoContiAutoesclusi = new MessaggioElencoContiAutoesclusi(CodiceAgenzia, IdTransazione, Inizio, Fine);
                    PgadGatewayElencoContiAutoesclusiResponse resp = pgad.ElencoContiAutoesclusi(elencoContiAutoesclusi);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayAperturaContoPersonaFisica2Response ApriContoPersonaFisica2(long IdTransazione, string PartitaIva, int CodiceAgenzia,
                    string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName,
                    string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita,
                    string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza,
                    DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,
                    sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti
                    )
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    //Settings.logger.Information($"Questa è la username:{UserName}");
                    //Settings.logger.Information($"{IdTransazione}{PartitaIva}{CodiceAgenzia}{CodiceConto}{CodiceFiscale}{Cognome}{Nome}{Sesso}{Email}{UserName}{Comune_Nazione_Nascita}{ProvinciaNascita}");
                    //Settings.logger.Information($"{DataNascita}{IndirizzoResidenza}{ComuneResidenza}{ProvinciaResidenza}{CapResidenza}{DataRilascioDocumento}{AutoritaRilascioDocumento}{LocalicaRilascioDocumento}{NumeroDocumento}{IdTipoDocumento}{NumeroLimiti}");
                    //foreach (var item in ElencoLimiti)
                    //{
                    //    if (item.limite != null)
                    //    {
                    //        Settings.logger.Information($"{item.limite.importo}{item.limite.tipoLimite}");
                    //    }
                    //}
                    MessaggioPersonaFisica2 persona = new MessaggioPersonaFisica2(CodiceAgenzia, CodiceConto, IdTransazione);
                    persona.setAnagrafica(CodiceFiscale, Cognome, Nome, Sesso, Email, UserName);
                    persona.SetDatiNascita(Comune_Nazione_Nascita, ProvinciaNascita, DataNascita);
                    persona.SetResidenza(IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza);
                    persona.SetDocumento(DataRilascioDocumento, AutoritaRilascioDocumento, LocalicaRilascioDocumento, NumeroDocumento, IdTipoDocumento);
                    persona.SetLimiti(NumeroLimiti, ElencoLimiti);
                    PgadGatewayAperturaContoPersonaFisica2Response resp = pgad.AperturaContoPersonaFisica2(persona);

                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }
        public PgadGatewayAperturaContoPersonaFisica3Response ApriContoPersonaFisica3(long IdTransazione, string PartitaIva, int CodiceAgenzia,
                    string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName,
                    string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita,
                    string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza,
                    DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,
                    sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti, sbyte tipoFornituraDatiPersonali
                    )
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                   
                    MessaggioPersonaFisica3 persona = new MessaggioPersonaFisica3(CodiceAgenzia, CodiceConto, IdTransazione, tipoFornituraDatiPersonali);
                    persona.setAnagrafica(CodiceFiscale, Cognome, Nome, Sesso, Email, UserName);
                    persona.SetDatiNascita(Comune_Nazione_Nascita, ProvinciaNascita, DataNascita);
                    persona.SetResidenza(IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza);
                    persona.SetDocumento(DataRilascioDocumento, AutoritaRilascioDocumento, LocalicaRilascioDocumento, NumeroDocumento, IdTipoDocumento);
                    persona.SetLimiti(NumeroLimiti, ElencoLimiti);
                    PgadGatewayAperturaContoPersonaFisica3Response resp = pgad.AperturaContoGiocoPersonaFisica3(persona);

                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayInterrogazioneSoggettoAutoesclusoResponse InterrogazioneSoggettoAutoescluso(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceFiscale)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazioneSoggettoAutoescluso persona = new MessaggioInterrogazioneSoggettoAutoescluso(CodiceAgenzia, IdTransazione, CodiceFiscale);
                    persona.SetCodiceFiscale(CodiceFiscale);
                    PgadGatewayInterrogazioneSoggettoAutoesclusoResponse resp = pgad.InterrogazioneSoggettoAutoescluso(persona);

                    if (resp.Esito != (int)EsitoRichiesta.OK && resp.Esito != 1404) // il soggetto non è autoescluso
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceFiscale}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceFiscale} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse AggiornaPseudonimoContoDiGioco(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string Pseudonimo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioAggiornaPseudonimoContoDiGioco persona = new MessaggioAggiornaPseudonimoContoDiGioco(CodiceAgenzia, IdTransazione, CodiceConto, Pseudonimo);
                    persona.SetCodiceFiscale(Pseudonimo);
                    PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse resp = pgad.AggiornaPseudonimoContoDiGioco(persona);

                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse AggiornaPostaElettronicaContoDiGioco(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string PostaElettronica)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioAggiornaPostaElettronicaContoDiGioco persona = new MessaggioAggiornaPostaElettronicaContoDiGioco(CodiceAgenzia, IdTransazione, CodiceConto, PostaElettronica);
                    persona.SetPostaElettronica(PostaElettronica);
                    PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse resp = pgad.AggiornaPostaElettronicaContoDiGioco(persona);

                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public static List<MessaggioDettaglioBonus> DatasetToMessaggioDettaglioBonus(DataSet dsElencoBonus)
        {
            List<MessaggioDettaglioBonus> listBonus = new List<MessaggioDettaglioBonus>();
            if (dsElencoBonus.Tables.Count > 0)
            {
                foreach (DataRow riga in dsElencoBonus.Tables[0].Rows)
                {
                    int Importo = Convert.ToInt32(riga["Importo"].ToString());
                    int TipoGioco = Convert.ToInt32(riga["CodiceGioco"].ToString());
                    int CodiceFamiglia = Convert.ToInt32(riga["CodiceFamiglia"].ToString());
                    MessaggioDettaglioBonus bonus = new MessaggioDettaglioBonus(Importo, CodiceFamiglia, TipoGioco);
                    listBonus.Add(bonus);
                }
            }
            return listBonus;
        }

        public static List<MessaggioDettaglioBonus> ListOfBonusToMessaggioDettaglioBonus(List<Bonus> bonus)
        {
            List<MessaggioDettaglioBonus> listBonus = new List<MessaggioDettaglioBonus>();

            if (bonus != null && bonus.Count > 0)
            {
                foreach (Bonus b in bonus)
                {
                    int Importo = b.Importo;
                    int TipoGioco = b.TipoGioco;
                    int CodiceFamiglia = b.CodiceFamiglia;
                    MessaggioDettaglioBonus mBonus = new MessaggioDettaglioBonus(Importo, CodiceFamiglia, TipoGioco);
                    listBonus.Add(mBonus);
                }
            }
            return listBonus;
        }

        public PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse InterrogazioneStoriaSoggettoAutoescluso(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceFiscale)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazioneStoriaSoggettoAutoescluso persona = new MessaggioInterrogazioneStoriaSoggettoAutoescluso(CodiceAgenzia, IdTransazione, CodiceFiscale);
                    persona.SetCodiceFiscale(CodiceFiscale);
                    PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse resp = pgad.InterrogazioneStoriaSoggettoAutoescluso(persona);

                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceFiscale}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceFiscale} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayInterrogazioneProvinciaResidenzaResponse InterrogazioneProvinciaResidenza(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazioneProvinciaResidenza interrogazioneProvinciaResidenza = new MessaggioInterrogazioneProvinciaResidenza(CodiceAgenzia, IdTransazione, CodiceConto);
                    PgadGatewayInterrogazioneProvinciaResidenzaResponse resp = pgad.InterrogazioneProvinciaResidenza(interrogazioneProvinciaResidenza);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayInterrogazionePseudonimoResponse InterrogazionePseudonimo(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazionePseudonimo interrogazionePseudonimo = new MessaggioInterrogazionePseudonimo(CodiceAgenzia, IdTransazione, CodiceConto);
                    PgadGatewayInterrogazionePseudonimoResponse resp = pgad.InterrogazionePseudonimo(interrogazionePseudonimo);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayInterrogazionePostaElettronicaResponse InterrogazionePostaElettronica(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazionePostaElettronica interrogazionePostaElettronica = new MessaggioInterrogazionePostaElettronica(CodiceAgenzia, IdTransazione, CodiceConto);
                    PgadGatewayInterrogazionePostaElettronicaResponse resp = pgad.InterrogazionePostaElettronica(interrogazionePostaElettronica);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayElencoContiAutoesclusi2Response ElencoContiAutoesclusi2(long IdTransazione, string PartitaIva, int CodiceAgenzia, int Inizio, int Fine)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioElencoContiAutoesclusi2 elencoContiAutoesclusi = new MessaggioElencoContiAutoesclusi2(CodiceAgenzia, IdTransazione, Inizio, Fine);
                    PgadGatewayElencoContiAutoesclusi2Response resp = pgad.ElencoContiAutoesclusi2(elencoContiAutoesclusi);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse StornoMovimentazioneBonusConto(long IdTransazione, string PartitaIva, CausaleMovimentoBonus CausaleDiMovimentoBonus, TipoStornoBonus TipoStornoBonus, string IdRicevutaBonusDaStornare, string ContoDiGioco, int TitolareSistema, int SaldoUtente, DateTime DataSaldo, List<Bonus> ListDettagliBonus, List<Bonus> ListDettagliBonusSaldo)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioStornoMovimentoBonusConto stornoMovimentoBonus = new MessaggioStornoMovimentoBonusConto(TitolareSistema, ContoDiGioco, IdTransazione);
                    stornoMovimentoBonus.SetSaldo(SaldoUtente, DataSaldo);
                    stornoMovimentoBonus.SetMovimentoBonus(CausaleDiMovimentoBonus, TipoStornoBonus, IdRicevutaBonusDaStornare);
                    
                    stornoMovimentoBonus.SetBonus(ListOfBonusToMessaggioDettaglioBonus(ListDettagliBonus));
                    stornoMovimentoBonus.SetBonusSaldo(ListOfBonusToMessaggioDettaglioBonus(ListDettagliBonusSaldo));
                    
                    PgadGatewayResponse response = pgad.StornoMovimentazioneBonusConto(stornoMovimentoBonus);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }

        public PgadGatewayResponse MovimentazioneBonusConto2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            //return MovimentazioneBonusConto2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListOfBonusToMessaggioDettaglioBonus(ListBonusDaAssegnare), ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti), SaldoUtente, DataSaldo);

            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {

                    IPgadGateway pgad = Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, TitolareSistema);
                    MessaggioMovimentoBonusConto2 movimentoBonus = new MessaggioMovimentoBonusConto2(TitolareSistema, ContoDiGioco, IdTransazione);

                    //Bonus Movimentato
                    movimentoBonus.SetMovimentoBonus(CausaleMovimentoBonus.BONUS, ListOfBonusToMessaggioDettaglioBonus(ListBonusDaAssegnare));

                    //Bonus Attivi
                    movimentoBonus.SetSaldoBonus(ListOfBonusToMessaggioDettaglioBonus(ListBonusPresenti));
                    movimentoBonus.SetSaldo(SaldoUtente, DataSaldo);

                    PgadGatewayResponse response = pgad.MovimentazioneBonusConto2(movimentoBonus);
                    if (response.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{response.Esito} - {response.Descrizione} - {ContoDiGioco}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return response;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{ContoDiGioco} - {ex.Message}");
                    throw;
                }

            }
        }


        public PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response InterrogazioneEstremiDocumentoTitolareContoGioco2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2 interrogazioneEstremiDocumentoTitolare = new MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2(CodiceAgenzia, IdTransazione, CodiceConto);
                    PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response resp = pgad.InterrogazioneEstremiDocumentoTitolareContoGioco2(interrogazioneEstremiDocumentoTitolare);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }
            }
        }

        public PgadGatewayResponse ApriContoPersonaGiuridica2(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string PartitaIvaPG, string RagioneSociale,
            string Indirizzo, string Comune, string Provincia, string Cap,
            string Email, string UserName, sbyte TipoContoPersonaGiuridica)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {

                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioPersonaGiuridica2 società = new MessaggioPersonaGiuridica2(CodiceAgenzia, CodiceConto, IdTransazione, TipoContoPersonaGiuridica);
                    società.SetAnagrafica(PartitaIvaPG, RagioneSociale, Email, UserName);
                    società.SetSede(Indirizzo, Comune, Provincia, Cap);
                    PgadGatewayResponse resp = pgad.ApriContoPersonaGiuridica2(società);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione} - {CodiceConto}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;
                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"Id:{CodiceConto} - {ex.Message}");
                    throw;
                }
            }
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(long IdTransazione, DateTime DataRichiesta, string PartitaIva, int CodiceAgenzia, sbyte stato, short inizio, short fine)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioElencoContiGiocoSenzaSubregistrazione2 elencoContiSenzaSubregistrazione = new MessaggioElencoContiGiocoSenzaSubregistrazione2(stato, inizio, fine, CodiceAgenzia, IdTransazione, DataRichiesta);
                    //personaSemplificatoIntegrazione.setStato(stato);
                    PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response resp = pgad.ElencoContiGiocoSenzaSubregistrazione2(elencoContiSenzaSubregistrazione);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;
                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }
            }
        }

        public PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(long IdTransazione, string PartitaIva, int CodiceAgenzia, int anno, int mese, int giorno, short inizio, short fine)
        {
            var stopWatch = Stopwatch.StartNew();
            string method = $"{MethodBase.GetCurrentMethod().Name}";

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));

            using (MappedDiagnosticsLogicalContext.SetScoped(items))
            {
                try
                {
                    IPgadGateway pgad = PgadCommunication.Factory.NewPgadInstance(Settings.Instance.PgadGatewayClass, PartitaIva, CodiceAgenzia);
                    MessaggioElencoContiGiocoDormienti2 elencoContiDormienti = new MessaggioElencoContiGiocoDormienti2(anno, mese, giorno, inizio, fine, CodiceAgenzia, IdTransazione);
                    PgadGatewayElencoContiGiocoDormienti2Response resp = pgad.ElencoContiGiocoDormienti2(elencoContiDormienti);
                    if (resp.Esito != (int)EsitoRichiesta.OK)
                    {
                        Settings.logger.Warning($"{resp.Esito} - {resp.Descrizione}");
                    }
                    Settings.logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
                    return resp;

                }
                catch (Exception ex)
                {
                    Settings.logger.Error( $"{ex.Message}");
                    throw;
                }

            }
        }
    }
}
