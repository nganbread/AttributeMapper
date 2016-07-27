using System.Collections.Generic;

namespace NganBread.AttributeMapper.Core.Contracts
{
    public interface IMemberInfoVerifier
    {
        void Verify<T>(IList<IMemberInfoProxy> properties);
    }
}