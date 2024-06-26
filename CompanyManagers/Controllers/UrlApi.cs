using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Controllers
{
    internal class UrlApi
    {
        public const string LinkTimviec = "https://api.timviec365.vn";
        #region Login
        public const string Url_Api_Login = "https://api.timviec365.vn/api/";
        public const string Name_Api_Login = "qlc/employee/login";
        #endregion
        #region Proposing
        public const string Url_Api_Proposing = "https://api.timviec365.vn/api/" /*"http://210.245.108.202:3005/api/"*/;
        public const string Name_Api_ShowHomeProposing = "vanthu/catedx/showHome";
        public const string Name_Api_CategoryProposing = "vanthu/catedx/searchcate";
        public const string Name_Api_ProposingSendToAll = "vanthu/DeXuat/user_send_deXuat_All";
        public const string Name_Api_ProposingSendToMe = "vanthu/DeXuat/deXuat_send_user";
        public const string Name_Api_ProposingFollow = "vanthu/DeXuat/deXuat_follow";
        public const string Name_Api_CreateProposingOnLeave = "vanthu/dexuat/De_Xuat_Xin_Nghi";
        public const string Name_Api_GetSettingPropose = "vanthu/dexuat/settingPropose";
        public const string Name_Api_GetSettingComfirm = "vanthu/dexuat/settingConfirm";
        public const string Name_Api_ListComfrimFollow = "vanthu/dexuat/showadd";
        #endregion
        #region Staff
        public const string Url_Api_Staff = "https://api.timviec365.vn/api/";
        public const string Name_Api_Staff = "qlc/managerUser/listAll";
        #endregion
        #region Shift
        public const string Url_Api_Shift  = "https://api.timviec365.vn/api/";
        public const string Name_Api_ListShiftAll = "qlc/shift/list";
        public const string Name_Api_ListShiftForDay = "vanthu/dexuat/empShiftInDay";
        #endregion
    }
}
