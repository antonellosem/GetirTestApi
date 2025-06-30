
using System.Runtime.Serialization;

namespace GrattaEVinci.Common.Contract.Requests
{
    public class LoginRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? idRivenditore { get; set; }
    }
}
