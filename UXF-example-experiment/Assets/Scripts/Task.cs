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
        private float start_time;

        //exact moment of pressing key
        private float exact_moment;

        //detect key is pressed
        private bool isPressed;

        //bool after calibration
        private bool isCalibration = false;

        
        //
        public static string[] headers = { "Exact moment of pressing key", "Pressed key" };
        UXFDataTable continuousDataTable = new UXFDataTable(headers);

        // Start is called before the first frame update
        void Start()
        {
            //starting with zero error
            error = 0;
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartClickR();

                Debug.Log("CALIBRATE!!!");

                //calibration is on
                isCalibration = true;

                //key is detected to be pressed
                isPressed = true;

                keycode = "R";

                //record the exact moment of pressing key
                exact_moment = Time.time;

                ContinueousData();

                Invoke("EndClickR", 2.0f);
            }


            else if (Input.GetKeyDown(KeyCode.Q) 
                & Session.instance.currentTrialNum >= 1
                & isCalibration == true)
            {
                StartClickQ();

                //key is detected to be pressed
                isPressed = true;

                keycode = "Q";

                //record the exact moment of pressing key
                exact_moment = Time.time;

                ContinueousData();

                Invoke("EndClickQ", 2.0f);
            }


            else if (Input.GetKeyDown(KeyCode.E) 
                & Session.instance.currentTrialNum >= 1
                & isCalibration == true)
            {
                StartClickE();

                //key is detected to be pressed
                isPressed = true;

                keycode = "E";

                //record the exact moment of pressing key
                exact_moment = Time.time;

                //adding errors if press
                error++;
                Debug.Log("number of errors are " + error);

                ContinueousData();

                Invoke("EndClickE",2);
            }
        }

        //print out the LAST keycode user click
        public void PrintOutKeycode()
        {
            Session.instance.CurrentTrial.result["pressed key"] = keycode;
        }

        //assign number of errors into the data
        public void PrintOutErrors()
        {
            Session.instance.CurrentTrial.result["number of error"] = error;
        }

        //print out reaction time when press Q
        public void PrintOutReactionTime()
        {
            Session.instance.CurrentTrial.result["reaction time"] = reaction_time;
        }

        public void ContinueousData ()
        {
            ////assign data into the data table
            UXFDataRow dataTableResponse = new UXF.UXFDataRow();

            dataTableResponse.Add(("Exact moment of pressing key", exact_moment));
            dataTableResponse.Add(("Pressed key", keycode));

            continuousDataTable.AddCompleteRow(dataTableResponse);
        }

        //Export continueous data after each trial ends
        public void ExportContinueousData()
        {
            //save output
            UXF.Session.instance.SaveDataTable(continuousDataTable, "ContinuousDataTable");
        }



        void StartClickR()
        {
            intro.gameObject.SetActive(false);
            clickR.gameObject.SetActive(true);
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

        void EndClickR()
        {
            clickR.gameObject.SetActive(false);
            intro.gameObject.SetActive(true);

            //after calibration is over, we start collecting time between this and before press Q
            start_time = Time.time;

            //key is detected to not be pressed
            isPressed = false;
        }

        void StartClickQ()
        {
            intro.gameObject.SetActive(false);
            clickE.gameObject.SetActive(false);
            clickR.gameObject.SetActive(false);
            clickQ.gameObject.SetActive(true);

            //calculate reaction time
            reaction_time = Time.time - start_time;
        }

        void EndClickQ()
        {
            clickQ.gameObject.SetActive(false);
            intro.gameObject.SetActive(true);

            //next trial
            Session.instance.EndCurrentTrial();
            Session.instance.BeginNextTrial();

            //reset calibration
            isCalibration = false;

            //reset error back to zero when move to next trial
            error = 0;

            //key is detected to not be pressed
            isPressed = false;

            //stop recording headset data until next trial
            StopCoroutine(ManualRecord());
        }
        
        //only click E can move onto next trial
        void StartClickE()
        {
            intro.gameObject.SetActive(false);
            clickE.gameObject.SetActive(true);
            clickR.gameObject.SetActive(false);
            clickQ.gameObject.SetActive(false);
        }

        void EndClickE()
        {
            clickE.gameObject.SetActive(false);
            intro.gameObject.SetActive(true);

            //key is detected to not be pressed
            isPressed = false;
        }

    }
}
