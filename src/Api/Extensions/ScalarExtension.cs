using Scalar.AspNetCore;

namespace Api.Extensions;

public static class ScalarExtension
{
    public static WebApplication AddScalarApiReference(this WebApplication app)
    {
        app.MapScalarApiReference(opt =>
        {
            opt.Title = "Budget API";
        });
        
        return app;
    }
}