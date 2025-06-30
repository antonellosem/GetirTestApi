
using System.Runtime.Serialization;

namespace GrattaEVinci.Common.Contract.Requests
{
    public class LogoutRequest
    {
        public string? IdContoGioco { get; set; }
        public string? token { get; set; }
        public string? idRivenditore { get; set; }
    }
}
