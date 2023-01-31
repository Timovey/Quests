using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using CommonInfrastructure.Db;

namespace AuthService.Database.Models
{
    /// <summary> Пользователь </summary>
    [Table("user"), Comment("Пользователь")]

    public class User : BaseDbModel
    {
        /// <summary> Имя пользователя </summary>
        [Column("name"), Comment("Имя пользователя")]
        public string Name { get; set; }

        /// <summary> Фамилия пользователя </summary>
        [Column("surname"), Comment("Фамилия пользователя")]
        public string Surname { get; set; }

        /// <summary> Логин пользователя </summary>
        [Column("username"), Comment("Логин пользователя")]
        public string Username { get; set; }

        /// <summary> Пароль пользователя </summary>
        [Column("password"), Comment("Пароль пользователя")]
        public string Password { get; set; }
    }
}
