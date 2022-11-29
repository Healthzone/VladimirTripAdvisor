﻿namespace VladimirTripAdvisor.Models
{
    public class ApplicationModel
    {
        [Key]
        public long Id { get; set; }

        [Column("application_type")]
        public int ApplicationType { get; set; }

        [Column("application_data")]
        public string ApplicationData { get; set; }

        [Column("id_user")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}