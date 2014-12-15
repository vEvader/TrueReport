using System.Collections.Generic;
using System.Xml;
using Common.Entities;
using Common.Responces;

namespace BusinessServiceInterface
{
    public interface ITrService
    {
        #region DataSource Methods
        EntityResponse<XmlDocument> SimpleGetDataSource(string dataSourceName);
        EntityListResponse<string> GetDataSourceList();

        #endregion DataSource Methods

        #region Template Methods
        BaseResponse SaveTemplate(List<ElementDto> template);
        EntityListResponse<ElementDto> LoadTemplate();

        EntityListResponse<ElementDto> LoadDemoTemplate();
        #endregion Template Methods

        #region Print Methods
        EntityResponse<byte[]> GetReport(List<ElementDto> template, string dataSourceName);

        #endregion Print Methods
    }
}
