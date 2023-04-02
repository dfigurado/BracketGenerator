using Application.Behaviour;
using Application.BracketGenerator;
using Application.Dto;
using Application.Features.Team.Command;
using Application.Features.Team.Query;
using Application.Features.Tournament.Command;
using Application.Features.TournamentSim;
using Application.Interfaces.BracketGenerator;
using Application.Interfaces.Features.Team;
using Application.Interfaces.Features.Tournament;
using Application.Interfaces.TournamentSim;
using Application.Mapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // A

            // B

            // C

            // D

            // E

            // F

            // G

            // H

            // I

            // J

            // H

            // K

            // L

            // M
            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(MappingProfile));

            // N 

            // O

            // P

            // Q

            // R

            // S

            // T
            services.AddScoped<ICreateTeamCommand, CreateTeamCommand>();
            services.AddScoped<ICreateTournamentCommand, CreateTournamentCommand>();
            services.AddScoped<IWorldCupTournament, WorldCupTournament>();
            services.AddScoped<IBracketGeneratorCommand, BracketGeneratorCommand>();
            services.AddScoped<ITournamentTeamsQuery, TournamentTeamsQuery>();

            services.AddSingleton<IList<TeamDto>>(_ => new List<TeamDto>());

            // U

            // Other
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
