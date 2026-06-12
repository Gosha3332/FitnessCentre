using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly ClientRepository _clientRepository;
    private readonly TrainerRepository _trainerRepository;

    public ClientsController(ClientRepository clientRepository, TrainerRepository trainerRepository)
    {
        _clientRepository = clientRepository;
        _trainerRepository = trainerRepository;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateClientDto dto)
    {
        try
        {
            _clientRepository.Create(dto.Surname, dto.Name, dto.Patronymic, dto.Birthday, dto.Phone, dto.Email, dto.IsActive, dto.TrainerId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateClientDto dto)
    {
        try
        {
            _clientRepository.Update(id, dto.Surname, dto.Name, dto.Patronymic, dto.Birthday, dto.Phone, dto.Email, dto.IsActive, dto.TrainerId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_clientRepository.ReadAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_clientRepository.ReadInfoId(id));
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
            return Ok(_clientRepository.ReadDeatilInfoId(id, _trainerRepository));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id}/status")]
    public IActionResult SetStatus(Guid id, [FromBody] SetStatusDto dto)
    {
        try
        {
            _clientRepository.ActiveOrDeactiveClient(id, dto.IsActive);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{clientId}/trainer/{trainerId}")]
    public IActionResult AssignTrainer(Guid clientId, Guid trainerId)
    {
        try
        {
            _clientRepository.AddTreinerForClient(clientId, trainerId, _trainerRepository);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}

public class CreateClientDto
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? TrainerId { get; set; }
}

public class UpdateClientDto
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public Guid? TrainerId { get; set; }
}

public class SetStatusDto
{
    public bool IsActive { get; set; }
}
