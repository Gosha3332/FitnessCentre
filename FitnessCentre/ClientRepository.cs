public class ClientRepository
{
    private List<Client> _clients = new List<Client>();

    public void Create(string surname, string name, string? patronymic, DateTime birthday, string phone, string email, bool isActive, Guid? trainerId)
    {
        _clients.Add(new Client(surname, name, patronymic, birthday, phone, email, isActive, trainerId));
    }

    public void Update(Guid id, string surname, string name, string? patronymic, DateTime birthday, string phone, string email, bool isActive, Guid? trainerId)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }

        client.Surname = surname;
        client.Name = name;
        client.Patronymic = patronymic;
        client.Birthday = birthday;
        client.Phone = phone;
        client.Email = email;
        client.IsActive = isActive;
        client.TrainerId = trainerId;
    }

    public List<Client> ReadAll() { return _clients; }

    public Client ReadInfoId(Guid id)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }

        return client;
    }

    public ClientDetailDTO ReadDeatilInfoId(Guid id, TrainerRepository treinerRepo)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);
        Trainer? trainer = treinerRepo.ReadAll().FirstOrDefault(c => c.Id == client.TrainerId);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }
        else if (trainer == null) { throw new Exception("Тренер с подобным идентификатором не найден"); }

        return new ClientDetailDTO {Trainer = trainer, Client = client };
    }

    public void ActiveOrDeactiveClient(Guid id, bool state)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }

        client.IsActive = state;
    }

    public void AddTreinerForClient(Guid idClient, Guid idTriner, TrainerRepository treinerRepo)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == idClient);
        Trainer? trainer = treinerRepo.ReadAll().FirstOrDefault(c => c.Id == idTriner);

        if (client == null) { throw new Exception("Клиент с подобным идентификатором не найден"); }
        else if (trainer == null) { throw new Exception("Тренер с подобным идентификатором не найден"); }

        client.TrainerId = trainer.Id;

    }

}

public class ClientDetailDTO
{
    public Trainer Trainer { get; set; }
    public Client Client { get; set; }
}
