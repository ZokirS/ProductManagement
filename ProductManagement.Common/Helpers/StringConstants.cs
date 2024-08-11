namespace ProductManagement.Common.Helpers
{
    public static class StringConstants
    {
        public static readonly (string Create, string Update, string Delete) ProductAuditAction =
            ("Product with Id {0} was created.", 
            "Product with Id {0} was changed: {1}.", 
            "Product with Id {1} was deleted.");
    }
}