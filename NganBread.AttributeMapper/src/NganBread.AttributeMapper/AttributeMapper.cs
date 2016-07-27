using NganBread.AttributeMapper.Core;
using NganBread.AttributeMapper.Core.Contracts;
using NganBread.AttributeMapper.TypeMaps;
using NganBread.AttributeMapper.TypeMaps.Contracts;
using NganBread.AttributeMapper.TypeMaps.Maps;

namespace NganBread.AttributeMapper
{
    public static class AttributeMapper
    {
        private static readonly IMapper Mapper;
        private static readonly ITypeMapContainer _container;

        static AttributeMapper()
        {
            var typeMapAdapterFactory = new TypeMapAdapterFactory();
            _container = new TypeMapContainer(typeMapAdapterFactory);
            var memberInfoVerifier = new MemberInfoVerifier();
            var memberInfoExtractor = new MemberInfoExtractor(memberInfoVerifier);
            var typeConverter = new TypeConverter(_container);

            Mapper = new Mapper(typeConverter, memberInfoExtractor);

            _container.RegisterMap(new EnumerableFlexibleTypeMap(Mapper));
        }

        public static TTo Map<TFrom, TTo>(TFrom source)
        {
            return Mapper.Map<TFrom, TTo>(source);
        }

        public static void RegisterMap<TFlexibleTypeMap>(TFlexibleTypeMap map)
            where TFlexibleTypeMap : IFlexibleTypeMap
        {
            _container.RegisterMap(map);
        }

        public static void RegisterMap<TTypeMap, TFrom, TTo>()
            where TTypeMap : ITypeMap<TFrom, TTo>
        {
            _container.RegisterMap<TTypeMap, TFrom, TTo>();
        }
    }
}
