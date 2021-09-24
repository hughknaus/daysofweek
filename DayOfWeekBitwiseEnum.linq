<Query Kind="Program">
  <Output>DataGrids</Output>
</Query>

void Main()
{
	var weekdays = 62; // 2 + 4 + 8 + 16 + 32
	var weekend = 65; // 1 + 64
	//OPTIONAL WAY OF WRITING IT:
	//var weekdays = (int)(MyDayOfWeek.Monday | MyDayOfWeek.Tuesday | MyDayOfWeek.Wednesday | MyDayOfWeek.Thursday | MyDayOfWeek.Friday);
	//var weekend = (MyDayOfWeek.Saturday | MyDayOfWeek.Sunday); 

	var today = (int)Enum.Parse(typeof(MyDayOfWeek), "Monday");

	if (today == (today & weekdays))
		"Weekday".Dump();
	else 
		"Weekend".Dump();
}

[Flags]
public enum MyDayOfWeek : int
{
    Sunday = 1,         // 1 =  0000001  
    Monday = 1 << 1,    // 2 =  0000010
    Tuesday = 1 << 2,   // 4 =  0000100
    Wednesday = 1 << 3, // 8 =  0001000
    Thursday = 1 << 4,  // 16 = 0010000
    Friday = 1 << 5,    // 32 = 0100000
	Saturday = 1 << 6,  // 64 = 1000000
}


public static class DayOfWeekExtensions
{
	public const int INT_BITS = 7;

	/// <summary>
	/// Function to left rotate bits in dayOfWeek by number of days
	/// </summary>
	/// <param name="dayOfWeek"></param>
	/// <param name="numberOfDays"></param>
	/// <returns></returns>
	public static MyDayOfWeek Next(this MyDayOfWeek dayOfWeek, int numberOfDays)
	{
		if (numberOfDays >= 7)
			numberOfDays = numberOfDays % 7;

		/* In dayOfWeek << days, last days bits are 0.
		To put first 3 bits of dayOfWeek at
		last, do bitwise or of dayOfWeek << days with
		dayOfWeek >> (INT_BITS - days) */
		var x = ((int)dayOfWeek << numberOfDays);
		var y = ((int)dayOfWeek >> (INT_BITS - numberOfDays));
		var result = x | y;

		// Handle beginning and end rollovers
		if (result < (int)Enum.GetValues(typeof(MyDayOfWeek)).Cast<MyDayOfWeek>().Min())
			result = 64;
		else if (result > (int)Enum.GetValues(typeof(MyDayOfWeek)).Cast<MyDayOfWeek>().Max())
			result = 1;

		return (MyDayOfWeek)result;
	}

	/// <summary>
	/// Function to right rotate bits in dayOfWeek by number of days
	/// </summary>
	/// <param name="dayOfWeek"></param>
	/// <param name="numberOfDays"></param>
	/// <returns></returns>
	public static MyDayOfWeek Previous(this MyDayOfWeek dayOfWeek, int numberOfDays)
	{
		if (numberOfDays > 7)
			numberOfDays = numberOfDays % 7;

		/* In dayOfWeek >> days, first days bits are 0.
		To put last 3 bits of at
		first, do bitwise or of dayOfWeek >> days
		with dayOfWeek << (INT_BITS - days) */
		var x = ((int)dayOfWeek >> numberOfDays);
		var y = ((int)dayOfWeek << (INT_BITS - numberOfDays));
		var result = x | y;

		// Handle beginning and end rollovers
		if (result < (int)Enum.GetValues(typeof(MyDayOfWeek)).Cast<MyDayOfWeek>().Min())
			result = 64;
		else if (result > (int)Enum.GetValues(typeof(MyDayOfWeek)).Cast<MyDayOfWeek>().Max())
			result = 1;

		return (MyDayOfWeek)result;
	}

	/// <summary>
	/// Creates an enumerable of all possible DayOfWeek combinations
	/// </summary>
	/// <param name="dayOfWeek"></param>
	/// <returns></returns>
	public static IEnumerable<MyDayOfWeek> AllPossibleCombinations(this MyDayOfWeek dayOfWeek)
	{
		var enumArray = Enum.GetValues(typeof(MyDayOfWeek)).Cast<MyDayOfWeek>().ToList();
		var combinations = enumArray.CreateCombinations(0, null);
		return combinations;
	}

	/// <summary>
	/// Creates an enumerable of all possible DayOfWeek combinations
	///	First convert DayOfWeek to a List:  Enum.GetValues(typeof(MyDayOfWeek)).Cast<MyDayOfWeek>().ToList();
	/// </summary>
	/// <param name="initialArray"></param>
	/// <param name="startIndex"></param>
	/// <param name="pair"></param>
	/// <returns></returns>
	private static IEnumerable<MyDayOfWeek> CreateCombinations(this IList<MyDayOfWeek> initialArray, int startIndex, MyDayOfWeek? pair)
	{
		var combinations = new List<MyDayOfWeek>();
		for (int i = startIndex; i < initialArray.Count; i++)
		{
			var value = (pair != null) ? (pair.Value | initialArray[i]) : initialArray[i];
			combinations.Add(value);
			combinations.AddRange(initialArray.CreateCombinations(i + 1, value));
		}

		return combinations;
	}
}
