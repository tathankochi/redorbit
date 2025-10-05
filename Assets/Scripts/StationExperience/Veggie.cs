using UnityEngine;

public class Veggie : MonoBehaviour, IInteractable
{
    public SceneSwitch sceneSwitch;
    void Start()
    {
        sceneSwitch = FindSceneSwitchInScene();
        if (sceneSwitch == null)
        {
            Debug.LogError("SceneSwitch component not found in the scene.");
        }
    }
    void Update()
    {

    }
    public void Interact()
    {
        Debug.Log("Interacted with Veggie");
        Dialog dialog = FindDialogInScene();
        if (dialog == null)
        {
            Debug.LogError("Dialog component not found in the scene.");
            return;
        }
        if (dialog.gameObject.activeSelf) return;


        dialog.ResetDialog();
        dialog.gameObject.SetActive(true);
        dialog.dialogLines = new string[] {
            "You found your veggie!",
            "Veggie is a plant growth system designed to grow fresh vegetables in microgravity.",
            "It provides light, water, and nutrients to plants, allowing astronauts to grow and eat crops like lettuce, radishes, and zinnias.",
            "Veggie helps study how plants adapt to space and supports future long-duration missions by producing fresh food onboard.",
            "Press SPACE to continue ..."
        };
        dialog.StartDialog();
        dialog.onDialogComplete += () =>
        {
            sceneSwitch.SwitchScene("Plants");
        };
        AddCompletedMission addCompletedMission = FindAddCompletedMissionInScene();
        if (addCompletedMission == null)
        {
            Debug.LogError("AddCompletedMission component not found in the scene.");
            return;
        }
        addCompletedMission.MarkMissionComplete("Interact with the Veggie");
        ChecklistManager checklistManager = FindChecklistManagerInScene();
        if (checklistManager == null)
        {
            Debug.LogError("ChecklistManager component not found in the scene.");
            return;
        }
        checklistManager.RefreshChecklist();
    }
    public bool IsInteractable()
    {
        return true;
    }
    private Dialog FindDialogInScene()
    {
        Dialog[] dialogs = Resources.FindObjectsOfTypeAll<Dialog>();
        foreach (Dialog dialog in dialogs)
        {
            if (dialog.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return dialog;
            }
        }
        return null;
    }
    private AddCompletedMission FindAddCompletedMissionInScene()
    {
        AddCompletedMission[] managers = Resources.FindObjectsOfTypeAll<AddCompletedMission>();
        foreach (AddCompletedMission manager in managers)
        {
            if (manager.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return manager;
            }
        }
        return null;
    }
    private ChecklistManager FindChecklistManagerInScene()
    {
        ChecklistManager[] managers = Resources.FindObjectsOfTypeAll<ChecklistManager>();
        foreach (ChecklistManager manager in managers)
        {
            if (manager.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return manager;
            }
        }
        return null;
    }
    private SceneSwitch FindSceneSwitchInScene()
    {
        SceneSwitch[] switches = Resources.FindObjectsOfTypeAll<SceneSwitch>();
        foreach (SceneSwitch sceneSwitch in switches)
        {
            if (sceneSwitch.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return sceneSwitch;
            }
        }
        return null;
    }
}