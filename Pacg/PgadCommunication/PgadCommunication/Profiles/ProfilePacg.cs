using AutoMapper;
using PgadData.Entity;
using PgadCommunication.Contracts.Requests;

namespace PgadCommunication.Profiles
{
    public class ProfilePacg : Profile
    {
        public ProfilePacg()
        {
            CreateMap<ConcessionarioInfo, HeaderPgad>()
                .ForMember(dest => dest.idCnConto, opt => opt.MapFrom(src => src.TitolareSistema))
                .ForMember(dest => dest.idCnc, opt => opt.MapFrom(src => src.Concessionario))
                .ForMember(dest => dest.idFsc, opt => opt.MapFrom(src => src.FornitoreSevizi))
                .ForMember(dest => dest.idRete, opt => opt.MapFrom(src => src.ReteConcessionario))
                .ForMember(dest => dest.idReteConto, opt => opt.MapFrom(src => src.ReteTitolare));


        }
    }
}
