using Voodoo.CodeGeneration.Helpers;

namespace Voodoo.CodeGeneration.Models.SourceControl
{
    public static class SourceControlProviderFactory
    {
        public static ISourceControlProvider GetProvider()
        {
            ISourceControlProvider provider = null;
            var providerName = Vs.Helper.Solution?.SourceControlProviderName;
            switch (providerName)
            {
                case null:
                    return null;
                //case "tfs":
                //	provider = new TfsSourceControlProvider();
                //	if (provider.IsActive)
                //		return provider;
                //	break;
                case "tfexe":
                    provider = new TfExeSourceControlProvider();
                    if (provider.IsActive)
                        return provider;
                    break;
            }
            return null;
        }
    }
}