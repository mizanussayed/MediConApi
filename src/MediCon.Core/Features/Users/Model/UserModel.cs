using MediCon.Core.Features.Users.Model.ApiResponse;

using Mapster;

namespace MediCon.Core.Features.Users.Model;

public class UserModel
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }

    public class UserModelMapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .NewConfig<UserLoginResponseUser, UserModel>()
                .TwoWays()
                .Map(x => x.EmailAddress, y => y.Email)
                .Map(x => x.UserName, y => y.Username)
                .Map(x => x.Id, y => y.UserId);
        }
    }
}
