using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;

namespace UXFtutorial
{
    public class TaskManager : MonoBehaviour
    {
        //number of errors
        private int error;

        //reaction time
        private float reactionTime;

        //two keycodes
        public Task q;
        public Task e;

        private void Update()
        {

        }
        public void StartTask(Trial trial)
        {
          

            StartCoroutine(EndAndPrepare());
        }

        IEnumerator EndAndPrepare()
        {

            //Dependent variable
            float altitude = this.gameObject.transform.position.z;
            Session.instance.CurrentTrial.result["travel distance"] = altitude;

            yield return new WaitForSeconds(5.0f);

            Session.instance.CurrentTrial.End();

            this.gameObject.transform.localPosition = Vector3.zero;

            Movement.travelDistance = 0;

            // if last trial, end session.
            if (Session.instance.CurrentTrial == Session.instance.LastTrial)
            {
                Session.instance.End();
            }
            else
            {
                // begin next after 2 second delay
                Session.instance.BeginNextTrial();
            }
        }

    }
}
