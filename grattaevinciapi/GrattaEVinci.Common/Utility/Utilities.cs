using GrattaEVinci.Common.Contract.Requests;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace GrattaEVinci.Common.Utility
{
    public static class Utilities
    {
        public static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;

        public static string ToLoggableRequest(this LoginRequest request)
        {
            var serializedRequest = JsonConvert.SerializeObject(request);
            return serializedRequest.Replace(request.password, "**");
        }
    }
}
