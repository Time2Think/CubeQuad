using Architecture;
using UnityEngine;

namespace UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        [SerializeField]
        private PanelType _type;

        public PanelType TypePanel => _type;
        
    }
}
