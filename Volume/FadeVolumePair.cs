namespace MornSound
{
    internal class FadeVolumePair
    {
        internal readonly string Key;
        internal float Volume;

        internal FadeVolumePair(string key)
        {
            Key = key;
            Volume = 0;
        }
    }
}