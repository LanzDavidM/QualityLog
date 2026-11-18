using Microsoft.AspNetCore.Mvc;

namespace QualityLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InspectionsController : ControllerBase
{
    private static readonly List<Inspection> _inspections =
    [
        new("Summer Air", new DateOnly(2026,07,04), Shift.First, "Heat Miser", InspectionResult.Fail, "Cold") { Id = 1 },
        new("Blinker Fluid", new DateOnly(2026, 09, 14), Shift.Second, "John Brown", InspectionResult.Pass, null) { Id = 2 },
        new("Winter Air", new DateOnly(2026,11,14), Shift.Third, "Snow Miser", InspectionResult.Rework, "Low PSI") { Id = 3 }
    ];
    
    [HttpGet]
    public ActionResult<IReadOnlyList<Inspection>>GetAll()
    {
        return Ok(_inspections);
    }
}
