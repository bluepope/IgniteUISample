﻿using IgniteUI.Blazor.Controls;

using Infragistics.Documents.Excel;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;

namespace BlazorApp1.Pages
{
    public partial class ExcelView : ComponentBase
    {
        [Inject]
        public IJSRuntime Runtime { get; set; }

        public bool canSave = false;
        public Workbook wb;
        public Worksheet ws;
        public List<string> worksheetRegion = null;
        public string selectedRegion = null;
        private Random Rand = new Random();

        protected override void OnInitialized()
        {
            this.WorkbookCreate();
        }

        private void CreateXls()
        {
            this.SaveFile(this.wb, "ExcelWorkbook", WorkbookFormat.Excel97To2003);
        }

        private void CreateXlsx()
        {
            this.SaveFile(this.wb, "ExcelWorkbook", WorkbookFormat.Excel2007);
        }

        public void WorkbookCreate()
        {
            Workbook.InProcessRuntime = this.Runtime as IJSInProcessRuntime;

            var wb = new Workbook(WorkbookFormat.Excel2007);
            var employeeSheet = wb.Worksheets.Add("Employees");
            var employeeHeader = employeeSheet.Rows[0];
            var companies = new string[] { "Amazon", "Ford", "Jaguar", "Tesla", "IBM", "Microsoft" };
            var firstNames = new string[] { "Andrew", "Mike", "Martin", "Ann", "Victoria", "John", "Brian", "Jason", "David" };
            var lastNames = new string[] { "Smith", "Jordan", "Johnson", "Anderson", "Louis", "Phillips", "Williams" };
            var countries = new string[] { "UK", "France", "USA", "Germany", "Poland", "Brazil" };
            var titles = new string[] { "Sales Rep.", "Engineer", "Administrator", "Manager" };
            var employeeColumns = new string[] { "Name", "Company", "Title", "Age", "Country" };
            for (var col = 0; col < employeeColumns.Length; col++)
            {
                employeeSheet.Columns[col].Width = 5000;
                employeeHeader.SetCellValue(col, employeeColumns[col]);
            }
            for (var i = 1; i < 20; i++)
            {
                var company = this.GetItem(companies);
                var title = this.GetItem(titles);
                var country = this.GetItem(countries);
                var name = this.GetItem(firstNames) + " " + this.GetItem(lastNames);
                var salary = this.GetRandom(45000, 95000);
                var age = this.GetRandom(20, 65);
                var wr = employeeSheet.Rows[i] as WorksheetRow;
                wr.SetCellValue(0, name);
                wr.SetCellValue(1, company);
                wr.SetCellValue(2, title);
                wr.SetCellValue(3, age);
                wr.SetCellValue(4, country);
                wr.SetCellValue(5, salary);
            }
            var expanseSheet = wb.Worksheets.Add("Expanses");
            var expanseHeader = expanseSheet.Rows[0];
            var expanseNames = new string[] { "Year", "Computers", "Research", "Travel", "Salary", "Software" };
            var expanseCol = 0;
            foreach (var key in expanseNames)
            {
                expanseSheet.Columns[expanseCol].Width = 5000;
                expanseHeader.SetCellValue(expanseCol, key);
                for (var i = 1; i < 20; i++)
                {
                    var wr = expanseSheet.Rows[i];
                    if (key == "Year")
                    {
                        wr.SetCellValue(expanseCol, 2010 + i);
                    }
                    else if (key == "Computers")
                    {
                        wr.SetCellValue(expanseCol, this.GetRandom(50000, 65000));
                    }
                    else if (key == "Research")
                    {
                        wr.SetCellValue(expanseCol, this.GetRandom(150000, 165000));
                    }
                    else if (key == "Travel")
                    {
                        wr.SetCellValue(expanseCol, this.GetRandom(20000, 25000));
                    }
                    else if (key == "Salary")
                    {
                        wr.SetCellValue(expanseCol, this.GetRandom(4000000, 450000));
                    }
                    else if (key == "Software")
                    {
                        wr.SetCellValue(expanseCol, this.GetRandom(100000, 150000));
                    }
                }
                expanseCol++;
            }
            var incomeSheet = wb.Worksheets.Add("Income");
            var incomeHeader = incomeSheet.Rows[0];
            var incomeNames = new string[] { "Year", "Phones", "Computers", "Software", "Services", "Royalties" };
            var incomeCol = 0;
            foreach (var key in incomeNames)
            {
                incomeSheet.Columns[incomeCol].Width = 5000;
                incomeHeader.SetCellValue(incomeCol, key);
                for (var i = 1; i < 20; i++)
                {
                    var wr = incomeSheet.Rows[i];
                    if (key == "Year")
                    {
                        wr.SetCellValue(incomeCol, 2010 + i);
                    }
                    else if (key == "Software")
                    {
                        wr.SetCellValue(incomeCol, this.GetRandom(700000, 850000));
                    }
                    else if (key == "Computers")
                    {
                        wr.SetCellValue(incomeCol, this.GetRandom(250000, 265000));
                    }
                    else if (key == "Royalties")
                    {
                        wr.SetCellValue(incomeCol, this.GetRandom(400000, 450000));
                    }
                    else if (key == "Phones")
                    {
                        wr.SetCellValue(incomeCol, this.GetRandom(6000000, 650000));
                    }
                    else if (key == "Services")
                    {
                        wr.SetCellValue(incomeCol, this.GetRandom(700000, 750000));
                    }
                }
                incomeCol++;
            }
            this.WorkbookParse(wb);
        }

