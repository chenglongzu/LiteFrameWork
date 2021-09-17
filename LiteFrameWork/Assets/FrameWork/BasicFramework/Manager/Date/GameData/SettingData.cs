using UnityEngine;

namespace LiteFramework
{
    /// <summary>
    /// 数据类 类的对象值就是当前设置的值
    /// </summary>
    public class SettingData : MonoBehaviour
    {
        public float audioVolume;
        public float musicVolume;

        public bool isAudioMute;
        public bool isMusicMute;

        public SettingData()
        {
            audioVolume = 1;
            musicVolume = 1;

            isAudioMute = false;
            isMusicMute = false;
        }

        #region 修改设置选项

        public void ChangeGameAudio(float Value)
        {
            audioVolume = Value;
            AudioManager.GetInstance().ChangeAudioVolume(Value);

        }
        public void ChangeGameMusic(float Value)
        {
            musicVolume = Value;
            AudioManager.GetInstance().ChangeMusicVolume(Value);
        }

        public void ChangeMute(int index, bool isMute)
        {
            switch (index)
            {
                case 0:
                    if (isMute)
                    {
                        AudioManager.GetInstance().ChangeAudioVolume(0);
                        AudioManager.GetInstance().ChangeMusicVolume(0);
                    }
                    else
                    {
                        //AudioManager.GetInstance().ChangeAudioVolume(GameDataManager.GetInstance().settingDate.audioVolume);
                        //AudioManager.GetInstance().ChangeMusicVolume(GameDataManager.GetInstance().settingDate.musicVolume);
                    }

                    break;
                case 1:
                    if (isMute)
                    {
                        AudioManager.GetInstance().ChangeMusicVolume(0);
                    }
                    else
                    {
                        //AudioManager.GetInstance().ChangeMusicVolume(GameDataManager.GetInstance().settingDate.musicVolume);
                    }

                    this.isMusicMute = isMute;

                    break;
                case 2:
                    if (isMute)
                    {
                        AudioManager.GetInstance().ChangeAudioVolume(0);
                    }
                    else
                    {
                        //AudioManager.GetInstance().ChangeAudioVolume(GameDataManager.GetInstance().settingDate.audioVolume);
                    }

                    this.isAudioMute = isMute;

                    break;

                default:
                    break;
            }

        }

        public void ChangeAudioMute(bool value)
        {
            isAudioMute = value;

        }
        public void ChangeMusicMute(bool value)
        {
            isMusicMute = value;


        }
        #endregion
    }
}

