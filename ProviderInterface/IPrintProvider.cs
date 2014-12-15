using System;
using System.Collections.Generic;
using System.Xml;
using Common.Entities;

namespace ProviderInterface
{
    public interface IPrintProvider
    {
        byte[] PrintReport(List<ElementDto> template, XmlDocument dataSource);
    }
}
