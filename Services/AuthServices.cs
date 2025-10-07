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

        public async Task<bool> RegisterAsync(EmployeeModel emp)
        {
            try
            {
                if (_con.State != System.Data.ConnectionState.Open)
                    await _con.OpenAsync();

                string query = @"
                    INSERT INTO t_aemployee 
                    (c_name, c_email, c_password, c_gender, c_profileimage)
                    VALUES (@name, @mail, @pass, @gender, @profile)
                ";

                using var cmd = new NpgsqlCommand(query, _con);
                cmd.Parameters.AddWithValue("@name", emp.EmpName);
                cmd.Parameters.AddWithValue("@mail", emp.EmpMail);
                cmd.Parameters.AddWithValue("@pass", emp.EmpPassword);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
        
                cmd.Parameters.AddWithValue("@profile", (object?)emp.Profile ?? DBNull.Value);
              

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Register Error]: {ex.Message}");
                return false;
            }
            finally
            {
                await _con.CloseAsync();
            }
        }
         public async Task<bool> IsEmailExistsAsync(string email)
        {
            try
            {
                if (_con.State != System.Data.ConnectionState.Open)
                    await _con.OpenAsync();

                string query = "SELECT COUNT(*) FROM t_aemployee WHERE c_email = @mail";
                using var cmd = new NpgsqlCommand(query, _con);
                cmd.Parameters.AddWithValue("@mail", email);

                int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CheckEmail Error]: {ex.Message}");
                return false;
            }
            finally
            {
                await _con.CloseAsync();
            }
        }
    }
}