#if USE_VCONTAINER
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace MornSound.UI
{
    [RequireComponent(typeof(Button))]
    public class MornSoundButton : MonoBehaviour, ISelectHandler, ISubmitHandler
    {
        [SerializeField] private string _onSelected;
        [SerializeField] private string _onSubmit;
        private IMornSoundSimplePlayer _player;

        private void Awake()
        {
            _player = VContainerSettings.Instance.GetOrCreateRootLifetimeScopeInstance().Container
                .Resolve<IMornSoundSimplePlayer>();
        }

        public void OnSelect(BaseEventData eventData)
        {
            _player.Play(_onSelected);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _player.Play(_onSubmit);
        }
    }
}
#endif