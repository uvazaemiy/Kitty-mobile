using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsService : Service<PrefabsService>
{
    public List<GameObject> Prefabs;
    
    public override void Execute()
    {

    }

    public static GameObject OpenUI(string alias) {
        if (!GetInstance().Prefabs.Exists(x => x.name == alias))
            return new GameObject();

        GameObject uiObject = Instantiate(GetInstance().Prefabs.Find(x => x.name == alias), GameObject.Find("Canvas").transform);
        uiObject.transform.SetAsLastSibling();
        return uiObject;
    }
    
    public static GameObject IntantiateRoot(string alias) {
        if (!GetInstance().Prefabs.Exists(x => x.name == alias))
            return new GameObject();

        return Instantiate(GetInstance().Prefabs.Find(x => x.name == alias));
    }
}
