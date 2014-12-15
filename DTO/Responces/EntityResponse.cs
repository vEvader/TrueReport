using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responces
{
    public class EntityResponse<TEntity> : BaseResponse where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}
