using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeCameraWithDelay());
    }

    private IEnumerator ChangeCameraWithDelay()
    {
        GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Hook);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Waterfall);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.River);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
