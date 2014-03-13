/*
fillTable: Fills a table with a 2D array
	id 		- the identifying string of the table you want to fill
	data 	- the 2D array you want to populate the table with
*/
function fillTable(id, data)
{
	var table = $(id).find('tbody');
	for (var i = 0; i < data.length; i++) 
	{
		var tr = $('<tr>');
		for(var j = 0; j < data[i].length; j++)
		{
			var td = $('<td>'); 
			td.append(data[i][j]);
			tr.append(td);
		}
		table.append(tr);
	}
}