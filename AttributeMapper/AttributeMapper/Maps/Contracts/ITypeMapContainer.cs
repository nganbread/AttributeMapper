namespace AttributeMapper.Maps.Contracts
{
    public interface ITypeMapContainer
    {
        void RegisterMap<TFlexibleTypeMap>(TFlexibleTypeMap map)
            where TFlexibleTypeMap : IFlexibleTypeMap;

        void RegisterMap<TTypeMap, TFrom, TTo>()
            where TTypeMap: ITypeMap<TFrom, TTo>;

        bool CanResolveMap<TFrom, TTo>();
        ITypeMap<TFrom, TTo> ResolveMap<TFrom, TTo>();
    }
}