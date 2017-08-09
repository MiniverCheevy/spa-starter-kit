using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.BlobOfTexts.Extras
{
    public class BlobOfTextRepository
    {
        private MainContext context;
        
        public BlobOfTextRepository(MainContext context)
        {
            this.context = context;
        }
    }
}

