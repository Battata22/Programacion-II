using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatAlertIcon : MonoBehaviour
{
    SpriteRenderer _renderer;
    [SerializeField] Sprite _alertIcon;
    [SerializeField] Canvas _canvas;
    [SerializeField] Image _timerFill;
    [SerializeField] Image _angerFill;

    float _maxTime=1, _maxAnger=1;

    //[SerializeField] 
    Transform _lookingAt;

    bool _active;
    public bool active
    {
        get
        {
            return _active;
        }
        set
        {
            if (value)
            {
                ActivateIcon();
            }
            else
            {
                DeactivateIcon();
            }
        }
    }

    float _charge;
    public float charge
    {
        get { return _charge; }
        set
        {
            UpdateAnger(value);
        }
    }

    float _timer;
    public float timer
    {
        get { return _timer; }
        set
        {
            UpdateTimer(value);
        }
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        //UpdateAnger(0f);
        //UpdateTimer(0f);
        active = false;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        _lookingAt = GameManager.Instance.Camera.transform;
    }

    private void Update()
    {
        if (!_active) return;
        transform.LookAt(_lookingAt.position);
    }

    void ActivateIcon()
    {
        _active = true;
        _renderer.sprite = _alertIcon;

        _canvas.gameObject.SetActive(true);
    }

    void DeactivateIcon()
    {
        _active = false;
        _renderer.sprite = null;

        UpdateAnger(0f);
        UpdateTimer(0f);

        _canvas.gameObject.SetActive(false);
    }

    public void SetMaxTimers(float newMaxTime, float newMaxAnger)
    {
        _maxTime = newMaxTime;
        _maxAnger = newMaxAnger;
    }

    void UpdateTimer(float newTime)
    {
        _timer = newTime;
        _timerFill.fillAmount = newTime / _maxTime;
    }

    void UpdateAnger(float newCharge)
    {
        _charge = newCharge;
        _angerFill.fillAmount = _charge / _maxAnger;
    }
}
