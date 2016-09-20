using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    public string[] pingnames;

   public Text txt;
    public float offset;
    public GameObject pin;
    public int isDown;
    public int size;
    private int sitesDown;
    IEnumerator Offset(float t)
    {
        yield return new WaitForSeconds(t);
    }
    IEnumerator pause()
    {
        yield return new WaitForEndOfFrame();
    }
    IEnumerator Timer()
    {
       // while (true)
        {
            isDown = 0;
            sitesDown = 0;
            size = pingnames.Length;
            for (int i = 0; i < size; i++)
            {
                Debug.Log("pinging " + pingnames[i]);
              
                StartCoroutine(CheckConnection(pingnames[i]));
                pause();
            }
            
            
            yield return new WaitForSeconds(10);
        }

    }

    IEnumerator CheckConnection(string ip)
    {
        const float timeout = 5f;
        float startTime = Time.timeSinceLevelLoad;
        UnityEngine.Ping ping = new UnityEngine.Ping(ip);

       // while (true)
        {
            Debug.Log("Checking network...");
            while (!ping.isDone)
            {
                Debug.Log("time since level load "+startTime);


                if (Time.timeSinceLevelLoad - startTime > timeout)
                {
                    Debug.Log("No network at " + ip + " (" + this.name + ")");
                    pin.GetComponent<Renderer>().material.color = Color.red;
                    isDown++;
                    sitesDown++;
                    yield break;

                }
                else
                {
                    Debug.Log("Network available.");
                    if (isDown == 0)
                    {

                        pin.GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        isDown--;
                    }

                    yield break;
                }

            }
            if (Time.timeSinceLevelLoad - startTime > timeout)
            {
                Debug.Log("No network at " + ip + " (" + this.name + ")");
                pin.GetComponent<Renderer>().material.color = Color.red;
                isDown++;
                sitesDown++;
                yield break;

            }
            yield return new WaitForEndOfFrame();
    }
    }
    void Start()
    {
        sitesDown = 0;
        Offset(offset);
      //  StartCoroutine(Timer());
        
    }	
    public void pingSites()
    {
        sitesDown = 0;
        Offset(offset);
        StartCoroutine(Timer());
    }

    public int getSiteTotal()
    {
        return size;
    }
    public int GetSitesDown()
    {
        return sitesDown;
    }
    
}

