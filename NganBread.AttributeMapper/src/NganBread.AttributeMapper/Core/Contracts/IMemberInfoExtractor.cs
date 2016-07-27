using System.Collections.Generic;

namespace NganBread.AttributeMapper.Core.Contracts
{
    public interface IMemberInfoExtractor
    {
        IList<IMemberInfoProxy> ExtractFromMembers<TContext, TFrom>();
        IList<IMemberInfoProxy> ExtractToMembers<TContext, TTo>();
    }
}