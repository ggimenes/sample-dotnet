using AutoMapper;

namespace SampleDotnet.Financial.Infra.Automapper
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AllowNullCollections = true;

                cfg.AddProfile(new ConsumerMappingProfile());
            });
        }
    }
}
