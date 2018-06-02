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
            _MapperConfig = new MapperConfiguration(mapper =>
            {
                LoadCloudInstanceMapper(mapper);
                LoadWorkerConfigurationMapper(mapper);
            });
        }

        private void LoadWorkerConfigurationMapper(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<InteractDomain.WorkerConfiguration, WorkerConfiguration>()
                .ForMember(target => target.Id, m => m.MapFrom(source => source.Id))
                .ForMember(target => target.WorkerTypeId, m => m.MapFrom(source => source.WorkerTypeId))
                .ForMember(target => target.Name, m => m.MapFrom(source => source.Name))
                .ForMember(target => target.Json, m => m.MapFrom(source => source.Json))
                .ForMember(target => target.Id, m => m.MapFrom(source => source.Id));
        }

        private void LoadCloudInstanceMapper(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<InteractDomain.CloudInstance, CloudInstance>()
                   .ForMember(target => target.Id, m => m.MapFrom(source => source.Id))
                   .ForMember(target => target.Threadgroup, m => m.MapFrom(source => source.Threadgroup))
                   .ForMember(target => target.ConsumerStatusId, m => m.MapFrom(source => source.ConsumerStatusId))
                   .ForMember(target => target.WorkerConfigurationId, m => m.MapFrom(source => source.WorkerConfigurationId))
                   .ForMember(target => target.ConsumerStatus, m => m.MapFrom(source => (ConsumerStatus)source.ConsumerStatusId));    
        }
    }
}