        public void WorkbookParse(Workbook wb)
        {
            if (wb == null)
            {
                this.worksheetRegion = null;
                this.selectedRegion = null;
            }
            else
            {
                var names = new List<string>();
                var worksheets = wb.Worksheets;
                var wsCount = worksheets.Count;
                for (var i = 0; i < wsCount; i++)
                {
                    var tables = worksheets[i].Tables;
                    var tCount = tables.Count;
                    for (var j = 0; j < tCount; j++)
                    {
                        names.Add(worksheets[i].Name + " - " + tables[j].Name);
                    }
                }
                this.worksheetRegion = names;
                this.selectedRegion = names.Count > 0 ? names[0] : null;
            }
            this.wb = wb;
            this.canSave = wb != null;
        }

        public double GetRandom(double min, double max)
        {
            return Math.Round(min + (Rand.NextDouble() * (max - min)));
        }

        public string GetItem(string[] array)
        {
            var index = (int)Math.Round(GetRandom(0, array.Length - 1));
            return array[index];
        }

        private void SaveFile(Workbook workbook, string fileNameWithoutExtension, WorkbookFormat format)
        {
            var ms = new System.IO.MemoryStream();
            workbook.SetCurrentFormat(format);
            workbook.Save(ms);

            string extension;

            switch (workbook.CurrentFormat)
            {
                default:
                case WorkbookFormat.StrictOpenXml:
                case WorkbookFormat.Excel2007:
                    extension = ".xlsx";
                    break;
                case WorkbookFormat.Excel2007MacroEnabled:
                    extension = ".xlsm";
                    break;
                case WorkbookFormat.Excel2007MacroEnabledTemplate:
                    extension = ".xltm";
                    break;
                case WorkbookFormat.Excel2007Template:
                    extension = ".xltx";
                    break;

                case WorkbookFormat.Excel97To2003:
                    extension = ".xls";
                    break;
                case WorkbookFormat.Excel97To2003Template:
                    extension = ".xlt";
                    break;
            }

            string fileName = fileNameWithoutExtension + extension;
            string mime;

            switch (workbook.CurrentFormat)
            {
                default:
                case WorkbookFormat.Excel2007:
                case WorkbookFormat.Excel2007MacroEnabled:
                case WorkbookFormat.Excel2007MacroEnabledTemplate:
                case WorkbookFormat.Excel2007Template:
                case WorkbookFormat.StrictOpenXml:
                    mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case WorkbookFormat.Excel97To2003:
                case WorkbookFormat.Excel97To2003Template:
                    mime = "application/vnd.ms-excel";
                    break;
            }

            ms.Position = 0;
            var bytes = ms.ToArray();
            this.SaveFile(bytes, fileName, mime);
        }

        private void SaveFile(byte[] bytes, string fileName, string mime)
        {
            if (this.Runtime is WebAssemblyJSRuntime wasmRuntime)
                wasmRuntime.InvokeUnmarshalled<string, string, byte[], bool>("BlazorDownloadFileFast", fileName, mime, bytes);
            else if (this.Runtime is IJSInProcessRuntime inProc)
                inProc.InvokeVoid("BlazorDownloadFile", fileName, mime, bytes);
        }


        //InputFile에 설정된 파일을 담을 곳
        IBrowserFile _file = null;
        private void LoadFile(InputFileChangeEventArgs e)
        {
            _file = null;

            if (e.FileCount < 1)
                return;

            string fileName = e.File.Name;
            string ext = Path.GetExtension(fileName).ToLower();

            if (new string[] { ".xls", ".xlsx", ".xlsm", ".xlt" }.Contains(ext) == false)
                return;

            _file = e.File;
        }

        async Task Upload()
        {
            if (_file == null)
                return;

            using (var file = _file.OpenReadStream())
            {
                //1. 엑셀 파일 열기
                Workbook workbook;
                using (var ms = new MemoryStream((int)file.Length))
                {
                    await file.CopyToAsync(ms); //반드시 비동기로 할것
                    workbook = Workbook.Load(ms);
                }

                //2. 엑셀 시트 접근
                var workSheet = workbook.Worksheets[0];
                string sheetName = workSheet.Name;

                var cellA1Info = workSheet.GetCell("A1");
                //cellA1Info.CellFormat.Fill.BackgroundColorInfo.Color
                string cellA1 = workSheet.GetCell("A1").Value.ToString();
                string cellB2 = workSheet.Rows[1].GetCellValue(1).ToString();

                //데이터를 만들어서, httpClient로 전송 (브라우저 상에서는 xhr, fetch 로 전달)

                return;
            }
        }
    }
}

