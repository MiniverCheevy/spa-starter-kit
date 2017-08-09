//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
using Core;
using Core.Models.Scratch;
namespace Core.Operations.BlobOfTexts.Extras
{
    public static partial class BlobOfTextExtensions
    {
        private static BlobOfTextRow toBlobOfTextRow(BlobOfText model, BlobOfTextRow message)
        {
            message.Id = model.Id;
            message.Text = model.Text;
            message.MemberId = model.MemberId;
            
            return message;
        }
        public static BlobOfText updateFromBlobOfTextRow(BlobOfTextRow message, BlobOfText model)
        {
            model.Text =message.Text;
            model.MemberId =message.MemberId;
            return model;
        }
        private static BlobOfTextDetail toBlobOfTextDetail(BlobOfText model, BlobOfTextDetail message)
        {
            message.Id = model.Id;
            message.Text = model.Text;
            message.MemberId = model.MemberId;
            
            return message;
        }
        public static BlobOfText updateFromBlobOfTextDetail(BlobOfTextDetail message, BlobOfText model)
        {
            model.Text =message.Text;
            model.MemberId =message.MemberId;
            return model;
        }
        
    }
}

