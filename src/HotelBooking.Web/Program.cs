using Core;
using Framework;
using Framework.EndpointResults;
using Framework.Middlewares;
using HotelBooking.Application;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Authentication;
using HotelBooking.Infrastructure.Database;
using HotelBooking.Web;

var builder = WebApplication.CreateBuilder(args);

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddInfrastructureAuthentication(builder.Configuration)
    .AddApplication();

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
            ModelStateToEnvelopeMapper.ToBadRequest(context.ModelState);
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth("HotelBookingAPI", "v1");

var app = builder.Build();

await AuthorizationSeeder.InitializeAsync(app.Services);
await DatabaseSeeder.InitializeAsync(app.Services);

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
