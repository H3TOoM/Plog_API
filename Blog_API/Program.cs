using Blog_API.Data;
using Blog_API.Repoistries;
using Blog_API.Repoistries.Base;
using Blog_API.Services;
using Blog_API.Services.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Register the DbContext with dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register the Unit of Work and Repository pattern services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IMainRepoistory<>), typeof(MainRepoistory<>));
builder.Services.AddScoped<ICategoryRepoistory, CategoryRepository>();  
builder.Services.AddScoped<ICommentRepoistory, CommentRepoistory>();
builder.Services.AddScoped<ILikeRepoistory, LikeRepoistory>();

// Register services
builder.Services.AddScoped<IPlogService, PlogService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
