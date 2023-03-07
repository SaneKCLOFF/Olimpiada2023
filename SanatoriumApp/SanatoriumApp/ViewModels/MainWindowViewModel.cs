﻿using Microsoft.EntityFrameworkCore;
using SanatoriumApp.Models;
using SanatoriumApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SanatoriumApp.ViewModels
{
    internal class MainWindowViewModel
    {
        public List<Role> Roles { get; set; }
        public List<User> Users { get; set; }
        public List<Client> Clients { get; set; }
        public List<SanatoriumProgram> SanatoriumPrograms { get; set; }
        public List<SanatoriumRoomCategory> SanatoriumRoomCategories { get; set; }
        public List<SanatoriumRoom> SanatoriumRooms { get; set; }
        public List<CostPerDay> CostPerDays { get; set; }
        public List<Treaty> Treaties { get; set; }
        public MainWindowViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                var role1 = new Role { Title = "Admin" };
                var user1 = new User { Login = "guest",Password="guest",Role=role1 };
                var client1 = new Client { LastName = "Аксёнов", FirstName = "Александр", MiddleName = "Игоревич", DateOfBirth = new DateTime(2003, 03, 20), Gender = 'М', Passport = "кам", User=user1};
                var sanatoriumProgram1 = new SanatoriumProgram { Title="Стандартная", QuantityOfProcedures=3, MinDays=4, Description="Отсутсвует", Cost=5999.99M };
                var sanatoriumRoomCategory1 = new SanatoriumRoomCategory { Title = "Бизнес - комфорт", Cost = 3000m };
                var sanatoriumRoom1 = new SanatoriumRoom { SanatoriumRoomCategory=sanatoriumRoomCategory1, RoomSize=4, QuantityOfSeats=3, QuantityOfRooms=2, RoomAmenities="Удобства не указаны", WindowView="Вид на океан", Description="Отсутсвует", Status="Не занят"};
                var costPerDay1 = new CostPerDay { Cost = sanatoriumProgram1.Cost + sanatoriumRoom1.SanatoriumRoomCategory.Cost, SanatoriumProgram = sanatoriumProgram1, SanatoriumRoom = sanatoriumRoom1 };
                var treaty1 = new Treaty { DateOfConclusion=DateTime.Now, DateOfCheckIn=DateTime.Now.AddDays(1), DateOfCheckOut=DateTime.Now.AddDays(1).AddMonths(1), PaymentAmount=costPerDay1.Cost, PaymentMethod="MasterCard", Client=client1, CostPerDay=costPerDay1 };

                context.Roles.Add(role1);
                context.Users.Add(user1);
                context.Clients.Add(client1);
                context.SanatoriumPrograms.Add(sanatoriumProgram1);
                context.SanatoriumRoomCategories.Add(sanatoriumRoomCategory1);
                context.SanatoriumRooms.Add(sanatoriumRoom1);
                context.CostsPerDays.Add(costPerDay1);
                context.Treaties.Add(treaty1);
                context.SaveChanges();

                Roles = new List<Role>(context.Roles);
                Users = new List<User>(context.Users.Include(r=>r.Role));
                Clients = new List<Client>(context.Clients.Include(u=>u.User).ThenInclude(r=>r.Role));
                SanatoriumPrograms = new List<SanatoriumProgram>(context.SanatoriumPrograms);
                SanatoriumRoomCategories = new List<SanatoriumRoomCategory>(context.SanatoriumRoomCategories);
                SanatoriumRooms = new List<SanatoriumRoom>(context.SanatoriumRooms.Include(sr=>sr.SanatoriumRoomCategory));
                CostPerDays = new List<CostPerDay>(context.CostsPerDays.Include(sp=>sp.SanatoriumProgram).Include(sr=>sr.SanatoriumRoom).ThenInclude(src=>src.SanatoriumRoomCategory));
                Treaties = new List<Treaty>(
                    context.Treaties.
                    Include(c=>c.Client).
                        ThenInclude(u=>u.User).
                        ThenInclude(r=>r.Role).
                    Include(cpd=>cpd.CostPerDay).
                        ThenInclude(sr=>sr.SanatoriumRoom).
                        ThenInclude(src=>src.SanatoriumRoomCategory).
                    Include(cpd=>cpd.CostPerDay).
                        ThenInclude(sp=>sp.SanatoriumProgram)
                        );
            }
        }

    }
}
