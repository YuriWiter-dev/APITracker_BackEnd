using APITracker.Data.DTO;
using APITracker.Entities;
using AutoMapper;

namespace APITracker.Profiles;

public class RequisicaoProfile : Profile
{
    public RequisicaoProfile()
    {
        CreateMap<EnderecoApi, RequisicaoDTO>().ReverseMap(); 
        CreateMap<EnderecoApi, StatusDTO>().ReverseMap();
    }
}