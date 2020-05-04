namespace BingGrabber.Shared.Types
{
	// https://codereview.stackexchange.com/a/196913
	public readonly struct RangeData<T>
	{
		public RangeData(T min, T max)
		{
			Min = min;
			Max = max;
		}

		public T Min { get; }

		public T Max { get; }
	}
}
