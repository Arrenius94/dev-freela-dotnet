namespace DevFreela.Application.ViewModels;

public class ProjectDetailsViewModel
{
    public ProjectDetailsViewModel(int id, string titile, string description, decimal totalCost, DateTime? startedAt, DateTime? finishedAt)
    {
        Id = id;
        Titile = titile;
        Description = description;
        TotalCost = totalCost;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }
    public int Id { get; private set; }
    public string Titile { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
}