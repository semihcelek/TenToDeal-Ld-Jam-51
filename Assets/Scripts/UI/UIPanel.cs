using UnityEngine;

namespace SemihCelek.TenToDeal.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void TogglePanel(bool value)
        {
            gameObject.SetActive(value);
        }  
    }
}