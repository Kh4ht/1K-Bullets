using KH;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerExpCollector : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameTags.EXP_GEM))
            return;

        if (collision.TryGetComponent(out ExpGem expGem))
        {
            expGem.DisableExpGem();
            expGem.gameObject.SetActive(false);
            LevelManager.Ins.LMExperience.AddExpPoints(expGem.Data.ExpPoints);
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
