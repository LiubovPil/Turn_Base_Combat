using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHandler : State
{
    
    public ScoreHandler()
    {
        
    }
    public override IEnumerator Enter()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
