using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMicroondas : SFX
{
    [SerializeField] AudioClip _microondas;

    [Header("<color=red>Pal Puzzle</color>")]
    [SerializeField] MicroondasPuzzle _microondasPuzzle;
    public MicroondasPuzzle MicroondasPuzzle { get { return _microondasPuzzle; } }

    protected override void Awake()
    {
        base.Awake();
        _audioClip = _microondas;
        _microondasPuzzle = GetComponent<MicroondasPuzzle>();
    }

    public override void PlayMusic(AudioClip _clip1)
    {
        //base.PlayMusic(_microondas);
        //_audioSource.clip = _microondas;

        Debug.Log($"<color=#b0f0df> El clip de mierda dura {_microondas.length} {_microondas.name}</color>");
        
        if (isPlaying == false)
        {
            isPlaying = true;
            _audioSource.loop = false;
            if (clip == false)
            {
                clip = true;
                _audioSource.clip = _microondas;
            }
            _audioSource.Play();
            _microondasPuzzle.StartCountDown(_microondas.length);
        }
        else if (isPlaying == true)
        {
            //isPlaying = false;
            //_audioSource.Pause();
            //_audioSource.loop = false;
            RestartDuration(_microondas);
        }
        _chocamiento.ChocoSonoro(transform.position);
        
    }

    void RestartDuration(AudioClip _clip)
    {
        Debug.Log($"<color=#b0f0df> El clip Reseteado</color>");

        _audioSource.Stop();
        _audioSource.loop = false;
        if (clip == false)
        {
            clip = true;
            _audioSource.clip = _clip;
        }
        _audioSource.Play();
        _microondasPuzzle.RestartCountDown(_microondas.length);
    }
}
