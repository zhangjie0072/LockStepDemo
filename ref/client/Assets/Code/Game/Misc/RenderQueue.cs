public static class RenderQueue
{
	public const int PlayGround = 2000;
	// Mid-ground.
	public const int PlayGroundLine = PlayGround + 1;       // +1, 2, 3, ... for additional layers
	// Lines on the ground.
	public const int IndicatorRings = PlayGroundLine + 1;
	public const int IndicatorAod = PlayGroundLine + 2;

	public const int Player = 2500;
	public const int PlayerInfo = Player + 1;

	public const int Gui = 3000;
	public const int ParticleOnGui = Gui + 10000;
}