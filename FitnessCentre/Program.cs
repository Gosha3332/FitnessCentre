using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=gymdb;Username=postgres;Password=123"));

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<TrainerRepository>();
builder.Services.AddScoped<LockerRepository>();
builder.Services.AddScoped<ServiñeRepository>();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())

using (AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    context.Database.EnsureCreated();

    if (!context.Lockers.Any())
    {
        for (int i = 1; i <= 20; i++)
        {
            context.Lockers.Add(new Locker(i, null));
        }
    }

    if (!context.Services.Any())
    {
        context.Services.AddRange(
            new Serviñe("SOLARIUM", "Ñîëÿðèé", 5000),
            new Serviñe("POOL", "Áàññåéí", 2000),
            new Serviñe("SAUNA", "Ñàóíà", 3000),
            new Serviñe("CRYOSAUNA", "Êðèîñàóíà", 1500),
            new Serviñe("CROSSFIT", "Êðîññôèò", 1500)
        );
    }

    context.SaveChanges();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
