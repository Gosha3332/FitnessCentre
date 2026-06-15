public class ClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(string surname, string name, string? patronymic, DateTime birthday, string phone, string email, bool isActive, Guid? trainerId, Guid? lockerId)
    {
        _context.Clients.Add(new Client(surname, name, patronymic, birthday, phone, email, isActive, trainerId, lockerId));
        _context.SaveChanges();
    }

    public void Update(Guid id, string surname, string name, string? patronymic, DateTime birthday, string phone, string email, bool isActive, Guid? trainerId, Guid? lockerId)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == id);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }

        client.Surname = surname;
        client.Name = name;
        client.Patronymic = patronymic;
        client.Birthday = birthday;
        client.Phone = phone;
        client.Email = email;
        client.IsActive = isActive;
        client.TrainerId = trainerId;
        client.LockerId = lockerId;

        _context.SaveChanges();
    }

    public List<Client> ReadAll() { return _context.Clients.ToList(); }

    public Client ReadInfoId(Guid id)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == id);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }

        return client;
    }

    public ClientDetailDTO ReadDeatilInfoId(Guid id)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == id);
        Trainer? trainer = _context.Trainers.FirstOrDefault(c => c.Id == client.TrainerId);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }
        else if (trainer == null) { throw new Exception("Тренер с подобным идентификатором не найден"); }

        return new ClientDetailDTO { Trainer = trainer, Client = client };
    }

    public void ActiveOrDeactiveClient(Guid id, bool state)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == id);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }

        client.IsActive = state;
        _context.SaveChanges();
    }

    public void AddTreinerForClient(Guid idClient, Guid idTriner)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == idClient);
        Trainer? trainer = _context.Trainers.FirstOrDefault(c => c.Id == idTriner);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }
        else if (trainer == null) { throw new Exception("Тренер с подобным идентификатором не найден"); }

        client.TrainerId = trainer.Id;
        _context.SaveChanges();
    }

    public void AssignLocker(Guid clientId, Guid lockerId)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }
        if (client.LockerId != null) { throw new Exception("у клиента уже есть шкафчик"); }

        Locker? locker = _context.Lockers.FirstOrDefault(l => l.Id == lockerId);
        if (locker == null) { throw new Exception("Шкафчик с подобным номером не найден"); }
        if (locker.ClientId == null) { throw new Exception("Шкафчик уже занят"); }

        client.LockerId = lockerId;
        _context.SaveChanges();
    }

    public void AddService(Guid clientId, string serviceId, ServiсeRepository serviceRepo)
    {
        Client? client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null) throw new Exception("Клиент с подобным идентификатором не найден");

        var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);
        if (service == null) throw new Exception("Услуга с подобным идентификатором не найдена");

        serviceRepo.AddClientService(clientId, serviceId);
    }

}

public class ClientDetailDTO
{
    public Trainer Trainer { get; set; }
    public Client Client { get; set; }
}
