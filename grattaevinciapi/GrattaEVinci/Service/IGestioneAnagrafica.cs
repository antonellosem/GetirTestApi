using CoreWCF;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Contract.Responses;

namespace GrattaEVinci.Service
{
    [ServiceContract]
    public interface IGestioneAnagrafica
    {
        [OperationContract]
        ResponseBase RegistraContoGioco(RegistraModificaContoRequest request);
        [OperationContract]
        ResponseBase ModificaContoGioco(RegistraModificaContoRequest request);
    }
}
