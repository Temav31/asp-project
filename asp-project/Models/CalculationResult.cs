// Модель результатов расчета поковки
namespace asp_project.Models;

public class CalculationResult
{
    public double H1 { get; set; }
    public double H2 { get; set; }
    
    public double Beta1 { get; set; }
    public double Delta1 { get; set; }
    public double Beta2 { get; set; }
    public double Delta2 { get; set; }
    
    public double DDetail { get; set; }
    public double dDetail { get; set; }

    public double HBlank { get; set; }
    public double HBlankTolerance { get; set; }
    public double DBlank { get; set; }
    public double DBlankTolerance { get; set; }
    public double dBlank { get; set; }
    public double dBlankTolerance { get; set; }

    public double HBlankWithSample { get; set; }
    public double HBlankWithSampleTolerance { get; set; }
    public double DBlankWithSample { get; set; }
    public double DBlankWithSampleTolerance { get; set; }
    public double dBlankWithSample { get; set; }
    public double dBlankWithSampleTolerance { get; set; }

    public double DiskNominalD { get; set; }
    public double DiskNominalR { get; set; }
    public double DiskNominalH { get; set; }
    public double DiskNominalV { get; set; }
    public double DiskNominalMass { get; set; }
    public double DiskNominalMassWithHole { get; set; }
    
    public double DiskMaxD { get; set; }
    public double DiskMaxR { get; set; }
    public double DiskMaxH { get; set; }
    public double DiskMaxV { get; set; }
    public double DiskMaxMass { get; set; }
    public double DiskMaxMassWithHole { get; set; }
    
    public double HoleNominalD { get; set; }
    public double HoleNominalR { get; set; }
    public double HoleNominalH { get; set; }
    public double HoleNominalV { get; set; }
    public double HoleNominalMass { get; set; }
    
    public double HoleMaxD { get; set; }
    public double HoleMaxR { get; set; }
    public double HoleMaxH { get; set; }
    public double HoleMaxV { get; set; }
    public double HoleMaxMass { get; set; }

    public double DiskWithSampleNominalD { get; set; }
    public double DiskWithSampleNominalR { get; set; }
    public double DiskWithSampleNominalH { get; set; }
    public double DiskWithSampleNominalV { get; set; }
    public double DiskWithSampleNominalMass { get; set; }
    public double DiskWithSampleNominalMassWithHole { get; set; }
    
    public double DiskWithSampleMaxD { get; set; }
    public double DiskWithSampleMaxR { get; set; }
    public double DiskWithSampleMaxH { get; set; }
    public double DiskWithSampleMaxV { get; set; }
    public double DiskWithSampleMaxMass { get; set; }
    public double DiskWithSampleMaxMassWithHole { get; set; }
    
    public double HoleWithSampleNominalD { get; set; }
    public double HoleWithSampleNominalR { get; set; }
    public double HoleWithSampleNominalH { get; set; }
    public double HoleWithSampleNominalV { get; set; }
    public double HoleWithSampleNominalMass { get; set; }
    
    public double HoleWithSampleMaxD { get; set; }
    public double HoleWithSampleMaxR { get; set; }
    public double HoleWithSampleMaxH { get; set; }
    public double HoleWithSampleMaxV { get; set; }
    public double HoleWithSampleMaxMass { get; set; }

    public double MassNominal => DiskNominalMass;
    public double MassMax => DiskMaxMass;
    public double MassNominalWithHole => DiskNominalMassWithHole;
    public double MassMaxWithHole => DiskMaxMassWithHole;
    public double MassWithSampleNominal => DiskWithSampleNominalMass;
    public double MassWithSampleMax => DiskWithSampleMaxMass;
    public double MassWithSampleNominalWithHole => DiskWithSampleNominalMassWithHole;
    public double MassWithSampleMaxWithHole => DiskWithSampleMaxMassWithHole;

    public CalculationInputModel Input { get; set; } = new();
}

