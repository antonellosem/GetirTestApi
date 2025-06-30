using Gamenet.Common.Enum;

namespace GrattaEVinci.Common.Entities
{
    public record UtenteBrand
    {
        public int IdUtente { get; set; }
        public Brand IdBrand { get; set; }
    }
}
