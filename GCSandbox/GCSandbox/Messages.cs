namespace GCSandbox
{
	public class Messages
	{
		public static Messages Dispatch { get; } = new Messages();

		public static string Allocated = "Allocated";
		public static string Finalized = "Finalized";
		public static string Disposal = "Disposal";
		public static string Log = "Log";
	}
}