using Microsoft.EntityFrameworkCore;
using WDRRP.Models;
using WDRRP.Repositories;
using WDRRP.Services;
using Microsoft.Extensions.FileProviders;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddDbContext<WdrrpContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IUserService, UserRepository>();
builder.Services.AddTransient<ISkillService, SkillRepository>();
builder.Services.AddTransient<IExperienceService, ExperienceRepository>();
builder.Services.AddTransient<IEducationService, EducationRepository>();
builder.Services.AddTransient<IEmploymentTypeService, EmploymentTypeRepository>();
builder.Services.AddTransient<IJobService, JobRepository>();
builder.Services.AddTransient<IFileService, FileRepository>();
builder.Services.AddTransient<IProfilePicService, ProfilePicRepository>();
builder.Services.AddTransient<IJobAppliedService, JobAppliedRepository>();
builder.Services.AddTransient<IEmailService, EmailRepository>();
builder.Services.AddHttpClient<IJobService, JobRepository>();
builder.Services.AddTransient<IChatBotService, ChatBotRepository>();
builder.Services.AddHttpClient<ChatBotRepository, ChatBotRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseStaticFiles(); 

var resourcesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Resources");

if (!Directory.Exists(resourcesDirectory))
{
    Directory.CreateDirectory(resourcesDirectory);
}

// For serving files from "Resources" directory
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(resourcesDirectory),
    RequestPath = new PathString("/files")
});

app.MapControllers();

app.Run();

