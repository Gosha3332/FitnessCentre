
public class Serviсe
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public Serviсe(string id, string name, int price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

public class ClientServiсe
{
    public Guid? ClientId { get; set; }
    public string ServiseId { get; set; }

    public ClientServiсe(Guid? clientId, string serviseId)
    {
        ClientId = clientId;
        ServiseId = serviseId;
    }
}