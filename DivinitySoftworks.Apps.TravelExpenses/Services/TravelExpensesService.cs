using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.Services {

    public interface ITravelExpensesService {

        Task<MonthlyData?> LoadAsync(int year, int month);

        Task SaveAsync(MonthlyData monthlyData);

        Task ExportAsync(DateOnly date);
    }

    public class TravelExpensesService : ITravelExpensesService {
        IAppSettings _appSettings;
        IConfigurationManager _configurationManager;
        DirectoryInfo _directoryInfo;

        public TravelExpensesService(IAppSettings appSettings, IConfigurationManager configurationManager) {
            _appSettings = appSettings;
            _configurationManager = configurationManager;

            _directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Divinity Softworks", "Travel Expenses"));
            if (!_directoryInfo.Exists) _directoryInfo.Create();
        }

        public Task ExportAsync(DateOnly date) {
            return Task.Run(async () => {
                try {
                    MonthlyData? monthlyData = await LoadAsync(date.Year, date.Month);
                    if (monthlyData is null || monthlyData.Days.Count == 0) throw new Exception($"No data found for {date.ToDateTime(TimeOnly.MinValue):MMMM yyyy} to export.");

                    string? name = await _configurationManager.GetUserSettingAsync<string?>("Name") ?? default;
                    string? department = await _configurationManager.GetUserSettingAsync<string?>("Department") ?? default;
                    string? manager = await _configurationManager.GetUserSettingAsync<string?>("Manager") ?? default;
                    string? workAddress = await _configurationManager.GetUserSettingAsync<string?>("WorkAddress") ?? default;
                    string? homeAddress = await _configurationManager.GetUserSettingAsync<string?>("HomeAddress") ?? default;
                    int? kilometers = await _configurationManager.GetUserSettingAsync<int?>("Kilometers") ?? default(int?);
                    double? price = await _configurationManager.GetUserSettingAsync<double?>("Price") ?? default(double?);

                    if (name is null) throw new Exception("'Name' cannot be empty.");
                    if (department is null) throw new Exception("'Department' cannot be empty.");
                    if (manager is null) throw new Exception("'Manager' cannot be empty.");
                    if (workAddress is null) throw new Exception("'WorkAddress' cannot be empty.");
                    if (homeAddress is null) throw new Exception("'HomeAddress' cannot be empty.");
                    if (kilometers is null) throw new Exception("'Kilometers' cannot be empty.");
                    if (price is null) throw new Exception("'Price' cannot be empty.");

                    using ExcelEngine excelEngine = new();
                    excelEngine.Excel.DefaultVersion = ExcelVersion.Xlsx;

                    IWorkbook workbook = excelEngine.Excel.Workbooks.Create();

                    while (workbook.Worksheets.Count > 1)
                        workbook.Worksheets.Remove(workbook.Worksheets[0]);

                    //Access first worksheet from the workbook
                    IWorksheet worksheet = workbook.Worksheets[0];
                    worksheet.EnableSheetCalculations();
                    worksheet.Name = "Declaration (extra) commuting KM";

                    worksheet.Range["A1"].Text = "Name : ";
                    worksheet.Range["A2"].Text = "Department : ";
                    worksheet.Range["A3"].Text = "Manager : ";
                    worksheet.Range["A4"].Text = "Maand : ";
                    worksheet.Range["A1:A4"].CellStyle.Font.Bold = true;
                    worksheet.Range["A1:A4"].HorizontalAlignment = ExcelHAlign.HAlignRight;

                    worksheet.Range["B1"].Text = name;
                    worksheet.Range["B2"].Text = department;
                    worksheet.Range["B3"].Text = manager;
                    worksheet.Range["B4"].Text = date.ToDateTime(TimeOnly.MinValue).ToString("MMMM yyyy");

                    worksheet.Range["A1:E4"].BorderAround();
                    worksheet.Range["A1:E4"].BorderInside();
                    worksheet.Range["A1:B1"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range["A2:B2"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range["A3:B3"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range["A4:B4"].BorderInside(ExcelLineStyle.Double);

                    worksheet.Range["B1:E1"].Merge();
                    worksheet.Range["B2:E2"].Merge();
                    worksheet.Range["B3:E3"].Merge();
                    worksheet.Range["B4:E4"].Merge();

                    worksheet.Range["A6"].CellStyle.Font.Bold = true;
                    worksheet.Range["A6"].CellStyle.Font.Color = ExcelKnownColors.Red;
                    worksheet.Range["A6"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range["A6"].Text = "Please fill in each way.";
                    worksheet.Range["A6:E6"].Merge();

                    worksheet.Range["A8"].Text = "Date";
                    worksheet.Range["B8"].Text = "From (departure)";
                    worksheet.Range["F8"].Text = "To (destination)";
                    worksheet.Range["J8"].Text = "Number of KM";
                    worksheet.Range["A8:K8"].CellStyle.Font.Bold = true;
                    worksheet.Range["A8:K8"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    
                    worksheet.Range["B8:E8"].Merge();
                    worksheet.Range["F8:I8"].Merge();
                    worksheet.Range["J8:K8"].Merge();

                    int row = 9;

                    foreach (DateItem dateItem in monthlyData.Days) {
                        worksheet.Range[$"A{row}"].Text = $"{dateItem.Date:dd/MM/yyyy}";
                        worksheet.Range[$"B{row}"].Text = homeAddress;
                        worksheet.Range[$"F{row}"].Text = workAddress;
                        worksheet.Range[$"J{row}"].Number = (double)kilometers;

                        worksheet.Range[$"B{row}:E{row}"].Merge();
                        worksheet.Range[$"F{row}:I{row}"].Merge();
                        worksheet.Range[$"J{row}:K{row++}"].Merge();

                        worksheet.Range[$"A{row}"].Text = $"{dateItem.Date:dd/MM/yyyy}";
                        worksheet.Range[$"B{row}"].Text = workAddress;
                        worksheet.Range[$"F{row}"].Text = homeAddress;
                        worksheet.Range[$"J{row}"].Number = (double)kilometers;

                        worksheet.Range[$"B{row}:E{row}"].Merge();
                        worksheet.Range[$"F{row}:I{row}"].Merge();
                        worksheet.Range[$"J{row}:K{row++}"].Merge();
                    }

                    worksheet.Range[$"A8:K{row - 1}"].BorderAround();
                    worksheet.Range[$"A8:K{row - 1}"].BorderInside();
                    worksheet.Range[$"A8:A9"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range[$"B8:E9"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range[$"F8:I9"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range[$"J8:K9"].BorderInside(ExcelLineStyle.Double);

                    row++;

                    worksheet.Range[$"F{row}"].Text = "Total Kilometers : ";
                    worksheet.Range[$"J{row}"].Formula = $"=SUM(J9:K{row-2})";
                    worksheet.Range[$"F{row}:I{row}"].Merge();
                    worksheet.Range[$"J{row}:K{row}"].Merge();
                    row++;
                    worksheet.Range[$"F{row}"].Text = "Net price per Kilometer : ";
                    worksheet.Range[$"J{row}"].Number = price.Value;
                    worksheet.Range[$"J{row}"].NumberFormat = "€ #,##0.00";
                    worksheet.Range[$"F{row}:I{row}"].Merge();
                    worksheet.Range[$"J{row}:K{row}"].Merge();
                    row++;
                    worksheet.Range[$"F{row}"].Text = "Net total amount business KM : ";
                    worksheet.Range[$"J{row}"].Formula = $"=SUM(J{row - 2}*J{row - 1})";
                    worksheet.Range[$"J{row}"].NumberFormat = "€ #,##0.00";
                    worksheet.Range[$"F{row}:I{row}"].Merge();
                    worksheet.Range[$"J{row}:K{row}"].Merge();

                    worksheet.Range[$"F{row - 2}:K{row}"].CellStyle.Font.Bold = true;
                    worksheet.Range[$"F{row - 2}:I{row}"].HorizontalAlignment = ExcelHAlign.HAlignRight;

                    worksheet.Range[$"F{row - 2}:K{row}"].BorderAround();
                    worksheet.Range[$"F{row - 2}:K{row}"].BorderInside();
                    worksheet.Range[$"I{row - 2}:J{row - 2}"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range[$"I{row - 1}:J{row - 1}"].BorderInside(ExcelLineStyle.Double);
                    worksheet.Range[$"I{row}:J{row}"].BorderInside(ExcelLineStyle.Double);

                    row += 3;

                    worksheet.Range[$"A{row}"].Text = name;
                    worksheet.Range[$"A{row}"].HorizontalAlignment = ExcelHAlign.HAlignRight;
                    worksheet.Range[$"A{row}"].VerticalAlignment = ExcelVAlign.VAlignBottom;
                    worksheet.Range[$"A{row}:D{row + 3}"].Merge();
                    worksheet.Range[$"A{row}:D{row + 3}"].BorderAround();

                    worksheet.Range[$"F{row}"].Text = manager;
                    worksheet.Range[$"F{row}"].HorizontalAlignment = ExcelHAlign.HAlignRight;
                    worksheet.Range[$"F{row}"].VerticalAlignment = ExcelVAlign.VAlignBottom;
                    worksheet.Range[$"F{row}:I{row + 3}"].Merge();
                    worksheet.Range[$"F{row}:I{row + 3}"].BorderAround();

                    if (LoadImage() is byte[] imageData) {
                        using MemoryStream stream = new(imageData);
                        IPictureShape shape = worksheet.Pictures.AddPicture(1, 9, 7, 12, stream);
                    }

                    using FileStream fileStream = new(@$"C:\Users\m.keeman\Desktop\New folder\{DateTime.Now.ToFileTime()}.xlsx", FileMode.Create);
                    workbook.SaveAs(fileStream);
                }
                catch (Exception exception) {
                    throw;
                }
            });
        }

        /// <inheritdoc/>
        public Task<MonthlyData?> LoadAsync(int year, int month) {
            return Task.Run(() => {
                if (year < 2000 || year > 2100) return null;
                if (month < 1 || month > 12) return null;

                DirectoryInfo directoryInfo = new(Path.Combine(_directoryInfo.FullName, _appSettings.DataPath));
                if (!directoryInfo.Exists) _directoryInfo.Create();
                FileInfo fileInfo = new(Path.Combine(directoryInfo.FullName, $"{year}{month.ToString().PadLeft(2, '0')}.dsdata"));
                if (!fileInfo.Exists) return new MonthlyData(year, month);

                return JsonConvert.DeserializeObject<MonthlyData>(File.ReadAllText(fileInfo.FullName));
            });
        }

        /// <inheritdoc/>
        public Task SaveAsync(MonthlyData monthlyData) {
            return Task.Run(() => {
                DirectoryInfo directoryInfo = new(Path.Combine(_directoryInfo.FullName, _appSettings.DataPath));
                if (!directoryInfo.Exists) directoryInfo.Create();
                FileInfo fileInfo = new(Path.Combine(directoryInfo.FullName, $"{monthlyData.Year}{monthlyData.Month.ToString().PadLeft(2, '0')}.dsdata"));

                File.WriteAllText(fileInfo.FullName, JsonConvert.SerializeObject(monthlyData, Formatting.Indented));
            });
        }

        private byte[]? LoadImage() {
            if (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) is not string directory)
                return default;
            
            FileInfo imageFile = new(Path.Combine(directory, "Resources", "Company Logo.jpg"));
            if (!imageFile.Exists) 
                return default;

            return File.ReadAllBytes(imageFile.FullName);
        }
    }
}
