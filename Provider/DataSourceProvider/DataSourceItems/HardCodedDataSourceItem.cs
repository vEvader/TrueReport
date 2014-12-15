using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ProviderInterface;

namespace Provider.DataSourceProvider.DataSourceItems
{
    public class HardCodedDataSourceItem : IDataSourceItem
    {
        public string Name { get { return "Hard Coded ds1"; } }
        XmlDocument IDataSourceItem.GetData()
        {
            XmlDocument hardCodedDataSource = new XmlDocument();
            hardCodedDataSource.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><report>" +
                                        "<reportTitle>Test Results</reportTitle>" +
                                        "<nameLabel>Name</nameLabel>" +
                                        "<nameValue>Candidate X</nameValue>" +
                                        "<resultLabel>Result</resultLabel>" +
                                        "<resultValue>Pass</resultValue>" +
                                        "<description>This is long description of test results. Test has been passed so every thing is ok!</description></report>");
            return hardCodedDataSource;
        }

    }
}
