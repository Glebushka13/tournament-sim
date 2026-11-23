using Godot;

public partial class StatsDemo : Node
{
	public override void _Ready()
	{
		Stats empty = new Stats();

		GD.Print("Tukša struktūra:");
		GD.Print("Energy: ", empty.Energy);
		GD.Print("MaxEnergy: ", empty.MaxEnergy);
		GD.Print("Focus: ", empty.Focus);

		Stats original = new Stats(100, 10);
		Stats copy = original;
		copy.Energy = 40;

		GD.Print("Oriģināls: ", original.Energy);
		GD.Print("Kopija: ", copy.Energy);

		Participant anna = new Participant();
		AddChild(anna);
		anna.Setup("Anna", 100);

		anna.ApplyPenalty(30);
		GD.Print("Pēc soda: ", anna.GetEnergy());

		anna.Heal(50);
		GD.Print("Pēc atjaunošanas: ", anna.GetEnergy());

		Stats annaCopy = anna.GetStats();
		annaCopy.Energy = 1;

		GD.Print("Annas īstā enerģija: ", anna.GetEnergy());
		GD.Print("Kopijas enerģija: ", annaCopy.Energy);

		anna.AddBuff(new Buff { Name = "Atbalsts", FocusBonus = 5 });
		anna.AddBuff(new Buff { Name = "Atpūta", FocusBonus = 3 });

		GD.Print("Kopējais fokuss: ", anna.GetTotalFocus());
		GD.Print("Kopējais fokuss atkārtoti: ", anna.GetTotalFocus());

		anna.RemoveBuffAt(0);
		GD.Print("Fokuss pēc bonusa noņemšanas: ", anna.GetTotalFocus());
	}
}
