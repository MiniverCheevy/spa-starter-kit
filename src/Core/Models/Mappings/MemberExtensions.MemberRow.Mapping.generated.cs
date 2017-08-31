
//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
using Core;
using Core.Models.Scratch;
using Core.Operations.Members.Extras;
namespace Core.Models.Mappings
{
    public static partial class MemberExtensions
    {
        private static MemberRow toMemberRow(Member model, MemberRow message)
        {
            message.Id = model.Id;
            message.Name = model.Name;
            message.Title = model.Title;
            message.RequiredInt = model.RequiredInt;
            message.OptionalInt = model.OptionalInt;
            return message;
        }
        public static Member updateFromMemberRow(MemberRow message, Member model)
        {
            model.Name=message.Name;
            model.Title=message.Title;
            model.RequiredInt=message.RequiredInt;
            model.OptionalInt=message.OptionalInt;
            return model;
        }
    }
}

