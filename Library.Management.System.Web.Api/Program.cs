using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Management.System.Application.Repositories;
using Library.Management.System.Application.Services.AuthorServices;
using Library.Management.System.Application.Services.BookServices;
using Library.Management.System.Application.Services.BorrowerServices;
using Library.Management.System.Application.Services.LoanServices;
using Library.Management.System.Application.Services.UserServices;
using Library.Management.System.Application.Validators.AuthorDTOsValidators;
using Library.Management.System.Application.Validators.BookDTOsValidators;
using Library.Management.System.Application.Validators.BorrowerDTOsValidators;
using Library.Management.System.Application.Validators.LoanDTOsValidators;
using Library.Management.System.Application.Validators.UserDTOsValidators;
using Library.Management.System.Infrastructure.Persistence.DbContext;
using Library.Management.System.Infrastructure.Repositories;
using Library.Management.System.Web.Api.Filters;
using Library.Management.System.Web.Api.Middlewares;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthorRepository,AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMapster();
builder.Services.AddScoped<LogFilter>();

// Add services to the container.

builder.Services.AddControllers();
// Register FluentValidation services (new approach)
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuthorDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBorrowerDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBorrowerDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateLoanDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateLoanDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SignInUserDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey12345SuperSecretKey12345")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Library Management System API", Version = "v1" });
 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management System API v1"));
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
