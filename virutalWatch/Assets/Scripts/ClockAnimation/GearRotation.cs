using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public class GearRotation : MonoBehaviour {

	public LoopType loopType = LoopType.Incremental;
	public float angle = 0f;
	public float duration = 1f;
	public float delayBefore = 0f;
	public float delayAfter = 0f;
	public EaseType easing = EaseType.Linear;
	
	// Use this for initialization
	void Start () {
		Sequence seq = new Sequence(new SequenceParms().Loops(-1, loopType));
		seq.AppendInterval(delayBefore);
		Tweener tween = HOTween.To(this.transform, duration, new TweenParms().Ease(easing).Prop("rotation", new Vector3(0,0,angle), true));
		seq.Append(tween);
		seq.AppendInterval(delayAfter);
		
		seq.Play();		
	}
	

}
