using pract10.Domain.Entites;

namespace pract10.Domain;

public interface IExportToFile
{
    void ExportToFile(List<Credits> credits);
}