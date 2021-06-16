using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using TestForTNNC.Database;
using TestForTNNC.Database.Models;
using TestForTNNC.Models;
using TestForTNNC.Models.UploadFiles;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestForTNNC.Controllers
{

    [Route("api/[controller]")]
    public class TestController : Controller
    {
        //контекст подключения и опции вынесены в сервисы - startup.cs
        // коннекшн стринг вынесен в appsettings.json
        private readonly TNNCDbContext _context;
        public TestController(TNNCDbContext context)
        {
            _context = context;
        }

        [HttpPost("uploadFile")]
        public async void UploadFile(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var filePath = Path.GetTempFileName();
            var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            var excel = new ExcelPackage(stream);
            var sheet = excel.Workbook.Worksheets.First();
            int rowCount = sheet.Dimension.End.Row;
            int colCount = sheet.Dimension.End.Column;
            List<ColumnsModel> listRow = new List<ColumnsModel>();
            for (int row = 2; row <= rowCount; row++)
            {
                if (sheet.Cells[row, 1].Value != null &&
                    sheet.Cells[row, 2].Value != null &&
                    sheet.Cells[row, 3].Value != null)
                {
                    ColumnsModel DataRow = new ColumnsModel();
                    DataRow.id_division = Convert.ToInt32(sheet.Cells[row, 1].Value.ToString());
                    DataRow.name_division = sheet.Cells[row, 2].Value.ToString();
                    DataRow.name_level1 = sheet.Cells[row, 3].Value.ToString();
                    DataRow.name_level2 = sheet.Cells[row, 4].Value.ToString();
                    DataRow.name_level3 = sheet.Cells[row, 5].Value.ToString();
                    DataRow.name_level4 = sheet.Cells[row, 6].Value.ToString();
                    DataRow.name_level5 = sheet.Cells[row, 7].Value.ToString();
                    DataRow.name_level6 = sheet.Cells[row, 8].Value.ToString();
                    DataRow.name_position = sheet.Cells[row, 9].Value.ToString();
                    // при конвертации ячейки в интеджер лишние символы при неправильном
                    // в случае использования модуля на реальной базе целесообразно добавить проверку посимвольно во избежании некорректной записи                    
                    DataRow.person_number = Convert.ToInt32(sheet.Cells[row, 10].Value.ToString());
                    DataRow.surname = sheet.Cells[row, 11].Value.ToString();
                    DataRow.firstname = sheet.Cells[row, 12].Value.ToString();
                    DataRow.fathername = sheet.Cells[row, 13].Value.ToString();
                    listRow.Add(DataRow);
                }
            }

            foreach (var row in listRow)
            {

                var division = _context.division.Where(x => x.id == row.id_division).FirstOrDefault();
                if (division == null)
                {
                    var new_division = new Division()
                    {
                        id = row.id_division,
                        name = row.name_division
                    };
                    division = new_division;
                    _context.division.Add(division);
                    _context.SaveChanges();


                }

                var Levels = _context.levels.Where(x => x.name == row.name_level1).FirstOrDefault();

                if (Levels == null)
                {
                    var Level1 = new Levels()
                    {
                        name = row.name_level1
                    };
                    _context.levels.Add(Level1);
                    _context.SaveChanges();
                    //_context.Dispose();
                    if (row.name_level2 != null)
                    {
                        if (!_context.levels.Any(x => x.name == row.name_level2))
                        {
                            var Level2 = new Levels()
                            {
                                name = row.name_level2,
                                parent_id = Level1.id

                            };
                            _context.levels.Add(Level2);
                            _context.SaveChanges();
                            //_context.Dispose();
                            if (row.name_level3 != null)
                            {
                                if (!_context.levels.Any(x => x.name == row.name_level3))
                                {
                                    var Level3 = new Levels()
                                    {
                                        name = row.name_level3,
                                        parent_id = Level2.id

                                    };
                                    _context.levels.Add(Level3);
                                    _context.SaveChanges();
                                    //_context.Dispose();
                                    if (row.name_level4 != null)
                                    {
                                        if (!_context.levels.Any(x => x.name == row.name_level4))
                                        {
                                            var Level4 = new Levels()
                                            {
                                                name = row.name_level4,
                                                parent_id = Level3.id

                                            };
                                            _context.levels.Add(Level4);
                                            _context.SaveChanges();
                                            //_context.Dispose();
                                            if (row.name_level5 != null)
                                            {
                                                if (!_context.levels.Any(x => x.name == row.name_level5))
                                                {
                                                    var Level5 = new Levels()
                                                    {
                                                        name = row.name_level5,
                                                        parent_id = Level4.id

                                                    };
                                                    _context.levels.Add(Level5);
                                                    _context.SaveChanges();
                                                    //_context.Dispose();
                                                    if (row.name_level6 != null)
                                                    {
                                                        if (!_context.levels.Any(x => x.name == row.name_level6))
                                                        {
                                                            var Level6 = new Levels()
                                                            {
                                                                name = row.name_level6,
                                                                parent_id = Level5.id

                                                            };
                                                            _context.levels.Add(Level6);
                                                            _context.SaveChanges();
                                                            //_context.Dispose();
                                                            Levels = Level6;
                                                        }
                                                    }
                                                    Levels = Level5;
                                                }
                                            }
                                            Levels = Level4;
                                        }
                                    }

                                    Levels = Level3;
                                }
                            }
                            Levels = Level2;
                        }

                    }
                    Levels = Level1;
                    _context.SaveChanges();

                }
                division.levels_id = Levels.id;
                //_context.division.Add(division);
                _context.SaveChanges();
                //_context.Dispose();
                var position = _context.position.Where(x => x.name == row.name_position).FirstOrDefault();
                if (position == null)
                {
                    var new_position = new Position()
                    {
                        name = row.name_position
                    };
                    position = new_position;
                    _context.position.Add(position);
                }
                position.division_id = division.id;               
                _context.SaveChanges();
                //_context.Dispose();

                // предполагается что табельный номер уникальный для каждого сотрудника

                var worker = _context.workers.Where(x => x.personal_id == row.person_number).FirstOrDefault();
                if (worker == null)
                {
                    var new_worker = new Workers()
                    {
                        surname = row.surname,
                        firstname = row.firstname,
                        fathername = row.fathername,
                        personal_id = row.person_number
                    };
                    worker = new_worker;
                }
                worker.division_id = division.id;
                worker.position_Id = position.id;
                _context.workers.Add(worker);
                _context.SaveChanges();
                //_context.Dispose();



            }
            _context.SaveChanges();
        }

        [HttpGet("getListWorkers")]
        public List<DownloadModelRow> GetListWorkers()
        {
            var result = new List<DownloadModelRow>();
            var workersFromDb = _context.workers;
            var divisionsFromDb = _context.division;
            var levelsFromDb = _context.levels;
            var groupedWorkers = workersFromDb.ToLookup(w => w.division_id);
            var groupedDivisions = divisionsFromDb.ToLookup(d => d.levels_id);
            //var groupedLevels = levelsFromDb.ToLookup(l => l.parent_id);


            foreach (var item in divisionsFromDb)
            {                
                //сортируем по табельному номеру и выбираем по нему начальника
                var dbworkers = groupedWorkers[item.id].OrderBy(w => w.personal_id);
                //записываем в модель начальника отдела
                var chief = dbworkers.FirstOrDefault();
                foreach (var worker in dbworkers)
                {
                    var dep = levelsFromDb.Where(x => x.id == item.levels_id).FirstOrDefault();
                    var row = new DownloadModelRow()
                    {
                        Fathername = worker.fathername,
                        Surname = worker.surname,
                        Firstname = worker.firstname,
                        Division = item.name,
                        Department = dep.name,
                        FathernameChief = chief.fathername,
                        FirstnameChief = chief.firstname,
                        SurnameChief = chief.surname
                    };
                    result.Add(row);

                }
            }
            return result;
        }

        [HttpGet("getFileWorkers")]
        public FileStreamResult GetFileWorkers()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var result = new List<DownloadModelRow>();
            var workersFromDb = _context.workers;
            var divisionsFromDb = _context.division;
            var levelsFromDb = _context.levels;
            var groupedWorkers = workersFromDb.ToLookup(w => w.division_id);
            var groupedDivisions = divisionsFromDb.ToLookup(d => d.levels_id);
            //var groupedLevels = levelsFromDb.ToLookup(l => l.parent_id);


            foreach (var item in divisionsFromDb)
            {                
                var dbworkers = groupedWorkers[item.id].OrderBy(w => w.personal_id);
                var chief = dbworkers.FirstOrDefault();
                foreach (var worker in dbworkers)
                {
                    var dep = levelsFromDb.Where(x => x.id == item.levels_id).FirstOrDefault();
                    var row = new DownloadModelRow()
                    {
                        Fathername = worker.fathername,
                        Surname = worker.surname,
                        Firstname = worker.firstname,
                        Division = item.name,
                        Department = dep.name,
                        FathernameChief = chief.fathername,
                        FirstnameChief = chief.firstname,
                        SurnameChief = chief.surname
                    };
                    result.Add(row);
                }
            }
            var memStream = ResultFileDownload(result);
            return File(memStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



        [HttpGet("getStaffTable")]
        public List<LevelModel> GetStaffTable()
        {
            var levelsFromDb = _context.levels;
            var divisionsFromDb = _context.division;
            var workersFromDb = _context.workers;
            //Группируем уровни по родителю
            var groupedLevels = levelsFromDb.ToLookup(l => l.parent_id);
            //Группируем подразделения по уровню
            var groupedDivisions = divisionsFromDb.ToLookup(d => d.levels_id);
            var groupedWorkers = workersFromDb.ToLookup(w => w.division_id);
            var result = new List<LevelModel>();
            FillResultTree(null, groupedLevels, groupedDivisions, groupedWorkers, result);
            //var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return result;
        }

        //Статичный класс для формирования дерева
        private static void FillResultTree(int? parentId,
            ILookup<int?, Levels> groupedLevels,
            ILookup<int?, Division> groupedDivisions,
            ILookup<int, Workers> groupedWorkers,
            List<LevelModel> result)
        {
            var dbLevels = groupedLevels[parentId].OrderBy(l => l.name);
            foreach (var dbLevel in dbLevels)
            {
                var dbDivisions = groupedDivisions[dbLevel.id].OrderBy(d => d.name);
                var divisions = new List<DivisionModel>();
                foreach (var row in dbDivisions)
                {
                    var dbworkers = groupedWorkers[row.id].OrderBy(w => w.personal_id);
                    var division = new DivisionModel(row.id, row.levels_id, row.name);
                    division.Workers.AddRange(dbworkers.Select(w => new WorkerModel(w.personal_id, w.division_id, w.surname, w.firstname, w.fathername)));
                    divisions.Add(division);
                }

                var level = new LevelModel(dbLevel.id, dbLevel.parent_id, dbLevel.name);
                level.Divisions.AddRange(divisions);
                result.Add(level);
                FillResultTree(level.Id, groupedLevels, groupedDivisions, groupedWorkers, level.Children);
            }
        }
        //статичный класс для формирования файла 
        private static MemoryStream ResultFileDownload(List<DownloadModelRow> modelRows)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var fileInfo = new FileInfo(Path.GetTempPath() + "\\" +
                            DateTime.Now.Ticks + ".xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(modelRows, true);
                package.Save();
                var memStream = new MemoryStream(package.GetAsByteArray());
                return memStream;
            }
            
        }
    }
}


    

