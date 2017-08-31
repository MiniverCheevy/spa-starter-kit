using Core;
using Core.Models.Scratch;
using Core.Context;
using Core.Operations.Members.Extras;
namespace Core.Models.Mappings
{
    public static partial class MemberExtensions
    {
        public static MemberRepository MemberRepository(this DatabaseContext context)
        {
            return new MemberRepository(context);
        }
        public static MemberRow ToMemberRow(this Member model)
        {
            var message = toMemberRow(model, new MemberRow());
            return message;
        }
        public static Member UpdateFrom(this  Member model, MemberRow message)
        {
            return updateFromMemberRow(message, model);
        }
        public static MemberDetail ToMemberDetail(this Member model)
        {
            var message = toMemberDetail(model, new MemberDetail());
            return message;
        }
        public static Member UpdateFrom(this  Member model, MemberDetail message)
        {
            return updateFromMemberDetail(message, model);
        }
    }
}

