using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveLikeApiDemo.Services;
using Swashbuckle.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<BroadcastService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyMethod() 
              .AllowAnyHeader(); 
    });
});
var app = builder.Build();
app.UseCors("AllowAll");
//app.UseSwagger();
app.UseStaticFiles(); // This line is required to serve wwwroot files

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();