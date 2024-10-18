using System.Collections.Generic;
using MornGlobal;
using UnityEngine;
using UnityEngine.Audio;

namespace MornSound
{
    [CreateAssetMenu(fileName = nameof(MornSoundGlobal), menuName = "Morn/" + nameof(MornSoundGlobal))]
    public class MornSoundGlobal : MornGlobalBase<MornSoundGlobal>
    {
#if DISABLE_MORN_SOUND_LOG
        protected override bool ShowLog => false;
#else
        protected override bool ShowLog => true;
#endif
        protected override string ModuleName => nameof(MornSound);
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private List<MornSoundSimpleClipEntity> _clipEntities;
        [SerializeField] private List<AudioClip> _clips;
        internal AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
        internal List<MornSoundSimpleClipEntity> ClipEntities => _clipEntities;
        internal List<AudioClip> Clips => _clips;
    }
}