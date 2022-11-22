using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Transform hoursPivot, minutesPivot, secondsPivot;
    const float degreesPerHour = 30f, degreesPerMinute = 6f, degreesPerSecond = 6f, degreesPerDayCycle = 15f;
    private DateTime timeDiscrete;
    private TimeSpan timeContinuous;
    public bool continuous = true;
    public Transform dayCycle;

    public Text textAMPM;
    private bool isAM;

    //[Range(0,23)]
    //public int hours;

    private float hours, minutes, hoursTemp;

    //upadate hours to float and dayCycle.localRotation to continuous time
    //update non-relative to change clock as well
    //move functionality to draffing on clock hands
    //set physics layers so don't delete parts of clock with left click
    //change paint feature to left click so it works on mobile version
    

    //Use this for initialization
    void Awake () 
    {
        timeDiscrete = DateTime.Now;
        timeContinuous = DateTime.Now.TimeOfDay;
        hours = (float)timeContinuous.TotalHours;
        minutes = (float)timeContinuous.TotalMinutes;
		hoursPivot.localRotation = Quaternion.Euler(0f, degreesPerHour * hours, 0f);
		minutesPivot.localRotation = Quaternion.Euler(0f, degreesPerMinute * minutes, 0f);
		secondsPivot.localRotation = Quaternion.Euler(0f, (float)timeContinuous.TotalSeconds * degreesPerSecond, 0f);
        //dayCycle.localRotation = Quaternion.Euler(hours * degreesPerDayCycle, 0f, 0f);
        
        if (hours <12)
        {
            isAM = true;
            textAMPM.text = "AM";
        }
        else 
        {
            isAM = false;
            textAMPM.text = "PM";
        }
	}
    private void Update () 
    {
        timeContinuous = DateTime.Now.TimeOfDay;
        //hours = (float)timeContinuous.TotalHours; //commented out for UpdateAMPM
        minutes = (float)timeContinuous.TotalMinutes;

        minutesPivot.localRotation = Quaternion.Euler(0f, minutes * degreesPerMinute, 0f);
        Debug.Log(hours);
        hoursPivot.localRotation = Quaternion.Euler(0f, hours * degreesPerHour, 0f);
        dayCycle.localRotation = Quaternion.Euler(hours * degreesPerDayCycle, 0f, 0f);//divided by 2 because
              
        
        if(continuous)
        {
            UpdateContinuous();
        }
        else 
        {
            UpdateDiscrete();
        }
    }

    void UpdateContinuous () 
    {
		secondsPivot.localRotation =
			Quaternion.Euler(0f, (float)timeContinuous.TotalSeconds * degreesPerSecond, 0f);
	}
    void UpdateDiscrete () 
    {
        timeDiscrete = DateTime.Now;
        secondsPivot.localRotation =
			Quaternion.Euler(0f, timeDiscrete.Second * degreesPerSecond, 0f);
	}

    public void UpdateTime(float clickedHourRotation) //click on an hour indicator
    {
        //if (clickedHourRotation < 0) clickedHourRotation +- 360;
        //realTime = false;
        hoursTemp = ((float)timeContinuous.TotalHours - (int)timeContinuous.TotalHours) + (clickedHourRotation / degreesPerHour);
        if (!isAM) hoursTemp += 12f;
        if((int)hoursTemp < (int)hours)
        {
            hours = hoursTemp;
            UpdateAMPM();
        }
        else hours = hoursTemp;
        
        //userSetHour - partialHourOffset + clickedHourRotation;
        //dayCycleOffset - dayCycleOffset + (clickedHourRotation - dayCycleOffset);
    }

    public void UpdateAMPM()
    {
        if(isAM)
        {
            isAM = false;
            textAMPM.text ="PM";
            hours += 12f;
        }
        else
        {
            isAM = true;
            textAMPM.text = "AM";
            hours -= 12f;
        }
    }

    public float getHours()
    {
        return hours;
    }

}




