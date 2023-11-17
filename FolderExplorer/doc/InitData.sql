-- Insert into dbo.Folder
INSERT INTO [Folders].dbo.Folder (id, path)
VALUES 
  (1, 'Creating Digital Images'),
  (2, 'Resources'),
  (3, 'Evidence'),
  (4, 'Graphic Products'),
  (5, 'Primary Resources'),
  (6, 'Secondary Resources'),
  (7, 'Process'),
  (8, 'Final Product');

-- Insert into dbo.FolderRelations
INSERT INTO [Folders].dbo.FolderRelations (father_id, child_id)
VALUES 
  (1, 2),
  (1, 3),
  (1, 4),
  (2, 5),
  (2, 6),
  (4, 7),
  (4, 8);