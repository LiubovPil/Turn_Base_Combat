using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
