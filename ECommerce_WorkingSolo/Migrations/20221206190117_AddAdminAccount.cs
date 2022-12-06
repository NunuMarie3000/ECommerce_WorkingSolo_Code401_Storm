using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace ECommerceWorkingSolo.Migrations
{
  /// <inheritdoc />
  public partial class AddAdminAccount: Migration
  {
    const string ADMIN_USER_GUID = "66e4db09-545c-41d4-9261-85dad6261791";
    const string ADMIN_ROLE_GUID = "021297ac-06d8-45e5-a69c-167ce36ef91a";


    /// <inheritdoc />
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      var hasher = new PasswordHasher<ApplicationUser>();

      var passwordHash = hasher.HashPassword(null, "Password100!"); // whatever password you choose, this is what you'll use to login as an admin
      // this needs to be hidden

      StringBuilder sb = new StringBuilder();

      sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp,LastName,ZipCode,Address1,Address2,FirstName)");
      sb.AppendLine("VALUES(");
      sb.AppendLine($"'{ADMIN_USER_GUID}'"); //id
      sb.AppendLine(",'admin@retrogaming.com'"); // UserName
      sb.AppendLine(",'ADMIN@RETROGAMING.COM'"); // NormalizedUserName
      sb.AppendLine(",'admin@retrogaming.com'"); //Email, NormalizedEmail
      sb.AppendLine(", 0"); // EmailConfirmed
      sb.AppendLine(", 0"); // PhoneNumberConfirmed
      sb.AppendLine(", 0"); // TwoFactorEnabled
      sb.AppendLine(", 0"); // LockoutEnabled
      sb.AppendLine(", 0"); // AccessFailedCount
      sb.AppendLine(", 'ADMIN@RETROGAMING.COM'"); // NormalizedEmail
      sb.AppendLine($", '{passwordHash}'"); // PasswordHash
      sb.AppendLine(", 0"); // SecurityStamp
      sb.AppendLine(", 'OBryant'"); // LastName
      sb.AppendLine(", 0"); // ZipCode
      sb.AppendLine(", 0"); // Address1
      sb.AppendLine(", ''"); // Address2
      sb.AppendLine(",'Admin'"); // FirstName
      sb.AppendLine(")");

      migrationBuilder.Sql(sb.ToString());

      migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}','Admin','ADMIN')");

      migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");


    }

    /// <inheritdoc />
    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId = '{ADMIN_ROLE_GUID}'");

      migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");

      migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");

    }
  }
}
