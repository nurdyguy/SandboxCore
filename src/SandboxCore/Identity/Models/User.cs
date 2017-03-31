using System;
using SandboxCore.Identity.Dapper.Entities;


namespace SandboxCore.Identity.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : DapperIdentityUser<int, UserClaim, UserRole, UserLogin>
    {
        //--added--
        string FirstName { get; set; }
        string LastName { get; set; }
        string Salt { get; set; }
        string TempPassword { get; set; }
        bool IsActive { get; set; }
        DateTime RegisteredOn { get; set; }
        DateTime LastLogOn { get; set; }
        string ThirdPartyGuid { get; set; }
    }

    
}


/*






-----------IdentityUser

 int 							AccessFailedCount 			{ get; set; }
 ICollection<TUserClaim>		Claims 						{ get; }
 string 						ConcurrencyStamp 			{ get; set; }
 string 						Email 						{ get; set; }
 bool 							EmailConfirmed 				{ get; set; }
 TKey 							Id 							{ get; set; }
 bool 							LockoutEnabled 				{ get; set; }
 DateTimeOffset? 				LockoutEnd 					{ get; set; }
 ICollection<TUserLogin> 		Logins 						{ get; }
 string 						PasswordHash 				{ get; set; }
 string 						PhoneNumber 				{ get; set; }
 bool 							PhoneNumberConfirmed 		{ get; set; }
 ICollection<TUserRole> 		Roles 						{ get; }
 string 						SecurityStamp 				{ get; set; }
 bool 							TwoFactorEnabled 			{ get; set; }
 string 						UserName 					{ get; set; }
 
 --added--
 string 						FirstName					{ get; set; }
 string							LastName					{ get; set; }
 string							Salt						{ get; set; }
 string							TempPassword				{ get; set; }
 bool							IsActive					{ get; set; }
 DateTime						RegisteredOn				{ get; set; }
 DateTime						LastLogOn					{ get; set; }
 string							ThirdPartyGuid				{ get; set; }	

create table Users(
	Id 							bigint identity(1,1) primary key not null,
	Email 						varchar(50) not null,
	UserName					varchar(50) not null,
	FirstName					varchar(50) null,
	LastName					varchar(50) null,
	Salt						varchar(50) not null default 'x',
	PasswordHash				varchar(max) not null,
	TempPassword				varchar(20) null,	
	IsActive					bit not null default 1,
	RegisteredOn				datetime null,
	LastLogOn					datetime null,
	EmailConfirmed 				bit not null default 0,
	PhoneNumber 				varchar(20) null,
	PhoneNumberConfirmed		bit not null default 0,
	AccessFailedCount			smallint not null default 0,
	LockoutEnabled 				bit not null default 0,
	LockoutEnd 					datetime null,	 				
	ConcurrencyStamp 			varchar(50) null,	
	SecurityStamp 				varchar(50) null,
	TwoFactorEnabled 			bit not null default 0,
	ThirdPartyGuid				varchar(50) null,
	 			
	constraint UX_User_Email UNIQUE NONCLUSTERED  (Email)
	
)


-----------IdentityRole

 ICollection<TRoleClaim>		Claims 						{ get; }
 TKey 							Id 							{ get; set; }
 string 						Name 						{ get; set; }
 ICollection<TUserRole> 		Users 						{ get; }

  --added--
  bool							IsActive					{ get; set; }

 create table Roles(
	Id 							int identity(1,1) primary key not null,
	Name 						varchar(20) not null,
	IsActive					bit not null default 0
 )
 
-----------IdentityUserRole
 
 TKey 							RoleId 						{ get; set; }
 TKey 							UserId 						{ get; set; }
 
 create table UserRoles(
	UserId						bigint,
	RoleId						int, 
	
	constraint PK_UserIdRoleId primary key (UserId, RoleId),
	constraint FK_UserRole_UserId foreign key (UserId) references Users(Id),
	constraint FK_UserRole_RoleId foreign key (RoleId) references Roles(Id)
 )
 
 
 
-----------IdentityUserClaim
 
 string 						ClaimType 					{ get; set; }
 string 						ClaimValue 					{ get; set; }
 int 							Id 							{ get; set; }
 TKey 							UserId 						{ get; set; }


 
create table UserClaims(
	Id							int identity(1,1) primary key not null,
	UserId						bigint not null, 
	ClaimType					varchar(256) not null,
	ClaimValue					varchar(256) null,
	
	constraint FK_UserClaim_UserId foreign key (UserId) references Users(Id),
	INDEX IX_UserClaims_UserId NONCLUSTERED (UserId)
 )
 
 
 -----------IdentityRoleClaim

  string 						ClaimType 					{ get; set; }
  string 						ClaimValue 					{ get; set; }
  int 							Id 							{ get; set; }
  TKey 							RoleId 						{ get; set; } 


  create table RoleClaims(
	Id							int identity(1,1) primary key not null,
	RoleId						int not null, 
	ClaimType					varchar(256) not null,
	ClaimValue					varchar(256) null,
	
	constraint FK_RoleClaim_RoleId foreign key (RoleId) references Roles(Id),
	INDEX IX_RoleClaims_RoleId NONCLUSTERED (RoleId)
	
 )
  
  
-------------IdentityUserLogin

 string 						LoginProvider 				{ get; set; }
 string 						ProviderDisplayName 		{ get; set; }
 string 						ProviderKey 				{ get; set; }
 TKey 							UserId 						{ get; set; }


create table UserLogins(
	LoginProvider				varchar(128) primary key not null,
	UserId						bigint not null, 	
	ProviderDisplayName			varchar(256) not null,
	ProviderKey					varchar(128) not null,
	
	constraint FK_RoleClaim_UserId foreign key (UserId) references Users(Id),
	INDEX IX_UserLogins_UserId NONCLUSTERED (UserId)
 )




*/
















