using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalStrike : MonoBehaviour
{
    //a
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _exploPrefab;
    [SerializeField] float _atkCd;
    [SerializeField] float _chargeTime;
    [SerializeField] float _scaleSpeed;
    [SerializeField] float _dmgRadius;
    [SerializeField] LayerMask _playerMask;

    float _lastAtk = 0;
    float _internalCharge = 0;

    bool _onAtk = false;
    bool _canAtk = false;

    [Header("<color=green>Dev</color>")]
    [SerializeField] KeyCode dev_startAtackIn;
    [SerializeField] KeyCode dev_stopAtackIn;

    private void Start()
    {
        _lastAtk = -_atkCd;
    }

    private void Update()
    {
        if(!_canAtk && Time.time - _lastAtk > _atkCd)
        {
            _canAtk=true;
        }

        if (Input.GetKeyDown(dev_startAtackIn))
        {
            StartAttack();
        }
        if (Input.GetKeyDown(dev_stopAtackIn))
        {
            StopAttack();
        }
    }

    void StartAttack()
    {
        if (!_canAtk) return;
        Debug.Log("<color=green>Arranca el ataque</color>");
        _canAtk = false;

        var newLaser = Instantiate(_laserPrefab, GameManager.Instance.Player.transform.position, Quaternion.identity);

        StartCoroutine(Attack(newLaser));
    }

    IEnumerator Attack(GameObject laser)
    {
        _onAtk=true;

        while (_onAtk && _internalCharge < _chargeTime)
        {
            //scale ray
            if(_chargeTime - _internalCharge > 0.8f)
            laser.transform.position = GameManager.Instance.Player.transform.position;
            if (laser.transform.lossyScale.x > 0)
                //laser.transform.localScale *= 0.8f * Time.deltaTime;
                laser.transform.localScale -= new Vector3(_scaleSpeed, _scaleSpeed, _scaleSpeed) * _internalCharge/_chargeTime;
            else
                laser.transform.localScale -= new Vector3(0, 0, 0);

            //laser.transform.localScale.y = 3f;

            _internalCharge += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        Destroy(laser);
        if (_onAtk)
        {
            //Complete attack
            //_lastAtk= Time.time;
            //_onAtk = false;
            StopAttack();

            Debug.Log("<color=red>Atacar al player</color>");

            //sumon dmg shit
            DamagePlayer(laser);
        }

    }

    void DamagePlayer(GameObject laser)
    {

        var explo = Instantiate(_exploPrefab, laser.transform.position, Quaternion.identity);
        explo.transform.localScale = new Vector3(_dmgRadius,_dmgRadius,_dmgRadius) * 1.8f;
        Destroy(explo, 0.3f);

        Collider[] _playerInRad;
        _playerInRad = Physics.OverlapSphere(laser.transform.position, _dmgRadius, _playerMask);

        foreach (var player in _playerInRad)
        {
            Debug.Log($"<color=red>Player = {player.name}</color>");
            player.GetComponent<Player>().GetDamage();
        }

        #region comment
        //GameManager.Instance.Player.GetDamage();

        //var player = GameManager.Instance.Player;
        //Debug.Log($"<color=yellow>Laser pos = {transform.position}</color>");
        //Debug.Log($"<color=#ff00ff>Player pos = {player.transform.position}</color>");


        //if (Vector3.SqrMagnitude(player.transform.position - transform.position) < (_dmgRadius * _dmgRadius))
        //{
        //    Debug.Log($"<color=red>Player = {player.name}</color>");

        //    player.GetDamage();
        //} 
        #endregion
    }

    void StopAttack()
    {
        Debug.Log("<color=yellow>Frena el ataque</color>");

        if (!_onAtk) return;
        _onAtk=false;
        _lastAtk = Time.time;
        _internalCharge = 0;
    }
}
