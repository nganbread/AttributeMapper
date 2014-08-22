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
            var container = new TypeMapContainer(new TypeMapAdapterFactory());
            Mapper = new Mapper(new TypeConverter(container), new MemberInfoExtractor(new MemberInfoVerifier()));

            container.RegisterMap(new EnumerableFlexibleTypeMap(Mapper));
        }

        public static TTo Map<TFrom, TTo>(TFrom source)
        {
            return Mapper.Map<TFrom, TTo>(source);
        }
    }
}
