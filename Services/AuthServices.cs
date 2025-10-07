using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using project.Models;

namespace project.Services
{
    public class AuthServices
    {
        private readonly NpgsqlConnection _con;

        public AuthServices(NpgsqlConnection con)
        {
            _con = con;
        }

        public async Task<bool> Login(Vm_login login)
        {
            try
            {
                if (_con.State != System.Data.ConnectionState.Open)
                    await _con.OpenAsync();

                string s = "SELECT COUNT(*) FROM t_aemployee WHERE LOWER(c_email) = LOWER(@mail) AND c_password = @pass";
                using var cmd = new NpgsqlCommand(s, _con);
                cmd.Parameters.AddWithValue("@mail", login.EmpMail.Trim());
                cmd.Parameters.AddWithValue("@pass", login.EmpPassword);

                var counts = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return counts > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<string> GetByEmail(string email)
        {
            try
            {
                if (_con.State != System.Data.ConnectionState.Open)
                    await _con.OpenAsync();

                string query = "SELECT c_role FROM t_aemployee WHERE LOWER(c_email) = LOWER(@Email) LIMIT 1";
                using var cmd = new NpgsqlCommand(query, _con);
                cmd.Parameters.AddWithValue("@Email", email.Trim());

                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync() && !reader.IsDBNull(0))
                {
                    string role = reader.GetString(0);
                    return role;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}