
namespace CompanyManagers.Models.Logins
{
    public class ComInfoLogin
    {
        public int com_id { get; set; }
        public object com_email { get; set; }
    }

    public class DataLogin
    {
        public bool result { get; set; }
        public string message { get; set; }
        public DataUserLogin data { get; set; }
    }
    public class DataUserLogin
    {
        public int _id { get; set; }
        public UserInfoLogin user_info { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int type { get; set; }
        public string ep_phone_tk { get; set; }
        public string password { get; set; }
    }
    public class RootLogin
    {
        public DataLogin data { get; set; }
        public object error { get; set; }
    }

    public class UserInfoLogin
    {
        public int ep_id { get; set; }
        public string ep_phone_tk { get; set; }
        public string ep_name { get; set; }
        public string ep_phone { get; set; }
        public string ep_pass { get; set; }
        public int com_id { get; set; }
        public int dep_id { get; set; }
        public string ep_address { get; set; }
        public int ep_birth_day { get; set; }
        public int ep_gender { get; set; }
        public int ep_married { get; set; }
        public int ep_education { get; set; }
        public int ep_exp { get; set; }
        public int ep_authentic { get; set; }
        public int role_id { get; set; }
        public object ep_image { get; set; }
        public int create_time { get; set; }
        public int update_time { get; set; }
        public int start_working_time { get; set; }
        public int position_id { get; set; }
        public int group_id { get; set; }
        public object ep_description { get; set; }
        public object ep_featured_recognition { get; set; }
        public string ep_status { get; set; }
        public int ep_signature { get; set; }
        public int allow_update_face { get; set; }
        public int version_in_use { get; set; }
        public int ep_id_tv365 { get; set; }
        public int scan { get; set; }
        public string com_name { get; set; }

        // Công ty
        public string com_email { get; set; }
        public string com_phone_tk { get; set; }
        public string type_timekeeping { get; set; }
        public string id_way_timekeeping { get; set; }
        public string com_pass { get; set; }
        public string com_address { get; set; }
        public int com_role_id { get; set; }
        public int com_size { get; set; }
        public object com_description { get; set; }
        public int com_create_time { get; set; }
        public int com_update_time { get; set; }
        public int com_authentic { get; set; }
        public string com_lat { get; set; }
        public string com_lng { get; set; }
        public object com_qr_logo { get; set; }
        public int enable_scan_qr { get; set; }
        public int com_vip { get; set; }
        public int com_ep_vip { get; set; }
        public int ep_crm { get; set; }
        public string com_id_tv365 { get; set; }
        public string com_quantity_time { get; set; }
        public string com_kd { get; set; }
        public string check_phone { get; set; }
    }
}
