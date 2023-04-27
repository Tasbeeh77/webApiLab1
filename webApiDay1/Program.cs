using webApiDay1.MiddleWares;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(); //di L asasya
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.(middlewares)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    requestsCount++;
    await next(context);
});

//app.UseMiddleware<RequestsCountMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program
{
    public static int requestsCount = 0;
}