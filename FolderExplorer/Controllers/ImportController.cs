using FolderExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using FolderExplorer.Data;

namespace FolderExplorer.Controllers
{
    public class ImportController : Controller
    {
        private readonly FoldersContext _context;

        public ImportController(FoldersContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFolderCatalog(IFormFile file1, IFormFile file2)
        {
            if (file1 == null || file2 == null)
            {
                return RedirectToAction("Index", "Import");
            }

            var folders = ParseFoldersFileToModels(file1);
            var folderRelations = ParseFolderRelationsFileToModels(file2);

            WriteFoldersToDataBase(folders);
            WriteFolderRelationsToDataBase(folderRelations);
            Console.WriteLine("UploadFolderCatalog");
            return RedirectToAction("Index", "Folders");
        }

        private List<Folder> ParseFoldersFileToModels(IFormFile file)
        {
            var folders = new List<Folder>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                folders = csv.GetRecords<Folder>().ToList();
                folders.ForEach(a => Console.WriteLine(a.Id + " " + a.Path));
            }

            return folders;
        }

        private List<FolderRelations> ParseFolderRelationsFileToModels(IFormFile file)
        {
            var folderRelations = new List<FolderRelations>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<FolderRelationsMap>();
                var records = csv.GetRecords<FolderRelations>().ToList();
                records.ForEach(a => Console.WriteLine(a.FatherId + " " + a.ChildId));
                folderRelations.AddRange(records);
            }

            return folderRelations;
        }

        private void WriteFoldersToDataBase(List<Folder> folders)
        {
            foreach (var relation in _context.FolderRelations)
            {
                _context.FolderRelations.Remove(relation);
            }
            foreach (var item in _context.Folder)
            {
                _context.Folder.Remove(item);
            }

            _context.SaveChanges();
            folders.ForEach(a=> _context.Folder.Add(a));
            _context.SaveChanges();
        }

        private void WriteFolderRelationsToDataBase(List<FolderRelations> relations)
        {
            relations.ForEach(a => _context.FolderRelations.Add(a));
            _context.SaveChanges();
        }



        private class FolderRelationsMap : ClassMap<FolderRelations>
        {
            public FolderRelationsMap()
            {
                Map(m => m.FatherId).Name("ParentId", "father_id").Index(0);
                Map(m => m.ChildId).Name("ChildId", "child_id").Index(1);
            }
        }

    }
}
