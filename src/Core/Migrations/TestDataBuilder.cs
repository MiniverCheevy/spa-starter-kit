using System;
using System.Data.Entity.Migrations;
using Core.Context;
using Core.Models.Scratch;
using Voodoo;

namespace Core.Migrations
{
    internal class TestDataBuilder
    {
        internal void Build(MainContext context)
        {
            var dev1 = new Member
            {
                Name = "Dev 1",
                RequiredDate = "1/1/2016".To<DateTime>(),
                RequiredDecimal = 1.1m,
                RequiredInt = 7
            };
            var dev2 = new Member
            {
                Name = "Dev 2",
                RequiredDate = "1/1/2017".To<DateTime>(),
                RequiredDecimal = 2.1m,
                RequiredInt = 8
            };
            var qa1 = new Member
            {
                Name = "Qa 1",
                RequiredDate = "1/1/2018".To<DateTime>(),
                RequiredDecimal = 3.1m,
                RequiredInt = 9
            };
            var scrum1 = new Member
            {
                Name = "Scrumster",
                RequiredDate = "1/1/2019".To<DateTime>(),
                RequiredDecimal = 4.1m,
                RequiredInt = 10
            };

            context.Members.AddOrUpdate(c => c.Name, dev1);
            context.Members.AddOrUpdate(c => c.Name, dev2);
            context.Members.AddOrUpdate(c => c.Name, qa1);
            context.Members.AddOrUpdate(c => c.Name, scrum1);
            context.SaveChanges();

            var project1 = new Project {
                Name = "Duct Tape & Rubber Bands" 
                ,Team= new Team { Name = "Awesome Team", Members = { dev1, qa1, scrum1 } }
            };
            var project2 = new Project { Name = "Shiny" , Team =
                new Team { Name = "Team Awesome", Members = { dev2, qa1, scrum1 } }
            };

            context.Projects.AddOrUpdate(c => c.Name, project1);
            context.Projects.AddOrUpdate(c => c.Name, project2);            
            context.SaveChanges();
            
        }
    }
}