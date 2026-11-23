using Godot;
using System;

public partial class Captain : Participant
{
	public override void ApplyPenalty(int amount)
	{
		if (amount <= 0) return;

		int reducedPenalty = Math.Max(1, amount - 10);
		base.ApplyPenalty(reducedPenalty);
	}
}
