using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.Persistence.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<User> Users => new List<User>()
    {
        new()
        {
            Name = "Василиса Петрова",
            Email = "vasilisa@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Curator,
        },
        new()
        {
            Name = "Вера Соловьева",
            Email = "vera@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Яна Серебрякова",
            Email = "yana@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Иван Орлов",
            Email = "ivan@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Арина Белова",
            Email = "arina@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Дарья Миронова",
            Email = "daria@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Роберт Сахаров",
            Email = "robert@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Жанна Ефимова",
            Email = "janna@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Psychologist
        },
        new()
        {
            Name = "Яков Иванов",
            Email = "jakob@goooooooogle.org",
            IsBlocked = false,
            //Password = "qwerty",
            RoleType = RoleType.Admin
        },
    };
    }
}
