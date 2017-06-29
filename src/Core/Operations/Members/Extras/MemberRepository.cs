using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.Members.Extras
{
    public class MemberRepository
    {
        private MainContext context;
        
        public MemberRepository(MainContext context)
        {
            this.context = context;
        }
    }
}

