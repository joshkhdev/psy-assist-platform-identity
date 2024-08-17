using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.Persistence.Data;

public static class FakeDataFactory
{
    public static IEnumerable<User> Users => new List<User>()
    {
        new()
        {
            Name = "Василиса Петрова",
            UserName = "VasilisaPetrova",
            Email = "vasilisa@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Curator,
        },
        new()
        {
            Name = "Вера Соловьева",
            UserName = "VeraSolovyeva",
            Email = "vera@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Яна Серебрякова",
            UserName = "YanaSerebryakova",
            Email = "yana@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Иван Орлов",
            UserName = "IvanOrlov",
            Email = "ivan@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Арина Белова",
            UserName = "ArinaBelova",
            Email = "arina@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Дарья Миронова",
            UserName = "DariaMironova",
            Email = "daria@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Роберт Сахаров",
            UserName = "RobertSakharov",
            Email = "robert@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Жанна Ефимова",
            UserName = "ZhannaEfimova",
            Email = "janna@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Admin",
            UserName = "Admin",
            Email = "jakob@goooooooogle.org",
            IsBlocked = false,
            RoleType = RoleType.Admin
        }
    };
}
