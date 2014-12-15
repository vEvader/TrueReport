using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Common.Exceptions;
using Provider.DataSourceProvider.DataSourceItems;
using ProviderInterface;

namespace Provider.DataSourceProvider
{
    public class DataSourceProvider : IDataSourceProvider
    {
        private List<IDataSourceItem> DataSources;

        public DataSourceProvider()
        {
            DataSources = new List<IDataSourceItem>();
            DataSources.Add(new HardCodedDataSourceItem());
            DataSources.Add(new AnotherHardCodedDataSourceItem());
        }

        public List<string> GetDataSourceList()
        {
            List<string> result = new List<string>();

            foreach (IDataSourceItem dataSourceItem in DataSources)
            {
                result.Add(dataSourceItem.Name);
            }

            return result;
        }

        public XmlDocument SimpleGetDataSource(string dataSourceName)
        {
            IDataSourceItem result = DataSources.FirstOrDefault(ds => ds.Name == dataSourceName);
            
            if (result == null)
                throw new DataSourceItemNotFoundException(string.Format("Data Source with name {0} not found", dataSourceName));

            return result.GetData();
        }
    }
}
