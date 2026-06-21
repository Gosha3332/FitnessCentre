
public class LockerRepository
{
    private readonly AppDbContext _context;

    public LockerRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddLocker(int number, Guid? clientId) 
    {
        _context.Lockers.Add(new Locker(number, clientId));
        _context.SaveChanges();
    }

    public List<LockerInfoDTO> ReadLockers() 
    {
        return _context.Lockers.Select(l => new LockerInfoDTO
        {
            Id = l.Id,
            Number = l.Number,
            LibertyStatus = l.ClientId == null
        }).ToList();
    }

    public List<Locker> Read()
    {
        return _context.Lockers.ToList();
    }
}

public class LockerInfoDTO
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool LibertyStatus { get; set; }
}

