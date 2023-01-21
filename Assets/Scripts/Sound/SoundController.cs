using UnityEngine;

namespace Managers
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;

        [SerializeField] private SoundMix _animation_eat;
        [SerializeField] private SoundMix _animation_game;
        [SerializeField] private SoundMix _animation_toilet;
        [SerializeField] private SoundMix _animation_sleep;
        [Space]
        [SerializeField] private SoundMix _get_item;
        [SerializeField] private SoundMix _use_item;
        [SerializeField] private SoundMix _remove_item;
        [SerializeField] private SoundMix _get_money;
        [SerializeField] private SoundMix _error_code;
        [SerializeField] private SoundMix _click;
        [Space]
        [SerializeField] private SoundMix _background;
        [SerializeField] private SoundMix cat_sound;

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void Eat() => _animation_eat.Play();
        public void Game() => _animation_game.Play();
        public void Toilet() => _animation_toilet.Play();
        public void Sleep() => _animation_sleep.Play();
        public void Happy() => cat_sound.Play();
        public void StopHappy() => cat_sound.Stop();
        public void GetItem() => _get_item.Play();
        public void UseItem() => _use_item.Play();
        public void Click() => _click.Play();
        public void RemoveItem() => _remove_item.Play();
        public void GetMoney() => _get_money.Play();
        public void ErrorCode() => _error_code.Play();
        public SoundMix CatBackground => _background;
    }
}
