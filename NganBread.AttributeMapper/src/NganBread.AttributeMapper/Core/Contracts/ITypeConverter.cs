namespace NganBread.AttributeMapper.Core.Contracts
{
    public interface ITypeConverter
    {
        TTo SafeConvert<TFrom, TTo>(TFrom o);
        bool CanConvert<TFrom, TTo>(TFrom o);
    }
}