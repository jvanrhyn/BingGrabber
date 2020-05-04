namespace BingGrabber.Shared.Types
{
	public readonly struct Range<T>
	{
		public Range(T min, T max)
		{
			Min = min;
			Max = max;
		}

		public T Min { get; }

		public T Max { get; }
	}
}
