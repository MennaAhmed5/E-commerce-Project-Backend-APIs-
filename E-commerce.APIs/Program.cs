using E_commerce.APIs;
using E_commerce.BL;
using E_commerce.BL.Managers.Products;
using E_commerce.BL.Managers;
using E_commerce.DAL;
using E_commerce.DAL.Data.Clients;
using E_commerce.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


#region identity

builder.Services.AddIdentityCore<ApplicationUser>(options => {
    //configure password validation => by default all true
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

})
    .AddEntityFrameworkStores<MyDbContext>();

#endregion


#region Authentication

builder.Services.AddAuthentication(options =>
{
    //configure used authentication

    options.DefaultAuthenticateScheme = "MyDefault";

    options.DefaultChallengeScheme = "MyDefault";
})
//Define the authentication scheme
    .AddJwtBearer("MyDefault", options =>
    {
        var keyFromConfig = builder.Configuration.GetValue<String>(Constants.AppSettings.SecretKey);
        var keyInBytes = Encoding.ASCII.GetBytes(keyFromConfig); //convert string to bytes
        var key = new SymmetricSecurityKey(keyInBytes); //convert arr of bytes to obj
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key
        };
    });

#endregion

#region Authorization
builder.Services.AddAuthorization(options =>
{                     //name of policy
    options.AddPolicy("AdminOnly", b =>
    {                   //name         value  value
        b.RequireClaim(ClaimTypes.Role, "Admin")
         .RequireClaim(ClaimTypes.NameIdentifier); //id exist
    });

});
#endregion

var AllowAllCorspolicy = "AllowAll";

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDALServices(builder.Configuration);
builder.Services.AddBlServices();

//Allow Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllCorspolicy, b =>
    {
        b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var staticFile = Path.Combine(Environment.CurrentDirectory, "Images");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFile),
    RequestPath = "/Images"
}) ;
app.UseHttpsRedirection();

app.UseCors(AllowAllCorspolicy); //to allow cors policy

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
