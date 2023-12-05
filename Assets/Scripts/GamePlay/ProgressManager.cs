using UnityEngine;

namespace GamePlay
{
    public  class ProgressManager 
    {
        private const string ProgressKey = "Progress";

        public void SaveProgress(int level)
        {
            PlayerPrefs.SetInt(ProgressKey, level);
            PlayerPrefs.Save();
        }

        public int LoadProgress()
        {
            int defaultLevel = 0;

            if (!PlayerPrefs.HasKey(ProgressKey))
            {
                SaveProgress(defaultLevel);
            }
            return PlayerPrefs.GetInt(ProgressKey);
        }
    }
}
