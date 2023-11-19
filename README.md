# Folder-explorer
Web-application for displaying directories structure. 
# Project description
  Asp.net.core mvc web application for displaying directories structure. Folder structure is loaded from database where are two tables. First table has columns (id, name) and second has columns (id, father_id, child_id).
Also you can download directories structure from csv files (structure same as database tables).
I hope I will implement also upload directories structure from OS (by choosing certain folder and all inner folders will be displayed).
# Install and run the project
To run the project you have to install Visual Studio 2022 and SQL Management Studio. Also you need to install .Net 7.0 and Entity Framework Core v.7
The app runs on https://localhost:7154.
For creating database and initializing tables you have to run scripts that are in the 'doc' folder.
To connect the app with database, you have to write you connection string in appsettings.json.
If you want to save your directories structure to file, you should click Save button and your structure will be written in 'id_and_name_file.csv' and 'parent_and_child_ids.csv'
