namespace AutoImovel.API.Features.Auth;

public sealed record LoginResponse(
    string Token,
    string Email,
    string Role,
    Guid? InvestidorId);
