using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Gamenet.Common.Enum;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Contract.Responses;
using GrattaEVinci.Common.Enum;
using GrattaEVinci.Common.Model;
using GrattaEVinci.Common.ModelConfiguration;
using GrattaEVinci.Common.Signature;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GrattaEVinci.Common.Utility
{
    public static class ExtensionMethod
    {
        public static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;

        private static RSACryptoServiceProvider RSAIlot = null;

        public static string NormalizeResultCodeDescription(this string str)
            => ReplaceSpecialCharToSpace(str).Trim();

        public static string ReplaceSpecialCharToSpace(this string value)
            => value.Replace('_', ' ');

        public static string ReplaceSpecialCharToPoint(this string value)
            => value.Replace(',', '.');

        public static int ToInt(this string id)
            => Convert.ToInt32(id);

        public static string FirmaStringa(this string firma, Brand idBrand, IOptions<Config> appSettings)
            => Firma.FirmaDati(firma, idBrand, appSettings);

        public static ResponseBase SetSuccess(this ResponseBase response)
        {
            response.Message = ResultCode.Success.ToString();
            response.ResultCode = ResultCode.Success;
            return response;
        }
        public static ResponseBase SetFailure(this ResponseBase response)
        {
            response.Message = ResultCode.SystemError.ToString();
            response.ResultCode = ResultCode.SystemError;
            return response;
        }
        public static Anagrafica SetLimitiLol(this Anagrafica response)
        {
            response.nome = response.nome.Length > 50 ? response.nome.Substring(0, 50) : response.nome;
            response.cognome = response.cognome.Length > 70 ? response.cognome.Substring(0, 70) : response.cognome;
            response.cod_fiscale = response.cod_fiscale;
            return response;
        }
        public static string Tosha256(this string password)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
