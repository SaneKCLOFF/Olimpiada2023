﻿using SanatoriumApp.Models;
using SanatoriumApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanatoriumApp.Services
{
    internal static class ClientService
    {
        internal static List<Client> GetAllClients()
        {
            using (var context = new ApplicationDbContext()) { return context.Clients.ToList(); }
        }
    }
}