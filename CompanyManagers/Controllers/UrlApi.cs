﻿
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
        public const string Name_Api_CreateProposeCalendarWork = "vanthu/dexuat/De_Xuat_Lich_Lam_Viec";
        public const string Name_Api_CreateProposeCongCong = "vanthu/dexuat/addDXC";
        public const string Name_Api_ShowDetailPropose = "vanthu/catedx/showCTDX";
        public const string Name_Api_DeletePropose = "vanthu/deletedx/delete_dx";
        public const string Name_Api_RefuseComfirm = "vanthu/editdx/edit_active";
        public const string Name_Api_EditProposeCalendarWork = "vanthu/dexuat/edit_llv";
        public const string Name_Api_EditProposeOnLeave = "vanthu/dexuat/edit_nghi_phep";
        #endregion
        #region Staff
        public const string Url_Api_Staff = "https://api.timviec365.vn/api/";
        public const string Name_Api_Staff = "qlc/managerUser/listAll";
        public const string Name_Api_SearchInforStaff = "qlc/employee/info";
        #endregion
        #region Shift
        public const string Url_Api_Shift  = "https://api.timviec365.vn/api/";
        public const string Name_Api_ListShiftAll = "qlc/shift/list";
        public const string Name_Api_ListShiftForDay = "vanthu/dexuat/empShiftInDay";
        #endregion
        #region RoseRevenue
        public const string Url_Api_Rose = "https://api.timviec365.vn/api/";
        public const string Name_Api_ListLeverlRevernue = "vanthu/dexuat/showMucDoanhThu";
        public const string Name_Api_AddProposingRose = "vanthu/dexuat/addDXHH";
        #endregion
    }
}
