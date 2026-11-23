using Godot;

[GlobalClass]
public partial class Obstacle : Node
{
	[Export]
	public string ObstacleName { get; set; } = "Šķērslis";

	[Export]
	public int Penalty { get; set; } = 10;
}
