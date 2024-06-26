using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timeLine;
    [SerializeField] PlayerInput abo_hamdi;


    bool isDoneFirstDecision = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        abo_hamdi.actions["Interact"].performed += onInteract;
        timeLine = GetComponent<PlayableDirector>();
        timeLine.Pause();
    }

    private void onInteract(InputAction.CallbackContext context)
    {
        if (!isDoneFirstDecision)
        {
            timeLine.Resume();
            isDoneFirstDecision = true;
        }
        else
            SceneManage.Instance.LoadBossFightLevel();
    }

}
