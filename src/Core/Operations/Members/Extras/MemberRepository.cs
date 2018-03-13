using Core;
using Core.Models.Scratch;
using Core.Context;
using Microsoft.EntityFrameworkCore;

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

