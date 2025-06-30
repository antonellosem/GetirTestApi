using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GrattaEVinci.Manager
{
    public static class Map
    {

        public static decimal ImportInEuroDecimal(int amount)
        {
            return decimal.Round(Convert.ToDecimal(amount) / 100, 2);
        }
        public static int ImportoInCentesimiDiEuro(string amount)
        {
            return (int)(decimal.Round(Convert.ToDecimal(amount.Replace(',', '.'), CultureInfo.InvariantCulture), 2) * 100);
        }
    }
}
