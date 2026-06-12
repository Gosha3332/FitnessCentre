using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/trainers")]
public class TrainersController : ControllerBase
{
    private readonly TrainerRepository _trainerRepository;
    private readonly ClientRepository _clientRepository;

    public TrainersController(TrainerRepository trainerRepository, ClientRepository clientRepository)
    {
        _trainerRepository = trainerRepository;
        _clientRepository = clientRepository;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTrainerDto dto)
    {
        try
        {
            _trainerRepository.Create(dto.Surname, dto.Name, dto.Patronimic, dto.Phone, dto.Status);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateTrainerDto dto)
    {
        try
        {
            _trainerRepository.Update(id, dto.Surname, dto.Name, dto.Patronimic, dto.Phone, dto.Status);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(Guid id, [FromBody] UpdateTrainerStatusDto dto)
    {
        try
        {
            _trainerRepository.UpdateStatus(id, dto.Status);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/detail")]
    public IActionResult GetDetailById(Guid id)
    {
        try
        {
            return Ok(_trainerRepository.ReadInfoId(id, _clientRepository));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_trainerRepository.ReadAll());
    }
}

public class CreateTrainerDto
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronimic { get; set; }
    public string Phone { get; set; }
    public Status Status { get; set; }
}

public class UpdateTrainerDto
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronimic { get; set; }
    public string Phone { get; set; }
    public Status Status { get; set; }
}

public class UpdateTrainerStatusDto
{
    public Status Status { get; set; }
}
