// Контроллер для обработки запросов расчета поковок
using Microsoft.AspNetCore.Mvc;
using asp_project.Models;
using asp_project.Models.ViewModels;
using asp_project.Services;

namespace asp_project.Controllers;

public class CalculationController : Controller
{
    private readonly ICalculationService _calculationService;
    private readonly IDrawingService _drawingService;
    private readonly ILogger<CalculationController> _logger;

    public CalculationController(
        ICalculationService calculationService,
        IDrawingService drawingService,
        ILogger<CalculationController> logger)
    {
        _calculationService = calculationService;
        _drawingService = drawingService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new CalculationInputModel
        {
            D = 1360,
            InnerDiameter = 540,
            H = 215,
            X = 2,
            Y = 20,
            Z = 1,
            Q = 60,
            ThermalTreatmentAllowance = 0,
            SteelDensity = 7.85
        };
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Calculate(CalculationInputModel model)
    {
        if (!ModelState.IsValid)
        {
            if (model.D == 0) model.D = 1360;
            if (model.InnerDiameter == 0) model.InnerDiameter = 540;
            if (model.H == 0) model.H = 215;
            if (model.X == 0) model.X = 2;
            if (model.Y == 0) model.Y = 20;
            if (model.Z == 0) model.Z = 1;
            if (model.Q == 0) model.Q = 60;
            if (model.ThermalTreatmentAllowance == 0) model.ThermalTreatmentAllowance = 0;
            if (model.SteelDensity == 0) model.SteelDensity = 7.85;
            
            return View("Index", model);
        }

        try
        {
            var result = _calculationService.Calculate(model);
            var (drawingWithoutSample, drawingWithSample) = _drawingService.GenerateDrawings(result);

            var jsonOptions = new System.Text.Json.JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                WriteIndented = false
            };
            var resultJson = System.Text.Json.JsonSerializer.Serialize(result, jsonOptions);
            
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("Result", resultJson);
            HttpContext.Session.SetString("DrawingWithoutSample", drawingWithoutSample);
            HttpContext.Session.SetString("DrawingWithSample", drawingWithSample);
            await HttpContext.Session.CommitAsync();

            return RedirectToAction("Result");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при выполнении расчета");
            ModelState.AddModelError("", $"Произошла ошибка при расчете: {ex.Message}");
            return View("Index", model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Result()
    {
        CalculationResult? result = null;
        string drawingWithoutSample = string.Empty;
        string drawingWithSample = string.Empty;

        await HttpContext.Session.LoadAsync();
        var resultJson = HttpContext.Session.GetString("Result");
        if (!string.IsNullOrEmpty(resultJson))
        {
            try
            {
                var jsonOptions = new System.Text.Json.JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                };
                
                result = System.Text.Json.JsonSerializer.Deserialize<CalculationResult>(resultJson, jsonOptions);
                drawingWithoutSample = HttpContext.Session.GetString("DrawingWithoutSample") ?? string.Empty;
                drawingWithSample = HttpContext.Session.GetString("DrawingWithSample") ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отображении результатов");
            }
        }

        var viewModel = new ResultViewModel
        {
            Result = result ?? new CalculationResult(),
            DrawingSvg = drawingWithoutSample,
            DrawingWithoutSample = drawingWithoutSample,
            DrawingWithSample = drawingWithSample
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }
}

