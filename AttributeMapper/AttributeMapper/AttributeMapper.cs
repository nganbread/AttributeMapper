using AttributeMapper.Core;
using AttributeMapper.Core.Contracts;
using AttributeMapper.TypeMaps;
using AttributeMapper.TypeMaps.Maps;

namespace AttributeMapper
{
    public static class AttributeMapper
    {
        private static readonly IMapper Mapper;
        static AttributeMapper()
        {
            var typeMapAdapterFactory = new TypeMapAdapterFactory();
            var container = new TypeMapContainer(typeMapAdapterFactory);
            var memberInfoVerifier = new MemberInfoVerifier();
            var memberInfoExtractor = new MemberInfoExtractor(memberInfoVerifier);
            var typeConverter = new TypeConverter(container);

            Mapper = new Mapper(typeConverter, memberInfoExtractor);

            container.RegisterMap(new EnumerableFlexibleTypeMap(Mapper));
        }

        public static TTo Map<TFrom, TTo>(TFrom source)
        {
            return Mapper.Map<TFrom, TTo>(source);
        }
    }
}
