using Core;
using Core.Models;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingRepository
    {
        private DatabaseContext context;
        
        public ApplicationSettingRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}

