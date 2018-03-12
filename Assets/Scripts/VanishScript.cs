using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishScript : MonoBehaviour {

    public void Vanish()
    {
        StartCoroutine(DoVanish());
    }

    IEnumerator DoVanish()
    {
        while (gameObject.activeSelf)
        {
            gameObject.transform.localScale = new Vector3(0.9f * gameObject.transform.localScale.x, 0.9f * gameObject.transform.localScale.y, 0.9f * gameObject.transform.localScale.z) ;

            if(gameObject.transform.localScale.x <= 0.1f)
            {
                gameObject.SetActive(false);
            }

            yield return null;
        }

        //canvasGroup.interactable = false;
        yield return null;
    }

    // Update is called once per frame
    void Update () {

        /*
         * For testing purposes using Down key and Mouse 0 Button
         * 
         */
         
        if (Input.GetKeyDown("down"))
        {
            Vanish();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit _hit;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray,out _hit, Mathf.Infinity))
            {
                if(_hit.collider.tag == "target")
                {
                    _hit.collider.GetComponent<VanishScript>().Vanish();
                }
            }
        }
    }
}
