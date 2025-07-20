using System;

namespace ChatApp.Extensions;

public static class CorsExtension
{
    public static void AddDefaultCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(s => true));
        });
    }
}
