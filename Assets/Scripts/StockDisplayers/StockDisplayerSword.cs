using Player;

namespace StockDisplayers
{
    public class StockDisplayerSword : StockDisplayer
    {
        public override void OnEnable()
        {
            base.OnEnable();
            PlayerBackpack.OnDropObjectSword += IncreaseStockObjectNumber;
        }
    
        public override void OnDisable()
        {
            base.OnDisable();
            PlayerBackpack.OnDropObjectSword -= IncreaseStockObjectNumber;
        }
    }
}
