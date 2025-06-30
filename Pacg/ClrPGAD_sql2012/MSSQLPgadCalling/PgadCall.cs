//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.SqlServer.Server;
using PGADInterface;

public partial class StoredProcedures
{
    //[Microsoft.SqlServer.Server.SqlProcedure]
    //public static void StatoConto(string ContractCode)
    //{
    //    //try
    //    //{
    //    //    SqlPipe sqlPipe = SqlContext.Pipe;
    //    //    string result = PgadInterface.CallPgadService(ContractCode)._descrizione;
    //    //    SqlContext.Pipe.Send(result);
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    SqlContext.Pipe.Send(ex.Message);
    //    //}

    //    //try
    //    //{
    //    //    SqlPipe sqlPipe = SqlContext.Pipe;
    //    //    PgadStatoContoResponse result = PgadInterface.CallPgadService(ContractCode);
    //    //    XmlSerializer xsSubmit = new XmlSerializer(result.GetType());
    //    //    StringWriter sww = new StringWriter();
    //    //    XmlTextWriter writer = new XmlTextWriter(sww);
    //    //    xsSubmit.Serialize(writer, result);
    //    //    string res = sww.ToString().Replace('<', '[').Replace("/>", "/]");
    //    //    SqlContext.Pipe.Send(res);
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    SqlContext.Pipe.Send(ex.Message);
    //    //}


    //}

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceAggiornaDocumento(string PartitaIva, int TitolareSistema, string CodiceContratto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.AggiornaDocumento(PartitaIva, TitolareSistema, CodiceContratto, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceApriConto(string PartitaIva, int TitolareSistema, string CodiceContratto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName, string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita, string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ApriConto(PartitaIva, TitolareSistema, CodiceContratto, CodiceFiscale, Cognome, Nome, Sesso, Email, UserName, Comune_Nazione_Nascita, ProvinciaNascita, DataNascita, IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceApriContoSemplificato(string PartitaIva, int TitolareSistema, string CodiceContratto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita, string ProvinciaResidenza, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ApriContoSemplificato(PartitaIva, TitolareSistema, CodiceContratto, CodiceFiscale, Cognome, Nome, Sesso, Comune_Nazione_Nascita, ProvinciaNascita, DataNascita, ProvinciaResidenza, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceApriContoSemplificatoIntegrazione(string PartitaIva, int TitolareSistema, string CodiceContratto, string Email, string UserName, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ApriContoSemplificatoIntegrazione(PartitaIva, TitolareSistema, CodiceContratto, Email, UserName, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceCambioStato(string PartitaIva, string CodiceContratto, int TitolareSistema, int CausaleCambioStato, int StatoConto, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.CambiaStato(PartitaIva, CodiceContratto, TitolareSistema, CausaleCambioStato, StatoConto, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceContoGiocoDormiente(int Saldo, string PartitaIva, int TitolareSistema, string CodiceContratto, DateTime DataDormiente, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ContoGiocoDormiente(Saldo, PartitaIva, TitolareSistema, CodiceContratto, DataDormiente, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceElencoContiGiocoDormienti(string PartitaIva, int TitolareSistema, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ElencoContiGiocoDormienti(PartitaIva, TitolareSistema, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceElencoContiGiocoSenzaSubRegistrazione(string PartitaIva, int TitolareSistema, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ElencoContiGiocoSenzaSubRegistrazione(PartitaIva, TitolareSistema, IndirizzoServizio); ;

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceModificaProvinciaResidenza(string PartitaIva, int TitolareSistema, string CodiceContratto, string ProvinciaResidenza, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.ModificaProvinciaResidenza(PartitaIva, TitolareSistema, CodiceContratto, ProvinciaResidenza, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceSubRegistrazione(string PartitaIva, int TitolareSistema, string CodiceContratto, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.SubRegistrazione(PartitaIva, TitolareSistema, CodiceContratto, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceSubRegistrazione2(int Saldo, int SaldoBonus, string PartitaIva, int TitolareSistema, string CodiceContratto, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime)
        );

        try
        {
            PgadResponse result = PgadInterface.SubRegistrazione2(Saldo, SaldoBonus, PartitaIva, TitolareSistema, CodiceContratto, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InterfaceStatoConto(string PartitaIva, int TitolareSistema, string CodiceContratto, string IndirizzoServizio)
    {
        SqlDataRecord rec = new SqlDataRecord(
        new SqlMetaData("Descrizione", SqlDbType.VarChar, -1),
        new SqlMetaData("Esito", SqlDbType.SmallInt),
        new SqlMetaData("IdRicevuta", SqlDbType.VarChar, -1),
        new SqlMetaData("IdTransazione", SqlDbType.BigInt),
        new SqlMetaData("Success", SqlDbType.Bit),
        new SqlMetaData("Timestamp", SqlDbType.DateTime),
        new SqlMetaData("Causale", SqlDbType.Int),
        new SqlMetaData("CodiceConto", SqlDbType.VarChar, -1),
        new SqlMetaData("IdCnConto", SqlDbType.BigInt),
        new SqlMetaData("IdReteConto", SqlDbType.Int),
        new SqlMetaData("Stato", SqlDbType.Int)
        );

        try
        {
            PgadStatoContoResponse result = PgadInterface.StatoConto(PartitaIva, TitolareSistema, CodiceContratto, IndirizzoServizio);

            SqlContext.Pipe.SendResultsStart(rec);
            {
                rec.SetSqlString(0, result._descrizione);
                rec.SetSqlInt16(1, result._esito);
                rec.SetSqlString(2, result._idRicevuta);
                rec.SetSqlInt64(3, result._idTransazione);
                rec.SetSqlBoolean(4, result._success);
                rec.SetSqlDateTime(5, result._timestamp);
                rec.SetSqlInt32(6, result.Causale);
                rec.SetSqlString(7, result.CodiceConto);
                rec.SetSqlInt64(8, result.IdCnConto);
                rec.SetSqlInt32(9, result.IdReteConto);
                rec.SetSqlInt32(10, result.Stato);

                SqlContext.Pipe.SendResultsRow(rec);
            }

            SqlContext.Pipe.SendResultsEnd();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }
}
