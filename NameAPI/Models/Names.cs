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
        public VibrationNumber Vibration { get; set; }
#nullable enable
        public NameCombo? NameCombo { get; set; }
#nullable disable
    }

    public class NameBaseModel
    {  
        public string Text { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool Active { get; set; }
        public bool Prefix { get; set; }
        public bool Suffix { get; set; }
        public string Description { get; set; }
    }

    public class NameViewModel: NameBaseModel 
    {
        public int? VibrationNumber { get; set; }
    }

    public class NameProfile: Profile {
        public NameProfile()
        {            
            CreateMap<Name, NameViewModel>()
                .ForMember(dest => dest.VibrationNumber, opts => opts.MapFrom(src => src.Vibration.Vibration));

            CreateMap<NameViewModel, Name>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Vibration, opts => opts.Ignore())
                .ForMember(dest => dest.NameCombo, opts => opts.Ignore());

        }
    }  
}
