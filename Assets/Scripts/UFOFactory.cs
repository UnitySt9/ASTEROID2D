using UnityEngine;

public class UFOFactory
{
    private GameObject ufoPrefab;

    public UFOFactory(GameObject prefab)
    {
        ufoPrefab = prefab;
    }

    public IUfo CreateUFO(Vector2 position, Transform spaceShipTransform)
    {
        GameObject ufoInstance = Object.Instantiate(ufoPrefab, position, Quaternion.identity);
        UFO ufoComponent = ufoInstance.GetComponent<UFO>();
        ufoComponent.Initialize(spaceShipTransform);
        return ufoComponent;
    }
}
