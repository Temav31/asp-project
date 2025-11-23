// Сервис для расчета параметров поковки (размеры, допуски, масса)
using asp_project.Models;

namespace asp_project.Services;

public class CalculationService : ICalculationService
{
    private readonly ILogger<CalculationService> _logger;
    private readonly IConfiguration _configuration;

    public CalculationService(ILogger<CalculationService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public CalculationResult Calculate(CalculationInputModel input)
    {
        const double PI = 3.14;
        
        var result = new CalculationResult
        {
            Input = input
        };

        try
        {
            result.H1 = input.H * input.X + (input.Y * input.Z) + input.ThermalTreatmentAllowance;
            result.H2 = input.H * input.X + (input.Y * input.Z) + input.Q + input.ThermalTreatmentAllowance;

            result.DDetail = input.D + input.ThermalTreatmentAllowance;
            result.dDetail = input.InnerDiameter - input.ThermalTreatmentAllowance;

            double betaD = GetToleranceCoefficient(input.D);
            double deltaD = betaD / 2.0;
            
            double betad = GetToleranceCoefficient(input.InnerDiameter);
            double deltad = betad / 2.0;
            
            double betaH1 = GetHeightToleranceCoefficient(result.H1);
            double deltaH1 = betaH1 / 2.0;
            
            double betaH2 = GetHeightToleranceCoefficient(result.H2);
            double deltaH2 = betaH2 / 2.0;

            result.Beta1 = betaH1;
            result.Delta1 = deltaH1;
            result.Beta2 = betaH2;
            result.Delta2 = deltaH2;

            result.HBlank = result.H1;
            result.HBlankTolerance = deltaH1;
            result.DBlank = input.D + betaD;
            result.DBlankTolerance = deltaD;
            result.dBlank = input.InnerDiameter - betad;
            result.dBlankTolerance = deltad;

            result.HBlankWithSample = result.H2;
            result.HBlankWithSampleTolerance = deltaH2;
            result.DBlankWithSample = input.D + betaD;
            result.DBlankWithSampleTolerance = deltaD;
            result.dBlankWithSample = input.InnerDiameter - betad;
            result.dBlankWithSampleTolerance = deltad;

            CalculateMass(result, input, betaH1, deltaH1, false, PI);
            CalculateMass(result, input, betaH2, deltaH2, true, PI);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при расчете поковки");
            throw;
        }
    }

    private void CalculateMass(CalculationResult result, CalculationInputModel input, double beta, double delta, bool withSample, double PI)
    {
        double density = input.SteelDensity * 1e-6;

        if (!withSample)
        {
            double HNominal = result.HBlank;
            double DNominal = result.DBlank;
            double dNominal = result.dBlank;
            
            double rOuterNominal = DNominal / 2.0;
            double rInnerNominal = dNominal / 2.0;
            
            double volumeFullDiskNominal = PI * rOuterNominal * rOuterNominal * HNominal;
            double massFullDiskNominal = volumeFullDiskNominal * density / 1000.0;
            
            result.DiskNominalD = DNominal;
            result.DiskNominalR = rOuterNominal;
            result.DiskNominalH = HNominal;
            result.DiskNominalV = volumeFullDiskNominal;
            result.DiskNominalMass = massFullDiskNominal;
            
            double volumeHoleNominal = PI * rInnerNominal * rInnerNominal * HNominal;
            double massHoleNominal = volumeHoleNominal * density / 1000.0;
            
            result.HoleNominalD = dNominal;
            result.HoleNominalR = rInnerNominal;
            result.HoleNominalH = HNominal;
            result.HoleNominalV = volumeHoleNominal;
            result.HoleNominalMass = massHoleNominal;
            
            result.DiskNominalMassWithHole = massFullDiskNominal - massHoleNominal;

            double HMax = result.HBlank + result.HBlankTolerance;
            double DMax = result.DBlank + result.DBlankTolerance;
            double dMax = result.dBlank - result.dBlankTolerance;
            
            double rOuterMax = DMax / 2.0;
            double rInnerMax = dMax / 2.0;
            
            double volumeFullDiskMax = PI * rOuterMax * rOuterMax * HMax;
            double massFullDiskMax = volumeFullDiskMax * density / 1000.0;
            
            result.DiskMaxD = DMax;
            result.DiskMaxR = rOuterMax;
            result.DiskMaxH = HMax;
            result.DiskMaxV = volumeFullDiskMax;
            result.DiskMaxMass = massFullDiskMax;
            
            double volumeHoleMax = PI * rInnerMax * rInnerMax * HMax;
            double massHoleMax = volumeHoleMax * density / 1000.0;
            
            result.HoleMaxD = dMax;
            result.HoleMaxR = rInnerMax;
            result.HoleMaxH = HMax;
            result.HoleMaxV = volumeHoleMax;
            result.HoleMaxMass = massHoleMax;
            
            result.DiskMaxMassWithHole = massFullDiskMax - massHoleMax;
        }
        else
        {
            double HNominal = result.HBlankWithSample;
            double DNominal = result.DBlankWithSample;
            double dNominal = result.dBlankWithSample;
            
            double rOuterNominal = DNominal / 2.0;
            double rInnerNominal = dNominal / 2.0;
            
            double volumeFullDiskNominal = PI * rOuterNominal * rOuterNominal * HNominal;
            double massFullDiskNominal = volumeFullDiskNominal * density / 1000.0;
            
            result.DiskWithSampleNominalD = DNominal;
            result.DiskWithSampleNominalR = rOuterNominal;
            result.DiskWithSampleNominalH = HNominal;
            result.DiskWithSampleNominalV = volumeFullDiskNominal;
            result.DiskWithSampleNominalMass = massFullDiskNominal;
            
            double volumeHoleNominal = PI * rInnerNominal * rInnerNominal * HNominal;
            double massHoleNominal = volumeHoleNominal * density / 1000.0;
            
            result.HoleWithSampleNominalD = dNominal;
            result.HoleWithSampleNominalR = rInnerNominal;
            result.HoleWithSampleNominalH = HNominal;
            result.HoleWithSampleNominalV = volumeHoleNominal;
            result.HoleWithSampleNominalMass = massHoleNominal;
            
            result.DiskWithSampleNominalMassWithHole = massFullDiskNominal - massHoleNominal;

            double HMax = result.HBlankWithSample + result.HBlankWithSampleTolerance;
            double DMax = result.DBlankWithSample + result.DBlankWithSampleTolerance;
            double dMax = result.dBlankWithSample - result.dBlankWithSampleTolerance;
            
            double rOuterMax = DMax / 2.0;
            double rInnerMax = dMax / 2.0;
            
            double volumeFullDiskMax = PI * rOuterMax * rOuterMax * HMax;
            double massFullDiskMax = volumeFullDiskMax * density / 1000.0;
            
            result.DiskWithSampleMaxD = DMax;
            result.DiskWithSampleMaxR = rOuterMax;
            result.DiskWithSampleMaxH = HMax;
            result.DiskWithSampleMaxV = volumeFullDiskMax;
            result.DiskWithSampleMaxMass = massFullDiskMax;
            
            double volumeHoleMax = PI * rInnerMax * rInnerMax * HMax;
            double massHoleMax = volumeHoleMax * density / 1000.0;
            
            result.HoleWithSampleMaxD = dMax;
            result.HoleWithSampleMaxR = rInnerMax;
            result.HoleWithSampleMaxH = HMax;
            result.HoleWithSampleMaxV = volumeHoleMax;
            result.HoleWithSampleMaxMass = massHoleMax;
            
            result.DiskWithSampleMaxMassWithHole = massFullDiskMax - massHoleMax;
        }
    }

    private double GetToleranceCoefficient(double diameter)
    {
        if (diameter <= 500) return 1.0;
        if (diameter <= 800) return 1.3;
        if (diameter <= 1250) return 1.5;
        if (diameter <= 2000) return 1.8;
        if (diameter <= 3150) return 2.2;
        return 2.5;
    }

    private double GetHeightToleranceCoefficient(double height)
    {
        if (height <= 100) return 1.0;
        if (height <= 160) return 1.2;
        if (height <= 250) return 1.4;
        if (height <= 400) return 1.6;
        if (height <= 630) return 1.8;
        return 2.0;
    }
}