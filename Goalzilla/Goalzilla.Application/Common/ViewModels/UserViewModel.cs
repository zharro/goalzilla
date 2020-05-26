using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Goalzilla.Goalzilla.Application.Common.Mapping;
using Goalzilla.Goalzilla.Domain;

namespace Goalzilla.Goalzilla.Application.Common.ViewModels
{
    public class UserViewModel : IMapFrom<User>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserViewModel>()
                .ForMember(d => d.Email, src => src.MapFrom(s => s.Email.Value));
        }
    }
}