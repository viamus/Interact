using AutoMapper;
using Interact.Library.Metadata;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Postgresql.Mapper
{
    public class InteractMapper
    {
        private static readonly InteractMapper _Instance = new InteractMapper();

        private MapperConfiguration _MapperConfig;

        public static InteractMapper Instance
        {
            get
            {
                return _Instance;
            }
        }


        private void LoadMapperConfig()
        {
            var config = new MapperConfiguration(mapper =>
            {
                LoadCloudInstanceMapper(mapper);
            });
        }

        private void LoadWorkerConfigurationMapper(IMapperConfigurationExpression mapper)
        {

        }

        private void LoadCloudInstanceMapper(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<InteractDomain.CloudInstance, CloudInstance>()
                   .ForMember(target => target.Id, m => m.MapFrom(source => source.Id))
                   .ForMember(target => target.Threadgroup, m => m.MapFrom(source => source.Threadgroup))
                   .ForMember(target => target.ConsumerStatus, m => m.MapFrom(source => (ConsumerStatus)source.ConsumerStatusId));
                   
        }



    }
}
