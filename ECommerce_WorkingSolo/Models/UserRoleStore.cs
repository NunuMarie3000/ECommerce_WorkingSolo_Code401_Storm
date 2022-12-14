using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_WorkingSolo.Models
{
  public class UserRoleStore: IUserRoleStore<ApplicationUser>
  {
    private readonly ECommerceDbContext _context;
    public UserRoleStore(ECommerceDbContext context)
    {
      _context = context;
    }

    public Task AddToRoleAsync( ApplicationUser user, string roleName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationUser> FindByIdAsync( string userId, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationUser> FindByNameAsync( string normalizedUserName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<string> GetNormalizedUserNameAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<IList<string>> GetRolesAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<string> GetUserIdAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<string> GetUserNameAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<IList<ApplicationUser>> GetUsersInRoleAsync( string roleName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync( ApplicationUser user, string roleName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task RemoveFromRoleAsync( ApplicationUser user, string roleName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync( ApplicationUser user, string normalizedName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task SetUserNameAsync( ApplicationUser user, string userName, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync( ApplicationUser user, CancellationToken cancellationToken )
    {
      throw new NotImplementedException();
    }
  }
}
