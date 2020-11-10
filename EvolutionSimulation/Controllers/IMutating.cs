interface IMutating
{
	public void Move(Tuple<float, float> centerFood, Tuple<float, float> red
			Tuple<float, float> green, Tuple<float, float> blue, float time);

	public static void Eat<T>(T object);

	public void Init(Genome g)

	public Tuple<float, float> getCoordinates();

	private void setCoordinates(Tuple<int, int> coord);

	public Color getColor();

	private void setColor(Color c);

	public float GetDamage(IMutable source);
}
