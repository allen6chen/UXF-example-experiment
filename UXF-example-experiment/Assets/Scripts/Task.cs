using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.UI;

namespace UXFtutorial
{
    public class Task : MonoBehaviour
    {
        //UI component
        public Text intro;
        public Text clickQ;
        public Text clickE;
        public Text clickR;

        //headset
        public PositionRotationTracker positionRotationTracker;

        //which keycode does the user press
        private string keycode;

        //number of error
        private float error;

        //reaction time
        private float reaction_time;

        //bool after calibration
        private bool isCalibration = false;

        // Start is called before the first frame update
        void Start()
        {
            error = 0;
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartClickR();

                Debug.Log("CALIBRATE!!!");

                isCalibration = true;

                keycode = "R";

                Invoke("EndClickR", 5.0f);
            }


            else if (Input.GetKeyDown(KeyCode.Q) 
                & Session.instance.currentTrialNum >= 1
                & isCalibration == true)
            {
                StartClickQ();

                //reset error back to zero when move to next trial
                error = 0;

                keycode = "Q";

                Invoke("EndClickQ", 2.0f);
            }


            else if (Input.GetKeyDown(KeyCode.E) 
                & Session.instance.currentTrialNum >= 1
                & isCalibration == true)
            {
                StartClickE();

                keycode = "E";

                //adding errors if press
                error++;
                Debug.Log("number of errors are " + error);

                Invoke("EndClickE",2);
            }
        }

        public void SetUp()
        {
            Session.instance.GetTrial();
        }

        //print out the keycode user click
        public void PrintOutKeycode()
        {
            Session.instance.CurrentTrial.result["pressed key"] = keycode;
        }

        //assign number of errors into the data
        public void PrintOutErrors()
        {
            Session.instance.CurrentTrial.result["number of error"] = error;
        }

        //print out reaction time
        public void PrintOutReactionTime()
        {
            Session.instance.CurrentTrial.result["reaction time"] = reaction_time;
        }

        public void StartClickR()
        {
            StartCoroutine(ManualRecord());
            
        }

        //manual record headset
        private IEnumerator ManualRecord()
        {
            while (true)
            {
                if (positionRotationTracker.Recording) positionRotationTracker.RecordRow();

                yield return new WaitForSeconds(0.2f);
            }
        }

        public void EndClickR()
        {
            intro.gameObject.SetActive(true);
            clickR.gameObject.SetActive(false);
        }

        public void StartClickQ()
        {
            intro.gameObject.SetActive(false);
            clickE.gameObject.SetActive(false);
            clickR.gameObject.SetActive(false);
            clickQ.gameObject.SetActive(true);
        }

        public void EndClickQ()
        {
            intro.gameObject.SetActive(true);
            clickQ.gameObject.SetActive(false);
            
            //next trial
            Session.instance.EndCurrentTrial();
            Session.instance.BeginNextTrial();

            
        }
        
        //only click E can move onto next trial
        public void StartClickE()
        {
            intro.gameObject.SetActive(false);
            clickE.gameObject.SetActive(true);
            clickR.gameObject.SetActive(false);
            clickQ.gameObject.SetActive(false);
        }

        public void EndClickE()
        {
            intro.gameObject.SetActive(true);
            clickE.gameObject.SetActive(false);
        }

    }
}
