using AutoMapper;
using BusinessObject.Models;
using Repository.Contract.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() {
            CreateMap<CreateAccountRequest, BranchAccount>().ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId));


        }
    }
}
