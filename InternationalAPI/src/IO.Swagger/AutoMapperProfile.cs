using AutoMapper;
using IO.Swagger.Models;
using LTS.DTOs;

namespace IO.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<NodeDTO, Node>();
            CreateMap<Node, NodeDTO>();

            CreateMap<WayDTO, Way>();
            CreateMap<Way, WayDTO>();
        }


    }
}
