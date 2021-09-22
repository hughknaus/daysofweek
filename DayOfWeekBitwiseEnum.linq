<Query Kind="Program">
  <Output>DataGrids</Output>
</Query>

void Main()
{
	var weekdays = 31; // 1 + 2 + 4 + 8 + 16
	var weekend = 96; // 32 + 64
	//OPTIONAL WAY OF WRITING IT:
	//var weekdays = (int)(System.DayOfWeek.Monday | System.DayOfWeek.Tuesday | System.DayOfWeek.Wednesday | System.DayOfWeek.Thursday | System.DayOfWeek.Friday);
	//var weekend = (System.DayOfWeek.Saturday | System.DayOfWeek.Sunday); 

	var today = (int)DateTime.Today.DayOfWeek;

	if (today == (today & weekdays))
		"Weekday".Dump();
	else 
		"Weekend".Dump();
}

// This is System.DayOfWeek
/*
public enum DayOfWeek
{
	Monday = 0x0000001, // 1
    Tuesday = 0x0000010, // 2
    Wednesday = 0x0000100, // 4
    Thursday = 0x0001000, // 8
    Friday = 0x0010000, // 16
    Saturday = 0x0100000, // 32
    Sunday = 0x1000000, // 64
}
*/

