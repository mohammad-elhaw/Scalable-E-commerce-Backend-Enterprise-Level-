namespace Ecommerce.Gateway.Extensions;

public static class InitializeApplication
{
    public static async Task<WebApplication> AddApplication(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        //app.UseAuthorization();
        //app.MapControllers();
        return app;
    }
}
