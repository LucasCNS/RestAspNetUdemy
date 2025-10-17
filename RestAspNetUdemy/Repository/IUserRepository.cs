using RestAspNetUdemy.Data.VO;
using RestAspNetUdemy.Model;

namespace RestWithASPNETUdemy.Repository
{
	public interface IUserRepository
	{
		User? ValidateCredentials(UserVO user);

		User? ValidateCredentials(string username);

		bool RevokeToken(string username);

		User? RefreshUserInfo(User? user);
	}
}