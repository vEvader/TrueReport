using System.Collections.Generic;
using Common.Entities;

namespace RepositoryInterface
{
    public interface ITrRepository
    {
        void SetData(List<ElementDto> initData);
        List<ElementDto> GetData();
    }
}
