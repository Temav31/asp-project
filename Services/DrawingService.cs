// Сервис для генерации SVG чертежей поковки
using asp_project.Models;

namespace asp_project.Services;

public class DrawingService : IDrawingService
{
    public (string withoutSample, string withSample) GenerateDrawings(CalculationResult result)
    {
        var withoutSampleSvg = GenerateDrawing(result, false);
        var withSampleSvg = GenerateDrawing(result, true);
        
        return (withoutSampleSvg, withSampleSvg);
    }
    
    private string GenerateDrawing(CalculationResult result, bool withSample)
    {
        double outerDiameter, innerDiameter, height, outerTolerance, innerTolerance, heightTolerance;
        double nominalMass, maxMass;
        string title;
        
        outerDiameter = result.Input.D;
        innerDiameter = result.Input.InnerDiameter;
        height = result.Input.H;
        
        if (withSample)
        {
            outerTolerance = result.Beta2;
            innerTolerance = result.Beta2; 
            heightTolerance = result.Beta2;
            nominalMass = result.DiskWithSampleNominalMassWithHole;
            maxMass = result.DiskWithSampleMaxMassWithHole;
            title = "Поковка кольцо раскатное (С ПРОБОЙ)";
        }
        else
        {
            outerTolerance = result.Beta1;
            innerTolerance = result.Beta1;
            heightTolerance = result.Beta1;
            nominalMass = result.DiskNominalMassWithHole;
            maxMass = result.DiskMaxMassWithHole;
            title = "Поковка кольцо раскатное (БЕЗ ПРОБЫ)";
        }

        var svg = $@"<svg width=""900"" height=""540"" xmlns=""http://www.w3.org/2000/svg"">
  <defs>
    <marker id=""arrowhead-left"" markerWidth=""10"" markerHeight=""7"" refX=""1"" refY=""3.5"" orient=""auto"">
      <polygon points=""10 0, 0 3.5, 10 7"" fill=""#000"" />
    </marker>
    <marker id=""arrowhead-right"" markerWidth=""10"" markerHeight=""7"" refX=""9"" refY=""3.5"" orient=""auto"">
      <polygon points=""0 0, 10 3.5, 0 7"" fill=""#000"" />
    </marker>
  </defs>
  
  <rect width=""900"" height=""540"" fill=""white""/>
  
  <!-- Заголовок -->
  <text x=""20"" y=""25"" font-family=""Arial"" font-size=""12"" font-weight=""bold"">Сверху - размеры детали</text>
  <text x=""20"" y=""40"" font-family=""Arial"" font-size=""12"" font-weight=""bold"">Снизу - размеры заготовки</text>
  
  <text x=""600"" y=""25"" font-family=""Arial"" font-size=""12"">Масса начальная {nominalMass:F3} тонны</text>
  <text x=""600"" y=""40"" font-family=""Arial"" font-size=""12"">Масса максимал {maxMass:F3} тонны</text>
  
  <!-- Основная схема поковки -->
  <g transform=""translate(150, 80)"">
    
    <!-- Основные контуры поковки -->
    <rect height=""79"" width=""500"" y=""115"" x=""50"" stroke=""#000"" fill=""#fff"" stroke-width=""2""/>
    <rect height=""66"" width=""488"" y=""120"" x=""56"" stroke=""#000"" fill=""#fff"" stroke-width=""2""/>
    
    <!-- Центральная ось -->
    <line stroke=""#000"" stroke-dasharray=""5,5"" y2=""220"" x2=""300"" y1=""60"" x1=""300"" fill=""none"" stroke-width=""1""/>
    
    <!-- Левая и правая части с штриховкой -->
    <rect height=""65"" width=""156"" y=""120"" x=""56"" stroke=""#000"" fill=""none"" stroke-width=""2""/>
    <rect height=""65"" width=""156"" y=""120"" x=""388"" stroke=""#000"" fill=""none"" stroke-width=""2""/>
    
    <!-- Штриховка левой части - диагональные линии по рамке главного элемента -->
    <line y2=""115"" x2=""65"" y1=""194"" x1=""50"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""78"" y1=""194"" x1=""63"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""91"" y1=""194"" x1=""76"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""104"" y1=""194"" x1=""89"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""117"" y1=""194"" x1=""102"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""130"" y1=""194"" x1=""115"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""143"" y1=""194"" x1=""128"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""156"" y1=""194"" x1=""141"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""169"" y1=""194"" x1=""154"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""182"" y1=""194"" x1=""167"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""195"" y1=""194"" x1=""180"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""208"" y1=""194"" x1=""193"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    
    <!-- Штриховка правой части - диагональные линии по рамке главного элемента -->
    <line y2=""115"" x2=""403"" y1=""194"" x1=""388"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""416"" y1=""194"" x1=""401"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""429"" y1=""194"" x1=""414"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""442"" y1=""194"" x1=""427"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""455"" y1=""194"" x1=""440"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""468"" y1=""194"" x1=""453"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""481"" y1=""194"" x1=""466"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""494"" y1=""194"" x1=""479"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""507"" y1=""194"" x1=""492"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""520"" y1=""194"" x1=""505"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""533"" y1=""194"" x1=""518"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    <line y2=""115"" x2=""546"" y1=""194"" x1=""531"" stroke=""#000"" fill=""none"" stroke-width=""0.8""/>
    
    <!-- Размерные линии отодвинутые от чертежа -->
    
    <!-- Наружный диаметр - сверху -->
    <line y1=""115"" x1=""50"" y2=""80"" x2=""50"" stroke=""#000"" fill=""none"" stroke-width=""1""/>
    <line y1=""115"" x1=""550"" y2=""80"" x2=""550"" stroke=""#000"" fill=""none"" stroke-width=""1""/>
    <line y1=""80"" x1=""50"" y2=""80"" x2=""550"" stroke=""#000"" fill=""none"" stroke-width=""1"" marker-end=""url(#arrowhead-right)"" marker-start=""url(#arrowhead-left)""/>
    
    <!-- Текст наружного диаметра - как в примере -->
    <text x=""300"" y=""55"" text-anchor=""middle"" font-family=""Arial"" font-size=""14"" font-weight=""bold"">Ø{outerDiameter:F0}</text>
    <text x=""300"" y=""65"" text-anchor=""middle"" font-family=""Arial"" font-size=""10"">{(withSample ? result.DBlankWithSample : result.DBlank):F0}+-{(withSample ? result.DBlankWithSampleTolerance : result.DBlankTolerance):F0}</text>
    
    <!-- Внутренний диаметр - снизу -->
    <line y1=""194"" x1=""212"" y2=""230"" x2=""212"" stroke=""#000"" fill=""none"" stroke-width=""1""/>
    <line y1=""194"" x1=""388"" y2=""230"" x2=""388"" stroke=""#000"" fill=""none"" stroke-width=""1""/>
    <line y1=""230"" x1=""212"" y2=""230"" x2=""388"" stroke=""#000"" fill=""none"" stroke-width=""1"" marker-end=""url(#arrowhead-right)"" marker-start=""url(#arrowhead-left)""/>
    
    <!-- Текст внутреннего диаметра - как в примере -->
    <text x=""300"" y=""240"" text-anchor=""middle"" font-family=""Arial"" font-size=""14"" font-weight=""bold"">Ø{innerDiameter:F0}</text>
    <text x=""300"" y=""250"" text-anchor=""middle"" font-family=""Arial"" font-size=""10"">{(withSample ? result.dBlankWithSample : result.dBlank):F0}+-{(withSample ? result.dBlankWithSampleTolerance : result.dBlankTolerance):F0}</text>
    
    <!-- Высота - справа -->
    <line y1=""115"" x1=""550"" y2=""115"" x2=""580"" stroke=""#000"" fill=""none"" stroke-width=""1""/>
    <line y1=""194"" x1=""550"" y2=""194"" x2=""580"" stroke=""#000"" fill=""none"" stroke-width=""1""/>
    <line y1=""115"" x1=""580"" y2=""194"" x2=""580"" stroke=""#000"" fill=""none"" stroke-width=""1"" marker-end=""url(#arrowhead-right)"" marker-start=""url(#arrowhead-left)""/>
    
    <!-- Текст высоты - как в примере -->
    <text x=""595"" y=""140"" text-anchor=""start"" font-family=""Arial"" font-size=""14"" font-weight=""bold"">{height:F0}</text>
    <text x=""595"" y=""150"" text-anchor=""start"" font-family=""Arial"" font-size=""10"">{(withSample ? result.HBlankWithSample : result.HBlank):F0}</text>
    
  </g>
  
  <!-- Дополнительная информация -->
  <g transform=""translate(50, 400)"">
    <text x=""0"" y=""20"" font-family=""Arial"" font-size=""12"">D дет = {result.DDetail:F1} мм</text>
    <text x=""0"" y=""35"" font-family=""Arial"" font-size=""12"">d дет = {result.dDetail:F1} мм</text>
    <text x=""0"" y=""50"" font-family=""Arial"" font-size=""12"">H дет = {result.Input.H:F1} мм</text>
    
    <text x=""250"" y=""20"" font-family=""Arial"" font-size=""12"">β = {(withSample ? result.Beta2 : result.Beta1):F0}</text>
    <text x=""250"" y=""35"" font-family=""Arial"" font-size=""12"">±Δ/2 = {(withSample ? result.Delta2 : result.Delta1):F0}</text>
    <text x=""250"" y=""50"" font-family=""Arial"" font-size=""12"">Плотность стали = {result.Input.SteelDensity:F2} т/м³</text>
    
    <text x=""500"" y=""20"" font-family=""Arial"" font-size=""12"" font-weight=""bold"">{title}</text>
    <text x=""500"" y=""35"" font-family=""Arial"" font-size=""12"">Номинальная масса: {nominalMass:F3} т</text>
    <text x=""500"" y=""50"" font-family=""Arial"" font-size=""12"">Максимальная масса: {maxMass:F3} т</text>
  </g>
  
  <!-- Итоговые размеры заготовки -->
  <g transform=""translate(50, 470)"">
    <text x=""10"" y=""18"" font-family=""Arial"" font-size=""12"" font-weight=""bold"">Размеры заготовки {(withSample ? "(с пробой)" : "(без пробы)")}: D: {outerDiameter:F1}±{outerTolerance:F1} мм | d: {innerDiameter:F1}±{innerTolerance:F1} мм | H: {height:F1}±{heightTolerance:F1} мм</text>
    <text x=""10"" y=""32"" font-family=""Arial"" font-size=""12"">
      Масса поковки: номинальная {nominalMass:F3} т, максимальная {maxMass:F3} т
    </text>
  </g>
</svg>";

        return svg;
    }
}