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

	var today = (int)DateTime.Today.MyDayOfWeek;

	if (today == (today & weekdays))
		"Weekday".Dump();
	else 
		"Weekend".Dump();
}


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


