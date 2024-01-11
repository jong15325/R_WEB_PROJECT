-- RWEB.dbo.Account definition

-- Drop table

-- DROP TABLE RWEB.dbo.Account;

CREATE TABLE RWEB.dbo.Account (
	Idx int IDENTITY(1,1) PRIMARY KEY NOT NULL,
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

EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'고유번호', 'schema', N'dbo', 'table', N'Account', 'column', N'Idx';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 아이디', 'schema', N'dbo', 'table', N'Account', 'column', N'UserId';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 타입', 'schema', N'dbo', 'table', N'Account', 'column', N'UserType';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 패스워드', 'schema', N'dbo', 'table', N'Account', 'column', N'UserPassword';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 패스워드 해시', 'schema', N'dbo', 'table', N'Account', 'column', N'UserPasswordSalt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 이름', 'schema', N'dbo', 'table', N'Account', 'column', N'UserName';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 권한 코드', 'schema', N'dbo', 'table', N'Account', 'column', N'UserRoleCd';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'생성일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserCreateAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'업데이트일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserUpdateAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'삭제일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserDeleteAt';

-- INSERT INTO RWEB.dbo.Account(idx, UserId, UserType, UserPassword, UserPasswordSalt, UserName, UserRoleCd, UserCreateAt, UserUpdateAt, UserDeleteAt)VALUES(1, N'howeer15325@naver.com', N'1', N'Fgul2zLMwyqMsRsvVCDOvNs4EhPup3bvoFcrwAutx/0=', N'r8STsh4ybHjn5+6hNBxkgI9WxWa4u88Nthgq7t3TTKs=', N'rrr', N'RW-CD', '2024-01-11 10:42:25.340', NULL, NULL);

CREATE TABLE RWEB.dbo.LogLogin (
	Idx int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	LoginUserId nvarchar(50) COLLATE Korean_Wansung_CI_AS,
	LoginAt datetime DEFAULT getdate() NOT NULL,
	LoginIp NVARCHAR(45) NOT NULL,
	LoginAgent NVARCHAR(MAX), -- 브라우저 정보 등
	LoginStatus NVARCHAR(20) NOT NULL -- 성공 또는 실패
);

EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'고유번호', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'Idx';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 아이디', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginUserId';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'로그인 일시', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'로그인 아이피', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginIp';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'로그인 장비', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginAgent';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'로그인 결과 상태', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginStatus';
