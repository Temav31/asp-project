// Модель входных данных для расчета поковки
using System.ComponentModel.DataAnnotations;

namespace asp_project.Models;

public class CalculationInputModel
{
    [Display(Name = "D - Наружный диаметр детали, мм")]
    [Range(1, 10000, ErrorMessage = "Значение должно быть от 1 до 10000")]
    public double D { get; set; }

    [Display(Name = "d - Внутренний диаметр детали, мм")]
    [Range(1, 10000, ErrorMessage = "Значение должно быть от 1 до 10000")]
    public double InnerDiameter { get; set; }

    [Display(Name = "H - Высота детали, мм")]
    [Range(1, 10000, ErrorMessage = "Значение должно быть от 1 до 10000")]
    public double H { get; set; }

    [Display(Name = "X - Количество деталей в поковке")]
    [Range(1, 100, ErrorMessage = "Значение должно быть от 1 до 100")]
    public int X { get; set; }

    [Display(Name = "Y - Длина реза, мм")]
    [Range(0, 1000, ErrorMessage = "Значение должно быть от 0 до 1000")]
    public double Y { get; set; }

    [Display(Name = "Z - Количество резов (обычно X-1)")]
    [Range(0, 100, ErrorMessage = "Значение должно быть от 0 до 100")]
    public int Z { get; set; }

    [Display(Name = "Q - Напуск на пробу, мм")]
    [Range(0, 1000, ErrorMessage = "Значение должно быть от 0 до 1000")]
    public double Q { get; set; }

    [Display(Name = "Напуск на термообработку, мм")]
    [Range(0, 1000, ErrorMessage = "Значение должно быть от 0 до 1000")]
    public double ThermalTreatmentAllowance { get; set; }

    [Display(Name = "Удельный вес стали, г/см³")]
    [Range(0.1, 20, ErrorMessage = "Значение должно быть от 0.1 до 20")]
    public double SteelDensity { get; set; }
}

