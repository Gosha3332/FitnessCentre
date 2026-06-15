
public class ServiсeRepository
{
    private readonly AppDbContext _context;

    public ServiсeRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddClientService(Guid clientId, string serviceId)
    {
        ClientServiсe? existing = _context.ClientServiсes
            .FirstOrDefault(cs => cs.ClientId == clientId && cs.ServiseId == serviceId);
        if (existing != null) { throw new Exception("Клиент уже подписан на данную услугу"); }

        _context.ClientServiсes.Add(new ClientServiсe(clientId, serviceId));
        _context.SaveChanges();
    }

    public List<Serviсe> GetAll()
    {
        return _context.Services.ToList();
    }

    public ServiceDetailDTO? GetByIdWithClients(string id, ClientRepository clientRepository)
    {
        Serviсe? service = _context.Services.FirstOrDefault(s => s.Id == id);
        if (service is null)
            return null;

        List<Client> clients = _context.ClientServiсes
            .Where(cs => cs.ServiseId == id)
            .Select(cs => clientRepository.ReadInfoId(cs.ClientId.Value))
            .ToList();

        return new ServiceDetailDTO { Service = service, Clients = clients };
    }
}

public class ServiceDetailDTO
{
    public Serviсe Service { get; set; }
    public List<Client> Clients { get; set; }
}