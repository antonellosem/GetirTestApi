using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.ModelConfiguration;
using GrattaEVinci.Common.Signature;
using Microsoft.Extensions.Options;

namespace GrattaEVinci.Common
{
    public class Validator
    {
        private readonly IOptions<Config> _appSettings;
        public Validator(IOptions<Config> appSettings)
        {
            _appSettings = appSettings;
        }
        public bool VerificaFirmeReserveIn(ReserveAddebitoRequestModel request)
        {
            string datiFirmati = $"{request.CodiceContratto}{request.Token}{request.IdTransazione}{request.CostoGiocata}{request.IdGioco}";

            if (datiFirmati != request.DatiFirmati)
                return false;

            return Firma.VerificaCorrettezzaFirma(datiFirmati, request.Firma, request.IdRivenditore, _appSettings);
        }

        public bool VerificaFirmeCommitIn(CommitAddebitoRequestModel request)
        {
            string datiFirmati = $"{request.Token}{request.CodiceContratto}{request.IdTransazione}{request.ImportoVincita}{request.FasciaVincita}{request.ImportoAccredito}{request.IdGioco}";

            if (datiFirmati != request.DatiFirmati)
                return false;

            return Firma.VerificaCorrettezzaFirma(datiFirmati, request.Firma, request.IdRivenditore, _appSettings);
        }

        public bool VerificaFirmeRollbackIn(RollbackAddebitoRequestModel request)
        {
            string datiFirmati = $"{request.IdTransazione}{request.Token}{request.CodiceContratto}{request.CostoGiocata}{request.IdGioco}";

            if (datiFirmati != request.DatiFirmati)
                return false;

            return Firma.VerificaCorrettezzaFirma(datiFirmati, request.Firma, request.IdRivenditore, _appSettings);
        }

        public bool VerificaFirmeChiaveContoGiocoIn(ChiaveContoGiocoRequestModel request)
        {
            string datiFirmati = $"{request.CodiceContratto}{request.IdRivenditore}";

            if (datiFirmati != request.DatiFirmati)
                return false;

            return Firma.VerificaCorrettezzaFirma(datiFirmati, request.Firma, request.IdRivenditore, _appSettings);
        }

    }
}
