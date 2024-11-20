using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour, IInteractable
{
    [Header("Para Tutorial")]
    [SerializeField] bool tornado = false, moco = false, sombra = false;
    //CreatePlayerTrap _createTrapScript;
    CreateShadow _createShadowScript;

    [SerializeField] GameObject _mesh, icono;

    //public delegate void VoidDelegate();
    public DelegateType.VoidDelegateTrans myAction;
    bool canAct = true;
    float _cd;

    [SerializeField] Sprite tornadoFoto, sombraFoto, encantarFoto, mocoFoto, hiloFoto;
    [SerializeField] Transform _lookingAt;
    SpriteRenderer _spriteRenderer;
    [SerializeField] AudioClip setTrap, setSlime, activarTrap;

    public delegate void VoidDelegate();
    public event VoidDelegate OnTrapActive;

    private void Awake()
    {

        if (tornado)
        {
            _spriteRenderer.sprite = tornadoFoto;
        }
        else if (moco)
        {
            _spriteRenderer.sprite = mocoFoto;
        }
        else if (sombra)
        {
            _spriteRenderer.sprite = sombraFoto;
        }
        else
        {
            GameManager.Instance.PlayerTraps.Add(this);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            myAction = delegate { };
            if (SelectorUI.habAct == 2)
            {
                gameObject.AddComponent<CreateShadow>();
                //_createShadowScript = GetComponent<CreateShadow>();
            }
            if (SelectorUI.habAct == 3)
                myAction += DestroyTrap;

            #region Icono
            if (SelectorUI.habAct == 1)
            {
                _spriteRenderer.sprite = tornadoFoto;
            }
            else if (SelectorUI.habAct == 2)
            {
                _spriteRenderer.sprite = sombraFoto;
            }
            else if (SelectorUI.habAct == 3)
            {
                _spriteRenderer.sprite = encantarFoto;
            }
            else if (SelectorUI.habAct == 4)
            {
                _spriteRenderer.sprite = mocoFoto;
            }
            else if (SelectorUI.habAct == 5)
            {
                _spriteRenderer.sprite = hiloFoto;
            }
            else if (SelectorUI.habAct == 6)
            {
                _spriteRenderer.sprite = null;
            }
            else if (SelectorUI.habAct == 7)
            {
                _spriteRenderer.sprite = null;
            }
            else if (SelectorUI.habAct == 8)
            {
                _spriteRenderer.sprite = null;
            }
            else if (SelectorUI.habAct == 0)
            {
                _spriteRenderer.sprite = null;
            }
            #endregion
        }


    }

    private void Update()
    {
        if (_lookingAt == null) _lookingAt = GameManager.Instance.Camera.transform;

        icono.transform.LookAt(_lookingAt.position);
    }
    public void Initialize(CreatePlayerTrap newScript, DelegateType.VoidDelegateTrans newAction, float newCD)
    {
        //_createTrapScript = newScript;
        myAction = newAction;
        _cd = newCD;
    }

    public void Interact()
    {
        if (!canAct) return;
        StartCoroutine(SetInactive());

        #region Comment
        //if (_createShadowScript)
        //{
        //    _createShadowScript.SpawnShadow(transform);
        //}
        //else 
        #endregion

        myAction(transform);
        OnTrapActive();
        Destroy(gameObject);
        //CreatePlayerTrap.TrapActivada();
    }

    #region Comment
    //private void OnTriggerEnter(Collider other)
    //{
    //    var asus = other.gameObject.GetComponent<Asustable>();
    //    if (asus && asus._scared)
    //    {
    //        AsustableDetected(asus);
    //    }
    //} 
    #endregion

    public void AsustableDetected(Asustable target)
    {
        print($"<color=#18f18F> Austable detectado </color>");
        // Trampa Activada +1
        Interact();
    }

    void Test()
    {
        print($"<color=#C8318F> Este mensaje es una prueba </color>");
    }

    IEnumerator SetInactive()
    {
        _mesh.SetActive(false);
        canAct = false;
        Destroy(gameObject);
        GameManager.Instance.createPlayerTrap.currentTraps--;

        yield return new WaitForSeconds(_cd);

        _mesh.SetActive(true);
        canAct = true;
    }

    void DestroyTrap(Transform a)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerTraps.Remove(this);
    }
}
