using UnityEngine;
using UnityEngine.UI;
/**
 * Celdas del inventario
 */
namespace Inventario
{
    public class InventoryCell : MonoBehaviour
    {
        public InventoryItemDescription _description;

        public InventoryItemDescription description
        {
            set
            {
                _description = value;
                amount += value.value;
                totalAmount += value.value;
                iconBackground.sprite = value.icon;
                if (_description.type == Type.GARBAGE)
                {
                    ChangeIcon();
                }
            }
            get { return _description; }
        }

        [SerializeField] private Image iconBackground;
        [SerializeField] public int totalAmount = 0;

        [SerializeField] private Text amountLabel;
        private int _amount = 0;

        public int amount
        {
            set
            {
                _amount = value;
                amountLabel.text = "x" + _amount;
                if (_amount == 0)
                {
                    //Destroy(iconBackground, t: 1);
                    //Destroy(gameObject, 1);
                }

            }
            get { return _amount; }
        }
        public void ChangeIcon()
        {
            Image basura = GameObject.Find("InventoryCell").GetComponent<Image>();
            if(amount == 0)
                basura.sprite = Resources.Load <Sprite>("garbage_iconBW");
            else if(amount == 1)
                basura.sprite = Resources.Load <Sprite>("garbage_icon1de3");
            else if(amount == 2)
                basura.sprite = Resources.Load <Sprite>("garbage_icon2de3");
            else
                basura.sprite = Resources.Load <Sprite>("garbage_icon");
        }
    }
}