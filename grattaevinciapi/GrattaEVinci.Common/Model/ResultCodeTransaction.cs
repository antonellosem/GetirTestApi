namespace GrattaEVinci.Common.Model
{
    public enum ResultCodeTransaction
    {
        Running = -99,
        Processed = 0,
        InvalidBalanceFault,
        AuthorizationDeniedFault,
        InvalidTokenFault,
        GenericFault,
        InvalidUserFault
    }
}
