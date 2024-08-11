namespace ProductManagement.Common.Exceptions.CustomExceptions;

public class ProductNotFoundException : BaseException
{
    private const string IdMessage = "Product with id {0} not found";
    private const string PartNumberMessage = "Product with part number {0} not found";
    private const string ManualCodeMessage = "Product with manual code {0} not found";

    public override int ErrorCode => -140;

    public ProductNotFoundException(long id) : base(string.Format(IdMessage, id))
    {
    }

    public ProductNotFoundException(string partNumber) : base(string.Format(PartNumberMessage, partNumber))
    {
    }

    public ProductNotFoundException(string manualCode, bool isManualCode) : base(string.Format(ManualCodeMessage, manualCode))
    {
    }
}
