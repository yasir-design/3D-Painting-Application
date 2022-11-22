using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
     /* Start is called before the first frame update
    void Start()
    {
        Debug.Log("this is the start method");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("this is the update method");
    }
    */

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + " was clicked, only world position returned is " + this.gameObject.transform.position);
    }
}
