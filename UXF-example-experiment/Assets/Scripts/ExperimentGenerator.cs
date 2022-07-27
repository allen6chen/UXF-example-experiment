using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.SceneManagement;

namespace UXFtutorial
{
    public class ExperimentGenerator : MonoBehaviour
    {

        public void Generate(Session session)
        {
            //we generate one block which is equal to independent variable
            Block task1 = session.CreateBlock(session.settings.GetInt("block1_numtrials"));

            //generate a delay to continue next move after calibrate
            foreach (var trial in task1.trials)
            {
                trial.settings.SetValue("delay", Random.Range(0.5f,5.0f));
            }
        }

    }
}
