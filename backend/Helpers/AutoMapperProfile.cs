namespace backend.Helpers;

using AutoMapper;
using backend.Entities;
using backend.Models;

public class AutoMapperProfile : Profile
{
  public AutoMapperProfile()
  {
    // CreateRequest -> User
    CreateMap<UserCreateRequest, User>();

    // UpdateRequest -> User
    CreateMap<UserUpdateRequest, User>()
        .ForAllMembers(x => x.Condition(
            (src, dest, prop) =>
            {
              // ignore both null & empty string properties
              if (prop == null) return false;
              if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

              // ignore null role
              // if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

              return true;
            }
        ));

    CreateMap<JobCreateRequest, Job>();
    CreateMap<JobUpdateRequest, Job>();

    CreateMap<JobRunCreateRequest, JobRun>();
  }
}