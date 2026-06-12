public class Client
{
    public Guid Id { get; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }

    private DateTime _birthday;
    public DateTime Birthday
    {
        get => _birthday;
        set
        {
            if (ValidateContact.DateBirthday(value)) { _birthday = value; }
            else { throw new ArgumentException("Некорректная дата рождения"); }
        }
    }
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

    private string _email;

    public string Email
    {
        get => _email;
        set
        {
            if (ValidateContact.Email(value)) { _email = value; }
            else { throw new ArgumentException("Некорректный адрес почты"); }
        }
    }
    public bool IsActive { get; set; } = true;
    public Guid? TrainerId { get; set; }

    public Client(string surname, string name, string? patronymic, DateTime birthday, string phone, string email, bool isActive, Guid? trainerId)
    {
        Id = Guid.NewGuid();
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Birthday = birthday;
        Phone = phone;
        Email = email;
        IsActive = isActive;
        TrainerId = trainerId;
    }
}
