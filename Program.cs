using BuberBreakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args); {
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
}

var app = builder.Build(); {
    app.UseExceptionHandler("/api/errors");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}



