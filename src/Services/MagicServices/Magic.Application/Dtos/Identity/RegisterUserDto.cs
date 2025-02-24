namespace Magic.Application.Dtos.Identity;

public record RegisterUserDto
(
    string FullName,
    string Email,
    string Mobile,
    string Password
);