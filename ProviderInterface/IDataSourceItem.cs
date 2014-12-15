using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProviderInterface
{
    public interface IDataSourceItem
    {
        string Name { get;}
        XmlDocument GetData();
    }
}
