using AutoMapper;
using EducationSystem.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Profiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateModel>()
                .ForMember(p => p.DateOfBirth, src => src.MapFrom(x => x.DateOfBirth.Value.ToString("dd MMM yyyy")))
                .ForMember(p => p.Gender, src => src.MapFrom(x => x.GenderId == 1 ? "Male" : "Female"));
        }
    }
}
