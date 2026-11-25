using System.Collections.ObjectModel;
using pract10.Domain.Entites;

namespace pract10.Domain;

public sealed class CreditsList
{
    public static CreditsList Instance { get; } = new();

    public ObservableCollection<Credits> Credits { get; } = new();

    public ObservableCollection<Credits> GetFilteredCredits(string text) =>
        new(Credits.Where(x =>
            x.Category.Contains(
                text,
                StringComparison.OrdinalIgnoreCase)));

    public string GetSums => $"Общая сумма: {Credits.Select(x => x.Sum).Sum()}";

    private CreditsList()
    {
    }
}