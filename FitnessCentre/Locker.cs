
public class Locker
{
    public Guid Id { get; }
    public int Number { get; set; }
    public Guid? ClientId { get; set; }

    public Locker(int number, Guid? clientId)
    {
        Id = Guid.NewGuid();
        Number = number;
        ClientId = clientId;
    }
}
