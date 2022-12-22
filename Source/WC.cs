namespace VladimirTripAdvisor
{
    public static class WC
    {
        [Display(Name ="Администратор")]
        public const string AdminRole = "Admin";
        [Display(Name ="Владелец")]
        public const string OwnerRole = "Owner";
        [Display(Name = "Простой пользователь")]
        public const string UserRole = "User";
    }
}
