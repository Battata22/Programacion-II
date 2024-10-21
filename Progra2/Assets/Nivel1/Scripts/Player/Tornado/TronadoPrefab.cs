using UnityEngine;

public class TronadoPrefab : MonoBehaviour
{
    [SerializeField] float radio, fuerzaTorque, speed, rotSpeed, radioDist, fuerzaArriba;
    [SerializeField] LayerMask maskTornado;
    [SerializeField] Collider[] colliders;


    void Start()
    {
        //Torque();
        Invoke("SelfDestruct", 5);
    }

    void FixedUpdate()
    {
        #region Comment
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //#region Comment
        //    //colliders[i].transform.LookAt(transform.position);
        //    //colliders[i].transform.right += new Vector3(rotSpeed * Time.fixedDeltaTime, 0f, 0f); 
        //    #endregion
        //    if (Vector3.Distance(colliders[i].transform.position, transform.position) < radioDist)
        //    {
        //        Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
        //        rb.AddForce(transform.right * rotSpeed);

        //        #region Comment
        //            //colliders[i].transform.position += (transform.position - colliders[i].transform.position) * Time.fixedDeltaTime * speed;


        //            //if (colliders[i].transform.position.y > transform.position.y + 0.5f || colliders[i].transform.position.y < transform.position.y - 0.5f)
        //            //{
        //            //    colliders[i].transform.position += new Vector3(0f, (transform.position.y - colliders[i].transform.position.y), 0f) * Time.fixedDeltaTime * speed;
        //            //} 
        //            #endregion
        //    }
        //} 
        #endregion
    }

    #region Comment
    public void Torque()
    {

        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Rigidbody>() != null && collider.GetComponent<Pickable>() != null)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                rb.AddForce(transform.up * fuerzaTorque);
                rb.AddTorque(transform.right * fuerzaTorque);
            }

        }
    } 
    #endregion


    private void OnTriggerStay(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, maskTornado);

        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Pickable>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            var dir = transform.position - other.transform.position;
            var conterDir = other.transform.position - transform.position;

            if(Vector3.Distance(other.transform.position, transform.position) > 1)
            {
                if (rb.mass <= 2)
                {
                    rb.AddForce((dir * rotSpeed * (1 / Vector3.Distance(transform.position, other.transform.position)) * Time.fixedDeltaTime) / rb.mass, ForceMode.Impulse);
                }
                else if (rb.mass > 2 && rb.mass <= 4)
                {
                    rb.AddForce((dir * rotSpeed * (1 / Vector3.Distance(transform.position, other.transform.position)) * Time.fixedDeltaTime) / rb.mass * 2, ForceMode.Impulse);
                }
            }
            else
            {
                rb.AddForce(conterDir * Time.fixedDeltaTime, ForceMode.Impulse);
            }

            rb.AddForce(transform.up * Time.fixedDeltaTime * fuerzaArriba, ForceMode.Impulse);

            rb.AddTorque(dir);

            #region Comment
            //rb.useGravity = false;

            //other.transform.LookAt(transform.position);
            //other.transform.RotateAround(Vector3.zero, transform.up, 10 * rotSpeed * Time.fixedDeltaTime);

            //other.transform.position += (transform.position - other.transform.position) * Time.fixedDeltaTime * speed;

            //other.isTrigger = true; 
            #endregion
        }

    }

    void SelfDestruct()
    {
        Destroy(gameObject);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radio - 0.5f, maskTornado);

        foreach (Collider collider in colliders)
        {
            if(collider.GetComponent<Pickable>() != null)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1)));
                rb.AddTorque(new Vector3(Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1)));
            }
  
        }
    }

    #region Comment
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Pickable>() != null)
    //    {
    //        Rigidbody rb = other.GetComponent<Rigidbody>();
    //        //rb.useGravity = true;
    //        //other.isTrigger = false;
    //    }
    //} 
    #endregion

}
