public partial class TimeKeeper : Participant
{
	public override void ApplyPenalty(int amount)
	{
		if (amount <= 0) return;

		base.ApplyPenalty(amount + 10);
	}
}
