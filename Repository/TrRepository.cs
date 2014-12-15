using System.Collections.Generic;
using Common.Entities;
using Common.Exceptions;
using RepositoryInterface;

namespace Repository
{
    public class TrRepository : ITrRepository
    {
        public List<ElementDto> DataStorage { get; set; }
        public void SetData(List<ElementDto> initData)
        {
            DataStorage = initData;
        }


        public List<ElementDto> GetData()
        {
            if (DataStorage == null)
                throw new TemplateNotFoundException("Template not found");

            return DataStorage;
        }
    }
}
