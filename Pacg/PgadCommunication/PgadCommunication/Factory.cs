using System;
using PgadCommunication.Utility;

namespace PgadCommunication
{
    public class Factory
    {
        public static IPgadGateway NewPgadInstance(string Username,int TitolareSistema)
        {
            return (IPgadGateway)Activator.CreateInstance(Type.GetType(Settings.Instance.PgadGatewayClass), Username, TitolareSistema);
        }

        public static IPgadGateway NewPgadInstance(string className, string Username, int TitolareSistema)
        {
            return (IPgadGateway)Activator.CreateInstance(Type.GetType(className), Username, TitolareSistema);
        }
    }                    
}
