using AutoMapper;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TestsXUnit
{
    public static class AutoMapperConfig
    {
        private static object _thisLock = new object();
        private static bool _initialized = false;

        private static IMapper _mapper;
        // Centralize automapper initialize
        public static void Initialize()
        {
            lock (_thisLock)
            {
                if (!_initialized)
                {
                    var config = new MapperConfiguration(opts =>
                    {
                        opts.CreateMap<User, AddUserViewModel>();
                        opts.CreateMap<User, EditUserViewModel>();
                        opts.CreateMap<User, EditPasswordUserViewModel>();
                    });
                    _initialized = true;
                    _mapper = config.CreateMapper();
                }
            }
        }

        public static IMapper GetMapper()
        {
            return _mapper;
        }
    }
}