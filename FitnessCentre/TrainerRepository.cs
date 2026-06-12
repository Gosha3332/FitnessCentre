public class TrainerRepository
{
    private List<Trainer> _treiners = new List<Trainer>();

    public void Create(string surname, string name, string? patronimic, string phone, Status status)
    {
        _treiners.Add(new Trainer(surname, name, patronimic, phone, status));
    }

    public void Update(Guid id, string surname, string name, string? patronimic, string phone, Status status)
    {
        Trainer? trainer = _treiners.FirstOrDefault(c => c.Id == id);

        if (trainer == null) { throw new Exception("Тренера с подобным идентификатором не найден"); }

        trainer.Surname = surname;
        trainer.Name = name;
        trainer.Patronimic = patronimic;
        trainer.Phone = phone;
        trainer.Status = status;
    }

    public void UpdateStatus(Guid id, Status status)
    {
        Trainer? trainer = _treiners.FirstOrDefault(c => c.Id == id);

        if (trainer == null) { throw new Exception("Тренера с подобным идентификатором не найден"); }

        trainer.Status = status;
    }

    public TrainerWithClientsDto ReadInfoId(Guid id, ClientRepository clientRepo)
    {
        Trainer? trainer = _treiners.FirstOrDefault(c => c.Id == id);

        if (trainer == null) { throw new Exception("Тренера с подобным идентификатором не найден"); }

        List<Client> clientsForTrainer = clientRepo.ReadAll().Where(c => c.TrainerId == id).ToList();

        return new TrainerWithClientsDto { Trainer = trainer, Clients = clientsForTrainer };
    }

    public List<Trainer> ReadAll() { return _treiners; }
}

public class TrainerWithClientsDto
{
    public Trainer Trainer { get; set; }
    public List<Client> Clients { get; set; } = new List<Client>();
}
