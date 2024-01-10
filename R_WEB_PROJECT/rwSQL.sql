-- RWEB.dbo.Account definition

-- Drop table

-- DROP TABLE RWEB.dbo.Account;

CREATE TABLE RWEB.dbo.Account (
	idx int IDENTITY(0,1) NOT NULL,
	UserId nvarchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserType nvarchar(10) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserPassword nvarchar(255) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserPasswordSalt nvarchar(255) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserName nvarchar(30) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserRoleCd nvarchar(20) COLLATE Korean_Wansung_CI_AS NULL,
	UserCreateAt datetime DEFAULT getdate() NOT NULL,
	UserUpdateAt datetime,
	UserDeleteAt datetime
);