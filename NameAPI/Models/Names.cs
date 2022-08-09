using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace NameBandit.Models 
{
    public class Name : NameBaseModel
    {  
        [Key]
        public int Id {get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool Active { get; set; }
        public bool Prefix { get; set; }
        public bool Suffix { get; set; }
        public VibrationNumber Vibration { get; set; }
#nullable enable
        public NameCombo? NameCombo { get; set; }
#nullable disable
    }

    public class NameBaseModel
    {  
        public string Text { get; set; }
        public string Description { get; set; }
    }

    public class NameViewModel: NameBaseModel 
    {
        public string Gender { get; set; }
        public VibrationNumberViewModel VibrationNumber { get; set; }
    }

    public class NameProfile: Profile {
        public NameProfile()
        {            
            CreateMap<Name, NameViewModel>()
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Male && src.Female ? "Both" : src.Male ? "Male" : "Female"))
                .ForMember(dest => dest.VibrationNumber, opts => opts.MapFrom(src => src.Vibration));

            CreateMap<NameViewModel, Name>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Active, opts => opts.Ignore())
                .ForMember(dest => dest.Male, opts => opts.Ignore())
                .ForMember(dest => dest.Female, opts => opts.Ignore())
                .ForMember(dest => dest.Prefix, opts => opts.Ignore())
                .ForMember(dest => dest.Suffix, opts => opts.Ignore())
                .ForMember(dest => dest.Vibration, opts => opts.Ignore())
                .ForMember(dest => dest.NameCombo, opts => opts.Ignore());

        }
    }  
}
