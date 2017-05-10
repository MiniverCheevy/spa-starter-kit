namespace Voodoo.CodeGeneration.Helpers
{
    public static class Vs
    {
        public static VisualStudioHelper Helper { get; set; }

        static Vs()
        {
            Helper = new VisualStudioHelper();
        }
    }
}