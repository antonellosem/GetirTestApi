using GrattaEVinci.Common.Enum;

namespace GrattaEVinci.Common.Model
{
    public record InfoTransaction
    {
        public Type Tipo { get; set; }
      
        public Reason Reason { get; set; }
    }
}
