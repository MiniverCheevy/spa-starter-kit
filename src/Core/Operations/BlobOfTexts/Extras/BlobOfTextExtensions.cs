using Core;
using Core.Models.Scratch;
using Core.Context;
namespace Core.Operations.BlobOfTexts.Extras
{
    public  static partial class BlobOfTextExtensions
    {
        public static BlobOfTextRepository BlobOfTextRepository(this MainContext context)
        {
            return new BlobOfTextRepository(context);
        }
        
        public static BlobOfTextRow ToBlobOfTextRow(this BlobOfText model)
        {
            var message = toBlobOfTextRow(model, new BlobOfTextRow());
            return message;
        }
        public static BlobOfText UpdateFrom(this  BlobOfText model, BlobOfTextRow message)
        {
            return updateFromBlobOfTextRow(message, model);
            
        }
        public static BlobOfTextDetail ToBlobOfTextDetail(this BlobOfText model)
        {
            var message = toBlobOfTextDetail(model, new BlobOfTextDetail());
            return message;
        }
        public static BlobOfText UpdateFrom(this  BlobOfText model, BlobOfTextDetail message)
        {
            return updateFromBlobOfTextDetail(message, model);
            
        }
        
    }
}

