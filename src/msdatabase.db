BEGIN TRANSACTION;
DROP TABLE IF EXISTS [__EFMigrationsHistory];
CREATE TABLE IF NOT EXISTS [__EFMigrationsHistory] (
	[MigrationId]	TEXT NOT NULL,
	[ProductVersion]	TEXT NOT NULL,
	CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY([MigrationId])
);
DROP TABLE IF EXISTS [AspNetRoles];
CREATE TABLE IF NOT EXISTS [AspNetRoles] (
	[Id]	TEXT NOT NULL,
	[Name]	TEXT,
	[NormalizedName]	TEXT,
	[ConcurrencyStamp]	TEXT,
	CONSTRAINT [PK_AspNetRoles] PRIMARY KEY([Id])
);
DROP TABLE IF EXISTS [AspNetUsers];
CREATE TABLE IF NOT EXISTS [AspNetUsers] (
	[Id]	TEXT NOT NULL,
	[ParentId]	nvarchar(450),
	[SpecialistId]	nvarchar(450),
	[UserBlocked]	INTEGER NOT NULL,
	[FirstName]	nvarchar(100),
	[LastName]	nvarchar(100),
	[Age]	TEXT NOT NULL,
	[Specialism]	nvarchar(100),
	[Description]	nvarchar(256),
	[UserName]	TEXT,
	[NormalizedUserName]	TEXT,
	[Email]	TEXT,
	[NormalizedEmail]	TEXT,
	[EmailConfirmed]	INTEGER NOT NULL,
	[PasswordHash]	TEXT,
	[SecurityStamp]	TEXT,
	[ConcurrencyStamp]	TEXT,
	[PhoneNumber]	TEXT,
	[PhoneNumberConfirmed]	INTEGER NOT NULL,
	[TwoFactorEnabled]	INTEGER NOT NULL,
	[LockoutEnd]	TEXT,
	[LockoutEnabled]	INTEGER NOT NULL,
	[AccessFailedCount]	INTEGER NOT NULL,
	CONSTRAINT [PK_AspNetUsers] PRIMARY KEY([Id])
);
DROP TABLE IF EXISTS [Chat];
CREATE TABLE IF NOT EXISTS [Chat] (
	[Id]	INTEGER NOT NULL,
	[Naam]	TEXT,
	[Beschrijving]	TEXT,
	[type]	INTEGER NOT NULL,
	[Onderwerp]	TEXT,
	CONSTRAINT [PK_Chat] PRIMARY KEY([Id] AUTOINCREMENT)
);
DROP TABLE IF EXISTS [AspNetRoleClaims];
CREATE TABLE IF NOT EXISTS [AspNetRoleClaims] (
	[Id]	INTEGER NOT NULL,
	[RoleId]	TEXT NOT NULL,
	[ClaimType]	TEXT,
	[ClaimValue]	TEXT,
	CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY([Id] AUTOINCREMENT),
	CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [AspNetRoles]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [AspNetUserClaims];
CREATE TABLE IF NOT EXISTS [AspNetUserClaims] (
	[Id]	INTEGER NOT NULL,
	[UserId]	TEXT NOT NULL,
	[ClaimType]	TEXT,
	[ClaimValue]	TEXT,
	CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY([Id] AUTOINCREMENT),
	CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [AspNetUserLogins];
CREATE TABLE IF NOT EXISTS [AspNetUserLogins] (
	[LoginProvider]	TEXT NOT NULL,
	[ProviderKey]	TEXT NOT NULL,
	[ProviderDisplayName]	TEXT,
	[UserId]	TEXT NOT NULL,
	CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY([LoginProvider],[ProviderKey]),
	CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [AspNetUserRoles];
CREATE TABLE IF NOT EXISTS [AspNetUserRoles] (
	[UserId]	TEXT NOT NULL,
	[RoleId]	TEXT NOT NULL,
	CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY([UserId],[RoleId]),
	CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [AspNetRoles]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [AspNetUserTokens];
CREATE TABLE IF NOT EXISTS [AspNetUserTokens] (
	[UserId]	TEXT NOT NULL,
	[LoginProvider]	TEXT NOT NULL,
	[Name]	TEXT NOT NULL,
	[Value]	TEXT,
	CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY([UserId],[LoginProvider],[Name]),
	CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [srcUsersrcUser];
CREATE TABLE IF NOT EXISTS [srcUsersrcUser] (
	[ChilderenId]	TEXT NOT NULL,
	[ClientsId]	TEXT NOT NULL,
	CONSTRAINT [PK_srcUsersrcUser] PRIMARY KEY([ChilderenId],[ClientsId]),
	CONSTRAINT [FK_srcUsersrcUser_AspNetUsers_ChilderenId] FOREIGN KEY([ChilderenId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_srcUsersrcUser_AspNetUsers_ClientsId] FOREIGN KEY([ClientsId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [ChatUsers];
CREATE TABLE IF NOT EXISTS [ChatUsers] (
	[UserId]	TEXT NOT NULL,
	[ChatId]	INTEGER NOT NULL,
	[Role]	INTEGER NOT NULL,
	CONSTRAINT [PK_ChatUsers] PRIMARY KEY([UserId],[ChatId]),
	CONSTRAINT [FK_ChatUsers_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_ChatUsers_Chat_ChatId] FOREIGN KEY([ChatId]) REFERENCES [Chat]([Id]) ON DELETE CASCADE
);
DROP TABLE IF EXISTS [Messages];
CREATE TABLE IF NOT EXISTS [Messages] (
	[Id]	INTEGER NOT NULL,
	[Naam]	TEXT,
	[Text]	TEXT,
	[timestamp]	TEXT NOT NULL,
	[ChatId]	INTEGER NOT NULL,
	CONSTRAINT [PK_Messages] PRIMARY KEY([Id] AUTOINCREMENT),
	CONSTRAINT [FK_Messages_Chat_ChatId] FOREIGN KEY([ChatId]) REFERENCES [Chat]([Id]) ON DELETE CASCADE
);
INSERT INTO [__EFMigrationsHistory] VALUES ('20220113161023_2','5.0.0');
INSERT INTO [__EFMigrationsHistory] VALUES ('20220113173820_3','5.0.0');
INSERT INTO [__EFMigrationsHistory] VALUES ('20220114095727_NieuweMigratie2','5.0.0');
INSERT INTO [AspNetUsers] VALUES ('ce29ccbf-0ddf-4329-af0a-4e6b3a4d0c75',NULL,NULL,0,'Alec','van Spronsen','2005-01-05 00:00:00',NULL,NULL,'Alecvanspr@gmail.com','ALECVANSPR@GMAIL.COM','Alecvanspr@gmail.com','ALECVANSPR@GMAIL.COM',0,'AQAAAAEAACcQAAAAENx5kcGETWIr0t7LOQrHUO8kO3ADwy6WbdsgdTuDUa4m9vgmeYiUAVgKkK0g8mrwsA==','6DBVKURL2YVP3OFWCANF767HMFNCFE6V','c42251a9-9a50-4aba-b8de-b65af7434998',NULL,0,0,NULL,1,0);
DROP INDEX IF EXISTS [IX_AspNetRoleClaims_RoleId];
CREATE INDEX IF NOT EXISTS [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] (
	[RoleId]
);
DROP INDEX IF EXISTS [RoleNameIndex];
CREATE UNIQUE INDEX IF NOT EXISTS [RoleNameIndex] ON [AspNetRoles] (
	[NormalizedName]
);
DROP INDEX IF EXISTS [IX_AspNetUserClaims_UserId];
CREATE INDEX IF NOT EXISTS [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] (
	[UserId]
);
DROP INDEX IF EXISTS [IX_AspNetUserLogins_UserId];
CREATE INDEX IF NOT EXISTS [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] (
	[UserId]
);
DROP INDEX IF EXISTS [IX_AspNetUserRoles_RoleId];
CREATE INDEX IF NOT EXISTS [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] (
	[RoleId]
);
DROP INDEX IF EXISTS [EmailIndex];
CREATE INDEX IF NOT EXISTS [EmailIndex] ON [AspNetUsers] (
	[NormalizedEmail]
);
DROP INDEX IF EXISTS [UserNameIndex];
CREATE UNIQUE INDEX IF NOT EXISTS [UserNameIndex] ON [AspNetUsers] (
	[NormalizedUserName]
);
DROP INDEX IF EXISTS [IX_ChatUsers_ChatId];
CREATE INDEX IF NOT EXISTS [IX_ChatUsers_ChatId] ON [ChatUsers] (
	[ChatId]
);
DROP INDEX IF EXISTS [IX_Messages_ChatId];
CREATE INDEX IF NOT EXISTS [IX_Messages_ChatId] ON [Messages] (
	[ChatId]
);
DROP INDEX IF EXISTS [IX_srcUsersrcUser_ClientsId];
CREATE INDEX IF NOT EXISTS [IX_srcUsersrcUser_ClientsId] ON [srcUsersrcUser] (
	[ClientsId]
);
COMMIT;
