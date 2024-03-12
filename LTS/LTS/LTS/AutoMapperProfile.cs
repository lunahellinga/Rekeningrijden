using AutoMapper;
using LTS.DTOs;
using LTS.Models;

namespace LTS;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<NodeDTO, Node>();
        CreateMap<Node, NodeDTO>();

        CreateMap<WayDTO, Way>();
        CreateMap<Way, WayDTO>();
    }


}