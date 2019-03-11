using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Toko.Entity;

namespace Toko.Data
{
    public class UsersCRUD : BaseCRUD
    {
        public Users GetUserBy(string userName, string password)
        {
            string sql = string.Format(@"SELECT
                                        `UserId`,
                                        `UserName`,
                                        `Passwords`,
                                        `NamaLengkap`,
                                        `Alamat`,
                                        `NoKTP`,
                                        `Level`
                                        FROM `users`
                                        WHERE `UserName` = @0 AND `Passwords` = MD5(REVERSE(@1))");
            return db.SingleOrDefault<Users>(sql, userName, password);
        }

        public DataTable GetAllUserAsTable()
        {
            string sql = string.Format(@"SELECT
                                        `UserId`,
                                        `UserName`,
                                        `Passwords`,
                                        `NamaLengkap`,
                                        `Alamat`,
                                        `NoKTP`,
                                        `Level`
                                        FROM `users`");
            return db.ExecuteReader(sql, null);
        }

        public DataTable GetUsersByAsTable(string nilai, string kriteria)
        {
            string sql = string.Format(@"SELECT
                                        `UserId`,
                                        `UserName`,
                                        `Passwords`,
                                        `NamaLengkap`,
                                        `Alamat`,
                                        `NoKTP`,
                                        `Level`
                                        FROM `users`
                                        WHERE " + kriteria + " LIKE '%" + nilai + "%'");
            return db.ExecuteReader(sql, null);

        }

        public bool UpdateUserWithPassword(Users oUsers)
        {
            bool result = false;
            string sql = string.Format(@"UPDATE `users`
                                    SET `UserName` = @1,
                                    `Passwords` = MD5(REVERSE(@2)),
                                    `NamaLengkap` = @3,
                                    `Alamat` = @4,
                                    `NoKTP` = @5,
                                    `Level` = @6
                                    WHERE `UserId` = @0");
            result = db.Execute(sql, oUsers.UserId,
                                     oUsers.UserName,
                                     oUsers.Passwords,
                                     oUsers.NamaLengkap,
                                     oUsers.Alamat,
                                     oUsers.NoKTP,
                                     oUsers.Level) == 1;
            return result;
        }

        public bool UpdateUserWithoutPassword(Users oUsers)
        {
            bool result = false;
            string sql = string.Format(@"UPDATE `users`
                                    SET `UserName` = @1,
                                    `NamaLengkap` = @3,
                                    `Alamat` = @4,
                                    `NoKTP` = @5,
                                    `Level` = @6
                                    WHERE `UserId` = @0");
            result = db.Execute(sql, oUsers.UserId,
                                     oUsers.UserName,
                                     oUsers.Passwords,
                                     oUsers.NamaLengkap,
                                     oUsers.Alamat,
                                     oUsers.NoKTP,
                                     oUsers.Level) == 1;
            return result;
        }

        public bool InsertUser(Users oUsers)
        {
            bool result = false;
            string sql = string.Format(@"INSERT INTO `users`
                                        (`UserName`,
                                        `Passwords`,
                                        `NamaLengkap`,
                                        `Alamat`,
                                        `NoKTP`,
                                        `Level`)
                                        VALUES (@0,
                                        MD5(REVERSE(@1)),
                                        @2,
                                        @3,
                                        @4,
                                        @5)");
            result = db.Execute(sql, oUsers.UserName,
                                     oUsers.Passwords,
                                     oUsers.NamaLengkap,
                                     oUsers.Alamat,
                                     oUsers.NoKTP,
                                     oUsers.Level) == 1;
            return result;
        }

        public bool DeleteUsersBy(string userId)
        {
            bool result = false;
            string sql = string.Format(@"DELETE
                                        FROM `users`
                                        WHERE `UserId` = @0");
            result = db.Execute(sql, userId) == 1;
            return result;
        }
    }
}
