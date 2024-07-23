using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/getProtectedData", (IDataProtectionProvider provider, string data) => {

    var protector = provider.CreateProtector("DataProtection");
    var protectedData = protector.Protect(data);
    return protectedData;
});


app.MapGet("/getUnProtectedData", (IDataProtectionProvider provider, string data) => {

    var protector = provider.CreateProtector("DataProtection");
    var unprotectedData = protector.Unprotect(data);
    return unprotectedData;
});


app.Run();
