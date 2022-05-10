
namespace BusinessLayer.Contracts
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public RoleTypeEnum RoleType { get; set; }
    }

    public enum RoleTypeEnum
    {
        Teacher = 0,
        Student = 1
    }
}
