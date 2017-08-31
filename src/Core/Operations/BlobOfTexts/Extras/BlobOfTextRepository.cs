using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.BlobOfTexts.Extras
{
    public class BlobOfTextRepository
    {
        private DatabaseContext context;
        
        public BlobOfTextRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}

