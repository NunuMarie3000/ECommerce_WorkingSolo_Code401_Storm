using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace ECommerceWorkingSolo.Migrations
{
  /// <inheritdoc />
  public partial class AddEditorAccount: Migration
  {
    const string EDITOR_USER_GUID = "5122a630-b74f-4c7e-b7dd-971a345bfcc6";
    const string EDITOR_ROLE_GUID = "6faeb7a3-4a48-4c5a-a72f-1def9c024bab";

    /// <inheritdoc />
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      var hasher = new PasswordHasher<ApplicationUser>();

      var passwordHash = hasher.HashPassword(null, "EditorPassword100!"); // whatever password you choose, this is what you'll use to login as an admin
      // this needs to be hidden

      StringBuilder sb = new StringBuilder();

      sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp,LastName,ZipCode,Address1,Address2,FirstName)");
      sb.AppendLine("VALUES(");
      sb.AppendLine($"'{EDITOR_USER_GUID}'"); //id
      sb.AppendLine(",'editor@retrogaming.com'"); // UserName
      sb.AppendLine(",'EDITOR@RETROGAMING.COM'"); // NormalizedUserName
      sb.AppendLine(",'editor@retrogaming.com'"); //Email, NormalizedEmail
      sb.AppendLine(", 0"); // EmailConfirmed
      sb.AppendLine(", 0"); // PhoneNumberConfirmed
      sb.AppendLine(", 0"); // TwoFactorEnabled
      sb.AppendLine(", 0"); // LockoutEnabled
      sb.AppendLine(", 0"); // AccessFailedCount
      sb.AppendLine(", 'EDITOR@RETROGAMING.COM'"); // NormalizedEmail
      sb.AppendLine($", '{passwordHash}'"); // PasswordHash
      sb.AppendLine(", 0"); // SecurityStamp
      sb.AppendLine(", 0"); // LastName
      sb.AppendLine(", 0"); // ZipCode
      sb.AppendLine(", 0"); // Address1
      sb.AppendLine(", ''"); // Address2
      sb.AppendLine(",'Editor'"); // FirstName
      sb.AppendLine(")");

      migrationBuilder.Sql(sb.ToString());

      migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{EDITOR_ROLE_GUID}','Editor','EDITOR')");

      migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{EDITOR_USER_GUID}','{EDITOR_ROLE_GUID}')");
    }

    /// <inheritdoc />
    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{EDITOR_USER_GUID}' AND RoleId = '{EDITOR_ROLE_GUID}'");

      migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{EDITOR_USER_GUID}'");

      migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{EDITOR_ROLE_GUID}'");
    }
  }
}
