using Core;
using Core.Models.Scratch;
using Core.Context;
namespace Core.Operations.Teams.Extras
{
    public  static partial class TeamExtensions
    {
        public static TeamRepository TeamRepository(this MainContext context)
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

