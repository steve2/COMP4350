/*
fillTable: Fills a table with a 2D array
	id 		- the identifying string of the table you want to fill
	data 	- the 2D array you want to populate the table with
*/

function insert(dest, tag)
{
	var ret = $('<' + tag + '>');
	dest.append(ret);
	return ret;
}

function insertTable(dest)
{
	return insert(dest, 'table');
}

function insertRow(table)
{
	return insert(table, 'tr');
}

function insertCell(row)
{
	return insert(row, 'td');
}

function fillTable(id, data)
{
	var table = $(id);
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