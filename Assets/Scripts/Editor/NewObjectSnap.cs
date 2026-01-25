using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
static class NewObjectSnap
{
    static NewObjectSnap()
    {
        ObjectChangeEvents.changesPublished += ChangesPublished;
    }

    private static void ChangesPublished(ref ObjectChangeEventStream stream)
    {
        if (!EditorSnapSettings.snapEnabled || !EditorSnapSettings.gridSnapEnabled)
            return;

        var len = stream.length;
        for (var i = 0; i < len; i++)
        {
            if (stream.GetEventType(i) != ObjectChangeKind.CreateGameObjectHierarchy)
                continue;

            stream.GetCreateGameObjectHierarchyEvent(i, out var e);
            int id = e.instanceId;

            if (AssetDatabase.Contains(id))
                continue;

            if (EditorUtility.InstanceIDToObject(id) is not GameObject go)
                continue;

            Handles.SnapToGrid(new[] { go.transform });
        }
    }
}
