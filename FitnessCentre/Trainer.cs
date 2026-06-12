public class Trainer
{
    public Guid Id { get; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronimic { get; set; }

    private string _phone;
    public string Phone
    {
        get => _phone;
        set
        {
            if (ValidateContact.Phone(value)) { _phone = value; }
            else { throw new ArgumentException("Некорректный номер телефона"); }
        }
    }
    public Status Status { get; set; }

    public Trainer(string surname, string name, string patronimic, string phone, Status status)
    {
        Id = Guid.NewGuid();
        Surname = surname;
        Name = name;
        Patronimic = patronimic;
        Phone = phone;
        Status = status;
    }

}
public enum Status { WORKING, ON_LEAVE, NOT_WORKING }

