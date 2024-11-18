using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip song;
    AudioSource source;
    [SerializeField] float speed;
    float waitMove, waitScale;
    bool pausado = false, susto = false;
    [SerializeField] Transform hijo;
    [SerializeField] LayerMask npc;
    [SerializeField] Asustable _npcInRange;
    [SerializeField] Slider sustos;

    void Start()
    {
        Destroy(gameObject, 4);
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        animacion();
        Escala();

        if (Input.GetKeyUp(KeyCode.Escape) && !pausado)
        {
            source.Pause();
            pausado = true;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && pausado)
        {
            source.Play();
            pausado = false;
        }

    }

    public void animacion()
    {
        waitMove += Time.deltaTime;
        if (waitMove <= 2)
        {
            hijo.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        }
    }

    public void Escala()
    {
        waitScale += Time.deltaTime;
        if (waitScale <= 1.5)
        {
            if (hijo.localScale.x <= 0.03)
            {
                hijo.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime * 0.05f;
            }
        }
        else
        {
            if (hijo.localScale.x >= 0.01)
            {
                hijo.localScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime * 0.05f;
            }
        }
    }

    #region Comment
    //public void Susto()
    //{
    //    if (!susto)
    //    {
    //        susto = true;
    //        Collider[] colliders;
    //        colliders = Physics.OverlapSphere(transform.position, 80f, npc);
    //        foreach (var collider in colliders)
    //        {
    //            //Debug.Log("<color=blue> Buscando Npc en Susto</color>");
    //            if (collider.TryGetComponent<NPC>(out _npcInRange))
    //            {
    //                //Debug.Log("<color=pink> NPC en area Susto </color>");
    //                if (Vector3.SqrMagnitude(transform.position - _npcInRange.transform.position) <= (16f * 16f) && 1 > 0)
    //                {
    //                    _npcInRange.GetScared(1);

    //                }
    //                else
    //                {
    //                    _npcInRange.GetDoubt(transform.position);
    //                }
    //            }
    //        }
    //    }
    //} 
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Asustable>(out _npcInRange))
        {
            if(_npcInRange != null)
            {
                _npcInRange.GetScared(1);
                sustos.value++;
            }
        }
    }

}
