// ViewModel для отображения результатов расчета
namespace asp_project.Models.ViewModels;

public class ResultViewModel
{
    public CalculationResult Result { get; set; } = new();
    public string DrawingSvg { get; set; } = string.Empty;
    public string DrawingWithoutSample { get; set; } = string.Empty;
    public string DrawingWithSample { get; set; } = string.Empty;
}

