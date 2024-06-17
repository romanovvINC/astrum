using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Calendar.ViewModels;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;

namespace Astrum.Calendar.Application.Mappings
{
    public class CalendarProfile : Profile
    {
        public CalendarProfile()
        {
            CreateMap<CalendarForm, Domain.Aggregates.Calendar>().ReverseMap();
            CreateMap<EventForm, Domain.Aggregates.Event>()
                .ReverseMap()
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => DateTime.Parse($"{src.Start:yyyy-MM-ddTHH:mm:ss.FFFZ}").ToUniversalTime()))
                .ForMember(dest => dest.End, opts => opts.MapFrom(src => DateTime.Parse($"{src.End:yyyy-MM-ddTHH:mm:ss.FFFZ}").ToUniversalTime()));

            CreateMap<CalendarListEntry, Domain.Aggregates.Calendar>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.GoogleId, opts => opts.MapFrom(src => src.Id));
            CreateMap<Event, Domain.Aggregates.Event>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.CalendarId, opts => opts.Ignore())
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => src.Start.DateTime ?? DateTime.Parse(src.Start.Date)))
                .ForMember(dest => dest.End, opts => opts.MapFrom(src => src.End.DateTime ?? DateTime.Parse(src.End.Date)))
                .ForMember(dest => dest.GoogleId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Yearly, opts => opts.MapFrom(src => src.Recurrence.Count > 0));
        }
    }
}
