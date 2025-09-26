namespace eAppointment.Application.Auth.Login;

public sealed record LoginCommandResponse
{
    public string AccessToken { get; set; } = default!;

    // letting people to leave their process for anything
    // so when they get back after some time, they can continue

    // public string RefreshToken { get; set; } = default!;
}

