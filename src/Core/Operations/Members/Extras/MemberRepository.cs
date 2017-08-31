using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.Members.Extras
{
    public class MemberRepository
    {
        private DatabaseContext context;
        
        public MemberRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}

