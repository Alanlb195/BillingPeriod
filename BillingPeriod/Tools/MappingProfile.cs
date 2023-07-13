using AutoMapper;
using BillingPeriod.Models;

namespace BillingPeriod.Tools
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActivityViewModel, Activity>();
        }
    }
}
