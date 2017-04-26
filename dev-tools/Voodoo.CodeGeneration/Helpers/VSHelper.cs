namespace Voodoo.CodeGeneration.Helpers
{
    public static class Vs
    {
        static Vs()
        {
            Helper = new VisualStudioHelper();
        }

        public static VisualStudioHelper Helper { get; set; }
    }
}