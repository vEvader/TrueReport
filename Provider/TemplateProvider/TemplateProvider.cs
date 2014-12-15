using System.Collections.Generic;
using Common.Entities;
using ProviderInterface;
using RepositoryInterface;

namespace Provider.TemplateProvider
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly ITrRepository TrRepository;

        public TemplateProvider(ITrRepository trRepository)
        {
            TrRepository = trRepository;
        }

        public List<ElementDto> LoadTemplate()
        {
            return TrRepository.GetData();
        }

        public void SaveTemplate(List<ElementDto> data)
        {
            TrRepository.SetData(data);
        }

        public List<ElementDto> LoadDemoTemplate()
        {
            var result = new List<ElementDto>
            {
                new ElementDto { Id = 1, Name = "reportTitle", Type = ElementType.Label, X = 387, Y = 34, Height = 34, Width = 117, BindValue = "reportTitle" },
                new ElementDto { Id = 2, Name = "nameLabel", Type = ElementType.Label, X = 325, Y = 86, Height = 20, Width = 116, BindValue = "nameLabel" },
                new ElementDto { Id = 3, Name = "nameEdit", Type = ElementType.Edit, X = 463, Y = 86, Height = 23, Width = 107, BindValue = "nameValue"},
                new ElementDto { Id = 4, Name = "resultLabel", Type = ElementType.Label, X = 327, Y = 117, Height = 21, Width = 111, BindValue = "resultLabel" },
                new ElementDto { Id = 4, Name = "resultValue", Type = ElementType.Edit, X = 463, Y = 117, Height = 21, Width = 111, BindValue = "resultValue" },
                new ElementDto { Id = 5, Name = "resultDescription", Type = ElementType.Edit, X = 330, Y = 150, Height = 50, Width = 244, BindValue = "description" },
            };
            return result;
        }
    }
}
