// Интерфейс сервиса расчета поковок
using asp_project.Models;

namespace asp_project.Services;

public interface ICalculationService
{
    CalculationResult Calculate(CalculationInputModel input);
}

