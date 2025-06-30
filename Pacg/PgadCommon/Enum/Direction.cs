using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public enum Direction
    {
        [EnumMember]
        Inwards = 0,
        [EnumMember]
        Outwards = 1
    }
}
