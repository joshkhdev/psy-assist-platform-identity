using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyAssistPlatform.Application.Extensions;
using PsyAssistPlatform.AuthService.Domain;
using PsyAssistPlatform.AuthService.Persistence;
using PsyAssistPlatform.AuthService.Persistence.Data;

namespace PsyAssistPlatform.AuthService.WebApi
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext context, string connectionString)
        {
            InitializeDatabase(context);
            //AddFakeData(context);
            EnsureSeedData(connectionString);
        }

        private static void InitializeDatabase(AuthDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void AddFakeData(AuthDbContext context)
        {

            //context.Roles.AddRange(FakeDataFactory.Roles);
            //context.Approaches.AddRange(FakeDataFactory.Approaches);
            //context.Statuses.AddRange(FakeDataFactory.Statuses);
            //context.Contacts.AddRange(FakeDataFactory.Contacts);
            //context.Users.AddRange(FakeDataFactory.Users);
            //context.Questionnaires.AddRange(FakeDataFactory.Questionnaires);
            //context.SaveChanges();

            //context.Psychologists.AddRange(FakeDataFactory.Psychologists);
            //context.SaveChanges();
        }


        public static async Task EnsureSeedData(string connectionString)
        {
            try
            {

                var services = new ServiceCollection();
                services.AddLogging();
                services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connectionString));

                services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<AuthDbContext>()
                    .AddDefaultTokenProviders();

                using var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

                var context = scope.ServiceProvider.GetService<AuthDbContext>();
                var t = context.Database.EnsureCreated();
                //context.Database.Migrate();
                //var t2 = context.Database.EnsureCreated();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
                {
                    var roleName = role.ToDatabaseString();

                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                AddFakeData(context);

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var firstUser = await userMgr.FindByNameAsync(FakeDataFactory.Users.FirstOrDefault().Name);

                if (firstUser == null)
                {
                    foreach (var fakeUser in FakeDataFactory.Users)
                    {
                        fakeUser.UserName = fakeUser.Name.Replace(" ", "");
                        var result = await userMgr.CreateAsync(fakeUser, "Pass123$");

                        if (result.Succeeded)
                        {
                            Console.WriteLine($"Fake user created: {fakeUser.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"Fake user creating failed: {result.Errors.First().Description}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }


            //var alice = userMgr.FindByNameAsync("alice").Result;
            //if (alice == null)
            //{
            //    alice = new User
            //    {
            //        UserName = "alice",
            //        Email = "AliceSmith@email.com",
            //        EmailConfirmed = true,
            //    };
            //    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }

            //    result = userMgr.AddClaimsAsync(alice, new Claim[]{
            //                new Claim(JwtClaimTypes.Name, "Alice Smith"),
            //                new Claim(JwtClaimTypes.GivenName, "Alice"),
            //                new Claim(JwtClaimTypes.FamilyName, "Smith"),
            //                new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            //                //new Claim(JwtClaimTypes.Role = )
            //            }).Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }
            //    Console.WriteLine("alice created");
            //}
            //else
            //{
            //    Console.WriteLine("alice already exists");
            //}

            //var bob = userMgr.FindByNameAsync("bob").Result;
            //if (bob == null)
            //{
            //    bob = new User
            //    {
            //        UserName = "bob",
            //        Email = "BobSmith@email.com",
            //        EmailConfirmed = true
            //    };
            //    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }

            //    result = userMgr.AddClaimsAsync(bob, new Claim[]{
            //                new Claim(JwtClaimTypes.Name, "Bob Smith"),
            //                new Claim(JwtClaimTypes.GivenName, "Bob"),
            //                new Claim(JwtClaimTypes.FamilyName, "Smith"),
            //                new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
            //                new Claim("location", "somewhere")
            //            }).Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }
            //    Console.WriteLine("bob created");
            //}
            //else
            //{
            //    Console.WriteLine("bob already exists");
            //}
        }
    }
}
