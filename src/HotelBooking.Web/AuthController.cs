using Core;
using Core.Abstractions;
using Framework;
using Framework.EndpointResults;
using HotelBooking.Application.Features.Auth;
using HotelBooking.Application.Features.Auth.Commands.DisableTwoFactor;
using HotelBooking.Application.Features.Auth.Commands.EnableTwoFactor;
using HotelBooking.Application.Features.Auth.Commands.GenerateTwoFactorSecret;
using HotelBooking.Application.Features.Auth.Commands.Login;
using HotelBooking.Application.Features.Auth.Commands.Logout;
using HotelBooking.Application.Features.Auth.Commands.Refresh;
using HotelBooking.Application.Features.Auth.Commands.Register;
using HotelBooking.Contracts.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Web;

public sealed class AuthController : ApplicationController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<EndpointResult<TokenResponse>> Register(
        [FromBody] RegisterRequest request,
        [FromServices] ICommandHandler<TokenResponse, RegisterCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword
        };

        return await handler.Handle(command, cancellationToken);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<EndpointResult<TokenResponse>> Login(
        [FromBody] LoginRequest request,
        [FromServices] ICommandHandler<TokenResponse, LoginCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand
        {
            UserName = request.UserName, Password = request.Password, TwoFactorCode = request.TwoFactorCode
        };

        return await handler.Handle(command, cancellationToken);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<EndpointResult<TokenResponse>> Refresh(
        [FromBody] RefreshRequest request,
        [FromServices] ICommandHandler<TokenResponse, RefreshCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new RefreshCommand { RefreshToken = request.RefreshToken };

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<EndpointResult> Logout(
        [FromBody] LogoutRequest request,
        [FromServices] ICommandHandler<LogoutCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new LogoutCommand { RefreshToken = request.RefreshToken };

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize]
    [HttpGet("2fa/secret")]
    public async Task<EndpointResult<string>> GenerateTwoFactorSecret(
        [FromServices] ICurrentUser currentUser,
        [FromServices] ICommandHandler<string, GenerateTwoFactorSecretCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new GenerateTwoFactorSecretCommand { UserId = currentUser.UserId };

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize]
    [HttpPost("2fa/enable")]
    public async Task<EndpointResult<Guid>> EnableTwoFactor(
        [FromBody] EnableTwoFactorRequest request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ICommandHandler<Guid, EnableTwoFactorCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new EnableTwoFactorCommand { UserId = currentUser.UserId, Code = request.Code };

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize]
    [HttpPost("2fa/disable")]
    public async Task<EndpointResult<Guid>> DisableTwoFactor(
        [FromServices] ICurrentUser currentUser,
        [FromServices] ICommandHandler<Guid, DisableTwoFactorCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DisableTwoFactorCommand { UserId = currentUser.UserId };

        return await handler.Handle(command, cancellationToken);
    }
}