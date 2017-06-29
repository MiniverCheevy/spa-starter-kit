using Core;
using Core.Models;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingRepository
    {
        private MainContext context;
        
        public ApplicationSettingRepository(MainContext context)
        {
            this.context = context;
        }
    }
}

