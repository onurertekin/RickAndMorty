namespace Contract.Request.IAM.User
{
    public class UpdateUsersRequest
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
    }
}
