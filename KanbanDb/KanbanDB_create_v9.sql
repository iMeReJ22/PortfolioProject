-- Created by Redgate Data Modeler (https://datamodeler.redgate-platform.com)
-- Last modification date: 2026-01-19 13:55:39.378

-- tables
-- Table: ActivityLog
CREATE TABLE ActivityLog (
    Id int  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    Description nvarchar(500)  NOT NULL,
    CreatedAt datetime  NOT NULL,
    BoardId int  NOT NULL,
    ActivityAuthorId int  NULL,
    TagId int  NULL,
    ColumnId int  NULL,
    TaskId int  NULL,
    TaskCommentId int  NULL,
    MemberId int  NULL,
    CONSTRAINT ActivityLog_pk PRIMARY KEY  (Id)
);

CREATE INDEX ALBoardIdIndex on ActivityLog (BoardId ASC)
;

CREATE INDEX UserIdIndex on ActivityLog (ActivityAuthorId ASC)
;

-- Table: BoardMembers
CREATE TABLE BoardMembers (
    Role nvarchar(50)  NOT NULL,
    BoardId int  NOT NULL,
    UserId int  NOT NULL,
    CONSTRAINT BoardMembers_pk PRIMARY KEY  (UserId,BoardId)
);

-- Table: Boards
CREATE TABLE Boards (
    Id int  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    Description nvarchar(500)  NOT NULL,
    CreatedAt datetime  NOT NULL,
    OwnerId int  NOT NULL,
    CONSTRAINT Boards_pk PRIMARY KEY  (Id)
);

-- Table: Columns
CREATE TABLE Columns (
    Id int  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    OrderIndex int  NOT NULL,
    BoardId int  NOT NULL,
    CONSTRAINT Columns_pk PRIMARY KEY  (Id)
);

CREATE INDEX ColBoardIdIndex on Columns (BoardId ASC)
;

-- Table: Tags
CREATE TABLE Tags (
    Id int  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    ColorHex nvarchar(7)  NOT NULL,
    CreatedAt datetime  NOT NULL,
    BoardId int  NOT NULL,
    CONSTRAINT Tags_pk PRIMARY KEY  (Id)
);

CREATE INDEX TagBoardIdIndex on Tags (BoardId ASC)
;

-- Table: TaskComments
CREATE TABLE TaskComments (
    Id int  NOT NULL,
    Content nvarchar(max)  NOT NULL,
    CreatedAt datetime  NOT NULL,
    TaskId int  NOT NULL,
    AuthorId int  NULL,
    CONSTRAINT TaskComments_pk PRIMARY KEY  (Id)
);

CREATE INDEX CommentTaskIdIndex on TaskComments (TaskId ASC)
;

-- Table: TaskTags
CREATE TABLE TaskTags (
    TagId int  NOT NULL,
    TaskId int  NOT NULL,
    CONSTRAINT TaskTags_pk PRIMARY KEY  (TaskId,TagId)
);

CREATE INDEX TagIdIndex on TaskTags (TagId ASC)
;

CREATE INDEX TaskIdIndex on TaskTags (TaskId ASC)
;

-- Table: TaskTypes
CREATE TABLE TaskTypes (
    Id int  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    ColorHex nvarchar(7)  NOT NULL,
    OrderIndex int  NOT NULL,
    CONSTRAINT TaskTypes_pk PRIMARY KEY  (Id)
);

-- Table: Tasks
CREATE TABLE Tasks (
    Id int  NOT NULL,
    Title nvarchar(200)  NOT NULL,
    Description nvarchar(max)  NOT NULL,
    OrderIndex int  NOT NULL,
    CreatedAt datetime  NOT NULL,
    ColumnId int  NOT NULL,
    TaskTypeId int  NOT NULL,
    CreatedByUserId int  NULL,
    CONSTRAINT Tasks_pk PRIMARY KEY  (Id)
);

CREATE INDEX OrderIndexIndex on Tasks (OrderIndex ASC)
;

CREATE INDEX ColumnIdIndex on Tasks (ColumnId ASC)
;

CREATE INDEX TaskTypeIdIndex on Tasks (TaskTypeId ASC)
;

-- Table: Users
CREATE TABLE Users (
    Id int  NOT NULL,
    Email nvarchar(256)  NOT NULL,
    PasswordHash nvarchar(512)  NOT NULL,
    DisplayName nvarchar(100)  NOT NULL,
    CreatedAt datetime  NOT NULL,
    CONSTRAINT Users_pk PRIMARY KEY  (Id)
);

CREATE UNIQUE INDEX EmailIndex on Users (Email ASC)
;

-- foreign keys
-- Reference: ActivityLog_Boards (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_Boards
    FOREIGN KEY (BoardId)
    REFERENCES Boards (Id);

-- Reference: ActivityLog_Columns (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_Columns
    FOREIGN KEY (ColumnId)
    REFERENCES Columns (Id)
    ON DELETE  SET NULL;

-- Reference: ActivityLog_Tags (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_Tags
    FOREIGN KEY (TagId)
    REFERENCES Tags (Id)
    ON DELETE  SET NULL;

-- Reference: ActivityLog_TaskComments (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_TaskComments
    FOREIGN KEY (TaskCommentId)
    REFERENCES TaskComments (Id)
    ON DELETE  SET NULL;

-- Reference: ActivityLog_Tasks (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_Tasks
    FOREIGN KEY (TaskId)
    REFERENCES Tasks (Id)
    ON DELETE  SET NULL;

-- Reference: ActivityLog_Users_Author (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_Users_Author
    FOREIGN KEY (ActivityAuthorId)
    REFERENCES Users (Id)
    ON DELETE  SET NULL;

-- Reference: ActivityLog_Users_Member (table: ActivityLog)
ALTER TABLE ActivityLog ADD CONSTRAINT ActivityLog_Users_Member
    FOREIGN KEY (MemberId)
    REFERENCES Users (Id);

-- Reference: BoardMemebers_Boards (table: BoardMembers)
ALTER TABLE BoardMembers ADD CONSTRAINT BoardMemebers_Boards
    FOREIGN KEY (BoardId)
    REFERENCES Boards (Id);

-- Reference: BoardMemebers_Users (table: BoardMembers)
ALTER TABLE BoardMembers ADD CONSTRAINT BoardMemebers_Users
    FOREIGN KEY (UserId)
    REFERENCES Users (Id)
    ON DELETE  CASCADE;

-- Reference: Boards_Users (table: Boards)
ALTER TABLE Boards ADD CONSTRAINT Boards_Users
    FOREIGN KEY (OwnerId)
    REFERENCES Users (Id);

-- Reference: Columns_Boards (table: Columns)
ALTER TABLE Columns ADD CONSTRAINT Columns_Boards
    FOREIGN KEY (BoardId)
    REFERENCES Boards (Id);

-- Reference: Tags_Boards (table: Tags)
ALTER TABLE Tags ADD CONSTRAINT Tags_Boards
    FOREIGN KEY (BoardId)
    REFERENCES Boards (Id);

-- Reference: TaskComments_Tasks (table: TaskComments)
ALTER TABLE TaskComments ADD CONSTRAINT TaskComments_Tasks
    FOREIGN KEY (TaskId)
    REFERENCES Tasks (Id);

-- Reference: TaskComments_Users (table: TaskComments)
ALTER TABLE TaskComments ADD CONSTRAINT TaskComments_Users
    FOREIGN KEY (AuthorId)
    REFERENCES Users (Id)
    ON DELETE  SET NULL;

-- Reference: TaskTags_Tags (table: TaskTags)
ALTER TABLE TaskTags ADD CONSTRAINT TaskTags_Tags
    FOREIGN KEY (TagId)
    REFERENCES Tags (Id);

-- Reference: TaskTags_Tasks (table: TaskTags)
ALTER TABLE TaskTags ADD CONSTRAINT TaskTags_Tasks
    FOREIGN KEY (TaskId)
    REFERENCES Tasks (Id);

-- Reference: Tasks_Columns (table: Tasks)
ALTER TABLE Tasks ADD CONSTRAINT Tasks_Columns
    FOREIGN KEY (ColumnId)
    REFERENCES Columns (Id);

-- Reference: Tasks_TaskType (table: Tasks)
ALTER TABLE Tasks ADD CONSTRAINT Tasks_TaskType
    FOREIGN KEY (TaskTypeId)
    REFERENCES TaskTypes (Id);

-- Reference: Tasks_Users (table: Tasks)
ALTER TABLE Tasks ADD CONSTRAINT Tasks_Users
    FOREIGN KEY (CreatedByUserId)
    REFERENCES Users (Id)
    ON DELETE  SET NULL;

-- End of file.

