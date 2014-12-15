using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ProviderInterface;

namespace Provider.DataSourceProvider.DataSourceItems
{
    class AnotherHardCodedDataSourceItem : IDataSourceItem
    {
        public string Name { get { return "Hard Coded ds2"; } }
        XmlDocument IDataSourceItem.GetData()
        {
            XmlDocument hardCodedDataSource = new XmlDocument();
            hardCodedDataSource.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><report>" +
                                        "<label1>text1</label1>" +
                                        "<label2>text2</label2>" +
                                        "<label3>text3</label3>" +
                                        "<label4>text4</label4>" +
                                        "</report>");
            return hardCodedDataSource;
        }

    }
}
