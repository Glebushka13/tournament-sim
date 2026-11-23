using Godot;
using System.Collections.Generic;

public partial class TournamentManager : Node
{
	private List<Participant> party = new List<Participant>();
	private List<Obstacle> obstacles = new List<Obstacle>();

	private int currentTurn = 0;

	public override void _Ready()
	{
		// Izveido dalībniekus
		Captain anna = new Captain();
		AddChild(anna);
		anna.Setup("Anna", 150);
		party.Add(anna);

		Presenter janis = new Presenter();
		AddChild(janis);
		janis.Setup("Jānis", 80);
		party.Add(janis);

		TimeKeeper ieva = new TimeKeeper();
		AddChild(ieva);
		ieva.Setup("Ieva", 100);
		party.Add(ieva);

		// Pieslēdz Finished signālus
		foreach (Participant p in party)
		{
			p.Finished += OnParticipantFinished;
		}

		// Atrod šķēršļus scēnā
		obstacles.Add(GetNode<Obstacle>("PrinterQueue"));
		obstacles.Add(GetNode<Obstacle>("WifiDrop"));
		obstacles.Add(GetNode<Obstacle>("MissingMarker"));

		// Izdrukā sākuma datus
		GD.Print("=== TURNĪRS SĀKAS ===");

		foreach (Participant p in party)
		{
			GD.Print(p.ParticipantName, ": ", p.GetEnergy());
		}

		foreach (Obstacle obstacle in obstacles)
		{
			GD.Print(obstacle.ObstacleName, ": ", obstacle.Penalty);
		}

		RunTournament();
	}

	private void OnParticipantFinished(string participantName)
	{
		GD.Print("IZSTĀJĀS: ", participantName);
	}

	private List<Participant> GetActiveParticipants()
	{
		List<Participant> active = new List<Participant>();

		foreach (Participant p in party)
		{
			if (p.IsActive())
				active.Add(p);
		}

		return active;
	}

	private void PlayTurn()
	{
		List<Participant> active = GetActiveParticipants();

		if (active.Count < 2 || obstacles.Count == 0)
			return;

		int participantIndex =
			(int)GD.RandRange(0, active.Count - 1);

		int obstacleIndex =
			(int)GD.RandRange(0, obstacles.Count - 1);

		Participant target = active[participantIndex];
		Obstacle obstacle = obstacles[obstacleIndex];

		currentTurn++;

		int before = target.GetEnergy();

		GD.Print("Kārta ", currentTurn, ": ",
			obstacle.ObstacleName, " -> ", target.ParticipantName);

		target.ApplyPenalty(obstacle.Penalty);

		GD.Print("Enerģija: ", before, " -> ", target.GetEnergy());
	}

	private void RunTournament()
	{
		while (GetActiveParticipants().Count > 1 && currentTurn < 1000)
		{
			PlayTurn();
		}

		List<Participant> active = GetActiveParticipants();

		GD.Print("=== TURNĪRS BEIDZIES ===");

		if (active.Count == 1)
		{
			GD.Print("UZVARĒTĀJS: ", active[0].ParticipantName);
		}
		else
		{
			GD.Print("Turnīrs apstājās. Drošības limits sasniegts.");
		}

		GD.Print("Kopējais kārtu skaits: ", currentTurn);
	}
}
