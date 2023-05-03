using AutoMapper;
using Second.DTOs;
using Second.Models;

namespace Second;


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Season, SeasonDto>().ReverseMap();
            CreateMap<Tour, TourDto>().ReverseMap();
            CreateMap<Tourist, TouristDto>().ReverseMap();
            CreateMap<TouristInfo, TouristInfoDto>().ReverseMap();
            CreateMap<Voucher, VoucherDto>().ReverseMap();
        }
    }
