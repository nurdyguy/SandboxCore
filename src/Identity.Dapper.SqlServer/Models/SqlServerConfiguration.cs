using SandboxCore.Identity.Dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandboxCore.Identity.Dapper.SqlServer.Models
{
    public class SqlServerConfiguration : SqlConfiguration
    {
        public SqlServerConfiguration()
        {
            ParameterNotation = "@";
            SchemaName = "[dbo]";
            UseQuotationMarks = true;

            InsertRoleQuery =           "INSERT INTO %SCHEMA%.%TABLENAME% %COLUMNS% VALUES(%VALUES%)";
            DeleteRoleQuery =           "DELETE FROM %SCHEMA%.%TABLENAME% WHERE Id = %ID%";
            UpdateRoleQuery =           "UPDATE %SCHEMA%.%TABLENAME% %SETVALUES% WHERE Id = %ID%";
            SelectRoleByNameQuery =     "SELECT * FROM %SCHEMA%.%TABLENAME% WHERE Name = %NAME%";
            SelectRoleByIdQuery =       "SELECT * FROM %SCHEMA%.%TABLENAME% WHERE Id = %ID%";
            InsertUserQuery =           "INSERT INTO %SCHEMA%.%TABLENAME% %COLUMNS% OUTPUT INSERTED.Id VALUES(%VALUES%)";
            DeleteUserQuery =           "DELETE FROM %SCHEMA%.%TABLENAME% WHERE Id = %ID%";
            UpdateUserQuery =           "UPDATE %SCHEMA%.%TABLENAME% %SETVALUES% WHERE Id = %ID%";
            SelectUserByUserNameQuery = "SELECT * FROM %SCHEMA%.%TABLENAME% WHERE UserName = %USERNAME%";
            SelectUserByEmailQuery =    "SELECT * FROM %SCHEMA%.%TABLENAME% WHERE Email = %EMAIL%";
            SelectUserByIdQuery =       "SELECT * FROM %SCHEMA%.%TABLENAME% WHERE Id = %ID%";
            InsertUserClaimQuery =      "INSERT INTO %SCHEMA%.%TABLENAME% %COLUMNS% VALUES(%VALUES%)";
            InsertUserLoginQuery =      "INSERT INTO %SCHEMA%.%TABLENAME% %COLUMNS% VALUES(%VALUES%)";
            InsertUserRoleQuery =       "INSERT INTO %SCHEMA%.%TABLENAME% %COLUMNS% VALUES(%VALUES%)";

            GetUserLoginByLoginProviderAndProviderKeyQuery = 
                                        @"SELECT TOP 1 %USERFILTER% 
                                        FROM %SCHEMA%.%USERTABLE% t1
                                            INNER JOIN %SCHEMA%.%USERLOGINTABLE% t2 on t2.UserId = t1.Id
                                        WHERE 
                                            LoginProvider = %LOGINPROVIDER% AND ProviderKey = %PROVIDERKEY%";

            GetClaimsByUserIdQuery =    "SELECT ClaimType, ClaimValue FROM %SCHEMA%.%TABLENAME% WHERE UserId = %ID%";

            GetRolesByUserIdQuery =     @"SELECT Name 
                                        FROM %SCHEMA%.%ROLETABLE% t1
                                            INNER JOIN %SCHEMA%.%USERROLETABLE% t2 on t2.RoleId = t1.Id
                                        WHERE 
                                            t2.UserId = %ID%";

            GetUserLoginInfoByIdQuery = @"SELECT LoginProvider, ProviderKey Name 
                                        FROM %SCHEMA%.%TABLENAME% 
                                        WHERE 
                                            UserId = %ID%";

            GetUsersByClaimQuery =      @"SELECT t1.*
                                        FROM %SCHEMA%.%USERTABLE% t1
                                            INNER JOIN %SCHEMA%.%USERCLAIMTABLE% t2 on t2.UserId = t1.Id
                                        WHERE 
                                            ClaimValue = %CLAIMVALUE% AND ClaimType = %CLAIMTYPE%";

            GetUsersInRoleQuery =       @"SELECT t1.* 
                                        FROM %SCHEMA%.%USERTABLE% t1
                                            INNER JOIN %SCHEMA%.%USERROLETABLE% t2 on t2.UserId = t1.Id
                                            INNER JOIN %SCHEMA%.%ROLETABLE% t3 on t3.Id = t2.RoleId
                                        WHERE 
                                            t3.Name = %ROLENAME%";

            IsInRoleQuery =             @"SELECT 1 
                                        FROM  %SCHEMA%.%USERTABLE% t1
                                            INNER JOIN %SCHEMA%.%USERROLETABLE% t2 on t2.UserId = t1.Id
                                            INNER JOIN %SCHEMA%.%ROLETABLE% t3 on t3.Id = t2.RoleId
                                        WHERE 
                                            t3.Name = %ROLENAME% 
                                            AND t1.Id = %USERID%";

            RemoveClaimsQuery =         @"DELETE FROM %SCHEMA%.%TABLENAME% 
                                        WHERE 
                                            UserId = %ID% 
                                            AND ClaimType = %CLAIMTYPE% 
                                            AND ClaimValue = %CLAIMVALUE%";

            RemoveUserFromRoleQuery =   @"DELETE t1                                             
                                            FROM %SCHEMA%.%USERROLETABLE% t1
                                            INNER JOIN %SCHEMA%.%ROLETABLE% t2 on t2.Id = t1.RoleId                                          
                                        WHERE 
                                            t1.UserId = %USERID% 
                                            AND t2.Name = %ROLENAME%";

            RemoveLoginForUserQuery =   @"DELETE FROM %SCHEMA%.%TABLENAME% 
                                        WHERE 
                                            UserId = %USERID% 
                                            AND LoginProvider = %LOGINPROVIDER% 
                                            AND ProviderKey = %PROVIDERKEY%";

            UpdateClaimForUserQuery =   @"UPDATE %SCHEMA%.%TABLENAME% 
                                        SET 
                                            ClaimType = %NEWCLAIMTYPE% 
                                            AND ClaimValue = %NEWCLAIMVALUE% 
                                        WHERE 
                                            UserId = %USERID% 
                                            AND ClaimType = %CLAIMTYPE% 
                                            AND ClaimValue = %CLAIMVALUE%";
            
            RoleTable = "IdentityRole";
            UserTable = "IdentityUser";
            UserClaimTable = "IdentityUserClaim";
            UserRoleTable = "IdentityUserRole";
            UserLoginTable = "IdentityLogin";
        }
    }
}
