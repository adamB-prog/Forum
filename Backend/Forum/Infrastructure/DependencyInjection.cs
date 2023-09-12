using Application.Data;
using Domain.Entities.Answers;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Posts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddDbContext<ForumDbContext>(options => options
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Programming\\Forum\\Backend\\Forum\\Infrastructure\\ForumTestDb.mdf;Integrated Security=True", 
                b => b.MigrationsAssembly("Infrastructure"))

            );

            services.AddScoped<IForumDbContext>(sp => sp.GetRequiredService<ForumDbContext>());

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ForumDbContext>());

            services.AddScoped<IAnswerRepository>(sp => sp.GetRequiredService<AnswerRepository>());


            services.AddScoped<IPostRepository>(sp => sp.GetRequiredService<PostRepository>());

            services.AddScoped<IUserRepository, UserRepository>();

            

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IAnswerRepository, AnswerRepository>();
            
            return services;
        }
    }
}