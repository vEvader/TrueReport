using System;
using System.Collections.Generic;
using Common.Entities;

namespace ProviderInterface
{
    public interface ITemplateProvider
    {

        List<ElementDto> LoadTemplate();

        void SaveTemplate(List<ElementDto> data);

        List<ElementDto> LoadDemoTemplate();
    }
}
