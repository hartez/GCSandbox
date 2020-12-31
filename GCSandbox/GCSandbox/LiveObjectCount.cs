namespace GCSandbox
{
	internal class LiveObjectCount 
	{ 
		public int Count { get; set; }
		public string TestType { get; set; }

		internal void Decrement()
		{
			Count -= 1;
		}

		internal void Increment()
		{
			Count += 1;
		}

		public override string ToString()
		{
			return $"{TestType} live objects: {Count}";
		}
	}
}