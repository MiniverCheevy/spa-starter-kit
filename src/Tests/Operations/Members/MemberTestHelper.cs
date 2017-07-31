
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Members;
using Core.Operations.Members.Extras;
using Voodoo.TestData;
using Core.Context;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
namespace Tests.Operations.Members
{
    [TestClass]
    public class MemberTestHelper
    {

        private static string[] first = new string[] {
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            "Chief",
            "Associate",
            "Junior",
            "Senior"
        };
        private static string[] second = new string[] {
            string.Empty,
            "Application",
            "Application Support",
            "Computer and Information Systems",
            "Computer Systems",
            "Information Systems",
            "Information Technology",
            "Data",
            "Data Center",
            "Data Quality",
            "Database",
            "Desktop",
            "Desktop Support",
            "Front End",
            "Help Desk",
            "IT Support",
            ".Net",
            "Network",
            "Quality Assurance",
            "Security",
            "Technology",
            "Web",
            "CSS",
            "HTML"

        };
        private static string[] third = new string[] {
            string.Empty,
            "Analyst",
            "Developer",
            "Engineer",
            "Officer",
            "Manager",
            "Administrator",
            "Specialist",
            "Coordinator",
            "Director",
            "Architect",
            "Technician",
            "Coordinator",
            "Programmer"
        };

        private static string getJobTitle()
        {
            var strings = new string[] {
            first.RandomElement(),
            second.RandomElement(),
             third.RandomElement()
            };

            var title = string.Join(" ", strings).Trim();
            return title;

        }

        public static MemberDetail GetNewMember()
        {
            var request = new MemberDetail();
            TestHelper.Randomizer.Randomize(request);
            request.Title = getJobTitle();
            return request;
        }

        public static async Task<MemberDetail> GetExistingMember()
        {
            var request = GetNewMember();
            var command = new MemberSaveCommand(request);
            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(MemberMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);

            var query = new MemberDetailQuery(new IdRequest { Id = response.NewItemId });
            var data = await query.ExecuteAsync();
            return data.Data;
        }
    }
}

