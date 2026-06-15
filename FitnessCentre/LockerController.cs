using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/lockers")]
public class LockerController : ControllerBase
{
    private readonly LockerRepository _repository;

    public LockerController(LockerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var lockers = _repository.ReadLockers();
        return Ok(lockers);
    }

    [HttpPost]
    public IActionResult AddLocker([FromBody] AddLockerRequest request)
    {
        _repository.AddLocker(request.Number, request.ClientId);
        return Ok();
    }
}

public class AddLockerRequest
{
    public int Number { get; set; }
    public Guid? ClientId { get; set; }
}