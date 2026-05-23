using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TrainMaster.Application.Interfaces;
using TrainMaster.Application.Services;
using TrainMaster.Application.Validators;

namespace TrainMaster.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMuscleService, MuscleService>();
        services.AddScoped<ISubGroupService, SubGroupService>();
        services.AddScoped<IWorkoutService, WorkoutService>();
        services.AddScoped<ITrainingService, TrainingService>();
        services.AddScoped<ITrainingWorkoutService, TrainingWorkoutService>();
        services.AddScoped<ITrainingSessionService, TrainingSessionService>();

        // Registra todos os validators do assembly automaticamente
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        return services;
    }
}
