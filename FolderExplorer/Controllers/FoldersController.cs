using System.Globalization;
using System.Text;
using CsvHelper;
using FolderExplorer.Data;
using FolderExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FolderExplorer.Controllers
{
    public class FoldersController : Controller
    {
        private readonly FoldersContext _context;
        

        public FoldersController(FoldersContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var folders = _context.Folder;
            var root = folders.First();
            if (FindRootDirectory() != null)
            {
                Console.WriteLine(folders.Find(FindRootDirectory()));
            }
            return View(root);
        }

        [HttpGet]
        [Route("Subdirectories/{id}")]
        public IActionResult Subdirectories(int id)
        {
            var ids = FindSubdirectories(id);
            var folders = _context.Folder.Where(a => ids.Contains(a.Id)).ToList();
            return View(folders);
        }


        private int? FindRootDirectory()
        {
            var list1 = _context.FolderRelations.Select(a => a.FatherId).ToList();
            var list2 = _context.FolderRelations.Select(b => b.ChildId).ToList();
            return  list1.Except(list2).First();
        }

        private List<int?> FindSubdirectories(int id)
        {
            return _context.FolderRelations
                .Where(fr => fr.FatherId == id)
                .Select(o => o.ChildId)
                .ToList();
        }

        public IActionResult SaveDataToFile()
        {
            var allFolders = _context.Folder.ToList();

            var idAndNameCsvExport = new StringBuilder();
            idAndNameCsvExport.AppendLine("Id,Path"); 

            var parentAndChildIdsCsvExport = new StringBuilder();
            parentAndChildIdsCsvExport.AppendLine("ParentId,ChildId"); 

            foreach (var folder in allFolders)
            {
                idAndNameCsvExport.AppendLine($"{folder.Id},{folder.Path}");


                var childIds = _context.FolderRelations
                    .Where(fr => fr.FatherId == folder.Id)
                    .Select(fr => fr.ChildId)
                    .ToList();


                foreach (var childId in childIds)
                {
                    parentAndChildIdsCsvExport.AppendLine($"{folder.Id},{childId}");
                }
            }

            var idAndNameFilePath = "path_to_id_and_name_file.csv";
            var parentAndChildIdsFilePath = "path_to_parent_and_child_ids_file.csv";

            System.IO.File.WriteAllText(idAndNameFilePath, idAndNameCsvExport.ToString());
            System.IO.File.WriteAllText(parentAndChildIdsFilePath, parentAndChildIdsCsvExport.ToString());

            return Content($"Files exported to: {idAndNameFilePath}, {parentAndChildIdsFilePath}");
        }

    }


}
