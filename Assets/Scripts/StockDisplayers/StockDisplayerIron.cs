public class StockDisplayerIron : StockDisplayer
{
    public override void OnEnable()
    {
        base.OnEnable();
        PlayerBackpack.OnDropObjectIron += IncreaseStockObjectNumber;
    }
    
    public override void OnDisable()
    {
        base.OnDisable();
        PlayerBackpack.OnDropObjectIron -= IncreaseStockObjectNumber;
    }
    
    public void DecreseStockObjectNumber()
    {
        _stockObjectNumber--;
        _stockObjectNumberText.text = _stockObjectNumber.ToString();
    }
}
