using System.Collections.Generic;
using System.Xml;
using BusinessServiceInterface;
using Common.Entities;
using Common.Responces;
using ProviderInterface;

namespace BusinessService
{
    public class TrService : ITrService
    {
        #region Fields

        private readonly ITemplateProvider TemplateProvider;
        private readonly IDataSourceProvider DataSourceProvider;
        private readonly IPrintProvider PrintProvider;

        #endregion Fields

        #region Constructor

        public TrService(ITemplateProvider templateProvider, IDataSourceProvider dataSourceProvider, IPrintProvider printProvider)
        {
            TemplateProvider = templateProvider;
            DataSourceProvider = dataSourceProvider;
            PrintProvider = printProvider;
        }

        #endregion Constructor
        
        #region DataSource

        public EntityResponse<XmlDocument> SimpleGetDataSource(string dataSourceName)
        {
            return ExceptionWrapper.ExceptionWrapper.Execute(() =>
            {
                XmlDocument dataSource = DataSourceProvider.SimpleGetDataSource(dataSourceName);
                return new EntityResponse<XmlDocument>
                {
                    Entity = dataSource,
                };
            });
        }

        public EntityListResponse<string> GetDataSourceList()
        {
            return ExceptionWrapper.ExceptionWrapper.Execute(() =>
            {
                List<string> dataSources = DataSourceProvider.GetDataSourceList();
                return new EntityListResponse<string>
                {
                    Entities = dataSources,
                };
            });
        }

        #endregion DataSource

        #region Templates
        
        public BaseResponse SaveTemplate(List<ElementDto> template )
        {
            return ExceptionWrapper.ExceptionWrapper.Execute(() =>
            {
                TemplateProvider.SaveTemplate(template);
                return new BaseResponse();
            });
        }

        public EntityListResponse<ElementDto> LoadTemplate()
        {
            return ExceptionWrapper.ExceptionWrapper.Execute(() =>
            {
                var template = TemplateProvider.LoadTemplate();
                return new EntityListResponse<ElementDto>
                {
                    Entities = template,
                };
            });
        }

        public EntityListResponse<ElementDto> LoadDemoTemplate()
        {
            return ExceptionWrapper.ExceptionWrapper.Execute(() =>
            {
                var template = TemplateProvider.LoadDemoTemplate();
                return new EntityListResponse<ElementDto>
                {
                    Entities = template,
                };
            });
        }

        #endregion Templates

        #region Print

        public EntityResponse<byte[]> GetReport(List<ElementDto> template, string dataSourceName)
        {
            return ExceptionWrapper.ExceptionWrapper.Execute(() =>
            {
                XmlDocument dataSource = DataSourceProvider.SimpleGetDataSource(dataSourceName);
                var report = PrintProvider.PrintReport(template, dataSource);
                return new EntityResponse<byte[]>
                {
                    Entity = report,
                };
            });
        }

        #endregion Print
    }
}
