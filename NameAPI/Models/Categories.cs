using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace NameBandit.Models 
{
    public class Category : CategoryViewModel
    {  
        public ICollection<Name> Names { get; set; }
    }

    public class CategoryViewModel
    {
        [Key]
        public int Id {get; set; }
        public string Title { get; set; }
    }

    public class CategoryProfile: Profile {
        public CategoryProfile()
        {            
            CreateMap<Category, CategoryViewModel>();

            CreateMap<CategoryViewModel, Category>()
                .ForMember(dest => dest.Names, opts => opts.Ignore());
        }
    }  
    
}  
