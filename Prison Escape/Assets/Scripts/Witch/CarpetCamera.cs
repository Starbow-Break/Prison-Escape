using UnityEngine;

public class CarpetCamera : MonoBehaviour, IFocusable
{
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera("Camera_RedCarpet");
        CursorLocker.instance.UnlockCursor();
        actor.GetComponent<PlayerMovement>().enabled = false;
        actor.GetComponent<PlayerItemPickUp>().enabled = false;
    }

    public void UnFocus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera("Player Camera");
        CursorLocker.instance.LockCursor();
        actor.GetComponent<PlayerMovement>().enabled = true;
        actor.GetComponent<PlayerItemPickUp>().enabled = true;
    }
}
