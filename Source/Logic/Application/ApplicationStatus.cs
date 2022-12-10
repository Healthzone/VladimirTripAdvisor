using System.ComponentModel;

namespace VladimirTripAdvisor.Logic.Application
{
    public enum ApplicationStatus
    {
        [Description("Заявление создано")]
        Created = 1,

        [Description("Принята администратором создано")]
        Taked = 2,

        [Description("В обработке")]
        Processing = 3,

        [Description("На редактировании")]
        Edit = 4,

        [Description("Обработана успешно")]
        Done = 5,

        [Description("Отклонена")]
        Canceled = 6
    }
}
