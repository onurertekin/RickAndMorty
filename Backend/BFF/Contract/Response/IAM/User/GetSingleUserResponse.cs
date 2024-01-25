﻿namespace Contract.Response.IAM.User
{
    public class GetSingleUsersResponse
    {
        public int id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
    }
}
