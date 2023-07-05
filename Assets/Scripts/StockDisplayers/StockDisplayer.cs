using DropZones;
using TMPro;
using UnityEngine;

namespace StockDisplayers
{
    [RequireComponent(typeof(DropZone))]
    public class StockDisplayer : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _stockObjectNumberText;
    
        private GameObject _child;
        private DropZone _dropZone;
        protected int _stockObjectNumber = 0;

        public int StockObjectNumber => _stockObjectNumber;

        private void Awake()
        {
            _dropZone = GetComponent<DropZone>();
        }
    
        public virtual void OnEnable()
        {
            _dropZone.OnPlayerEnter += ActivateStockDisplay;
            _dropZone.OnPlayerExit += DeactivateStockDisplay;
        }

        public virtual void OnDisable()
        {
            _dropZone.OnPlayerEnter -= ActivateStockDisplay;
            _dropZone.OnPlayerExit -= DeactivateStockDisplay;
        }
    
        private void Start()
        {
            _child = transform.GetChild(0).gameObject;
            _stockObjectNumberText.text = _stockObjectNumber.ToString();
        }

        private void ActivateStockDisplay()
        {
            _child.SetActive(true);
        }
    
        private void DeactivateStockDisplay()
        {
            _child.SetActive(false);
        }

        protected void IncreaseStockObjectNumber()
        {
            _stockObjectNumber++;
            _stockObjectNumberText.text = _stockObjectNumber.ToString();
        }
    }
}
