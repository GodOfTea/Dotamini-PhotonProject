using UnityEngine;
using UnityEngine.Events;

public class OpenURLOnClick : MonoBehaviour
{
    [SerializeField] private UnityEvent<string> goToUrlsController;

    private void OnClick ()
	{
		UILabel lbl = GetComponent<UILabel>();
		
		if (lbl != null)
		{
			string url = lbl.GetUrlAtPosition(UICamera.lastWorldPosition);
            
			if (string.IsNullOrEmpty(url) == false)
                Application.OpenURL(url);
		}
	}
}
