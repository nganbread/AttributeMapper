using System.Collections.Generic;

namespace AttributeMapper.Core.Contracts
{
    public interface IMemberInfoVerifier
    {
        void Verify<T>(IList<IMemberInfoProxy> properties);
    }
}