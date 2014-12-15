using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responces
{
    public class EntityListResponse<TEntity> : BaseResponse
    {
        public List<TEntity> Entities { get; set; } 
    }
}
