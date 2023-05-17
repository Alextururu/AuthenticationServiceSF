using AutoMapper;

namespace AuthenticationServiceSF
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>().ConstructUsing(x => new UserViewModel(x));
        }
    }
}
