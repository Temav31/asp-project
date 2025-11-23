// Интерфейс сервиса генерации чертежей
using asp_project.Models;

namespace asp_project.Services;

public interface IDrawingService
{
    (string withoutSample, string withSample) GenerateDrawings(CalculationResult result);
}

