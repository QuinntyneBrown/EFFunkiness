using EFFunkiness.Server.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace EFFunkiness.Server.Data
{
    public static class SeedData
    {
        public static void Seed(EFFunkinessDbContext context, IConfiguration configuration)
        {
            var user = context.Users.SingleOrDefault(x => x.Name == "Quinn");

            if(user == null)
            {
                user = new User { Name = "Quinn" };

                context.Users.Add(user);

                context.SaveChanges();
            }

            foreach(var name in new string[3] { "Michael Jordan", "Magic Johnson", "Derrick Rose"})
            {
                CreateClientIfDoenstExist(name, user, context);
            }

            context.SaveChanges();

            void CreateClientIfDoenstExist(string name, User createdByUser, EFFunkinessDbContext context)
            {
                if (context.Clients.SingleOrDefault(x => x.Name == name) == null)
                {
                    context.Clients.Add(new Client { Name = name, CreatedByUserId = createdByUser.UserId });
                }
            }
        }
    }
}
