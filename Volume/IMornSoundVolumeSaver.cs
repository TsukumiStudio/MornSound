namespace MornSound
{
    public interface IMornSoundVolumeSaver
    {
        float Load(string volumeKey);
        void Save(string volumeKey, float volumeRate);
    }
}