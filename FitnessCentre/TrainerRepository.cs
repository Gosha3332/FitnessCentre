public class TrainerRepository
{
    private readonly AppDbContext _context;

    public TrainerRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(string surname, string name, string? patronimic, string phone, Status status)
    {
        _context.Trainers.Add(new Trainer(surname, name, patronimic, phone, status));
        _context.SaveChanges();
    }

    public void Update(Guid id, string surname, string name, string? patronimic, string phone, Status status)
    {
        Trainer? trainer = _context.Trainers.FirstOrDefault(c => c.Id == id);

        if (trainer == null) { throw new Exception("Тренера с подобным идентификатором не найден"); }

        trainer.Surname = surname;
        trainer.Name = name;
        trainer.Patronimic = patronimic;
        trainer.Phone = phone;
        trainer.Status = status;

        _context.SaveChanges();
    }

    public void UpdateStatus(Guid id, Status status)
    {
        Trainer? trainer = _context.Trainers.FirstOrDefault(c => c.Id == id);

        if (trainer == null) { throw new Exception("Тренера с подобным идентификатором не найден"); }

        trainer.Status = status;
        _context.SaveChanges();
    }

    public TrainerWithClientsDto ReadInfoId(Guid id)
    {
        Trainer? trainer = _context.Trainers.FirstOrDefault(c => c.Id == id);

        if (trainer == null) { throw new Exception("Тренера с подобным идентификатором не найден"); }

        List<Client> clientsForTrainer = _context.Clients.Where(c => c.TrainerId == id).ToList();

        return new TrainerWithClientsDto { Trainer = trainer, Clients = clientsForTrainer };
    }

    public List<Trainer> ReadAll() { return _context.Trainers.ToList(); }
}

public class TrainerWithClientsDto
{
    public Trainer Trainer { get; set; }
    public List<Client> Clients { get; set; } = new List<Client>();
}
