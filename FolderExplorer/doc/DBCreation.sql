CREATE DATABASE Folders;
USE Folders;

CREATE TABLE Folder (
    id int IDENTITY(1, 1),
    [path] varchar(511),
    PRIMARY KEY(id)
);

CREATE TABLE FolderRelations (
    father_id int,
    child_id int,
    FOREIGN KEY (father_id) REFERENCES Folder(id),
    FOREIGN KEY (child_id) REFERENCES Folder(id)
);