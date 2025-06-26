using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos.Ferroviario.Modelos;

public class APIFerroviarioContext : DbContext
{
    public APIFerroviarioContext(DbContextOptions<APIFerroviarioContext> options)
        : base(options)
    {
    }

    public DbSet<Modelos.Ferroviario.Modelos.Client> Clientes { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.Roles> Roles { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.Ruta> Rutas { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.Schedule> Schedules { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.Seat> Seats { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.Ticket> Tickets { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.Train> Trains { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.TrainCar> TrainsCars { get; set; } = default!;

    public DbSet<Modelos.Ferroviario.Modelos.ClientesRoles> ClientesRoles { get; set; } = default!;

public DbSet<Modelos.Ferroviario.Modelos.PasswordResetToken> PasswordResetTokens { get; set; } = default!;
}
