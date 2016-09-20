using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class SceneController : MonoBehaviour {

    public Camera[] cameras;
    public int iter;
    public Text clock;
    public Text siteInfo;
//arrays of sites
    public GameObject[] USSites;
    public GameObject[] AUSites;
    public GameObject[] UKSites;
    bool ispinging = false;

   

	// Update is called once per frame
	void Update () {
        DateTime theClock = DateTime.Now;
        float time = Time.time;
        float t = time % 100;



        string min = "";
        if (theClock.Minute < 10) min = "0" + min;
        else min = theClock.Minute+"";
        //the following tests the modulo of time and decides based on the timer, what area to display info for.
        //US View and info
        if (t<33.3)
        {
            
            int ussitesdown = 0;
            int ussites = 0;
            
            for(int i =0; i< USSites.Length; i++)
            {
                ussitesdown += USSites[i].GetComponent<Node>().GetSitesDown();
                ussites += USSites[i].GetComponent<Node>().getSiteTotal();
                if (t < 0.5 && t > 0 || t<30 && t>29.5)
                {
                    USSites[i].GetComponent<Node>().pingSites();
                    ispinging = true;
                }
                
            }
           
            clock.text = theClock.Hour + ":" + min;
            cameras[1].gameObject.SetActive(false);
            cameras[2].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
            siteInfo.text = "Total sites: " + ussites + "\nSites down: " + ussitesdown;
            
        }
        //EU view and info
        else if (t > 33.33 && t< 66.33)
        {
            
            int uksitesdown = 0;
            int uksites = 0;
            for (int i = 0; i < UKSites.Length; i++)
            {
                uksitesdown += UKSites[i].GetComponent<Node>().GetSitesDown();
                uksites += UKSites[i].GetComponent<Node>().getSiteTotal();
               

            }
            siteInfo.text = "Total sites: " + uksites + "\nSites down: " + uksitesdown;

            if (theClock.Hour + 7 > 24)
            {
                clock.text = (theClock.Hour -17) + ":" + min;
            }
            else clock.text = (theClock.Hour+7) + ":" + min;

            for (int i = 0; i < UKSites.Length; i++)
            {
                uksitesdown += UKSites[i].GetComponent<Node>().GetSitesDown();
                uksites += UKSites[i].GetComponent<Node>().getSiteTotal();
                if (t < 33.5 && t > 33 || t < 66.33 && t > 60)
                {
                    UKSites[i].GetComponent<Node>().pingSites();
                    ispinging = true;
                }

            }


            cameras[1].gameObject.SetActive(false);
            cameras[2].gameObject.SetActive(true);
            cameras[0].gameObject.SetActive(false);
        }
        //AU view and info
        else
        {
            if (theClock.Hour + 16 > 24)
            {
                clock.text = (theClock.Hour - 8) + ":" + min;
            }
            else
            clock.text = (theClock.Hour + 16) + ":" + min;


            int ausites = 0 ;
            int ausitesdown = 0;
            for (int i = 0; i < AUSites.Length; i++)
            {
                ausitesdown += AUSites[i].GetComponent<Node>().GetSitesDown();
                ausites += AUSites[i].GetComponent<Node>().getSiteTotal();
               
                if (t < 67.5 && t > 66.33 || t < 100 && t > 99.3)
                {
                    AUSites[i].GetComponent<Node>().pingSites();
                    ispinging = true;
                }

            }
            siteInfo.text = "Total sites: " + ausites + "\nSites down: " + ausitesdown;
            cameras[1].gameObject.SetActive(true);
            cameras[0].gameObject.SetActive(false);
            cameras[2].gameObject.SetActive(false);
        }
      

	}
}
