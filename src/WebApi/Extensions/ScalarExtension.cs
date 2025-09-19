using Scalar.AspNetCore;

namespace WebApi.Extensions;

public static class ScalarExtension
{
    public static WebApplication AddScalarApiReference(this WebApplication app)
    {
        app.MapScalarApiReference(opt =>
        {
            opt.Title = "Budget API";
            opt.Layout = ScalarLayout.Classic;
        });
        
        return app;
    }
}