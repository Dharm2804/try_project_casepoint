using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace project.Services
{
    public class AuthServices
    {
        private readonly NpgsqlConnection _con;

        public AuthServices(NpgsqlConnection con)
        {
            _con = con;
        }
    }
}