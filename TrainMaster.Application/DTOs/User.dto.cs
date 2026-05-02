namespace TrainMaster.Application.DTOs;
using TrainMaster.Domain.Enums;


public record CreateUserDto (
    string Name,
    string Email,
    string Password,
    DateTime Birthday,
    UserGoal Goal
);