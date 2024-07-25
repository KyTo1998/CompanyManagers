using System.Collections.Generic;

namespace CompanyManagers.Models.ModelsAll
{
    public class Account_InforStaff
    {
        public int birthday { get; set; }
        public int gender { get; set; }
        public int married { get; set; }
        public int experience { get; set; }
        public int education { get; set; }
        public string _id { get; set; }
    }
    public class CompanyName_InforStaff
    {
        public int _id { get; set; }
        public string userName { get; set; }
    }
    public class Data_InforStaff
    {
        public bool result { get; set; }
        public string message { get; set; }
        public Data_InforStaff data { get; set; }
        public int _id { get; set; }
        public InForPerson inForPerson { get; set; }
        public string userName { get; set; }
        public int dep_id { get; set; }
        public int com_id { get; set; }
        public int position_id { get; set; }
        public int start_working_time { get; set; }
        public int idQLC { get; set; }
        public string phoneTK { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string avatarUser { get; set; }
        public int authentic { get; set; }
        public int birthday { get; set; }
        public int gender { get; set; }
        public int married { get; set; }
        public int experience { get; set; }
        public int education { get; set; }
        public object emailContact { get; set; }
        public string nameDeparment { get; set; }
        public string positionName { get; set; }
        public List<ListOrganizeDetailId_InforStaff> listOrganizeDetailId { get; set; }
        public CompanyName_InforStaff companyName { get; set; }
    }

    public class Employee_InforStaff
    {
        public int com_id { get; set; }
        public int organizeDetailId { get; set; }
        public int dep_id { get; set; }
        public int start_working_time { get; set; }
        public int position_id { get; set; }
        public int team_id { get; set; }
        public int group_id { get; set; }
        public int time_quit_job { get; set; }
        public object ep_description { get; set; }
        public object ep_featured_recognition { get; set; }
        public string ep_status { get; set; }
        public int ep_signature { get; set; }
        public int allow_update_face { get; set; }
        public int version_in_use { get; set; }
        public int role_id { get; set; }
        public string _id { get; set; }
        public List<ListOrganizeDetailId_InforStaff> listOrganizeDetailId { get; set; }
    }

    public class InForPerson
    {
        public int scan { get; set; }
        public Account_InforStaff account { get; set; }
        public Employee_InforStaff employee { get; set; }
        public string _id { get; set; }
    }

    public class ListOrganizeDetailId_InforStaff
    {
        public int level { get; set; }
        public int organizeDetailId { get; set; }
    }

    public class Root_InforStaff
    {
        public Data_InforStaff data { get; set; }
        public object error { get; set; }
    }


}
