using System.IO;
using System.Text;
using System.Text.Json;
using pract10.Domain.Entites;

namespace pract10.Domain;

public abstract class ExportCreator
{
    public abstract IExportToFile CreateExportToFile();

    public void Export(List<Credits> credits)
    {
        IExportToFile file = CreateExportToFile();
        file.ExportToFile(credits);
    }
}

public class CsvExportCreator : ExportCreator
{
    public override IExportToFile CreateExportToFile()
    {
        return new CsvExportService();
    }
}

public class JsonExportCreator : ExportCreator
{
    public override IExportToFile CreateExportToFile()
    {
        return new JsonExportService();
    }
}

public class CsvExportService : IExportToFile
{
    private const string Format = ".csv";
    public void ExportToFile(List<Credits> credits)
    {
        var csv = new StringBuilder();

        csv.AppendLine("Категория;Сумма;Дата;Описание");

        foreach (var credit in credits)
        {
            csv.AppendLine(string.Join(";",
                Escape(credit.Category),
                Escape(credit.Sum),
                Escape(credit.Date.ToString("dd.MM.yyyy")),
                Escape(credit.Description)
            ));
        }

        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "credits" + Format);

        File.WriteAllText(path, csv.ToString(), new UTF8Encoding(true));
    }
    
    private static string Escape(object? value)
    {
        string text = value?.ToString() ?? "";

        return "\"" + text.Replace("\"", "\"\"") + "\"";
    }
}

public class JsonExportService : IExportToFile
{
    private const string Format = ".json";

    public void ExportToFile(List<Credits> credits)
    {
        string json = JsonSerializer.Serialize(
            credits,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "credits" + Format);

        File.WriteAllText(path, json, new UTF8Encoding(true));
    }
}