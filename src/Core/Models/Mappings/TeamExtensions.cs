using Core;
using Core.Models.Scratch;
using Core.Context;
using Core.Operations.Teams.Extras;
namespace Core.Models.Mappings
{
    public static partial class TeamExtensions
    {
        public static TeamRepository TeamRepository(this DatabaseContext context)
        {
            return new TeamRepository(context);
        }
        public static TeamRow ToTeamRow(this Team model)
        {
            var message = toTeamRow(model, new TeamRow());
            return message;
        }
        public static Team UpdateFrom(this  Team model, TeamRow message)
        {
            return updateFromTeamRow(message, model);
        }
        public static TeamDetail ToTeamDetail(this Team model)
        {
            var message = toTeamDetail(model, new TeamDetail());
            return message;
        }
        public static Team UpdateFrom(this  Team model, TeamDetail message)
        {
            return updateFromTeamDetail(message, model);
        }
    }
}

