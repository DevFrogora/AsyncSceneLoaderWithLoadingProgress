using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallback();

        }
    }

    IEnumerator waiter()
    {
        Loader.LoaderCallback();
        yield return new WaitForSeconds(2);

    }
}
