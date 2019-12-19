using UnityEngine;

/// <summary>
/// Obstacle that starts moving forward in its lane when the player is close enough.
/// </summary>
public class Missile : Obstacle
{
	static int s_DeathHash = Animator.StringToHash("Death");
	static int s_RunHash = Animator.StringToHash("Run");

	public Animator animator;
	public AudioClip[] movingSound;

	protected TrackSegment m_OwnSegement;

	protected bool m_IsMoving;
	protected AudioSource m_Audio;

    protected const int k_LeftMostLaneIndex = -1;
    protected const int k_RightMostLaneIndex = 1;
    protected const float k_Speed = 5f;

	public void Awake()
	{
		m_Audio = GetComponent<AudioSource>();
    }

	public override void Spawn(TrackSegment segment, float t)
	{
        int lane = Random.Range(k_LeftMostLaneIndex, k_RightMostLaneIndex + 1);

		Vector3 position;
		Quaternion rotation;
		segment.GetPointAt(t, out position, out rotation);

		GameObject obj = Instantiate(gameObject, position, rotation);
		obj.transform.SetParent(segment.objectRoot, true);
		obj.transform.position += obj.transform.right * lane * segment.manager.laneOffset;

		obj.transform.forward = -obj.transform.forward;

		obj.GetComponent<Missile>().m_OwnSegement = segment;
	}

	public override void Impacted()
	{
		base.Impacted();

		if (animator != null)
		{
			animator.SetTrigger(s_DeathHash);
		}
	}

	public void Update()
	{
	}
}
