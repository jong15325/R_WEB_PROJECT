-- RWEB.dbo.Account definition

-- Drop table

-- DROP TABLE RWEB.dbo.Account;

-- 계정 테이블
CREATE TABLE RWEB.dbo.Account (
	Idx int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserType nvarchar(10) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserPassword nvarchar(255) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserPasswordSalt nvarchar(255) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserName nvarchar(30) COLLATE Korean_Wansung_CI_AS NOT NULL,
	UserCreateAt datetime DEFAULT getdate() NOT NULL,
	UserUpdateAt datetime NULL,
	UserDeleteAt datetime NULL,
	UserLockAt datetime NULL,
	CONSTRAINT PK__Account__DC501A7888A1FF4F PRIMARY KEY (Idx)
);

EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'고유번호', 'schema', N'dbo', 'table', N'Account', 'column', N'Idx';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 아이디', 'schema', N'dbo', 'table', N'Account', 'column', N'UserId';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 타입', 'schema', N'dbo', 'table', N'Account', 'column', N'UserType';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 패스워드', 'schema', N'dbo', 'table', N'Account', 'column', N'UserPassword';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 패스워드 해시', 'schema', N'dbo', 'table', N'Account', 'column', N'UserPasswordSalt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'유저 이름', 'schema', N'dbo', 'table', N'Account', 'column', N'UserName';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'생성일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserCreateAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'업데이트일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserUpdateAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'삭제일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserDeleteAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'계정 잠금일', 'schema', N'dbo', 'table', N'Account', 'column', N'UserLockAt';

/*INSERT INTO RWEB.dbo.Account (Idx, UserId, UserType, UserPassword, UserPasswordSalt, UserName, UserRoleCd, UserCreateAt, UserUpdateAt, UserDeleteAt, UserLockAt) VALUES(2, N'howeer15325@naver.com', N'1', N'Fgul2zLMwyqMsRsvVCDOvNs4EhPup3bvoFcrwAutx/0=', N'r8STsh4ybHjn5+6hNBxkgI9WxWa4u88Nthgq7t3TTKs=', N'rrr', N'RW-CD', '2024-01-11 10:42:25.340', NULL, NULL, '2024-01-31 17:25:00.000');*/

-- 로그인 로그 테이블
CREATE TABLE RWEB.dbo.LogLogin (
	Idx int IDENTITY(1,1) NOT NULL,
	LoginUserId nvarchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	LoginAt datetime DEFAULT getdate() NOT NULL,
	LoginIp nvarchar(45) COLLATE Korean_Wansung_CI_AS NOT NULL,
	LoginAgent nvarchar(MAX) COLLATE Korean_Wansung_CI_AS NOT NULL,
	LoginMessage nvarchar(200) COLLATE Korean_Wansung_CI_AS NULL,
	LoginStatusCode int NOT NULL,
	CONSTRAINT PK__LogLogin__C496003E0F972420 PRIMARY KEY (Idx)
);

EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'고유번호', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'Idx';
EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'유저 아이디', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginUserId';
EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'로그인 일시', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginAt';
EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'로그인 아이피', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginIp';
EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'로그인 장비', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginAgent';
EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'로그인 결과 메세지', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginMessage';
EXEC RWEB.sys.sp_updateextendedproperty 'MS_Description', N'로그인 결과 상태 코드', 'schema', N'dbo', 'table', N'LogLogin', 'column', N'LoginStatusCode';

-- 권한 테이블
CREATE TABLE RWEB.dbo.[Role] (
	idx int IDENTITY(0,1) PRIMARY KEY NOT NULL,
	RoleCd nvarchar(20) NOT NULL,
	RoleName nvarchar(20) NOT NULL,
	CreateIdx int NOT NULL,
	CreateAt datetime NOT NULL,
	UpdateIdx int NULL,
	UpdateAt datetime NULL
);
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'권한 정보', 'schema', N'dbo', 'table', N'Role';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'고유번호', 'schema', N'dbo', 'table', N'Role', 'column', N'idx';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'권한 코드', 'schema', N'dbo', 'table', N'Role', 'column', N'RoleCd';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'권한 이름', 'schema', N'dbo', 'table', N'Role', 'column', N'RoleName';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'생성자 IDX', 'schema', N'dbo', 'table', N'Role', 'column', N'CreateIdx';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'생성일자', 'schema', N'dbo', 'table', N'Role', 'column', N'CreateAt';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'수정자 IDX', 'schema', N'dbo', 'table', N'Role', 'column', N'UpdateIdx';
EXEC RWEB.sys.sp_addextendedproperty 'MS_Description', N'수정일자', 'schema', N'dbo', 'table', N'Role', 'column', N'UpdateAt';

-- 유저-권한 맵핑 테이블
CREATE TABLE RWEB.dbo.UserRole (
	UserIdx int NOT NULL,
	RoleIdx int NOT NULL,
	primary key (UserIdx, RoleIdx),
	CONSTRAINT FK_UserRole_User FOREIGN KEY (UserIdx) REFERENCES Account(idx),
    CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleIdx) REFERENCES Role(idx)
);
