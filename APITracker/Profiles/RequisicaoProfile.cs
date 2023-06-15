using AutoMapper;
using APITracker.Data.DTO;
using APITracker.Models;

namespace APITracker.Profiles;

public class RequisicaoProfile : Profile
{
    public RequisicaoProfile()
    {
       CreateMap<Requisicao,RequisicaoDTO>();    
    }
}