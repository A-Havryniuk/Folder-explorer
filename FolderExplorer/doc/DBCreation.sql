CREATE DATABASE Folders;
GO
USE Folders;

CREATE TABLE Folder (
    id int NOT NULL,
    [path] varchar(511) NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE FolderRelations (
	id int IDENTITY(1,1),
    father_id int NOT NULL,
    child_id int NOT NULL,
	PRIMARY KEY(id),
    FOREIGN KEY (father_id) REFERENCES Folder(id),
    FOREIGN KEY (child_id) REFERENCES Folder(id)
);
