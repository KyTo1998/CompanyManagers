using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Controllers
{
    internal class UrlApi
    {
        #region Login
        public const string apiLogin = "https://api.timviec365.vn/api/qlc/employee/login";
        #endregion
        #region Proposing
        public const string apiShowHomeProposing = "http://210.245.108.202:3005/api/vanthu/catedx/showHome";
         public const string apiCategoryProposing = "https://api.timviec365.vn/api/vanthu/catedx/searchcate";
        #endregion
    }
}
