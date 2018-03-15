 CREATE DATABASE `MyWebSite`;
 USE  `MyWebSite`;

CREATE TABLE `AspNetRoles` (
    `Id` VARCHAR(256) NOT NULL,
    `ConcurrencyStamp` TEXT NULL,
    `Discriminator` TEXT NOT NULL,
    `Name` VARCHAR(256) NULL,
    `NormalizedName` VARCHAR(256) NULL,
    `Remark` TEXT NULL,
    PRIMARY KEY (`Id`)
);
CREATE TABLE `AspNetUsers` (
    `Id` VARCHAR(256) NOT NULL,
    `AccessFailedCount` INT NOT NULL,
    `ConcurrencyStamp` TEXT NULL,
    `Email` VARCHAR(256) NULL,
    `EmailConfirmed` BIT NOT NULL,
    `LockoutEnabled` BIT NOT NULL,
    `LockoutEnd` TIMESTAMP NULL,
    `NickName` VARCHAR(256) NULL,
    `NormalizedEmail` VARCHAR(256) NULL,
    `NormalizedUserName` VARCHAR(256) NULL,
    `PasswordHash` TEXT NULL,
    `PhoneNumber` TEXT NULL,
    `PhoneNumberConfirmed` BIT NOT NULL,
    `SecurityStamp` TEXT NULL,
    `TwoFactorEnabled` BIT NOT NULL,
    `UserName` VARCHAR(256) NULL,
    PRIMARY KEY (`Id`)
);
CREATE TABLE `EssayArchives` (
    `EssayArchiveID` INT NOT NULL AUTO_INCREMENT,
    `Name` TEXT NOT NULL,
    PRIMARY KEY (`EssayArchiveID`)
);
CREATE TABLE `EssayCatalogs` (
    `EssayCatalogID` INT NOT NULL AUTO_INCREMENT,
    `Name` TEXT NOT NULL,
    PRIMARY KEY (`EssayCatalogID`)
);
CREATE TABLE `EssayTags` (
    `EssayTagID` INT NOT NULL AUTO_INCREMENT,
    `Name` TEXT NOT NULL,
    PRIMARY KEY (`EssayTagID`)
);
CREATE TABLE `Menus` (
    `Id` VARCHAR(256) NOT NULL,
    `Icon` VARCHAR(50) NOT NULL,
    `IndexCode` INT NOT NULL,
    `MenuType` INT NOT NULL,
    `Name` VARCHAR(256) NOT NULL,
    `ParentId` TEXT NULL,
    `Remarks` TEXT NULL,
    `Url` VARCHAR(256) NULL,
    PRIMARY KEY (`Id`)
);
CREATE TABLE `AspNetRoleClaims` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `ClaimType` TEXT NULL,
    `ClaimValue` TEXT NULL,
    `RoleId` VARCHAR(256) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`)
        REFERENCES `AspNetRoles` (`Id`)
        ON DELETE CASCADE
);
CREATE TABLE `AspNetUserClaims` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `ClaimType` TEXT NULL,
    `ClaimValue` TEXT NULL,
    `UserId` VARCHAR(256) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`)
        REFERENCES `AspNetUsers` (`Id`)
        ON DELETE CASCADE
);
CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` VARCHAR(256) NOT NULL,
    `ProviderKey` VARCHAR(256) NOT NULL,
    `ProviderDisplayName` TEXT NULL,
    `UserId` VARCHAR(256) NOT NULL,
    PRIMARY KEY (`LoginProvider` , `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`)
        REFERENCES `AspNetUsers` (`Id`)
        ON DELETE CASCADE
);
CREATE TABLE `AspNetUserRoles` (
    `UserId` VARCHAR(256) NOT NULL,
    `RoleId` VARCHAR(256) NOT NULL,
    PRIMARY KEY (`UserId` , `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`)
        REFERENCES `AspNetRoles` (`Id`)
        ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`)
        REFERENCES `AspNetUsers` (`Id`)
        ON DELETE CASCADE
);
CREATE TABLE `AspNetUserTokens` (
    `UserId` VARCHAR(256) NOT NULL,
    `LoginProvider` VARCHAR(256) NOT NULL,
    `Name` VARCHAR(256) NOT NULL,
    `Value` TEXT NULL,
    PRIMARY KEY (`UserId` , `LoginProvider` , `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`)
        REFERENCES `AspNetUsers` (`Id`)
        ON DELETE CASCADE
);
CREATE TABLE `Essays` (
    `EssayID` VARCHAR(256) NOT NULL,
    `Content` TEXT NULL,
    `CreateTime` DATETIME NOT NULL,
    `EssayArchiveID` INT NULL,
    `EssayCatalogID` INT NULL,
    `Summary` TEXT NULL,
    `Title` TEXT NOT NULL,
    `UpdateTime` DATETIME NOT NULL,
    PRIMARY KEY (`EssayID`),
    CONSTRAINT `FK_Essays_EssayArchives_EssayArchiveID` FOREIGN KEY (`EssayArchiveID`)
        REFERENCES `EssayArchives` (`EssayArchiveID`)
        ON DELETE RESTRICT,
    CONSTRAINT `FK_Essays_EssayCatalogs_EssayCatalogID` FOREIGN KEY (`EssayCatalogID`)
        REFERENCES `EssayCatalogs` (`EssayCatalogID`)
        ON DELETE RESTRICT
);
CREATE TABLE `RoleMenus` (
    `RoleId` VARCHAR(256) NOT NULL,
    `MenuId` VARCHAR(256) NOT NULL,
    PRIMARY KEY (`RoleId` , `MenuId`),
    CONSTRAINT `FK_RoleMenus_Menus_MenuId` FOREIGN KEY (`MenuId`)
        REFERENCES `Menus` (`Id`)
        ON DELETE CASCADE,
    CONSTRAINT `FK_RoleMenus_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`)
        REFERENCES `AspNetRoles` (`Id`)
        ON DELETE CASCADE
);
CREATE TABLE `EssayTagAssignments` (
    `EssayID` VARCHAR(256) NOT NULL,
    `EssayTagID` INT NOT NULL,
    PRIMARY KEY (`EssayID` , `EssayTagID`),
    CONSTRAINT `FK_EssayTagAssignments_Essays_EssayID` FOREIGN KEY (`EssayID`)
        REFERENCES `Essays` (`EssayID`)
        ON DELETE CASCADE,
    CONSTRAINT `FK_EssayTagAssignments_EssayTags_EssayTagID` FOREIGN KEY (`EssayTagID`)
        REFERENCES `EssayTags` (`EssayTagID`)
        ON DELETE CASCADE
);
CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON AspNetRoleClaims (`RoleId`);
CREATE UNIQUE INDEX `RoleNameIndex` ON AspNetRoles (`NormalizedName`);
CREATE INDEX `IX_AspNetUserClaims_UserId` ON AspNetUserClaims (`UserId`);
CREATE INDEX `IX_AspNetUserLogins_UserId` ON AspNetUserLogins (`UserId`);
CREATE INDEX `IX_AspNetUserRoles_RoleId` ON AspNetUserRoles (`RoleId`);
CREATE INDEX `EmailIndex` ON AspNetUsers (`NormalizedEmail`);
CREATE UNIQUE INDEX `UserNameIndex` ON AspNetUsers (`NormalizedUserName`);
CREATE INDEX `IX_Essays_EssayArchiveID` ON Essays (`EssayArchiveID`);
CREATE INDEX `IX_Essays_EssayCatalogID` ON Essays (`EssayCatalogID`);
CREATE INDEX `IX_EssayTagAssignments_EssayTagID` ON EssayTagAssignments (`EssayTagID`);
CREATE INDEX `IX_RoleMenus_MenuId` ON RoleMenus (`MenuId`);
