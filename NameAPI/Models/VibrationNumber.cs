using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace NameBandit.Models 
{
    public class VibrationNumber : VibrationNumberBaseModel
    {  
        [Key]
        public int Id {get; set; }
    }

    public class VibrationNumberBaseModel
    {  
        public int Vibration { get; set; }
        public int Destiny { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class VibrationNumberViewModel: VibrationNumberBaseModel {}

    public class VibrationNumberProfile: Profile {
        public VibrationNumberProfile()
        {            
            CreateMap<VibrationNumber, VibrationNumberViewModel>();

            CreateMap<VibrationNumberViewModel, VibrationNumber>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
        }
    } 
}