-- Created by Redgate Data Modeler (https://datamodeler.redgate-platform.com)
-- Last modification date: 2026-01-18 15:41:13.314

-- foreign keys
ALTER TABLE ActivityLog DROP CONSTRAINT ActivityLog_Boards;

ALTER TABLE ActivityLog DROP CONSTRAINT ActivityLog_Columns;

ALTER TABLE ActivityLog DROP CONSTRAINT ActivityLog_Tags;

ALTER TABLE ActivityLog DROP CONSTRAINT ActivityLog_TaskComments;

ALTER TABLE ActivityLog DROP CONSTRAINT ActivityLog_Tasks;

ALTER TABLE ActivityLog DROP CONSTRAINT ActivityLog_Users;

ALTER TABLE BoardMembers DROP CONSTRAINT BoardMemebers_Boards;

ALTER TABLE BoardMembers DROP CONSTRAINT BoardMemebers_Users;

ALTER TABLE Boards DROP CONSTRAINT Boards_Users;

ALTER TABLE Columns DROP CONSTRAINT Columns_Boards;

ALTER TABLE Tags DROP CONSTRAINT Tags_Boards;

ALTER TABLE TaskComments DROP CONSTRAINT TaskComments_Tasks;

ALTER TABLE TaskComments DROP CONSTRAINT TaskComments_Users;

ALTER TABLE TaskTags DROP CONSTRAINT TaskTags_Tags;

ALTER TABLE TaskTags DROP CONSTRAINT TaskTags_Tasks;

ALTER TABLE Tasks DROP CONSTRAINT Tasks_Columns;

ALTER TABLE Tasks DROP CONSTRAINT Tasks_TaskType;

ALTER TABLE Tasks DROP CONSTRAINT Tasks_Users;

-- tables
DROP TABLE ActivityLog;

DROP TABLE BoardMembers;

DROP TABLE Boards;

DROP TABLE Columns;

DROP TABLE Tags;

DROP TABLE TaskComments;

DROP TABLE TaskTags;

DROP TABLE TaskTypes;

DROP TABLE Tasks;

DROP TABLE Users;

-- End of file.

