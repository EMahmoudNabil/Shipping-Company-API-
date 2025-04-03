namespace Shipping.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddInfrastructure(builder.Configuration);
            var app = builder.Build();
            if(app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                //app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json","v1"));
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(txt);
            app.MapControllers();
            app.Run();
        }
    }
}
