
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
        private static MemberDetail toMemberDetail(Member model, MemberDetail message)
        {
            message.Id = model.Id;
            message.Name = model.Name;
            message.Title = model.Title;
            message.RequiredInt = model.RequiredInt;
            message.OptionalInt = model.OptionalInt;
            message.RequiredDate = model.RequiredDate;
            message.OptionalDate = model.OptionalDate;
            message.OptionalDateTimeOffset = model.OptionalDateTimeOffset;
            message.RequiredDecimal = model.RequiredDecimal;
            message.OptionalDecimal = model.OptionalDecimal;
            message.ManagerId = model.ManagerId;
            return message;
        }
        public static Member updateFromMemberDetail(MemberDetail message, Member model)
        {
            model.Name=message.Name;
            model.Title=message.Title;
            model.RequiredInt=message.RequiredInt;
            model.OptionalInt=message.OptionalInt;
            model.RequiredDate=message.RequiredDate;
            model.OptionalDate=message.OptionalDate;
            model.OptionalDateTimeOffset=message.OptionalDateTimeOffset;
            model.RequiredDecimal=message.RequiredDecimal;
            model.OptionalDecimal=message.OptionalDecimal;
            model.ManagerId=message.ManagerId;
            return model;
        }
    }
}
